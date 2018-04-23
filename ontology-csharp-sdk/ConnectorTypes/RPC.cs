using Newtonsoft.Json.Linq;
using Interface;
using Network;
using System.Collections.Generic;
using Common;
using System;
using Common.Net;

namespace ConnectorTypes
{

    public class RPC : IConnector
    {

        JObject result;
        IList<object> param = new List<object>();

        private string net;

        public RPC(string network = "test")
        {
            net = network;
        }

        public int getBlockGenerationTime()
        {

            var url = NetworkBuilder.getRPCURL(net);
            result = RPCrequests.sendRPCrequest(url,"getgenerateblocktime", null);
            return (int)result["result"];
        }

        public int getBlockHeight()
        {
            var url = NetworkBuilder.getRPCURL(net);
            result = RPCrequests.sendRPCrequest(url,"getblockcount", null);
            return (int)result["result"];
        }

        public int getBlockHeightByTxHash(string txHash)
        {
            param.Clear();
            param.Add(txHash);
            var url = NetworkBuilder.getRPCURL(net);
            result = RPCrequests.sendRPCrequest(url,"getblockheightbytxhash", param);
            return (int)result["result"];
            
        }

        public string getBlockHex(int blockHeight)
        {
            param.Clear();
            param.Add(blockHeight);
            var url = NetworkBuilder.getRPCURL(net);
            result = RPCrequests.sendRPCrequest(url,"getblock", param);
            return result["result"].ToString();
        }

        public string getBlockHex(string blockHash)
        {
            param.Clear();
            param.Add(blockHash);
            var url = NetworkBuilder.getRPCURL(net);
            result = RPCrequests.sendRPCrequest(url,"getblock", param);
            return result["result"].ToString();
        }

        public string getBlockJson(int blockHeight)
        {
            param.Clear();
            param.Add(blockHeight);
            param.Add(1);
            var url = NetworkBuilder.getRPCURL(net);
            result = RPCrequests.sendRPCrequest(url,"getblock", param);
            return result["result"].ToString();
        }

        public string getBlockJson(string blockHash)
        {
            param.Clear();
            param.Add(blockHash);
            param.Add(1);
            var url = NetworkBuilder.getRPCURL(net);
            result = RPCrequests.sendRPCrequest(url,"getblock", param);
            return result["result"].ToString();
        }

        public int getNodeCount()
        {
            var url = NetworkBuilder.getRPCURL(net);
            result = RPCrequests.sendRPCrequest(url,"getconnectioncount", null);
            return (int)result["result"];
        }

        public string getAddressBalance(string address)
        {
            param.Clear();
            param.Add(address);

            var url = NetworkBuilder.getRPCURL(net);
            result = RPCrequests.sendRPCrequest(url,"getbalance", param);

            return result["result"].ToString();
        }


        public string getRawTransactionHex(string TxHash)
        {
            param.Clear();
            param.Add(TxHash);

            var url = NetworkBuilder.getRPCURL(net);
            result = RPCrequests.sendRPCrequest(url,"getrawtransaction", param);
            return result["result"].ToString();
        }

        public string getRawTransactionJson(string TxHash)
        {
            param.Clear();
            param.Add(TxHash);
            var url = NetworkBuilder.getRPCURL(net);
            result = RPCrequests.sendRPCrequest(url, "getcontractstate", param);
            return result["result"].ToString();
        }


        public string getSmartCodeEvent(int blockHeight)
        {
            param.Clear();
            param.Add(blockHeight);
            var url = NetworkBuilder.getRPCURL(net);
            result = RPCrequests.sendRPCrequest(url, "getsmartcodeevent", param);
            return result["result"].ToString();
        }

        public string getSmartCodeEvent(string txHash)
        {
            param.Clear();
            param.Add(txHash);
            var url = NetworkBuilder.getRPCURL(net);
            result = RPCrequests.sendRPCrequest(url, "getsmartcodeevent", param);
            return result["result"].ToString();
        }

    }

}

