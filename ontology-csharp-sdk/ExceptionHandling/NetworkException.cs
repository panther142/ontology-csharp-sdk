using OntologyCSharpSDK.Common;
using OntologyCSharpSDK.Network;
using System;


namespace OntologyCSharpSDK.ExceptionHandling
{

    public class NetworkException : Exception
    {
        readonly public string _networkRequestSent;
        readonly public int _networkErrorCode;
        readonly public string _networkErrorDescription;
        readonly public string _networkRawError;
        readonly public Protocol _connectionMethod;


        public NetworkException()
        {
        }

        public NetworkException(string message)
            : base(message)
        {
        }

        public NetworkException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public NetworkException(string message, NetworkResponse response, Protocol connectionmethod, string request)
            : base(message)
        {
            try
            {
                string errorfield = "Error";
                string descriptionfield = "Desc";

                if (connectionmethod == Protocol.RPC)
                {
                    errorfield = "error";
                    descriptionfield = "desc";
                }

                _networkErrorCode = Convert.ToInt32(response.jobjectResponse.GetValue(errorfield).ToString());
                _networkErrorDescription = response.jobjectResponse.GetValue(descriptionfield).ToString();
                _networkRawError = response.rawResponse;
                _connectionMethod = connectionmethod;
                _networkRequestSent = request;
            }
            catch { throw; }
        }
    }
}
