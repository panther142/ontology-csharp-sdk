using Interface;
using System.Collections.Generic;
using Common.Enums;
using System;
using Network.NetworkHelper;
using Common.Constants;
using Common.Cryptology;

namespace ConnectorTypes
{

    public class REST : IConnector
    {
        IList<object> param = new List<object>();

        public string getAddressBalance(string address)
        {
            param.Clear();
            param.Add(address);
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.REST, "GET", Constants.REST_getAddressBalance, param);
            return response.jobjectResponse["Result"].ToString();
        }

        public string getBestBlockHash()
        {
            throw new NotImplementedException();
        }

        public int getBlockGenerationTime()
        {
            param.Clear();
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.REST, "GET", Constants.REST_getBlockGenerationTime, param);
            return (int)response.jobjectResponse["Result"];
        }

        public string getBlockHashByHeight(int blockHeight)
        {
            throw new NotImplementedException();
        }

        public int getBlockHeight()
        {
            param.Clear();
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.REST, "GET", Constants.REST_getBlockHeight, param);
            return (int)response.jobjectResponse["Result"];
        }

        public int getBlockHeightByTxHash(string txHash)
        {
            param.Clear();
            param.Add(txHash);
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.REST, "GET", Constants.REST_getBlockHeightByTxHash, param);
            return (int)response.jobjectResponse["Result"];

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
            param.Add(blockHeight);
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.REST, "GET", Constants.REST_getBlockByHeight, param);
            return response.jobjectResponse["Result"].ToString();
        }

        public string getBlockJson(string blockHash)
        {
            param.Clear();
            param.Add(blockHash);
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.REST, "GET", Constants.REST_getBlockByHash, param);
            return response.jobjectResponse["Result"].ToString();
        }

        public int getBlockSysFee(int index)
        {
            throw new NotImplementedException();
        }

        public string getContractJson(string contractHash)
        {
            param.Clear();
            param.Add(contractHash);
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.REST, "GET", Constants.REST_getContract, param);
            return response.jobjectResponse["Result"].ToString();
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
            param.Clear();
            param.Add(hash);
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.REST, "GET", Constants.REST_getMerkleProof, param);
            return response.jobjectResponse["Result"].ToString();
        }

        public int getNodeCount()
        {
            param.Clear();
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.REST, "GET", Constants.REST_getNodeCount, param);
            return (int)response.jobjectResponse["Result"];
        }

        public string getRawTransactionHex(string txHash)
        {
            throw new NotImplementedException();
        }

        public string getRawTransactionJson(string txHash)
        {
            param.Clear();
            param.Add(txHash);
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.REST, "GET", Constants.REST_getTransactionByHash, param);
            return response.jobjectResponse["Result"].ToString();
        }

        public string getSmartCodeEvent(int blockHeight)
        {
            param.Clear();
            param.Add(blockHeight);
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.REST, "GET", Constants.REST_getSmartCodeEventByHeight, param);
            return response.jobjectResponse["Result"].ToString();
        }

        public string getSmartCodeEvent(string txHash)
        {
            param.Clear();
            param.Add(txHash);
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.REST, "GET", Constants.RESTful_getSmartCodeEventByTxHash, param);
            return response.jobjectResponse["Result"].ToString();
        }

        public string getStorage(string contractHash, string key)
        {
            key = Crypto.StringToHexString(key).ToString();

            param.Clear();
            param.Add(contractHash);
            param.Add(key);
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.REST, "GET", Constants.REST_getStorage, param);
            return response.jobjectResponse["result"].ToString();
        }

        public int getVersion()
        {
            throw new NotImplementedException();
        }

        public string setSendRawTransaction(string tx)
        {
            param.Clear();
            param.Add(tx);
            NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.REST, "POST", Constants.REST_sendRawTransaction, param);
            return response.jobjectResponse["result"].ToString();
        }
    }
}

