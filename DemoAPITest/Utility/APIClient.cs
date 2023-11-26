using RestSharp;

namespace DemoAPITest.Utility
{
    public class APIClient
    {
        private RestRequest request;

        public APIClient()
        {
        }

        private static string baseUrl = "https://reqres.in/";

        private String GetURLPath(String requestName)
        {
            String urlPath = "";

            switch(requestName.ToLower())
            {
                case "list users":
                    urlPath = "api/users?page=2";
                    break;

                case "single user":
                case "delete":
                    urlPath = "api/users/2";
                    break;

                case "single user not found":
                    urlPath = "api/users/23";
                    break;

                case "single resource not found":
                    urlPath = "api/unknown/23";
                    break;

                case "create":
                    urlPath = "api/users";
                    break;

                case "register-successful":
                case "register-unsuccessful":
                    urlPath = "api/register";
                    break;

                case "login-successful":
                case "login-unsuccessful":
                    urlPath = "api/login";
                    break;

                default:
                    break;
            }

            return urlPath;
        }

        public async Task<RestResponse> ExecuteGetRequest(String requestName)
        {
            var client = Helper.BuildUrl(baseUrl, GetURLPath(requestName));
            var request = CreateGetRequest();
            request.RequestFormat = DataFormat.Json;

            var response = await Helper.GetResponseAsync(client, request);

            return response;
        }

        public async Task<RestResponse> ExecutePostRequest(String requestName, dynamic payload)
        {
            var client = Helper.BuildUrl(baseUrl, GetURLPath(requestName));
            var request = CreatePostRequest(payload);
            request.RequestFormat = DataFormat.Json;

            var response = await Helper.GetResponseAsync(client, request);

            return response;
        }

        public async Task<RestResponse> ExecuteDeleteRequest(String requestName)
        {
            var client = Helper.BuildUrl(baseUrl, GetURLPath(requestName));
            var request = CreateDeleteRequest();
            request.RequestFormat = DataFormat.Json;

            var response = await Helper.GetResponseAsync(client, request);

            return response;
        }

        public RestRequest CreateGetRequest()
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

            return request;
        }

        public RestRequest CreatePostRequest<T>(T payload) where T : class
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

        public RestRequest CreateDeleteRequest()
        {
            request = new RestRequest()
            {
                Method = Method.Delete
            };
            request.AddHeader("Accept", "application/json");
            return request;
        }
    }
}
