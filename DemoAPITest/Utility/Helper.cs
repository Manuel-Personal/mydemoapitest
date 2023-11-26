using NUnit.Framework;
using RestSharp;
using System.Net;
using TechTalk.SpecFlow;

namespace DemoAPITest.Utility
{
    public class Helper
    {
        public static RestClient BuildUrl(string baseUrl, string endpoint)
        {
            var url = Path.Combine(baseUrl, endpoint);
            return new RestClient(url);
        }

        public static async Task<RestResponse> GetResponseAsync(RestClient client, RestRequest request)
        {
            return await client.ExecuteAsync(request);
        }

        public static void VerifyStatusResponse(String statusCode, RestResponse response)
        {
            HttpStatusCode actualCode = response.StatusCode;
            var code = (int)actualCode;

            int expectedCode = Int32.Parse(statusCode);

            Assert.AreEqual(expectedCode, code);
        }
    }
}
