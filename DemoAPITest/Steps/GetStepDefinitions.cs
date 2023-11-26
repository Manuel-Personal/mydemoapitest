using DemoAPITest.Data;
using DemoAPITest.Objects;
using DemoAPITest.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Net;
using System.Net.Http;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DemoAPITest.Steps
{
    [Binding]
    public class GetStepDefinitions
    {
        private RestResponse response;
        private HttpStatusCode statusCode;

        private void VerifyUserList(Table table)
        {
            JObject contentResponse = JObject.Parse(response.Content);
            JArray dataArrayFromResponse = (JArray)contentResponse["data"];
            int currentIndex = 0;

            foreach (var row in table.Rows)
            {
                var data = row.CreateInstance<DataModel>();

                Assert.AreEqual(data.id, dataArrayFromResponse[currentIndex]["id"].ToString());
                Assert.AreEqual(data.email, dataArrayFromResponse[currentIndex]["email"].ToString());
                Assert.AreEqual(data.first_name, dataArrayFromResponse[currentIndex]["first_name"].ToString());
                Assert.AreEqual(data.last_name, dataArrayFromResponse[currentIndex]["last_name"].ToString());
                Assert.AreEqual(data.avatar, dataArrayFromResponse[currentIndex]["avatar"].ToString());

                currentIndex++;
            }
        }

        [Then(@"validate correct page details are returned")]
        public void ThenValidateCorrectPageDetailsAreReturned(Table table)
        {
            var userList = table.CreateInstance<UserListModel>();

            JObject contentResponse = JObject.Parse(response.Content);

            Assert.AreEqual(userList.page, contentResponse["page"].ToString());
            Assert.AreEqual(userList.per_page, contentResponse["per_page"].ToString());
            Assert.AreEqual(userList.total, contentResponse["total"].ToString());
            Assert.AreEqual(userList.total_pages, contentResponse["total_pages"].ToString());

            Helper.VerifyStatusResponse("200", response);
        }

        [Then(@"validate that users are listed")]
        public void ThenValidateThatUsersAreListed(Table table)
        {
            VerifyUserList(table);
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
            Helper.VerifyStatusResponse("404", response);
        }

        [Then(@"validate that single user is found")]
        public async void ThenValidateThatSingleUserIsFound(Table table)
        {
            VerifyUserList(table);
        }
    }
}
