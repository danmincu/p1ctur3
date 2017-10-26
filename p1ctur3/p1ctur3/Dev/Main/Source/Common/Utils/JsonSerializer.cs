using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telekad.Utils
{
    public static class JsonSerializer
    {

        public static string ToJson(Dictionary<string, string> input)
        {
            string json = JsonConvert.SerializeObject(input, Formatting.Indented);
            return json;
        }

        public static Dictionary<string, string> FromJson(string xml)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(xml);
        }        
    }


    public static class JsonSerializer<T>
    {

        public static string ToJson(T input)
        {
            string json = JsonConvert.SerializeObject(input, Formatting.Indented);
            return json;
        }

        public static T FromJson(string xml)
        {
            return JsonConvert.DeserializeObject<T>(xml);
        }
    }

}
