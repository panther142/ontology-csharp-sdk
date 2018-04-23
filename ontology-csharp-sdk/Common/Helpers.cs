using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Common.Helpers
{
    public static class Helpers
    {

        /// <summary>
        /// Builds a JSON string used to query an RPC node
        /// </summary>
        /// <param name="method"></param>
        /// <param name="param"></param>
        /// <returns></returns>

        public static string jsonRequestBuilder(string method, IList<object> param)
        {

            JObject jsonObject = new JObject();

            jsonObject["jsonrpc"] = "2.0";
            jsonObject["method"] = method;
            jsonObject["id"] = 1;

            JArray jsonArray = new JArray(param);

            jsonObject.Add("params", jsonArray);

            string json = JsonConvert.SerializeObject(jsonObject);
            return json;

        }

    }
}
