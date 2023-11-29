using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoAPITest.Utility
{
    public class Config
    {
        public static string getBaseUrl()
        {
            string json = File.ReadAllText(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.ToString() + "\\Constants.json");
            dynamic data = JObject.Parse(json);

            return data.baseUrl;
        }
    }
}
