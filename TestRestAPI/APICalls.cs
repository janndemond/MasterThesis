using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Common;

namespace TestRestAPI
{
    public class ApiCalls
    {
        public static bool GetWorkItems(Settings settings)
        {
            Task a = GetWorkItemsById(settings);
            a.Wait();
            return true;
        }

        static async Task GetWorkItemsById(Settings settings)
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
                    var ids = File.ReadAllLines(settings.PathIDsWorkItems);
                    var path = Path.Combine(settings.BasePath, "WorkItems");

                    Util.checkRootPath(path);
                    foreach (var id in ids)

                    {
                        int intID = 0;
                        if (!id.IsNullOrEmpty() && int.TryParse(id, out intID) && intID > 0)
                        {
                            var link = settings.OrgUrl + "//" + settings.Organization + @"/_apis/wit/workitems/" + id +
                                       "?api-version=" + settings.ApiVersion;
                            using (HttpResponseMessage response = await client.GetAsync(
                                link))
                            {
                                response.EnsureSuccessStatusCode();
                                string responseBody = await response.Content.ReadAsStringAsync();


                                File.WriteAllText(Path.Combine(path, id + ".json"), responseBody);
                            }

                            Console.WriteLine(link);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static async Task GetchangesetItemsById(Settings settings)
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
                    var path =Path.Combine(settings.BasePath, "DetailsChangeSets");
                    if (File.Exists(settings.BasePath + @"\changesetIDs.csv") &&  Util.checkRootPath(path))
                    
                    {
                        var ids = File.ReadAllLines(settings.BasePath + @"\changesetIDs.csv");
                        
                        foreach (var id in ids)

                        {
                            int intID = 0;
                            if (!id.IsNullOrEmpty() && int.TryParse(id, out intID) && intID > 0)
                            {
                                var link = settings.OrgUrl + "/" + settings.Organization + @"/_apis/tfvc/changesets/" +
                                           id +
                                           "/changes?api-version=" + settings.ApiVersion;
                                using (HttpResponseMessage response = await client.GetAsync(
                                    link))
                                {
                                    response.EnsureSuccessStatusCode();
                                    string responseBody = await response.Content.ReadAsStringAsync();

                                    
                                   
                                        File.WriteAllText(path + "\\Commit_" + id + ".json", responseBody);
                                    
                                }

                                Console.WriteLine(link);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("File with ChangeSet Ids does not Exist");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static async Task GetGitCommits(Settings settings, string fromDate, string toDate, int i)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(
                            System.Text.ASCIIEncoding.ASCII.GetBytes(
                                string.Format("{0}:{1}", "", settings.Token))));
                    using (HttpResponseMessage response = await client.GetAsync(
                        settings.OrgUrl + "/" + settings.Organization + "/" + settings.Project +
                        @"/_apis/tfvc/changesets?" + settings.ItemPath + "searchCriteria.fromDate=" + fromDate +
                        "&searchCriteria.toDate=" + toDate + "&api-version=" + settings.ApiVersion)
                    )
                    {
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();

                        var path = Path.Combine(settings.BasePath, "changeSets");
                        if (Util.checkRootPath(path))
                        {
                            await File.WriteAllTextAsync(Path.Combine(path, "changeSet_List_" + i + ".json"),
                                responseBody);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}