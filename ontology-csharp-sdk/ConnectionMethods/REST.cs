using OntologyCSharpSDK.Common;
using OntologyCSharpSDK.Interface;
using OntologyCSharpSDK.Network;
using System;
using System.Collections.Generic;

namespace OntologyCSharpSDK.ConnectionMethods
{

    public class REST : IConnectionMethod
    {
        IList<object> param = new List<object>();

        public string getAddressBalance(string address)
        {
            try
            {
                param.Clear();
                param.Add(address);
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.REST, "GET", Constants.REST_getAddressBalance, param);
                return response.jobjectResponse["Result"].ToString();
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
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.REST, "GET", Constants.REST_getBlockHeightByTxHash, param);
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
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.REST, "GET", Constants.REST_getBlockGenerationTime, param);
                return (int)response.jobjectResponse["Result"];
            }
            catch { throw; }
        }

        public string getBlockHashByHeight(int blockHeight)
        {
            throw new NotImplementedException();
        }

        public int getBlockHeight()
        {
            try
            {
                param.Clear();
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.REST, "GET", Constants.REST_getBlockHeight, param);
                return (int)response.jobjectResponse["Result"];
            }
            catch { throw; }
        }

        public int getBlockHeightByTxHash(string txHash)
        {
            try
            {
                param.Clear();
                param.Add(txHash);
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.REST, "GET", Constants.REST_getBlockHeightByTxHash, param);
                return (int)response.jobjectResponse["Result"];
            }
            catch { throw; }
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
            try
            {
                param.Clear();
                param.Add(blockHeight);
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.REST, "GET", Constants.REST_getBlockByHeight, param);
                return response.jobjectResponse["Result"].ToString();
            }
            catch { throw; }
        }

        public string getBlockJson(string blockHash)
        {
            try
            {
                param.Clear();
                param.Add(blockHash);
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.REST, "GET", Constants.REST_getBlockByHash, param);
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
                param.Add(blockHeight);
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.REST, "GET", Constants.REST_getBlockTxsByHeight, param);
                return response.jobjectResponse["Result"].ToString();
            }
            catch { throw; }
        }

        public string getContractJson(string contractHash)
        {
            try
            {
                param.Clear();
                param.Add(contractHash);
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.REST, "GET", Constants.REST_getContract, param);
                return response.jobjectResponse["Result"].ToString();
            }
            catch { throw; }
        }

        public string getGasPrice()
        {
            try
            {
                param.Clear();
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.REST, "GET", Constants.REST_getGasPrice, param);
                return response.jobjectResponse["Result"].ToString();
            }
            catch { throw; }
        }

        public string getMempoolTxState(string txHash)
        {
            throw new NotImplementedException();
        }

        public string getMerkleProof(string hash)
        {
            try
            {
                param.Clear();
                param.Add(hash);
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.REST, "GET", Constants.REST_getMerkleProof, param);
                return response.jobjectResponse["Result"].ToString();
            }
            catch { throw; }
        }

        public int getNodeCount()
        {
            try
            {
                param.Clear();
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.REST, "GET", Constants.REST_getNodeCount, param);
                return (int)response.jobjectResponse["Result"];
            }
            catch { throw; }
        }

        public string getRawTransactionHex(string txHash)
        {
            throw new NotImplementedException();
        }

        public string getRawTransactionJson(string txHash)
        {
            try
            {
                param.Clear();
                param.Add(txHash);
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.REST, "GET", Constants.REST_getTransactionByHash, param);
                return response.jobjectResponse["Result"].ToString();
            }
            catch { throw; }
        }

        public string getSmartCodeEvent(int blockHeight)
        {
            try
            {
                param.Clear();
                param.Add(blockHeight);
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.REST, "GET", Constants.REST_getSmartCodeEventByHeight, param);
                return response.jobjectResponse["Result"].ToString();
            }
            catch { throw; }
        }

        public string getSmartCodeEvent(string txHash)
        {
            try
            {
                param.Clear();
                param.Add(txHash);
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.REST, "GET", Constants.REST_getSmartCodeEventByTxHash, param);
                return response.jobjectResponse["Result"].ToString();
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
                NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.REST, "GET", Constants.REST_getStorage, param);
                return response.jobjectResponse["result"].ToString();
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
                param.Add(tx);
                if (preExec)
                {
                    NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.REST, "POST", Constants.REST_sendRawTransactionPreExec, param);
                    return response.jobjectResponse["result"].ToString();
                }
                else
                {
                    NetworkResponse response = NetworkHelper.sendNetworkRequest(Protocol.REST, "POST", Constants.REST_sendRawTransaction, param);
                    return response.jobjectResponse["result"].ToString();
                }
            }
            catch { throw; }
        }
    }
}

