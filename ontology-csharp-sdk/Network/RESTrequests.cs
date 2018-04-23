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
    public class RESTrequests
    {

        /// <summary>
        /// Sends RPC request to an RPC node and returns response
        /// </summary>
        /// <param name="method"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static JObject sendRESTrequest(string method, IList<object> parameters)
        {

            string jsonRequest = Helpers.RPCJsonRequestBuilder(method, parameters);
            WebResponse RESTResponse = null;
            HttpWebRequest ontRESTRequest = null;
            byte[] byteArray;


            // Create WebRequest object
            try
            {
                ontRESTRequest = (HttpWebRequest)WebRequest.Create("http://ont-privnet:20336");
                ontRESTRequest.ContentType = "application/json-rpc";
                ontRESTRequest.Method = "POST";
                byteArray = Encoding.UTF8.GetBytes(jsonRequest);
                ontRESTRequest.ContentLength = byteArray.Length;
            }
            catch (Exception ex)
            {
                throw new Exception("RESTrequests error while creating HttpWebRequest object", ex.InnerException);
            }

            // Send JSON request
            try
            {
                using (Stream ontRESTRequestStream = ontRESTRequest.GetRequestStream())
                {
                    ontRESTRequestStream.Write(byteArray, 0, byteArray.Length);
                }
            }
            catch (WebException)
            {
                throw;
            }

            //Receive response from REST node
            try
            {
                using (RESTResponse = ontRESTRequest.GetResponse())
                {
                    using (Stream str = RESTResponse.GetResponseStream())
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


