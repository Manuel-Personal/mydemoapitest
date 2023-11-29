using DemoAPITest.Objects;
using DemoAPITest.Utility;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Net;
using TechTalk.SpecFlow;

namespace DemoAPITest.Steps
{
    [Binding]
    public class CommonStepDefinitions
    {
        [Given(@"the following details")]
        public void GivenTheFollowingDetails(Table table)
        {
            var payload = new Dictionary<string, string>();
            foreach (var row in table.Rows)
            {
                payload.Add(row[0], row[1]);
            }

            ScenarioContext.Current.Add("Payload", payload);
        }

        [When(@"user uses API endpoint '([^']*)'")]
        public void WhenUserUsesAPIEndpoint(string endpoint)
        {
            var clientUrl = Path.Combine(Config.getBaseUrl(), endpoint);
            ScenarioContext.Current.Add("ClientUrl", clientUrl);
        }

        [Then(@"validate response code is '([^']*)'")]
        public void ThenValidateResponseCodeIs(string expectedCode)
        {
            RestResponse response = (RestResponse)ScenarioContext.Current["Response"];
            HttpStatusCode actualCode = response.StatusCode;
            var code = (int)actualCode;

            Assert.That(code.ToString(), Is.EqualTo(expectedCode));
        }

        [When(@"user sends a POST request")]
        public async Task WhenSendsAPOSTRequest()
        {
            var api = new APIClient();
            RestResponse response = await api.ExecutePOST((String)ScenarioContext.Current["ClientUrl"], (dynamic)ScenarioContext.Current["Payload"]);
            ScenarioContext.Current.Add("Response", response);
        }

        [Then(@"validate response body is correct")]
        public void ThenValidateResponseBodyIsCorrect(Table table)
        {
            var dictionary = DictionaryUtil.ToDictionary(table);

            RestResponse response = (RestResponse)ScenarioContext.Current["Response"];
            JObject contentResponse = JObject.Parse(response.Content);

            foreach(var row in table.Rows)
            {
                string keyToCompare = row[0];
                var value = contentResponse[keyToCompare];

                Assert.That(value.ToString(), Is.EqualTo(row[1]));
            }
        }
    }
}
