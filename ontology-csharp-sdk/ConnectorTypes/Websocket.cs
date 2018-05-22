using System;
using System.Collections.Generic;
using Interface;
using Common.Enums;
using Network.NetworkHelper;
using Common.Constants;
using Common.Cryptology;
using Newtonsoft.Json.Linq;

namespace ConnectorTypes
{

    public class Websocket : IConnector
    {
        IList<object> param = new List<object>();

        public string getAddressBalance(string address)
        {
            param.Clear();
            param.Add(new KeyValuePair<string, object>("Raw", 0));
            param.Add(new KeyValuePair<string, object>("Addr", address));
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "getbalance", param);
            return response.jobjectResponse["Result"].ToString();
        }

        public string getBestBlockHash()
        {
            throw new NotImplementedException();
        }

        public int getBlockGenerationTime()
        {
            param.Clear();
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "getgenerateblocktime", param);
            return (int)response.jobjectResponse["Result"];
        }

        public string getBlockHashByHeight(int blockHeight)
        {
            param.Clear();
            param.Add(new KeyValuePair<string, object>("Height", blockHeight));
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "getblockhash", param);
            return response.jobjectResponse["Result"].ToString();
        }

        public int getBlockHeight()
        {
            param.Clear();
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "getblockheight", param);
            return (int)response.jobjectResponse["Result"];
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
            param.Clear();
            param.Add(new KeyValuePair<string, object>("Height", blockHeight));
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "getblockbyheight", param);
            return response.jobjectResponse["Result"].ToString();
        }

        public string getBlockJson(string blockHash)
        {
            param.Clear();
            param.Add(new KeyValuePair<string, object>("Raw", 0));
            param.Add(new KeyValuePair<string, object>("Hash", blockHash));
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "getblockbyhash", param);
            return response.jobjectResponse["Result"].ToString();
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
            param.Clear();
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "getconnectioncount", param);
            return (int)response.jobjectResponse["Result"];
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


