using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Collections.Generic;
using Common.Helpers;

namespace Network
{
    public class RPCrequests
    {

        public static JObject sendRPCrequest(string method, IList<object> parameters)
        {
            WebResponse RPCResponse = null;
            string jsonRequest = Helpers.jsonRequestBuilder(method, parameters);
            HttpWebRequest ontRPCRequest = (HttpWebRequest)WebRequest.Create("http://ont-privnet:20336");

            ontRPCRequest.ContentType = "application/json-rpc";
            ontRPCRequest.Method = "POST";

            byte[] byteArray = Encoding.UTF8.GetBytes(jsonRequest);
            ontRPCRequest.ContentLength = byteArray.Length;

            // Send JSON request
            try
            {
                using (Stream ontRPCRequestStream = ontRPCRequest.GetRequestStream())
                {
                    ontRPCRequestStream.Write(byteArray, 0, byteArray.Length);
                }
            }
            catch (WebException)
            {
                throw;
            }

            //Receive response
            try
            {
                using (RPCResponse = ontRPCRequest.GetResponse())
                {
                    using (Stream str = RPCResponse.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(str))
                        {

                            JObject response = JsonConvert.DeserializeObject<JObject>(sr.ReadToEnd());
                            return response;

                        }
                    }
                }
            }
            catch (WebException)
            {
                throw;
            }

        }
    }
}


