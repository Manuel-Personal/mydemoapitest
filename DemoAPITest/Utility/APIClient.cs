using RestSharp;

namespace DemoAPITest.Utility
{
    public class APIClient
    {
        private RestRequest request;

        public APIClient()
        {
        }

        public async Task<RestResponse> ExecuteDELETE(String requestURL)
        {
            request = new RestRequest()
            {
                Method = Method.Delete
            };
            request.AddHeader("Accept", "application/json");

            request.RequestFormat = DataFormat.Json;

            var response = await CommonUtils.GetResponseAsync(new RestClient(requestURL), request);

            return response;
        }

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

            var response = await CommonUtils.GetResponseAsync(new RestClient(requestURL), request);

            return response;
        }

        public async Task<RestResponse> ExecutePOST(String requestURL, dynamic payload)
        {
            var request = BuildRequestPayload(payload);
            request.RequestFormat = DataFormat.Json;

            var response = await CommonUtils.GetResponseAsync(new RestClient(requestURL), request);

            return response;
        }

        public RestRequest BuildRequestPayload<T>(T payload) where T : class
        {
            request = new RestRequest()
            {
                Method = Method.Post
            };

            request.AddHeader("Accept", "application/json");
            request.AddBody(payload);
            request.RequestFormat = DataFormat.Json;

            return request;
        }
    }
}
