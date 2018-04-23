using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Collections.Generic;
using Common.Helpers;
using System;

namespace Network
{
    public class RPCrequests
    {

        /// <summary>
        /// Sends RPC request to an RPC node and returns response
        /// </summary>
        /// <param name="method"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static JObject sendRPCrequest(string url, string method, IList<object> parameters)
        {

            string jsonRequest = Helpers.jsonRequestBuilder(method, parameters);
            WebResponse RPCResponse = null;
            HttpWebRequest ontRPCRequest = null;
            byte[] byteArray;

            
            // Create WebRequest object
            try
            {
                ontRPCRequest = (HttpWebRequest)WebRequest.Create(url);
                ontRPCRequest.ContentType = "application/json-rpc";
                ontRPCRequest.Method = "POST";
                byteArray = Encoding.UTF8.GetBytes(jsonRequest);
                ontRPCRequest.ContentLength = byteArray.Length;
            }
            catch (Exception ex)
            {
                throw new Exception("RPCrequests error while creating HttpWebRequest object", ex.InnerException);
            }

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

            //Receive response from RPC node
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


