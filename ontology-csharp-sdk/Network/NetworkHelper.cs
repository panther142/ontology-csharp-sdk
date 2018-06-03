using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OntologyCSharpSDK.Common;
using OntologyCSharpSDK.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using WebSocketSharp;


namespace OntologyCSharpSDK.Network
{
    class NetworkHelper
    {
        //TODO: Nodelist implementation

        public static NetworkResponse sendNetworkRequest(Protocol protocol, string requestType, string method, IList<object> param)
        {

            try
            {
                requestType = requestType.ToUpper();

                string host = Basic.Account._node;

                string request = "";

                switch (protocol)
                {
                    case Protocol.RPC:
                        {
                            request = rpcRequestBuilder(method, param);
                            return sendRPCRequest(request, requestType, host);
                        }

                    case Protocol.REST:
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

                    case Protocol.Websocket:
                        {
                            request = webSocketRequestBuilder(method, param);
                            return sendWebSocketRequest(request, host);
                        }

                    default:
                        return null;
                }
            }
            catch { throw; }
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
                throw new NetworkException("Error while creating HttpWebRequest object", ex.InnerException);
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

                            if (Convert.ToInt32(response.jobjectResponse.GetValue("error")) == 0)
                            {
                                return response;
                            }
                            else
                            {
                                throw new NetworkException("An error response was received from the server.", response, Protocol.RPC, request);
                            }
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

                            if (Convert.ToInt32(response.jobjectResponse.GetValue("Error")) == 0)
                            {
                                return response;
                            }
                            else
                            {
                                throw new NetworkException("An error response was received from the server.", response, Protocol.REST, request);
                            }
                        }
                    }
                }
            }
            catch (WebException)
            {
                throw;
            }
        }

        private static NetworkResponse sendWebSocketRequest(string request, string host)
        {
            try
            {

                NetworkResponse response = new NetworkResponse();

                using (var ws = new WebSocket(host))
                {

                    ws.OnMessage += (sender, e) =>
                    {
                        response.rawResponse = e.Data;
                        response.jobjectResponse = JsonConvert.DeserializeObject<JObject>(response.rawResponse);
                        ws.Close();
                    };

                    ws.OnError += (sender, e) =>
                    {
                        response.rawResponse = e.Exception.InnerException.ToString();
                        ws.Close();
                    };

                    ws.Connect();
                    ws.Send(request);

                    while (ws.IsAlive)
                    {
                    }

                    if (Convert.ToInt32(response.jobjectResponse.GetValue("Error")) == 0)
                    {
                        return response;
                    }
                    else
                    {
                        throw new NetworkException("An error response was received from the server.", response, Protocol.REST, request);
                    }
                }
            }
            catch { throw; }
        }

        private static string rpcRequestBuilder(string method, IList<object> param)
        {
            try
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
            catch { throw; }
        }


        private static string restRequestBuilder(string method, IList<object> param)
        {
            try
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
            catch { throw; }
        }

        private static string webSocketRequestBuilder(string method, IList<object> param)
        {
            try
            {
                JObject jsonObject = new JObject();

                jsonObject["Action"] = method;
                jsonObject["Version"] = "1.0.0";

                foreach (KeyValuePair<string, object> kvp in param)
                {
                    JToken jt = JToken.FromObject(kvp.Value);
                    jsonObject.Add(kvp.Key, jt);
                }

                string json = JsonConvert.SerializeObject(jsonObject);
                return json;
            }
            catch { throw; }
        }

        public static string restBuildSendRawTransaction(string method, IList<object> param)
        {
            try
            {
                JObject jsonObject = new JObject();

                jsonObject["Action"] = "sendrawtransaction";
                jsonObject["Version"] = "1.0.0";
                jsonObject["Data"] = param[0].ToString();

                string json = JsonConvert.SerializeObject(jsonObject);
                return json;
            }
            catch { throw; }
        }


    }

    //TODO: Implement status codes
    public class NetworkResponse
    {
        public string rawResponse { get; set; }
        public JObject jobjectResponse { get; set; }
    }
}
