using System;
using System.Collections.Generic;
using Interface;
using Common.Enums;
using Network.NetworkHelper;
using Common.Constants;
using Common.Cryptology;

namespace ConnectorTypes
{

    public class Websocket : IConnector
    {
        IList<object> param = new List<object>();

        public string getAddressBalance(string address)
        {
            throw new NotImplementedException();
        }

        public string getBestBlockHash()
        {
            throw new NotImplementedException();
        }

        public int getBlockGenerationTime()
        {
            throw new NotImplementedException();
        }

        public string getBlockHashByHeight(int blockHeight)
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

        public int getBlockSysFee(int index)
        {
            throw new NotImplementedException();
        }

        public string getContractJson(string contractHash)
        {
            throw new NotImplementedException();
        }

        public string getContractState(string scriptHash)
        {
            throw new NotImplementedException();
        }

        public string getMempoolTxState(string txHash)
        {
            throw new NotImplementedException();
        }

        public string getMerkleProof(string hash)
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

        public string getStorage(string contractHash, string key)
        {
            throw new NotImplementedException();
        }

        public int getVersion()
        {
            throw new NotImplementedException();
        }

        public string setSendRawTransaction(string tx)
        {
            throw new NotImplementedException();
        }
    }
}


