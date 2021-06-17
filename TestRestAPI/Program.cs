using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Common;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TestRestAPI
{
	class Program
	{
		static void Main(string[] args)
		{

			var settings = InitSettings();

			if (!string.IsNullOrEmpty(settings.Task))
			{
				var temp = settings.Task.Split(',');
				if (temp.Length > 0)
				{
					foreach (var task in temp)
					{
						if (task == "1")
						{

						}

						if (task == "2")
						{
							if (ApiCalls.GetWorkItems(settings))
							{
								Console.WriteLine("Work Items Loaded");
							}
						}

						if (task == "3")
						{
							if (JSONtoWorkItem.ExtractWorkItems(settings))
							{
								Console.WriteLine("Work Items Converted");
							}

						}

						if (task == "4")
						{
							var dates = ExtractDates.InitTest();
							int i = 0;
							foreach (var date in dates)
							{
								i++;
								Task a =  ApiCalls.GetGitCommits(settings, date.startDate, date.endDate, i);
								a.Wait();
							}
							Console.WriteLine("Changesets Downloaded");
						
							

						}

						if (task == "5")
						{
							if (JSONtoWorkItem.ExtractChangeSets(settings))
							{
								Console.WriteLine("Changesets IDs extracted");
								Task b = ApiCalls.GetchangesetItemsById(settings);
								b.Wait();
								Console.WriteLine("Changesets IDs downloaded");
							}
							
						}
					}
				}
			}


			/*
			var settings = new Settings();
			if (!args.IsNullOrEmpty())
			{
				settings.Token = args[0].Trim();
				if (args.Length > 1)
				{
					settings.IsTest = true;
				}
			}
			//Task t = GetProjects(args);
		   //t.Wait();
			//List<extrackDates> dates = initTest();
			//List<extrackDates> dates = init();
			//foreach (var date in dates)
			//	Task a = GetWIQL(date);
				//a.Wait();
			//}
			Task a = GetGitCommits( settings);
			a.Wait();
			*/
			Console.WriteLine("Hello World!");
			Console.ReadKey();
		}

		private static Settings InitSettings()
		{
			var settings = new Settings();
			if (File.Exists(settings.BasePath + "\\settings.json"))
			{
				settings = JsonConvert.DeserializeObject<Settings>(
					File.ReadAllText(settings.BasePath + "\\settings.json"));
			}

			JObject js = JObject.FromObject(settings);
			File.WriteAllText(settings.BasePath + "\\settings.json", js.ToString());
			return settings;
		}



		public static async Task GetProjects(Settings settings)
		{
			try
			{

				using (HttpClient client = new HttpClient())
				{
					client.DefaultRequestHeaders.Accept.Add(
						new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

					client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
						Convert.ToBase64String(
							System.Text.ASCIIEncoding.ASCII.GetBytes(
								string.Format("{0}:{1}", "", settings.Token))));

					using (HttpResponseMessage response = await client.GetAsync(
						"https://dev.azure.com/janndemond/_apis/projects"))
					{
						response.EnsureSuccessStatusCode();
						string responseBody = await response.Content.ReadAsStringAsync();
						var path = new Uri(
							System.IO.Path.GetDirectoryName(
								System.Reflection.Assembly.GetExecutingAssembly().CodeBase)
						).LocalPath;
						File.WriteAllText(path + @"\test.json", responseBody);
						Console.WriteLine(responseBody);
					}
				}


			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}





		public static async Task GetWIQL(ExtractDates date, Settings settings)
		{
			Uri orgUrl = new Uri(settings.OrgUrl);

			VssConnection connection = new VssConnection(orgUrl, new VssBasicCredential(string.Empty, settings.Token));
			WorkItemTrackingHttpClient witClient = connection.GetClient<WorkItemTrackingHttpClient>();
			Wiql wiql = new Wiql();

			wiql.Query = "SELECT * "
			             + " FROM WorkItems" +
			             "  WHERE [System.CreatedDate] >='" + date.startDate +
			             "' AND [System.CreatedDate] <'" + date.endDate + "'";

			;
			WorkItemQueryResult tasks = await witClient.QueryByWiqlAsync(wiql);

			IEnumerable<WorkItemReference> tasksRefs;
			tasksRefs = tasks.WorkItems.OrderBy(x => x.Id);
			List<Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem> tasksList =
				witClient.GetWorkItemsAsync(tasksRefs.Select(wir => wir.Id)).Result;

			string jsonString = JsonConvert.SerializeObject(tasksList);
			var path = new Uri(
				System.IO.Path.GetDirectoryName(
					System.Reflection.Assembly.GetExecutingAssembly().CodeBase)
			).LocalPath;
			File.WriteAllText(path + @"\WorkItems_" + date.id + ".json", jsonString);

		}
	}
}
