using DemoAPITest.Data;
using DemoAPITest.Objects;
using DemoAPITest.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using System.Xml.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DemoAPITest.Steps
{
    [Binding]
    public class PostStepDefinitions
    {
        private CreateReq createReq;
        private RegisterLoginReq regLoginReq;
        private RestResponse response;
        private HttpStatusCode statusCode;
        private dynamic payload;

        private void DeterminePayload(String request)
        {
            switch(request.ToLower())
            {
                case "create":
                    payload = createReq;
                    break;
                case "register-successful":
                case "register-unsuccessful":
                case "login-successful":
                case "login-unsuccessful":
                    payload = regLoginReq;
                    break;
                default:
                    break;
            }
        }

        public PostStepDefinitions(CreateReq createReq, RegisterLoginReq regLoginReq)
        {
            this.createReq = createReq;
            this.regLoginReq = regLoginReq;
        }

        [Given(@"user inputs the name '([^']*)'")]
        public void GivenUserInputsTheName(string name)
        {
            createReq.name = name;
        }

        [Given(@"user inputs the job '([^']*)'")]
        public void GivenUserInputsTheJob(string job)
        {
            createReq.job = job;
        }

        [When(@"user sends '([^']*)' post request")]
        public async Task WhenUserSendsPostRequest(string request)
        {
            var api = new APIClient();
            DeterminePayload(request);
            response = await api.ExecutePostRequest(request, payload);
        }

        [Then(@"validate user is created")]
        public void ThenValidateUserIsCreated()
        {
            var content = ResponseParser.GetContent<CreateRes>(response);
            Assert.AreEqual(createReq.name, content.name);
            Assert.AreEqual(createReq.job, content.job);

            Helper.VerifyStatusResponse("201", response);
        }

        [Given(@"user inputs the email '([^']*)'")]
        public void GivenUserInputsTheEmail(string email)
        {
            regLoginReq.email = email;
        }

        [Given(@"user inputs the password '([^']*)'")]
        public void GivenUserInputsThePassword(string password)
        {
            regLoginReq.password = password;
        }

        [Then(@"validate user is registered successfully")]
        public void ThenValidateUserIsRegisteredSuccessfully()
        {
            var content = ResponseParser.GetContent<RegisterLoginRes>(response);
            Assert.AreEqual("4", content.id);
            //Assert.AreEqual("QpwL5tke4Pnpja7X4", content.token); TODO: Check how to properly validate tokens

            Helper.VerifyStatusResponse("200", response);
        }

        [Then(@"validate user is not registered due to missing password")]
        public void ThenValidateUserIsNotRegisteredDueToMissingPassword()
        {
            var content = ResponseParser.GetContent<RegisterLoginRes>(response);
            Assert.AreEqual("Missing password", content.error);

            Helper.VerifyStatusResponse("400", response);
        }

        [Then(@"validate user is logged in successfully")]
        public void ThenValidateUserIsLoggedInSuccessfully()
        {
            var content = ResponseParser.GetContent<RegisterLoginRes>(response);
            //Assert.AreEqual("QpwL5tke4Pnpja7X4", content.token); TODO: Check how to properly validate tokens

            Helper.VerifyStatusResponse("200", response);
        }

        [Then(@"validate missing password error is returned")]
        public void ThenValidateMissingPasswordErrorIsReturned()
        {
            var content = ResponseParser.GetContent<RegisterLoginRes>(response);
            Assert.AreEqual("Missing password", content.error);

            Helper.VerifyStatusResponse("400", response);
        }

        [Then(@"validate status code and message is correct")]
        public void ThenValidateStatusCodeAndMessageIsCorrect(Table table)
        {
            var data = table.CreateInstance<StatusRes>();

            var content = ResponseParser.GetContent<RegisterLoginRes>(response);
            Assert.AreEqual(data.Message, content.error);

            Helper.VerifyStatusResponse(data.StatusCode, response);
        }

        [Then(@"validate status code is '([^']*)'")]
        public void ThenValidateStatusCodeIs(string expectedStatusCode)
        {
            var content = ResponseParser.GetContent<RegisterLoginRes>(response);

            Helper.VerifyStatusResponse(expectedStatusCode, response);
        }
    }
}
