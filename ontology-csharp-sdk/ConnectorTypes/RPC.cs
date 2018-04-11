using Newtonsoft.Json.Linq;
using Interface;
using Network;
using System.Collections.Generic;

namespace ConnectorTypes
{

    public class RPC : IConnector
    {

        JObject result;
        IList<object> param = new List<object>();

        public int getBlockGenerationTime()
        {
            result = RPCrequests.sendRPCrequest("getgenerateblocktime", null, 0);
            return (int)result["result"];
        }

        public int getBlockHeight()
        {
            result = RPCrequests.sendRPCrequest("getblockcount", null, 0);
            return (int)result["result"];
        }

        public int getBlockHeightByTxHash(string TxHash)
        {
            param.Clear();
            param.Add(TxHash);
            result = RPCrequests.sendRPCrequest("getblockheightbytxhash", param, 0);
            return (int)result["result"];
        }

        public int getNodeCount()
        {
            result = RPCrequests.sendRPCrequest("getconnectioncount", null, 0);
            return (int)result["result"];
        }

        public string getONTBalance(string ONTAddress)
        {
            param.Clear();
            param.Add(ONTAddress);

            result = RPCrequests.sendRPCrequest("getbalance", param, 1);

            return (string)result["result"].ToString();
        }

    }

}

