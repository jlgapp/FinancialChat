using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FinancialChat.Application.Utilites
{
    public class FilesUtilites
    {
        private static string GetCSV(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();            

            StreamReader sr = new StreamReader(resp.GetResponseStream());
            string results = sr.ReadToEnd();
            sr.Close();

            return results;
        }

        public static List<string> SplitCSV(string url)
        {
            List<string> splitted = new List<string>();
            string fileList = GetCSV(url);
            string[] tempStr;

            tempStr = fileList.Split(',');

            foreach (string item in tempStr)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                      splitted.Add(item);
                }
            }

            return splitted;
        }
    }
}
