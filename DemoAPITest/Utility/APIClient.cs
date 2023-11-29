using Newtonsoft.Json;
using RestSharp;

namespace DemoAPITest.Utility
{
    public class APIClient
    {
        private RestRequest request;

        public APIClient()
        {
        }

        private static Dictionary<string, string> acceptHeader = new Dictionary<string, string>
        {
            { "Accept", "application/json" }
        };

        public async Task<RestResponse> ExecuteGET(String requestURL)
        {
            request = new RestRequest()
            {
                Method = Method.Get
            };

            request.AddHeaders(new Dictionary<string, string>
            {
                { "Accept", "application/json" },
                { "Content-Type", "application/json" }
            });

            request.RequestFormat = DataFormat.Json;

            var response = await GetResponseAsync(new RestClient(requestURL), request);

            return response;
        }

        public async Task<RestResponse> ExecuteDELETE(String requestURL)
        {
            request = new RestRequest()
            {
                Method = Method.Delete
            };
            request.AddHeaders(acceptHeader);

            request.RequestFormat = DataFormat.Json;

            var response = await GetResponseAsync(new RestClient(requestURL), request);

            return response;
        }

        public async Task<RestResponse> ExecutePOST(String requestURL, dynamic payload)
        {
            var request = BuildRequestPayload(payload);
            request.RequestFormat = DataFormat.Json;

            var response = await GetResponseAsync(new RestClient(requestURL), request);

            return response;
        }

        private RestRequest BuildRequestPayload<T>(T payload) where T : class
        {
            request = new RestRequest()
            {
                Method = Method.Post
            };

            request.AddHeaders(acceptHeader);
            request.AddBody(payload);
            request.RequestFormat = DataFormat.Json;

            return request;
        }

        private static async Task<RestResponse> GetResponseAsync(RestClient client, RestRequest request)
        {
            return await client.ExecuteAsync(request);
        }

        public static T GetContent<T>(RestResponse response)
        {
            return JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}
