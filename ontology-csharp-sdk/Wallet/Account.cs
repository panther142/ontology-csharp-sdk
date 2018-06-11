using System.Collections;

namespace OntologyCSharpSDK.Wallet
{
    public class Account
    {
        
        public string address = "";
        public string enc_alg = "";
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
            this.algorithm = "ECDSA";
            this.parameters["curve"] = "secp256r1";
            this.enc_alg = "aes-256-ctr";
            this.extra = null;
        }

    }
}

