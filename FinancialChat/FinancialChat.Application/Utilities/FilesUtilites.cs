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

        public static string SplitCSV(string url)
        {
            try
            {
                string fileList = GetCSV(url);
                string[] tempStr;

                if (fileList.Contains(","))
                    tempStr = fileList.Split(',');
                else
                    tempStr = fileList.Split(';');

                List<string> resultHeader = new List<string>();
                List<string> resultDetails = new List<string>();

                bool header = true;
                bool accuracyData = true;

                foreach (string item in tempStr)
                {
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        if (item.Contains("\r") && header)
                        {
                            header = false;
                            string[] temData = item.Replace('\n', ' ').Trim().Split('\r');
                            resultHeader.Add(temData[0]);
                            resultDetails.Add(temData[1]);
                        }
                        else
                        {

                            if (header)
                                resultHeader.Add(item);
                            else
                            {
                                if (item.Equals("N/D")) { accuracyData = false; break; }
                                resultDetails.Add(item);
                            }

                        }
                    }
                }
                if (accuracyData)
                {
                    StringBuilder finalData = new StringBuilder();
                    int i = 0;
                    foreach (var item in resultHeader)
                    {
                        finalData.Append(item.Trim());
                        finalData.Append(": ");
                        finalData.Append(resultDetails[i]);
                        if (i < resultDetails.Count() - 1)
                            finalData.Append(" <-> ");
                        i++;
                    }
                    return finalData.ToString();
                }
                else return $"Stock {resultDetails[0]} data not found";
            }
            catch (Exception ex)
            {

                return "Sorry!, We have some problems trying to get the data " + ex.Message;
            }            
            
        }
    }
}
