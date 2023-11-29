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
    public class RegisterAPIStepDefinition
    {
        [Then(@"validate correct register response details are returned")]
        public void ThenValidateCorrectRegisterResponseDetailsAreReturned()
        {
            var responseContent = APIClient.GetContent<RegisterLoginRes>((RestResponse)ScenarioContext.Current["Response"]);

            Assert.IsFalse(responseContent.id == "");
            Assert.IsFalse(responseContent.token == "");
        }
    }
}
