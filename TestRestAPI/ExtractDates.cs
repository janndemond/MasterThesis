using System.Collections.Generic;

namespace TestRestAPI
{
    public class ExtractDates
    {
        public string startDate;
        public string endDate;
        public int id;

        public ExtractDates(int id,string startDate, string endDate)
        {
            this.startDate = startDate;
            this.endDate = endDate;
            this.id = id;
        }
        public static List<ExtractDates> InitTest()
                {
        	        List<ExtractDates> dates = new List<ExtractDates>();
        	        int id = 1;
        	        var start = "2021-06-13";
        	        var end = "2021-06-17";
        	        var temp = new ExtractDates(id,start,end);
        	        id++;
	                dates.Add(temp);
        	         start = "2021-06-08";
        	         end = "2021-06-10";
        	         temp = new ExtractDates(id,start,end);
        	        id++;
        	        dates.Add(temp);
        	        return dates;
                }
        
        public static List<ExtractDates> init()
                {
        	        List<ExtractDates> dates = new List<ExtractDates>();
        	        int id = 1;
        	        for (int i = 2019; i < 2022; i++)
        	        {
        		        var start = i+"-02-25-";
        		        var end = i+"-03-02";
        		        var temp = new ExtractDates(id,start,end);
        		        id++;
        		        dates.Add(temp);
        		        start = i+"-03-02" ;
        		        end = i+"-03-09" ;
        		         temp = new ExtractDates(id,start,end);
        		        id++;
        		        dates.Add(temp);
        		        start = i+"-03-09" ;
        		        end = i+"-03-17" ;
        		        temp = new ExtractDates(id,start,end);
        		        id++;
        		        dates.Add(temp);
        		        start = i+"-03-17" ;
        		        end = i+"-03-24";
        		        temp = new ExtractDates(id,start,end);
        		        id++;
        		        dates.Add(temp);
        		        start = i+"-03-24" ;
                        end = i + "-03-31";
        		        temp = new ExtractDates(id,start,end);
        		        id++;
        		        dates.Add(temp);
        	        }
        	        return dates;
                }
    }
}