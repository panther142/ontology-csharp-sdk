using System.Collections;

namespace OntologyCSharpSDK.Wallet
{
    public class Control
    {
        public string algorithm = "ECDSA";
        public string id = "";
        public string key = "";
        public IDictionary parameters = new Hashtable();
        

        public Control()
        {
        }

        public Control(string key, string id)
        {
            this.key = key;
            algorithm = "ECDSA";
            this.id = id;
            parameters["curve"] = "secp256r1";
        }
    }
}

