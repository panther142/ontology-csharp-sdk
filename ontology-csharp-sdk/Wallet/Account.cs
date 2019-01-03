using System.Collections;

namespace OntologyCSharpSDK.Wallet
{
    public class Account
    {
        public string address { get; set; } = "";
        public string encAlg { get; } = "";
        public string key = "";
        public string hash = "";
        public string algorithm = "";
        public IDictionary parameters = new Hashtable();
        public string label = "";
        public string publicKey = "";
        public string signatureScheme = "";
        public bool isDefault = false;
        public bool @lock = false;
        public string passwordHash = "";
        
        
        public object extra = null;

        public Account()
        {
            algorithm = "ECDSA";
            parameters["curve"] = "secp256r1";
            encAlg = "aes-256-ctr";
            extra = null;
        }

    }
}

