using Newtonsoft.Json.Linq;
using Interface;
using Network;
using System.Collections.Generic;
using Common;
using System;

namespace ConnectorTypes
{

    public class REST : IConnector
    {

        JObject result;
        IList<object> param = new List<object>();

        public string getAddressBalance(string address)
        {
            throw new NotImplementedException();
        }

        public int getBlockGenerationTime()
        {
            throw new NotImplementedException();
        }

        public int getBlockHeight()
        {
            throw new NotImplementedException();
        }

        public int getBlockHeightByTxHash(string txHash)
        {
            throw new NotImplementedException();
        }

        public string getBlockHex(int blockHeight)
        {
            throw new NotImplementedException();
        }

        public string getBlockHex(string blockHash)
        {
            throw new NotImplementedException();
        }

        public string getBlockJson(int blockHeight)
        {
            throw new NotImplementedException();
        }

        public string getBlockJson(string blockHash)
        {
            throw new NotImplementedException();
        }

        public string getContractJson(string contractHash)
        {
            throw new NotImplementedException();
        }

        public int getNodeCount()
        {
            throw new NotImplementedException();
        }

        public string getRawTransactionHex(string txHash)
        {
            throw new NotImplementedException();
        }

        public string getRawTransactionJson(string txHash)
        {
            throw new NotImplementedException();
        }

        public string getSmartCodeEvent(int blockHeight)
        {
            throw new NotImplementedException();
        }

        public string getSmartCodeEvent(string txHash)
        {
            throw new NotImplementedException();
        }
    }
}

