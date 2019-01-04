namespace OntologyCSharpSDK.Common
{
    public class PubKey
    {
        public KeyType type { get; set; }
        public string publicKey { get; set; }
        public PubKey(KeyType type, string publicKey)
        {
            this.type = type;
            this.publicKey = publicKey;
        }
        public string Serialize()
        {
            var result = "";
            result += "12"; //ecdsa
            result += "02"; //p256
            result += publicKey;
            var length = Crypto.NumberToHex(result.Length / 2);
            return length + result;
        }
    }
}