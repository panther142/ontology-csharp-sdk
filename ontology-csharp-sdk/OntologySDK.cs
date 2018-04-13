using ConnectorTypes;

namespace OntologyCSharpSDK
{
    public class OntologySDK : RPC
    {
        private Basic.Account account = null;

        public OntologySDK()
        {
            account = new Basic.Account();
        }

        public string createPrivateKey()
        {
            return account.createPrivateKey();
        }

        public string getPublicKey(string privatekey)
        {
            return account.getPublicKey(privatekey);
        }

    }
}

