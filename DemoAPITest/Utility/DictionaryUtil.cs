using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Net;
using TechTalk.SpecFlow;

namespace DemoAPITest.Utility
{
    public class DictionaryUtil
    {
        public static string baseUrl = "https://reqres.in/api";

        public static Dictionary<string, string> ToDictionary(Table table)
        {
            var dictionary = new Dictionary<string, string>();
            foreach (var row in table.Rows)
            {
                dictionary.Add(row[0], row[1]);
            }

            return dictionary;
        }

        public static String GetJsonValue(String key)
        {
            String jsonPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            String jsonSource = File.ReadAllText(jsonPath + "\\Utility\\Constants.json");
            dynamic data = JObject.Parse(jsonSource);

            return data[key];
        }
    }
}
