using Newtonsoft.Json;
using OntologyCSharpSDK.Interface;
using OntologyCSharpSDK.Network;

namespace OntologyCSharpSDK
{
    public class OntologySdk
    {
        private readonly Basic.Account _account = null;
        public IConnectionMethod ConnectionManager { get; }
        public WebsocketSubscribe WebsocketSubscribe = new WebsocketSubscribe();


        public OntologySdk(string node, ConnectionMethodFactory.ConnectionMethod connectionMethod)
        {
            var factory = new ConnectionMethodFactory();
            ConnectionManager = factory.SetConnectionMethod(connectionMethod);
            _account = new Basic.Account(node);
        }

        public string CreateWallet()
        {
            var wallet = new Wallet.Wallet();
            var json = JsonConvert.SerializeObject(wallet, Formatting.Indented).Replace("enc_alg", "enc-alg");
            return json;
        }
        public string CreatePrivateKey()
        {
            return _account.createPrivateKey();
        }

        public string GetPublicKey(string privatekey)
        {
            return _account.getPublicKey(privatekey);
        }

        public string CreateOntid(string privatekey)
        {
            return _account.createONTID(privatekey);
        }


        public NetworkResponse RegisterOntid(string ontid, string privatekey)
        {
            return _account.registerONTID(ontid, privatekey);
        }


        public string CreateAddressFromPublickKey(string publickey)
        {
            return _account.createAddressFromPublickKey(publickey);
        }


        public NetworkResponse TransferFund(string name, string fromaddress, string toaddress, decimal value, string privatekey)
        {
            return _account.transferFund(name, fromaddress, toaddress, value, privatekey);
        }


        public NetworkResponse RegisterClaim(string context, string metadata,
           string content, string type, string issuer, string privatekey)
        {
            return _account.registerClaim(context, metadata, content, type, issuer, privatekey);
        }

        public NetworkResponse AddPublicKey(string ontid, string newPublickey, string publickey, string privatekey)
        {
            return _account.addPublicKey(ontid, newPublickey, publickey, privatekey);
        }

    }
}

