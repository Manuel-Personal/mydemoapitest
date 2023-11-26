using DemoAPITest.Utility;
using NUnit.Framework;
using RestSharp;
using System;
using System.Net;
using TechTalk.SpecFlow;

namespace DemoAPITest.Steps
{
    [Binding]
    public class DeleteStepDefinitions
    {
        private RestResponse response;
        private HttpStatusCode statusCode;

        [When(@"user sends '([^']*)' delete request")]
        public async Task WhenUserSendsDeleteRequest(string request)
        {
            var api = new APIClient();
            response = await api.ExecuteDeleteRequest(request);
        }

        [Then(@"validate that user is deleted")]
        public void ThenValidateThatUserIsDeleted()
        {
            Helper.VerifyStatusResponse("204", response);
        }
    }
}
