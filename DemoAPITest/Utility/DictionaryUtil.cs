using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Net;
using TechTalk.SpecFlow;

namespace DemoAPITest.Utility
{
    public class DictionaryUtil
    {
        public static Dictionary<string, string> ToDictionary(Table table)
        {
            var dictionary = new Dictionary<string, string>();
            foreach (var row in table.Rows)
            {
                dictionary.Add(row[0], row[1]);
            }

            return dictionary;
        }
    }
}
