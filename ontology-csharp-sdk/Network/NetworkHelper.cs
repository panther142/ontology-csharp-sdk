﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using Common.Constants;

namespace Network.NetworkHelper
{
    class NetworkHelper
    {
        //TODO: Nodelist implementation

        public static NetworkResponse sendNetworkRequest(Common.Enums.Protocol protocol, string requestType, string method, IList<object> param)
        {

            requestType = requestType.ToUpper();

            string host = Basic.Account._node;

            string request = "";

            switch (protocol)
            {
                case Common.Enums.Protocol.RPC:
                    {
                        request = rpcRequestBuilder(method, param);
                        return sendRPCRequest(request, requestType, host);
                    }

                case Common.Enums.Protocol.REST:
                    {
                        if (requestType == "GET")
                        {
                            request = restRequestBuilder(method, param);
                        }
                        else
                        {
                            request = restBuildSendRawTransaction(method, param);
                        }
                        return sendRESTRequest(request, requestType, host);
                    }
                default:
                    return null;
            }

        }

        private static NetworkResponse sendRPCRequest(string request, string requestType, string host)
        {
            NetworkResponse response = null;

            WebResponse webResponse = null;
            HttpWebRequest webRequest = null;
            byte[] byteArray;

            // Create WebRequest object
            try
            {
                webRequest = (HttpWebRequest)WebRequest.Create(host);
                webRequest.ContentType = "application/json-rpc";
                webRequest.Method = requestType;
                byteArray = Encoding.UTF8.GetBytes(request);
                webRequest.ContentLength = byteArray.Length;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while creating HttpWebRequest object", ex.InnerException);
            }

            // Send network request
            try
            {

                using (Stream requestStream = webRequest.GetRequestStream())
                {
                    requestStream.Write(byteArray, 0, byteArray.Length);
                }
            }
            catch (WebException)
            {
                throw;
            }


            response = new NetworkResponse();
            //Receive response from network node
            try
            {
                using (webResponse = webRequest.GetResponse())
                {
                    using (Stream str = webResponse.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(str))
                        {
                            response.rawResponse = sr.ReadToEnd();
                            response.jobjectResponse = JsonConvert.DeserializeObject<JObject>(response.rawResponse);
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

        private static NetworkResponse sendRESTRequest(string request, string requestType, string host)
        {

            NetworkResponse response = null;
            WebResponse webResponse = null;
            HttpWebRequest webRequest = null;
            byte[] byteArray;

            // Create WebRequest object
            try
            {
                switch (requestType)
                {
                    case "GET":
                        webRequest = (HttpWebRequest)WebRequest.Create(host + request);
                        break;
                    case "POST":
                        webRequest = (HttpWebRequest)WebRequest.Create(host + Constants.REST_sendRawTransaction);
                        break;
                    default:
                        break;
                }

                webRequest.ContentType = "application/json";
                webRequest.Method = requestType;


            }
            catch (Exception ex)
            {
                throw new Exception("Error while creating HttpWebRequest object", ex.InnerException);
            }

            response = new NetworkResponse();

            // Send request and receive network response
            try
            {

                if (requestType == "POST")
                {
                    byteArray = Encoding.UTF8.GetBytes(request);
                    webRequest.ContentLength = byteArray.Length;

                    using (Stream stream = webRequest.GetRequestStream())
                    {
                        stream.Write(byteArray, 0, byteArray.Length);
                    }
                }

                using (webResponse = webRequest.GetResponse())
                {
                    using (Stream str = webResponse.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(str))
                        {
                            response.rawResponse = sr.ReadToEnd();
                            response.jobjectResponse = JsonConvert.DeserializeObject<JObject>(response.rawResponse);
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


        private static string rpcRequestBuilder(string method, IList<object> param)
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


        private static string restRequestBuilder(string method, IList<object> param)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(method);

            if (param != null)
            {
                foreach (object o in param)
                {
                    sb.Append("/" + o);
                }

                return sb.ToString();
            }
            else
            {
                return sb.ToString();
            }
        }

        public static string restBuildSendRawTransaction(string method, IList<object> param)
        {

            JObject jsonObject = new JObject();

            jsonObject["Action"] = "sendrawtransaction";
            jsonObject["Version"] = "1.0.0";
            jsonObject["Data"] = param[0].ToString();

            string json = JsonConvert.SerializeObject(jsonObject);
            return json;
        }


    }

    //TODO: Implement status codes
    public class NetworkResponse
    {
        public string rawResponse { get; set; }
        public JObject jobjectResponse { get; set; }
    }
}