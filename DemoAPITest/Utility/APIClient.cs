using RestSharp;

namespace DemoAPITest.Utility
{
    public class APIClient
    {
        private Helper helper;

        public APIClient()
        {
            helper = new Helper();
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
                case "single user not found":
                case "delete":
                    urlPath = "api/users/2";
                    break;
                case "single user":
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
            var client = helper.BuildUrl(baseUrl, GetURLPath(requestName));
            var request = helper.CreateGetRequest();
            request.RequestFormat = DataFormat.Json;

            var response = await helper.GetResponseAsync(client, request);

            return response;
        }

        public async Task<RestResponse> ExecutePostRequest(String requestName, dynamic payload)
        {
            var client = helper.BuildUrl(baseUrl, GetURLPath(requestName));
            var request = helper.CreatePostRequest(payload);
            request.RequestFormat = DataFormat.Json;

            var response = await helper.GetResponseAsync(client, request);

            return response;
        }

        public async Task<RestResponse> ExecuteDeleteRequest(String requestName)
        {
            var client = helper.BuildUrl(baseUrl, GetURLPath(requestName));
            var request = helper.CreateDeleteRequest();
            request.RequestFormat = DataFormat.Json;

            var response = await helper.GetResponseAsync(client, request);

            return response;
        }
    }
}
