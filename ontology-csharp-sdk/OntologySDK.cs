using Newtonsoft.Json;
using OntologyCSharpSDK.Interface;
using OntologyCSharpSDK.Network;

namespace OntologyCSharpSDK
{
    public class OntologySDK
    {
        private Basic.Account account = null;
        public IConnectionMethod connectionManager = null;
        public WebsocketSubscribe websocketSubscribe = new WebsocketSubscribe();


        public OntologySDK(string node, ConnectionMethodFactory.ConnectionMethod connectionMethod)
        {
            ConnectionMethodFactory factory = new ConnectionMethodFactory();
            connectionManager = factory.setConnectionMethod(connectionMethod);

            account = new Basic.Account(node);
        }

        public string createWallet()
        {
            Wallet.Wallet wallet = new Wallet.Wallet();
            string json = JsonConvert.SerializeObject(wallet, Formatting.Indented).Replace("enc_alg", "enc-alg");
            return json;
        }
        public string createPrivateKey()
        {
            return account.createPrivateKey();
        }

        public string getPublicKey(string privatekey)
        {
            return account.getPublicKey(privatekey);
        }

        public string createONTID(string privatekey)
        {
            return account.createONTID(privatekey);
        }


        public NetworkResponse registerONTID(string ontid, string privatekey)
        {
            return account.registerONTID(ontid, privatekey);
        }


        public string createAddressFromPublickKey(string publickey)
        {
            return account.createAddressFromPublickKey(publickey);
        }


        public NetworkResponse transferFund(string name, string fromaddress, string toaddress, decimal value, string privatekey)
        {
            return account.transferFund(name, fromaddress, toaddress, value, privatekey);
        }


        public NetworkResponse registerClaim(string context, string metadata,
           string content, string type, string issuer, string privatekey)
        {
            return account.registerClaim(context, metadata, content, type, issuer, privatekey);
        }

        public NetworkResponse addPublicKey(string ontid, string new_publickey, string publickey, string privatekey)
        {
            return account.addPublicKey(ontid, new_publickey, publickey, privatekey);
        }

    }
}

