using ConnectorTypes;
using Network;
using Network.NetworkHelper;

namespace OntologyCSharpSDK
{
    public class OntologySDK : REST
    {
        private Basic.Account account = null;

        public OntologySDK(string node)
        {
            account = new Basic.Account(node);
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

    }

}

