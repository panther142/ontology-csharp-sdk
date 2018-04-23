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
        public static APIResult sendRESTrequest(string url, string verb, List<HttpRequestHeader> headers, string content)
        {
            APIResult result = null;
            try
            {


                // Create a request using a URL that can receive a post. 
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                // Set the Method property of the request to POST.
                //request.Headers.Add("Authorization", authheader);
                if(headers!=null)
                {
                    if (headers.Count > 0)
                    {
                        foreach (var header in headers)
                        {
                            request.Headers.Add(header.key, header.value);
                        }
                    }
                }
               

                request.Method = verb;
                request.Timeout = 600000;
                if (headers != null)
                {
                    if (headers.Count > 0)
                    {
                        foreach (var header in headers)
                        {
                            request.Headers[header.key] = header.value;
                        }
                    }
                }


                //request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*;q=0.8";
                //request.Headers["SOAPAction"] = "\"Retrieve\"";
                // Create POST data and convert it to a byte array.string postData = "This is a test that posts this string to a Web server.";
                request.ContentType = "application/json";

                if (!String.IsNullOrEmpty(content))
                {
                    byte[] byteArray = Encoding.UTF8.GetBytes(content.ToString());
                    // Set the ContentType property of the WebRequest.

                    request.ContentLength = byteArray.Length;

                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(byteArray, 0, byteArray.Length);
                    }
                }
                else
                {
                    request.ContentLength = 0;
                }


                result = new APIResult();

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    result.status_code = (int)response.StatusCode;
                    // Get response
                    using (Stream stream = response.GetResponseStream())
                    {
                        Stream dataStream = response.GetResponseStream();
                        // Open the stream using a StreamReader for easy access.
                        StreamReader reader = new StreamReader(dataStream);

                        // Read the content.
                        result.content = reader.ReadToEnd();
                    }


                }


            }
            catch (Exception ex)
            {
                result.content = ex.Message + ex.StackTrace;
                result.status_code = 400;
            }

            return result;
        }

    }

    public class HttpRequestHeader
    {
        public string key { get; set; }
        public string value { get; set; }
    }

    public class APIResult
    {
        public int status_code { get; set; }
        public string content { get; set; }
    }
}


