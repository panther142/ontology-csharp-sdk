using System.Collections;

namespace OntologyCSharpSDK.Wallet
{
    public class Control
    {
        public string algorithm = "ECDSA";
        public IDictionary parameters = new Hashtable();
        public string id = "";
        public string key = "";

        public Control()
        {
        }

        public Control(string key, string id)
        {
            this.key = key;
            this.algorithm = "ECDSA";
            this.id = id;
            this.parameters["curve"] = "secp256r1";
        }
    }
}

