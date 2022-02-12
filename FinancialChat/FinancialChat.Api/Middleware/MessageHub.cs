using FinancialChat.Application.Models;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net;

namespace FinancialChat.Api.Middleware
{
    public class MessageHub : Hub
    {

        private IConfiguration _configuration;

        public MessageHub(IConfiguration configuration) { _configuration = configuration; }

        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
        public async Task NewMessage(Message msg, string conectionId)
        {
            if (msg.MessageIncome.StartsWith("/"))
            {
                if (msg.MessageIncome.Contains("="))
                {
                    string[] dataToFind = msg.MessageIncome.Split('=');
                    if (dataToFind[0].Equals("/stock"))
                    {
                        var dataUrl = _configuration.GetSection("ExternalUrl:ChatBot");

                        var botResponse = GetItemUrl(dataUrl.Value + dataToFind[1]);

                        msg.MessageIncome = botResponse;
                    }
                    else
                        msg.MessageIncome = "Sorry!, Command can't be processed!!!";
                }
                else msg.MessageIncome = "Sorry!, Command can't be processed!!!";

                msg.Type = "BotResponse";
                await Clients.Client(conectionId).SendAsync("MessageReceived", msg);
            }
            else
                await Clients.All.SendAsync("MessageReceived", msg);
        }
        public static string GetItemUrl(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return "Error";
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            // Do something with responseBody
                            //Console.WriteLine(responseBody);
                            return responseBody;
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                return "Error " + ex.Message;
            }
        }
    }
}
