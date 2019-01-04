namespace OntologyCSharpSDK.Common
{
    public class Uint160
    {
        public byte[] value { get; set; }
        public string serialize()
        {
            var hex = Crypto.ByteArrayToHexString(value);
            return Crypto.HexToVarBytes(hex);
        }
    }
}