using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Common.Helpers
{
    public class Helpers
    {
        public static string jsonRequestBuilder(string method, int id, IList<object> param)
        {

            JObject jsonObject = new JObject();

            jsonObject["jsonrpc"] = "2.0";
            jsonObject["method"] = method;
            jsonObject["id"] = id;

            JArray jsonArray = new JArray(param);

            jsonObject.Add("params", jsonArray);

            string json = JsonConvert.SerializeObject(jsonObject);
            return json;

        }

    }



}
