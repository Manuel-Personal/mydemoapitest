using DemoAPITest.Data;
using DemoAPITest.Objects;
using DemoAPITest.Utility;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Configuration;
using System.Net;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DemoAPITest.Steps
{
    [Binding]
    public class UserAPIStepDefinition
    {
        private void VerifyDataModel(JObject actualResponse, DataModel expectedValue)
        {
            Assert.That(actualResponse["id"].ToString(), Is.EqualTo(expectedValue.id));
            Assert.That(actualResponse["email"].ToString(), Is.EqualTo(expectedValue.email));
            Assert.That(actualResponse["first_name"].ToString(), Is.EqualTo(expectedValue.first_name));
            Assert.That(actualResponse["last_name"].ToString(), Is.EqualTo(expectedValue.last_name));
            Assert.That(actualResponse["avatar"].ToString(), Is.EqualTo(expectedValue.avatar));
        }

        [Then(@"validate correct create user details are returned")]
        public void ThenValidateCorrectCreateUserDetailsAreReturned()
        {
            RestResponse response = (RestResponse)ScenarioContext.Current["Response"];
            JObject contentResponse = JObject.Parse(response.Content);

            Dictionary<String, String> payload = (Dictionary<String, String>)ScenarioContext.Current["Payload"];

            foreach (var row in payload)
            {
                string keyToCompare = row.Key;
                var value = contentResponse[keyToCompare];

                Assert.That(value.ToString(), Is.EqualTo(row.Value));
            }

            Assert.IsFalse(contentResponse["id"].ToString() == "");
            Assert.IsFalse(contentResponse["createdAt"].ToString() == "");
        }

        [When(@"user sends a DELETE request")]
        public async Task WhenSendsADELETERequest()
        {
            var api = new APIClient();
            RestResponse response = await api.ExecuteDELETE((String)ScenarioContext.Current["ClientUrl"]);
            ScenarioContext.Current.Add("Response", response);
        }

        [When(@"user sends a GET request")]
        public async Task WhenSendsARequest()
        {
            var api = new APIClient();
            RestResponse response = await api.ExecuteGET((String)ScenarioContext.Current["ClientUrl"]);
            ScenarioContext.Current.Add("Response", response);
        }

        [Then(@"validate that correct users are listed")]
        public void ThenValidateThatUsersAreListed(Table table)
        {
            RestResponse response = (RestResponse)ScenarioContext.Current["Response"];

            JObject contentResponse = JObject.Parse(response.Content);
            int currentIndex = 0;

            JArray dataArrayFromResponse = (JArray)contentResponse["data"];

            foreach (var row in table.Rows)
            {
                var expecetedData = row.CreateInstance<DataModel>();
                contentResponse = (JObject)dataArrayFromResponse[currentIndex];

                VerifyDataModel(contentResponse, expecetedData);

                currentIndex++;
            }
        }

        [Then(@"validate that correct user is returned")]
        public void ThenValidateThatCorrectUserIsReturned(Table table)
        {
            RestResponse response = (RestResponse)ScenarioContext.Current["Response"];

            JObject contentResponse = JObject.Parse(response.Content);
            contentResponse = (JObject)contentResponse["data"];
            var expecetedData = table.CreateInstance<DataModel>();

            VerifyDataModel(contentResponse, expecetedData);
        }
    }
}
