using ConnectorTypes;
using Network;

namespace OntologyCSharpSDK
{
    public class OntologySDK : RPC
    {
        private Basic.Account account = null;

        public OntologySDK(string network = "test")
        {
            account = new Basic.Account(network);
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
        
        public APIResult registerONTID(string ontid, string privatekey)
        {
            return account.registerONTID(ontid, privatekey);
        }

        public string createAddressFromPublickKey(string publickey)
        {
            return account.createAddressFromPublickKey(publickey);
        }

        public APIResult transferFund(string name, string fromaddress, string toaddress, decimal value, string privatekey)
        {
            return account.transferFund(name, fromaddress, toaddress, value, privatekey);
        }

}
}

