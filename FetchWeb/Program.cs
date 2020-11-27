using System;
using System.IO;
using System.Net;



namespace FetchWeb
{
    class Program
    {
        public const int numStatus = 9;
        static void Main(string[] args)
        {
            string[] Status = { "AC", "NA", "WA", "TLE", "MLE", "OLE", "RE", "CE" };
            char[] CDW = { 'a', '<', '>', '\"', ' ', '/' };
            Console.WriteLine("Insert GreenJudge user name:");
            string name = Console.ReadLine(); 
            //string name = "x21530317x";
            string str = string.Empty;
            string[] status = new string[numStatus];
            WebRequest wr = WebRequest.Create(@"http://www.tcgs.tc.edu.tw:1218/ShowUserStatistic?account="+name);
            HttpWebRequest hwr = (HttpWebRequest)wr;
            hwr.Timeout = 10000;
            hwr.Method = "GET";
            using (WebResponse response = hwr.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        str = sr.ReadToEnd();
                    }
                }
            }
            //status
            for(int i=0;i<numStatus-1; i++)
            {
                status[i] = str.Substring(str.IndexOf("status=" + Status[i] + "\">") + ("status=" + Status[i] + "\">").Length, 3);
                status[i] = status[i].Trim(CDW);
                Console.WriteLine(Status[i] + ": " + status[i]);
            }
            //Rank status[9]
            status[8] = str.Substring(str.IndexOf("<a href=\"./RankList?tab") + ("<a href=\"./RankList?tab#").Length + name.Length, 50);
            status[8] = status[8].Substring(status[8].IndexOf(">") + 1, status[8].IndexOf("<") - status[8].IndexOf(">")-1);
            Console.WriteLine("Rank: " + status[8]);
            Console.WriteLine("Press to Continue...");
            Console.ReadKey();
           // Console.Write(str);
        }
    }
}
