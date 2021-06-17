using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using LINQtoCSV;
using Newtonsoft.Json.Linq;

namespace TestRestAPI
{
    public class JSONtoWorkItem
    {
         public static bool ExtractWorkItems (Settings settings)
         {
	         var dir = Path.Combine(settings.BasePath, "WorkItems");
	        if (!Directory.Exists(dir))
	        {
		        Console.WriteLine("Ordner existiert nicht:" +dir);
		        return false;
	        }
			var listWorkItems = new List<WorkItem>();
			
			var users = new ListPersons(settings.PathAnonymezedNames);
	        var files = Directory.GetFiles(dir);
	        foreach (var file in files)
	        {
		        if (NewMethod(file, users, out WorkItem tempWorkItem))
		        {
			        listWorkItems.Add(tempWorkItem);
		        }
	        }

	        users.listToCSV();
	        return SaveList(settings,listWorkItems); 
        }
         public static bool ExtractChangeSets (Settings settings)
         {
	         var dir = Path.Combine(settings.BasePath, "changeSets");
	         if (!Directory.Exists(dir))
	         {
		         Console.WriteLine("Ordner existiert nicht:" +dir);
		         return false;
	         }
	         var listChangesetIds = new List<string>();
	         
	         var files = Directory.GetFiles(dir);
	         foreach (var file in files)
	         {
		         JObject data = JObject.Parse(File.ReadAllText(file));
		       
		         if (data.First != null)
		         {
			         int count = 0;
			         string s = data["count"]?.ToString() ?? "";
			         if (!string.IsNullOrWhiteSpace(s) && int.TryParse(s, out count) && count > 0)
			         {
				         foreach (var VARIABLE in data["value"])
				         {
					         var a = VARIABLE["changesetId"]?.ToString() ?? "";
					         if (!string.IsNullOrWhiteSpace(a) && !listChangesetIds.Contains(a))
					         {
						         listChangesetIds.Add(a);
					         }

				         } 
			         }
			         else
			         {
				         Console.WriteLine("Eroor in file: " + file);
			         }

			         if (count > 99)
			         {
				         Console.WriteLine("To many IDs in file: " + file);
			         }
			         
			         
		         }
	         }
	         
	         File.WriteAllLines(Path.Combine(settings.BasePath,"changesetIDs.csv"), listChangesetIds);
	         return true;
         }

         private static bool SaveList(Settings settings, List<WorkItem> listWorkItems)
         {
				 var time = DateTime.Now.Ticks.ToString();
	        
		         CsvFileDescription outputFileDescription = new CsvFileDescription
		         {
			         SeparatorChar = ';', // tab delimited
			         FirstLineHasColumnNames = true, // no column names in first record
               
		         };
		         CsvContext cc = new CsvContext();
		         var path = Path.Combine(settings.BasePath, "workItems_" + time);
		         cc.Write(
			         listWorkItems,
			         path+".csv",
			         outputFileDescription);
		         
		         JArray js = JArray.FromObject(listWorkItems.ToArray());
		         File.WriteAllText(path+".json",js.ToString());
		         return true;
	         
         }
         private static bool NewMethod(string file,ListPersons users , out WorkItem tempWorkItem)
         {
	         JObject data = JObject.Parse(File.ReadAllText(file));
	         tempWorkItem = new WorkItem();
	         if (data.First != null)
	         {
		         var tempId = data["id"]?.ToString() ?? "";
		         var tempType = data["fields"]?["System.WorkItemType"]?.ToString() ?? "";
		         if (!string.IsNullOrEmpty(tempId) || !string.IsNullOrEmpty(tempType))
		         {
			         
			         tempWorkItem.Id = tempId;
			         tempWorkItem.WorkItemType = tempType;
			         tempWorkItem.CommentCount = data["fields"]?["System.CommentCount"]?.ToString() ?? "";
			         tempWorkItem.HyperLinkCount =data["fields"]?["System.HyperLinkCount"]?.ToString() ?? "";
			         tempWorkItem.FileCount = data["fields"]?["System.FileCount"]?.ToString() ?? "";
			         tempWorkItem.Status = data["fields"]?["System.State"]?.ToString() ?? "";
			         tempWorkItem.StatusChange = data["fields"]?["Microsoft.VSTS.Common.StateChangeDate"]?.ToString() ?? "";
			         tempWorkItem.Priority = data["fields"]?["Microsoft.VSTS.Common.Priority"]?.ToString() ?? "";
			         tempWorkItem.Area = data["fields"]?["System.AreaPath"]?.ToString() ?? "";
			         tempWorkItem.Iteration = data["fields"]?[ "System.IterationPath"]?.ToString() ?? "";
			         
			         tempWorkItem.AcceptedDate = data["fields"]?["System.AcceptedDate"]?.ToString() ?? "";
			         tempWorkItem.ActivatedDate = data["fields"]?["System.ActivatedDate"]?.ToString() ?? "";
			         tempWorkItem.ChangedDate = data["fields"]?["System.ChangedDate"]?.ToString() ?? "";
			         tempWorkItem.ClosedDate = data["fields"]?["System.ClosedDate"]?.ToString() ?? "";
			         tempWorkItem.CreatedDate = data["fields"]?["System.CreatedDate"]?.ToString() ?? "";
			         tempWorkItem.FinishDate = data["fields"]?["System.FinishDate"]?.ToString() ?? "";
			         tempWorkItem.ResolvedDate = data["fields"]?["System.ResolvedDate"]?.ToString() ?? "";
			         tempWorkItem.ReviewedDate = data["fields"]?["System.ReviewedDate"]?.ToString() ?? "";
			         
			         
			         
			         tempWorkItem.AcceptedBy = users.GetAnonymPerson( data["fields"]?["System.AcceptedBy"]?["displayName"]?.ToString() ?? "");
			         tempWorkItem.ActivatedBy =users.GetAnonymPerson( data["fields"]?["System.ActivatedBy"]?["displayName"]?.ToString() ?? "");
			         tempWorkItem.ChangedBy = users.GetAnonymPerson(data["fields"]?["System.ChangedBy"]?["displayName"]?.ToString() ?? "");
			         tempWorkItem.ClosedBy = users.GetAnonymPerson(data["fields"]?["System.ClosedBy"]?["displayName"]?.ToString() ?? "");
			         tempWorkItem.CreatedBy =users.GetAnonymPerson( data["fields"]?["System.CreatedBy"]?["displayName"]?.ToString() ?? "");
			         tempWorkItem.FinishBy = users.GetAnonymPerson(data["fields"]?["System.FinishBy"]?["displayName"]?.ToString() ?? "");
			         tempWorkItem.ResolvedBy = users.GetAnonymPerson(data["fields"]?["System.ResolvedBy"]?["displayName"]?.ToString() ?? "");
			         tempWorkItem.ReviewedBy = users.GetAnonymPerson(data["fields"]?["System.ReviewedBy"]?["displayName"]?.ToString() ?? "");
			         return true; 
		         }
	         }

	         return false ;
         }
    }
}