using DemoAPITest.Data;
using DemoAPITest.Objects;
using DemoAPITest.Utility;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Net;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DemoAPITest.Steps
{
    [Binding]
    public class CommonStepDefinitions
    {
        public String clientUrl = "";
        private RestResponse response;
        private dynamic payload;

        CreateReq createReq;
        RegisterLoginReq regLoginReq;

        public CommonStepDefinitions(CreateReq createReq, RegisterLoginReq regLoginReq)
        {
            this.createReq = createReq;
            this.regLoginReq = regLoginReq;
        }

        [Given(@"user uses API endpoint '([^']*)'")]
        public void WhenUserUsesAPIEndpoint(string endpoint)
        {
            clientUrl = Path.Combine(CommonUtils.baseUrl, endpoint);
        }

        [When(@"user sends a GET request")]
        public async Task WhenSendsARequest()
        {
            var api = new APIClient();
            response = await api.ExecuteGET(clientUrl);
        }

        [Then(@"validate correct page details are returned")]
        public void ThenValidateCorrectPageDetailsAreReturned(Table table)
        {
            var dictionary = CommonUtils.ToDictionary(table);

            var responseContent = ResponseParser.GetContent<PageInfoModel>(response);
            Assert.That(responseContent.page, Is.EqualTo(dictionary["page"]));
            Assert.That(responseContent.per_page, Is.EqualTo(dictionary["per_page"]));
            Assert.That(responseContent.total, Is.EqualTo(dictionary["total"]));
            Assert.That(responseContent.total_pages, Is.EqualTo(dictionary["total_pages"]));
        }

        [Then(@"validate that correct user/s are listed")]
        public void ThenValidateThatUsersAreListed(Table table)
        {
            JObject contentResponse = JObject.Parse(response.Content);
            int currentIndex = 0;

            JArray dataArrayFromResponse = new JArray();
            if (table.RowCount > 1)
            {
                dataArrayFromResponse = (JArray)contentResponse["data"];
            }
            else
            {
                contentResponse = (JObject)contentResponse["data"];
            }

            foreach (var row in table.Rows)
            {
                var data = row.CreateInstance<DataModel>();

                if (table.RowCount > 1)
                {
                    contentResponse = (JObject)dataArrayFromResponse[currentIndex];
                }

                Assert.AreEqual(data.id, contentResponse["id"].ToString());
                Assert.AreEqual(data.email, contentResponse["email"].ToString());
                Assert.AreEqual(data.first_name, contentResponse["first_name"].ToString());
                Assert.AreEqual(data.last_name, contentResponse["last_name"].ToString());
                Assert.AreEqual(data.avatar, contentResponse["avatar"].ToString());

                currentIndex++;
            }
        }

        [Then(@"validate response code is '([^']*)'")]
        public void ThenValidateResponseCodeIs(string expectedCode)
        {
            HttpStatusCode actualCode = response.StatusCode;
            var code = (int)actualCode;

            Assert.AreEqual(expectedCode, code.ToString());
        }

        [Given(@"uses the following details")]
        public void GivenTheFollowingDetails(Table table)
        {
            var dictionary = CommonUtils.ToDictionary(table);

            if (clientUrl.Contains("users"))
            {
                createReq.name = dictionary["name"];
                createReq.job = dictionary["job"];
                payload = createReq;
            }
            else if(clientUrl.Contains("register") || clientUrl.Contains("login"))
            {
                regLoginReq.email = dictionary["email"];

                try
                {
                    regLoginReq.password = dictionary["password"];
                }
                catch (KeyNotFoundException e)
                {
                    //Left empty for a reason
                }

                payload = regLoginReq;
            }
        }

        [When(@"user sends a POST request")]
        public async Task WhenSendsAPOSTRequest()
        {
            var api = new APIClient();
            response = await api.ExecutePOST(clientUrl, payload);
        }

        [Then(@"validate correct create user details are returned")]
        public void ThenValidateCorrectCreateUserDetailsAreReturned()
        {
            var responseContent = ResponseParser.GetContent<CreateRes>(response);

            Assert.That(responseContent.name, Is.EqualTo(createReq.name));
            Assert.That(responseContent.job, Is.EqualTo(createReq.job));
            Assert.IsFalse(responseContent.id == "");
            Assert.IsFalse(responseContent.createdAt.ToString() == "");
        }

        [When(@"user sends a DELETE request")]
        public async Task WhenSendsADELETERequest()
        {
            var api = new APIClient();
            response = await api.ExecuteDELETE(clientUrl);
        }

        [Then(@"validate correct register response details are returned")]
        public void ThenValidateCorrectRegisterResponseDetailsAreReturned()
        {
            var responseContent = ResponseParser.GetContent<RegisterLoginRes>(response);

            Assert.IsFalse(responseContent.id == "");
            Assert.IsFalse(responseContent.token == "");
        }

        [Then(@"validate error message is correct")]
        public void ThenValidateErrorMessageIsCorrect(Table table)
        {
            var dictionary = CommonUtils.ToDictionary(table);

            JObject contentResponse = JObject.Parse(response.Content);

            Assert.AreEqual(dictionary["error"], contentResponse["error"].ToString());
        }

        [Then(@"validate correct login response details are returned")]
        public void ThenValidateCorrectLoginResponseDetailsAreReturned()
        {
            var responseContent = ResponseParser.GetContent<RegisterLoginRes>(response);

            Assert.IsFalse(responseContent.token == "");
        }
    }
}
