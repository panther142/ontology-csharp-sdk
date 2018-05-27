using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebSocketSharp;

namespace OntologyCSharpSDK.Network
{
    public class WebsocketSubscribe
    {
        public async Task SubscribeAsync(string contractFilter, bool subscribeEvents, bool subscribeJsonBlock, bool subscribeHexBlock, bool subscribeBlockTxHashes, string node, IProgress<string> progress)
        {
            JObject jsonObject = new JObject();

            jsonObject["Action"] = "subscribe";
            jsonObject["Version"] = "1.0.0";
            jsonObject["contractAddress"] = contractFilter;
            jsonObject["SubscribeEvent"] = subscribeEvents;
            jsonObject["SubscribeJsonBlock"] = subscribeJsonBlock;
            jsonObject["SubscribeRawBlock"] = subscribeHexBlock;
            jsonObject["SubscribeBlockTxHashs"] = subscribeBlockTxHashes;

            string jsonSubscribe = JsonConvert.SerializeObject(jsonObject);

            var ws = new WebSocket(node);

            ws.OnMessage += (sender, e) =>
            {
                progress.Report(e.Data);
            };

            ws.OnError += (sender, e) =>
            {
                ws.Close();
            };

            Action<bool> completed = null;

            ws.Connect();
            ws.SendAsync(jsonSubscribe, completed);

            // While connection is still alive, send heartbeat every 4 minutes (required by websocket server else session expires)
            while (ws.IsAlive)
            {
                JObject heartbeat = new JObject();

                heartbeat["Action"] = "heartbeat";
                heartbeat["Version"] = "1.0.0";

                string jsonHeartbeat = JsonConvert.SerializeObject(heartbeat);
                ws.SendAsync(jsonHeartbeat, completed);

                await Task.Delay(200000);
            }

        }
    }
}
