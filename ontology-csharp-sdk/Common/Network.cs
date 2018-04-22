namespace Common.Net
{
    public static class NetworkBuilder
    {
        private static string MAIN_NODE = "54.222.182.88";
        private static string TEST_NODE = "139.219.111.50";
        private static string HTTP_REST_PORT = "20334";
        private static string HTTP_WS_PORT = "20335";
        private static string HTTP_JSON_PORT = "20336";

        private static string getBalance = "/api/v1/balance";
        private static string sendRawTx = "/api/v1/transaction";

        public static string getSendRawTxURL(string net = "test")
        {
            string url = "";
            string node = "";

            switch (net)
            {
                case "main":
                    node = MAIN_NODE;
                    break;
                case "test":
                    node = TEST_NODE;
                    break;
                default:
                    node = net;
                    break;
            }
            
            url = "http://" + node + ":" + HTTP_REST_PORT + sendRawTx;
            return url;
        }


    }
}
