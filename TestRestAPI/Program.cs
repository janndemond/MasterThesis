using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Newtonsoft.Json;

namespace TestRestAPI
{
    class Program
    {
        static void Main(string[] args)
        {
	        //Task t = GetProjects(args);
	       // t.Wait();
			//List<extrackDates> dates = initTest();
			List<extrackDates> dates = init();
			foreach (var date in dates)
			{
				Task a = GetWIQL(date);
				a.Wait();
			}
			
	        
            Console.WriteLine("Hello World!");
        }

        static List<extrackDates> initTest()
        {
	        List<extrackDates> dates = new List<extrackDates>();
	        int id = 1;
	        var start = "25.05.2021";
	        var end = "31.05.2021";
	        var temp = new extrackDates(id,start,end);
	        id++;
	        dates.Add(temp);
	         start = "01.06.2021";
	         end = "10.06.2021";
	         temp = new extrackDates(id,start,end);
	        id++;
	        dates.Add(temp);
	        return dates;
        }

        static List<extrackDates> init()
        {
	        List<extrackDates> dates = new List<extrackDates>();
	        int id = 1;
	        for (int i = 2019; i < 2022; i++)
	        {
		        var start = "25.02." + i.ToString();
		        var end = "02.03." + i.ToString();
		        var temp = new extrackDates(id,start,end);
		        id++;
		        dates.Add(temp);
		        start = "02.03." + i.ToString();
		        end = "09.03." + i.ToString();
		         temp = new extrackDates(id,start,end);
		        id++;
		        dates.Add(temp);
		        start = "09.03." + i.ToString();
		        end = "17.03." + i.ToString();
		        temp = new extrackDates(id,start,end);
		        id++;
		        dates.Add(temp);
		        start = "17.03." + i.ToString();
		        end = "24.03." + i.ToString();
		        temp = new extrackDates(id,start,end);
		        id++;
		        dates.Add(temp);
		        start = "24.03." + i.ToString();
		        end = "31.03." + i.ToString();
		        temp = new extrackDates(id,start,end);
		        id++;
		        dates.Add(temp);
	        }
	        return dates;
        }
        
        public static async Task GetProjects( string[] args)
        {
        	try
        	{
        		var personalaccesstoken = "6ndav4oxmfdeyglv4irlizwnivbwa2tddiypab7u3gqqvg2l4yna";
        
        		using (HttpClient client = new HttpClient())
        		{
        			client.DefaultRequestHeaders.Accept.Add(
        				new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        
        			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
        				Convert.ToBase64String(
        					System.Text.ASCIIEncoding.ASCII.GetBytes(
        						string.Format("{0}:{1}", "", personalaccesstoken))));
        
        			using (HttpResponseMessage response = await client.GetAsync(
        						"https://dev.azure.com/janndemond/_apis/projects"))
        			{
        				response.EnsureSuccessStatusCode();
        				string responseBody = await response.Content.ReadAsStringAsync();
                        var path = new Uri(
	                        System.IO.Path.GetDirectoryName(
		                        System.Reflection.Assembly.GetExecutingAssembly().CodeBase)
                        ).LocalPath;
                        File.WriteAllText(path+@"\test.json", responseBody);
        				Console.WriteLine(responseBody);
        			}
        		}

                
            }
        	catch (Exception ex)
        	{
        		Console.WriteLine(ex.ToString());
        	}
        }

        public static async Task GetWIQL(extrackDates date)
        {
	        Uri orgUrl = new Uri("https://dev.azure.com/janndemond/");  
	        String personalAccessToken = "6ndav4oxmfdeyglv4irlizwnivbwa2tddiypab7u3gqqvg2l4yna";
	        VssConnection connection = new VssConnection(orgUrl, new VssBasicCredential(string.Empty, personalAccessToken));
	        WorkItemTrackingHttpClient witClient = connection.GetClient<WorkItemTrackingHttpClient>(); 
	        Wiql wiql = new Wiql();

	        wiql.Query = "SELECT * "
	                     + " FROM WorkItems" +
	                     "  WHERE [System.CreatedDate] >='"+ date.startDate +
	                     "' AND [System.CreatedDate] <'"+date.endDate+"'"
	        ; 
	        WorkItemQueryResult tasks = await witClient.QueryByWiqlAsync(wiql);  
  
	        IEnumerable<WorkItemReference> tasksRefs;  
	        tasksRefs = tasks.WorkItems.OrderBy(x => x.Id);  
	        List<WorkItem> tasksList =witClient.GetWorkItemsAsync(tasksRefs.Select(wir => wir.Id)).Result;
	     
	        string jsonString = JsonConvert.SerializeObject(tasksList);
	        var path = new Uri(
		        System.IO.Path.GetDirectoryName(
			        System.Reflection.Assembly.GetExecutingAssembly().CodeBase)
	        ).LocalPath;
	        File.WriteAllText(path+@"\WorkItems_"+date.id+".json",  jsonString);
	       
        }
        
    }

    class extrackDates
    {
	    
	    public string startDate;
	    public string endDate;
	    public int id;

	    public extrackDates(int id,string startDate, string endDate)
	    {
		    this.startDate = startDate;
		    this.endDate = endDate;
		    this.id = id;
	    }
    }
}
