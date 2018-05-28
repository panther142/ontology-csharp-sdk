using OntologyCSharpSDK.Common;
using OntologyCSharpSDK.Interface;
using OntologyCSharpSDK.Network;
using System;
using System.Collections.Generic;
using OntologyCSharpSDK.ExceptionHandling;

namespace OntologyCSharpSDK.ConnectionMethods
{

    public class RPC : IConnectionMethod
    {
        IList<object> param = new List<object>();

        public int getBlockGenerationTime()
        {
            try
            {
                param.Clear();
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getgenerateblocktime", param);
                return (int)response.jobjectResponse["result"];
            }
            catch { throw; }
        }

        public int getBlockHeight()
        {
            try
            {
                param.Clear();
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getblockcount", param);
                return (int)response.jobjectResponse["result"];
            }
            catch { throw; }
        }

        public int getBlockHeightByTxHash(string txHash)
        {
            try
            {
                param.Clear();
                param.Add(txHash);
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getblockheightbytxhash", param);
                return (int)response.jobjectResponse["result"];
            }
            catch { throw; }
        }

        public string getBlockHex(int blockHeight)
        {
            try
            {
                param.Clear();
                param.Add(blockHeight);
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getblock", param);
                return response.jobjectResponse["result"].ToString();
            }
            catch { throw; }
        }

        public string getBlockHex(string blockHash)
        {
            try
            {
                param.Clear();
                param.Add(blockHash);
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getblock", param);
                return response.jobjectResponse["result"].ToString();
            }
            catch { throw; }
        }

        public string getBlockJson(int blockHeight)
        {
            try
            {
                param.Clear();
                param.Add(blockHeight);
                param.Add(1);
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getblock", param);
                return response.jobjectResponse["result"].ToString();
            }
            catch { throw; }
        }

        public string getBlockJson(string blockHash)
        {
            try
            {
                param.Clear();
                param.Add(blockHash);
                param.Add(1);
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getblock", param);
                return response.jobjectResponse["result"].ToString();
            }
            catch { throw; }
        }

        public int getNodeCount()
        {
            try
            {
                param.Clear();
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getconnectioncount", param);
                return (int)response.jobjectResponse["result"];
            }
            catch { throw; }
        }

        public string getAddressBalance(string address)
        {
            try
            {
                param.Clear();
                param.Add(address);
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getbalance", param);
                return response.jobjectResponse["result"].ToString();
            }
            catch { throw; }
        }

        public string getRawTransactionHex(string TxHash)
        {
            try
            {
                param.Clear();
                param.Add(TxHash);
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getrawtransaction", param);
                return response.jobjectResponse["result"].ToString();
            }
            catch { throw; }
        }

        public string getRawTransactionJson(string TxHash)
        {
            try
            {
                param.Clear();
                param.Add(TxHash);
                param.Add(1);
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getrawtransaction", param);
                return response.jobjectResponse["result"].ToString();
            }
            catch { throw; }
        }


        public string getSmartCodeEvent(int blockHeight)
        {
            try
            {
                param.Clear();
                param.Add(blockHeight);
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getsmartcodeevent", param);
                return response.jobjectResponse["result"].ToString();
            }
            catch { throw; }
        }

        public string getSmartCodeEvent(string txHash)
        {
            try
            {
                param.Clear();
                param.Add(txHash);
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getsmartcodeevent", param);
                return response.jobjectResponse["result"].ToString();
            }
            catch { throw; }
        }

        public string getBestBlockHash()
        {
            try
            {
                param.Clear();
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getbestblockhash", param);
                return response.jobjectResponse["result"].ToString();
            }
            catch { throw; }
        }

        public string getBlockHashByHeight(int blockHeight)
        {
            try
            {
                param.Clear();
                param.Add(blockHeight);
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getblockhash", param);
                return response.jobjectResponse["result"].ToString();
            }
            catch { throw; }
        }

        public string getStorage(string contractHash, string key)
        {
            try
            {
                key = Crypto.StringToHexString(key).ToString();

                param.Clear();
                param.Add(contractHash);
                param.Add(key);
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getstorage", param);
                return response.jobjectResponse["result"].ToString();
            }
            catch { throw; }
        }

        public int getVersion()
        {
            try
            {
                param.Clear();
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getversion", param);
                return (int)response.jobjectResponse["result"];
            }
            catch { throw; }
        }

        public int getBlockSysFee(int blockHeight)
        {
            try
            {
                param.Clear();
                param.Add(blockHeight);
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getblocksysfee", param);
                return (int)response.jobjectResponse["result"];
            }
            catch { throw; }
        }

        public string getContractJson(string contractHash)
        {
            try
            {
                param.Clear();
                param.Add(contractHash);
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getcontractstate", param);
                return response.jobjectResponse["result"].ToString();
            }
            catch { throw; }
        }

        public string getMempoolTxState(string txHash)
        {
            try
            {
                param.Clear();
                param.Add(txHash);
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getmempooltxstate", param);
                return response.jobjectResponse["result"].ToString();
            }
            catch { throw; }
        }

        public string setSendRawTransaction(string tx, bool preExec)
        {
            try
            {
                param.Clear();
                param.Add(tx);
                param.Add(Convert.ToInt32(preExec));
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "sendrawtransaction", param);
                return response.jobjectResponse["result"].ToString();
            }
            catch { throw; }
        }

        public string getMerkleProof(string hash)
        {
            try
            {
                param.Clear();
                param.Add(hash);
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getmerkleproof", param);
                return response.jobjectResponse["result"].ToString();
            }
            catch { throw; }
        }

        public string getGasPrice()
        {
            try
            {
                param.Clear();
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getgasprice", param);
                return response.jobjectResponse["result"].ToString();
            }
            catch { throw; }
        }

        public string getAllowance(string asset, string fromAddress, string toAddress)
        {
            try
            {
                param.Clear();
                param.Add(asset);
                param.Add(fromAddress);
                param.Add(toAddress);
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getallowance", param);
                return response.jobjectResponse["result"].ToString();
            }
            catch { throw; }
        }
    }
}

