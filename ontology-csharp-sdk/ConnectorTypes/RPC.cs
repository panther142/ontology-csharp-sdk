using Newtonsoft.Json.Linq;
using Interface;
using Network;
using System.Collections.Generic;
using Common;
using System;

namespace ConnectorTypes
{

    public class RPC : IConnector
    {

        JObject result;
        IList<object> param = new List<object>();


        public int getBlockGenerationTime()
        {
            result = RPCrequests.sendRPCrequest("getgenerateblocktime", null);
            return (int)result["result"];
        }

        public int getBlockHeight()
        {
            result = RPCrequests.sendRPCrequest("getblockcount", null);
            return (int)result["result"];
        }

        public int getBlockHeightByTxHash(string txHash)
        {
            param.Clear();
            param.Add(txHash);
            result = RPCrequests.sendRPCrequest(url, "getblockheightbytxhash", param);
            return (int)result["result"];
            
        }

        public string getBlockHex(int blockHeight)
        {
            param.Clear();
            param.Add(blockHeight);
            result = RPCrequests.sendRPCrequest("getblock", param);
            return result["result"].ToString();
        }

        public string getBlockHex(string blockHash)
        {
            param.Clear();
            param.Add(blockHash);
            result = RPCrequests.sendRPCrequest("getblock", param);
            return result["result"].ToString();
        }

        public string getBlockJson(int blockHeight)
        {
            param.Clear();
            param.Add(blockHeight);
            param.Add(1);
            result = RPCrequests.sendRPCrequest("getblock", param);
            return result["result"].ToString();
        }

        public string getBlockJson(string blockHash)
        {
            param.Clear();
            param.Add(blockHash);
            param.Add(1);
            result = RPCrequests.sendRPCrequest("getblock", param);
            return result["result"].ToString();
        }

        public int getNodeCount()
        {
            result = RPCrequests.sendRPCrequest("getconnectioncount", null);
            return (int)result["result"];
        }

        public string getAddressBalance(string address)
        {
            param.Clear();
            param.Add(address);

            result = RPCrequests.sendRPCrequest("getbalance", param);

            return result["result"].ToString();
        }


        public string getRawTransactionHex(string TxHash)
        {
            param.Clear();
            param.Add(txHash);
            result = RPCrequests.sendRPCrequest(url, "getrawtransaction", param);


            result = RPCrequests.sendRPCrequest("getrawtransaction", param);
            return result["result"].ToString();
        }

        public string getRawTransactionJson(string TxHash)
        {
            param.Clear();
            param.Add(contractHash);
            result = RPCrequests.sendRPCrequest(url, "getcontractstate", param);
            return result["result"].ToString();
        }


        public string getSmartCodeEvent(int blockHeight)
        {
            param.Clear();
            param.Add(blockHeight);
            result = RPCrequests.sendRPCrequest(url, "getsmartcodeevent", param);
            return result["result"].ToString();
        }

        public string getSmartCodeEvent(string txHash)
        {
            param.Clear();
            param.Add(txHash);
            result = RPCrequests.sendRPCrequest(url, "getsmartcodeevent", param);
            return result["result"].ToString();
        }

    }

}

