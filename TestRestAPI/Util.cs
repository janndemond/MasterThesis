using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace TestRestAPI
{
    
    public class Util
    {
        public static char CSV_SEPARATOR = ';';
        public static bool writteToCSV(string path, List<dynamic> data, string type)
        {
            if (type == "WorkItem")
            {
                
            }
            return true;
        }

        public string pathWithTimeStamp()
        {
            return "";
        }

        public static bool checkRootPath(string path)
        {
            if (Directory.Exists(path))
            {
                return true;
            }
            Console.WriteLine("The directory has been created: "+ path);
            Directory.CreateDirectory(path);
            
            return true;
        }
    }
}