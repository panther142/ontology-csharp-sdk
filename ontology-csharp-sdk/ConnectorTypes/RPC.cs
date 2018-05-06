using Interface;
using System.Collections.Generic;
using Network.NetworkHelper;
using Common.Enums;
using Common.Cryptology;

namespace ConnectorTypes
{

    public class RPC : IConnector
    {

        IList<object> param = new List<object>();

        public int getBlockGenerationTime()
        {
            param.Clear();
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getgenerateblocktime", param);
            return (int)response.jobjectResponse["result"];
        }

        public int getBlockHeight()
        {
            param.Clear();
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getblockcount", param);
            return (int)response.jobjectResponse["result"];
        }

        public int getBlockHeightByTxHash(string txHash)
        {
            param.Clear();
            param.Add(txHash);
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getblockheightbytxhash", param);
            return (int)response.jobjectResponse["result"];
        }

        public string getBlockHex(int blockHeight)
        {
            param.Clear();
            param.Add(blockHeight);
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getblock", param);
            return response.jobjectResponse["result"].ToString();
        }

        public string getBlockHex(string blockHash)
        {
            param.Clear();
            param.Add(blockHash);
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getblock", param);
            return response.jobjectResponse["result"].ToString();
        }

        public string getBlockJson(int blockHeight)
        {
            param.Clear();
            param.Add(blockHeight);
            param.Add(1);
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getblock", param);
            return response.jobjectResponse["result"].ToString();
        }

        public string getBlockJson(string blockHash)
        {
            param.Clear();
            param.Add(blockHash);
            param.Add(1);
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getblock", param);
            return response.jobjectResponse["result"].ToString();
        }

        public int getNodeCount()
        {
            param.Clear();
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getconnectioncount", param);
            return (int)response.jobjectResponse["result"];
        }

        public string getAddressBalance(string address)
        {
            param.Clear();
            param.Add(address);
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getbalance", param);
            return response.jobjectResponse["result"].ToString();
        }


        public string getRawTransactionHex(string TxHash)
        {
            param.Clear();
            param.Add(TxHash);
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getrawtransaction", param);
            return response.jobjectResponse["result"].ToString();
        }

        public string getRawTransactionJson(string TxHash)
        {
            param.Clear();
            param.Add(TxHash);
            param.Add(1);
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getrawtransaction", param);
            return response.jobjectResponse["result"].ToString();
        }


        public string getSmartCodeEvent(int blockHeight)
        {
            param.Clear();
            param.Add(blockHeight);
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getsmartcodeevent", param);
            return response.jobjectResponse["result"].ToString();
        }

        public string getSmartCodeEvent(string txHash)
        {
            param.Clear();
            param.Add(txHash);
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getsmartcodeevent", param);
            return response.jobjectResponse["result"].ToString();
        }

        public string getBestBlockHash()
        {
            param.Clear();
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getbestblockhash", param);
            return response.jobjectResponse["result"].ToString();
        }

        public string getBlockHashByHeight(int blockHeight)
        {
            param.Clear();
            param.Add(blockHeight);
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getblockhash", param);
            return response.jobjectResponse["result"].ToString();
        }

        public string getStorage(string contractHash, string key)
        {
            key = Crypto.StringToHexString(key).ToString();

            param.Clear();
            param.Add(contractHash);
            param.Add(key);
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getstorage", param);
            return response.jobjectResponse["result"].ToString();
        }

        public int getVersion()
        {
            param.Clear();
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getversion", param);
            return (int)response.jobjectResponse["result"];
        }

        public int getBlockSysFee(int index)
        {
            param.Clear();
            param.Add(index);
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getblocksysfee", param);
            return (int)response.jobjectResponse["result"];
        }

        public string getContractState(string scriptHash)
        {
            param.Clear();
            param.Add(scriptHash);
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getcontractstate", param);
            return response.jobjectResponse["result"].ToString();
        }

        public string getMempoolTxState(string txHash)
        {
            param.Clear();
            param.Add(txHash);
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "getmempooltxstate", param);
            return response.jobjectResponse["result"].ToString();
        }

        public string setSendRawTransaction(string tx)
        {
            param.Clear();
            param.Add(tx);
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.RPC, "POST", "sendrawtransaction", param);
            return response.jobjectResponse["result"].ToString();
        }
    }
}

