namespace OntologyCSharpSDK.Common
{
    public class TransactionAttribute
    {
        public TransactionAttributeUsage usage { get; set; }
        public string data { get; set; }
        public string size { get; set; }

        public string serialize()
        {
            var result = Crypto.NumberToHex(Crypto.HexToInteger(usage));
            if (!IsValidAttributeType(usage))
            {
                result = "[TxAttribute] error, Unsupported attribute Description.";
            }
            result += Crypto.HexToVarBytes(data);
            return result;
        }

        public bool IsValidAttributeType(TransactionAttributeUsage usage)
        {
            return usage == TransactionAttributeUsage.Nonce || 
                   usage == TransactionAttributeUsage.Script|| 
                   usage == TransactionAttributeUsage.Description || 
                   usage == TransactionAttributeUsage.DescriptionUrl;
        }
    }
}