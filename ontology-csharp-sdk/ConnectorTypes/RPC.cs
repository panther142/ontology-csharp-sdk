using Newtonsoft.Json.Linq;
using Interface;
using Network;
using Common.Net;
using System.Collections.Generic;

namespace ConnectorTypes
{

    public class RPC : IConnector
    {

        JObject result;
        IList<object> param = new List<object>();

        private string net;
        private string url;

        public RPC(string network = "test")
        {
            net = network;
            url = NetworkBuilder.getRPCURL(net);
        }

        public int getBlockGenerationTime()
        {
            result = RPCrequests.sendRPCrequest(url,"getgenerateblocktime", null);
            return (int)result["result"];
        }

        public int getBlockHeight()
        {
            result = RPCrequests.sendRPCrequest(url, "getblockcount", null);
            return (int)result["result"];
        }

        public int getBlockHeightByTxHash(string TxHash)
        {
            param.Clear();
            param.Add(TxHash);
            result = RPCrequests.sendRPCrequest(url, "getblockheightbytxhash", param);
            return (int)result["result"];
        }

        public string getBlockHex(int blockHeight)
        {
            param.Clear();
            param.Add(blockHeight);
            result = RPCrequests.sendRPCrequest(url, "getblock", param);
            return result["result"].ToString();
        }

        public string getBlockHex(string blockHash)
        {
            param.Clear();
            param.Add(blockHash);
            result = RPCrequests.sendRPCrequest(url, "getblock", param);
            return result["result"].ToString();
        }

        public string getBlockJson(int blockHeight)
        {
            param.Clear();
            param.Add(blockHeight);
            param.Add(1);
            result = RPCrequests.sendRPCrequest(url, "getblock", param);
            return result["result"].ToString();
        }

        public string getBlockJson(string blockHash)
        {
            param.Clear();
            param.Add(blockHash);
            param.Add(1);
            result = RPCrequests.sendRPCrequest(url, "getblock", param);
            return result["result"].ToString();
        }

        public int getNodeCount()
        {
            result = RPCrequests.sendRPCrequest(url, "getconnectioncount", null);
            return (int)result["result"];
        }

        public string getAddressBalance(string ONTAddress)
        {
            param.Clear();
            param.Add(ONTAddress);

            result = RPCrequests.sendRPCrequest(url, "getbalance", param);

            return result["result"].ToString();
        }

        public string getRawTransactionHex(string TxHash)
        {
            param.Clear();
            param.Add(TxHash);
            result = RPCrequests.sendRPCrequest(url, "getrawtransaction", param);
            return result["result"].ToString();
        }

        public string getRawTransactionJson(string TxHash)
        {
            param.Clear();
            param.Add(TxHash);
            param.Add(1);
            result = RPCrequests.sendRPCrequest(url, "getrawtransaction", param);
            return result["result"].ToString();
        }
    }

}

