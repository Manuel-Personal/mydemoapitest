using NUnit.Framework;
using RestSharp;
using System.Net;
using TechTalk.SpecFlow;

namespace DemoAPITest.Utility
{
    public class CommonUtils
    {
        public static string baseUrl = "https://reqres.in/api";

        public static Dictionary<string, string> getDeleteHeader = new Dictionary<string, string>
        {
            { "Accept", "application/json" }
        };

        public static Dictionary<string, string> postHeader = new Dictionary<string, string>
        {
            { "Accept", "application/json" },
            { "Content-Type", "application/json" }
        };

        public static async Task<RestResponse> GetResponseAsync(RestClient client, RestRequest request)
        {
            return await client.ExecuteAsync(request);
        }

        public static Dictionary<string, string> ToDictionary(Table table)
        {
            var dictionary = new Dictionary<string, string>();
            foreach (var row in table.Rows)
            {
                dictionary.Add(row[0], row[1]);
            }

            return dictionary;
        }
    }
}
