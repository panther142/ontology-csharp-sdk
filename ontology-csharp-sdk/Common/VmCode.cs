namespace OntologyCSharpSDK.Common
{
    public class VmCode
    {
        public string code { get; set; }
        public VmType vmType { get; set; }

        public string serialize()
        {
            var result = "";
            result += Crypto.NumberToHex(Crypto.HexToInteger(vmType));
            result += Crypto.HexToVarBytes(code);
            return result;
        }
    }
}