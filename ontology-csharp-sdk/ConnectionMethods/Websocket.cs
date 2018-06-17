using OntologyCSharpSDK.Common;
using OntologyCSharpSDK.Interface;
using OntologyCSharpSDK.Network;
using System;
using System.Collections.Generic;

namespace OntologyCSharpSDK.ConnectionMethods
{

    public class Websocket : IConnectionMethod
    {
        IList<object> param = new List<object>();

        public string getAddressBalance(string address)
        {
            try
            {
                param.Clear();
                param.Add(new KeyValuePair<string, object>("Raw", 0));
                param.Add(new KeyValuePair<string, object>("Addr", address));
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "getbalance", param);
                return response.jobjectResponse["Result"].ToString();
            }
            catch { throw; }
        }

        public string getAllowance(string asset, string fromAddress, string toAddress)
        {
            try
            {
                param.Clear();
                param.Add(new KeyValuePair<string, object>("Asset", "ont"));
                param.Add(new KeyValuePair<string, object>("From", fromAddress));
                param.Add(new KeyValuePair<string, object>("To", toAddress));
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "getallowance", param);
                return response.jobjectResponse["Result"].ToString();
            }
            catch { throw; }
        }

        public string getBestBlockHash()
        {
            throw new NotImplementedException();
        }

        public int getBlockGenerationTime()
        {
            try
            {
                param.Clear();
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "getgenerateblocktime", param);
                return (int)response.jobjectResponse["Result"];
            }
            catch { throw; }
        }

        public string getBlockHashByHeight(int blockHeight)
        {
            try
            {
                param.Clear();
                param.Add(new KeyValuePair<string, object>("Height", blockHeight));
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "getblockhash", param);
                return response.jobjectResponse["Result"].ToString();
            }
            catch { throw; }
        }

        public int getBlockHeight()
        {
            try
            {
                param.Clear();
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "getblockheight", param);
                return (int)response.jobjectResponse["Result"];
            }
            catch { throw; }
        }

        public int getBlockHeightByTxHash(string txHash)
        {
            try
            {
                param.Clear();
                param.Add(new KeyValuePair<string, object>("Hash", txHash));
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "getblockheightbytxhash", param);
                return (int)response.jobjectResponse["Result"];
            }
            catch { throw; }
        }

        public string getBlockHex(int blockHeight)
        {
            try
            {
                param.Clear();
                param.Add(new KeyValuePair<string, object>("Raw", 1));
                param.Add(new KeyValuePair<string, object>("Height", blockHeight));
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "getblockbyheight", param);
                return response.jobjectResponse["Result"].ToString();
            }
            catch { throw; }
        }

        public string getBlockHex(string blockHash)
        {
            try
            {
                param.Clear();
                param.Add(new KeyValuePair<string, object>("Raw", 1));
                param.Add(new KeyValuePair<string, object>("Hash", blockHash));
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "getblockbyhash", param);
                return response.jobjectResponse["Result"].ToString();
            }
            catch { throw; }
        }

        public string getBlockJson(int blockHeight)
        {
            try
            {
                param.Clear();
                param.Add(new KeyValuePair<string, object>("Raw", 0));
                param.Add(new KeyValuePair<string, object>("Height", blockHeight));
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "getblockbyheight", param);
                return response.jobjectResponse["Result"].ToString();
            }
            catch { throw; }
        }

        public string getBlockJson(string blockHash)
        {
            try
            {
                param.Clear();
                param.Add(new KeyValuePair<string, object>("Raw", 0));
                param.Add(new KeyValuePair<string, object>("Hash", blockHash));
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "getblockbyhash", param);
                return response.jobjectResponse["Result"].ToString();
            }
            catch { throw; }
        }

        public int getBlockSysFee(int index)
        {
            throw new NotImplementedException();
        }

        public string getBlockTxsByHeight(int blockHeight)
        {
            try
            {
                param.Clear();
                param.Add(new KeyValuePair<string, object>("Height", blockHeight));
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "getblocktxsbyheight", param);
                return response.jobjectResponse["Result"].ToString();
            }
            catch { throw; }
        }

        public string getContractJson(string contractHash)
        {
            try
            {
                param.Clear();
                param.Add(new KeyValuePair<string, object>("Hash", contractHash));
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "getcontract", param);
                return response.jobjectResponse["Result"].ToString();
            }
            catch { throw; }
        }

        public string getGasPrice()
        {
            try
            {
                param.Clear();
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "getgasprice", param);
                return response.jobjectResponse["Result"].ToString();
            }
            catch { throw; }
        }

        public string getMempoolTxState(string txHash)
        {
            try
            {
                param.Clear();
                param.Add(txHash);
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "getmempooltxstate", param);
                return response.jobjectResponse["result"].ToString();
            }
            catch { throw; }
        }

        public string getMerkleProof(string hash)
        {
            try
            {
                param.Clear();
                param.Add(new KeyValuePair<string, object>("Hash", hash));
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "getmerkleproof", param);
                return response.jobjectResponse["Result"].ToString();
            }
            catch { throw; }
        }

        public int getNodeCount()
        {
            try
            {
                param.Clear();
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "getconnectioncount", param);
                return (int)response.jobjectResponse["Result"];
            }
            catch { throw; }
        }

        public string getRawTransactionHex(string txHash)
        {
            try
            {
                param.Clear();
                param.Add(new KeyValuePair<string, object>("Raw", 1));
                param.Add(new KeyValuePair<string, object>("Hash", txHash));
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "gettransaction", param);
                return response.jobjectResponse["Result"].ToString();
            }
            catch { throw; }
        }

        public string getRawTransactionJson(string txHash)
        {
            try
            {
                param.Clear();
                param.Add(new KeyValuePair<string, object>("Raw", 0));
                param.Add(new KeyValuePair<string, object>("Hash", txHash));
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "gettransaction", param);
                return response.jobjectResponse["Result"].ToString();
            }
            catch { throw; }
        }

        public string getSmartCodeEvent(int blockHeight)
        {
            try
            {
                param.Clear();
                param.Add(new KeyValuePair<string, object>("Height", blockHeight));
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "getsmartcodeeventbyheight", param);
                return response.jobjectResponse["Result"].ToString();
            }
            catch { throw; }
        }

        public string getSmartCodeEvent(string txHash)
        {
            try
            {
                param.Clear();
                param.Add(new KeyValuePair<string, object>("Height", txHash));
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "getsmartcodeeventbyhash", param);
                return response.jobjectResponse["Result"].ToString();
            }
            catch { throw; }
        }

        public string getStorage(string contractHash, string key)
        {
            try
            {
                param.Clear();
                param.Add(new KeyValuePair<string, object>("Hash", contractHash));
                param.Add(new KeyValuePair<string, object>("Key", key));
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "getstorage", param);
                return response.jobjectResponse["Result"].ToString();
            }
            catch { throw; }
        }

        public string getUnclaimedONG(string address)
        {
            try
            {
                param.Clear();
                param.Add(new KeyValuePair<string, object>("Addr", address));
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "getunclaimong", param);
                return response.jobjectResponse["Result"].ToString();
            }
            catch { throw; }
        }

        public int getVersion()
        {
            throw new NotImplementedException();
        }

        public string setSendRawTransaction(string tx, bool preExec)
        {
            try
            {
                param.Clear();
                param.Add(new KeyValuePair<string, object>("PreExec", Convert.ToInt32(preExec)));
                param.Add(new KeyValuePair<string, object>("Data", tx));
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.Websocket, "", "sendrawtransaction", param);
                return response.jobjectResponse["Result"].ToString();
            }
            catch { throw; }
        }
    }
}


