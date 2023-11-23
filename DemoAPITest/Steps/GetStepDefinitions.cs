using DemoAPITest.Objects;
using DemoAPITest.Utility;
using NUnit.Framework;
using RestSharp;
using System;
using System.Net;
using TechTalk.SpecFlow;

namespace DemoAPITest.Steps
{
    [Binding]
    public class GetStepDefinitions
    {
        private RestResponse response;
        private HttpStatusCode statusCode;

        [Then(@"validate that users are listed")]
        public void ThenValidateThatUsersAreListed()
        {
            //TODO
        }

        [When(@"user sends '([^']*)' request")]
        public async Task WhenUserSendsRequest(string request)
        {
            var api = new APIClient();
            response = await api.ExecuteGetRequest(request);
        }

        [Then(@"validate that single user is not found")]
        [Then(@"validate that single resource is not found")]
        public void ThenValidateThatSingleUserIsNotFound()
        {
            statusCode = response.StatusCode;
            var code = (int)statusCode;
            Assert.AreEqual(404, code);
        }

    }
}
