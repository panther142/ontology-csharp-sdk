using System.Collections;

namespace OntologyCSharpSDK.Wallet
{
    public class Account
    {
        public string label = "";
        public string address = "";
        public bool isDefault = false;
        public bool @lock = false;
        public string algorithm = "";
        public IDictionary parameters = new Hashtable();
        public string key = "";
        public object extra = null;

        public Account()
        {
            this.algorithm = "ECDSA";
            this.parameters["curve"] = "secp256r1";
            this.extra = null;
        }

    }
}

