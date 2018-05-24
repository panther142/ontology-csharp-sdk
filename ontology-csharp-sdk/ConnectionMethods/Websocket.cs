using System;
using System.Collections.Generic;
using OntologyCSharpSDK.Interface;
using OntologyCSharpSDK.Common;
using OntologyCSharpSDK.Network;

namespace OntologyCSharpSDK.ConnectionMethods
{

    public class Websocket : IConnectionMethod
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
            param.Clear();
            param.Add(new KeyValuePair<string, object>("Hash", txHash));
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "getblockheightbytxhash", param);
            return (int)response.jobjectResponse["Result"];
        }

        public string getBlockHex(int blockHeight)
        {
            param.Clear();
            param.Add(new KeyValuePair<string, object>("Raw", 1));
            param.Add(new KeyValuePair<string, object>("Height", blockHeight));
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "getblockbyheight", param);
            return response.jobjectResponse["Result"].ToString();
        }

        public string getBlockHex(string blockHash)
        {
            param.Clear();
            param.Add(new KeyValuePair<string, object>("Raw", 1));
            param.Add(new KeyValuePair<string, object>("Hash", blockHash));
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "getblockbyhash", param);
            return response.jobjectResponse["Result"].ToString();
        }

        public string getBlockJson(int blockHeight)
        {
            param.Clear();
            param.Add(new KeyValuePair<string, object>("Raw", 0));
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
            param.Clear();
            param.Add(new KeyValuePair<string, object>("Hash", contractHash));
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "getcontract", param);
            return response.jobjectResponse["Result"].ToString();
        }

        public string getMempoolTxState(string txHash)
        {
            throw new NotImplementedException();
        }

        public string getMerkleProof(string hash)
        {
            param.Clear();
            param.Add(new KeyValuePair<string, object>("Hash", hash));
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "getmerkleproof", param);
            return response.jobjectResponse["Result"].ToString();
        }

        public int getNodeCount()
        {
            param.Clear();
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "getconnectioncount", param);
            return (int)response.jobjectResponse["Result"];
        }

        public string getRawTransactionHex(string txHash)
        {
            param.Clear();
            param.Add(new KeyValuePair<string, object>("Raw", 1));
            param.Add(new KeyValuePair<string, object>("Hash", txHash));
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "gettransaction", param);
            return response.jobjectResponse["Result"].ToString();
        }

        public string getRawTransactionJson(string txHash)
        {
            param.Clear();
            param.Add(new KeyValuePair<string, object>("Raw", 0));
            param.Add(new KeyValuePair<string, object>("Hash", txHash));
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "gettransaction", param);
            return response.jobjectResponse["Result"].ToString();
        }

        public string getSmartCodeEvent(int blockHeight)
        {
            param.Clear();
            param.Add(new KeyValuePair<string, object>("Height", blockHeight));
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "getsmartcodeeventbyheight", param);
            return response.jobjectResponse["Result"].ToString();
        }

        public string getSmartCodeEvent(string txHash)
        {
            param.Clear();
            param.Add(new KeyValuePair<string, object>("Height", txHash));
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "getsmartcodeeventbyhash", param);
            return response.jobjectResponse["Result"].ToString();
        }

        public string getStorage(string contractHash, string key)
        {
            param.Clear();
            param.Add(new KeyValuePair<string, object>("Hash", contractHash));
            param.Add(new KeyValuePair<string, object>("Key", key));
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "getstorage", param);
            return response.jobjectResponse["Result"].ToString();
        }

        public int getVersion()
        {
            throw new NotImplementedException();
        }

        public string setSendRawTransaction(string tx, bool preExec)
        {            
            param.Clear();
            param.Add(new KeyValuePair<string, object>("PreExec", Convert.ToInt32(preExec)));
            param.Add(new KeyValuePair<string, object>("Data", tx));
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "sendrawtransaction", param);
            return response.jobjectResponse["Result"].ToString();
        }
    }
}


