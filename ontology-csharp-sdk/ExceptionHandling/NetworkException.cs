using System;
using OntologyCSharpSDK.Common;
using OntologyCSharpSDK.Network;
using System.Runtime.CompilerServices;

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
            _networkErrorCode = Convert.ToInt32(response.jobjectResponse.GetValue("error"));
            _networkErrorDescription = response.jobjectResponse.GetValue("desc").ToString();
            _networkRawError = response.rawResponse;
            _connectionMethod = connectionmethod;
            _networkRequestSent = request;
        }

    }

}
