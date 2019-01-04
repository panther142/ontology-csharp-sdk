namespace OntologyCSharpSDK.Common
{
    public class TransactionAttribute
    {
        public TransactionAttributeUsage Usage { get; set; }
        public string Data { get; set; }
        public string Size { get; set; }

        public string Serialize()
        {
            var result = Crypto.NumberToHex(Crypto.HexToInteger(Usage));
            if (!IsValidAttributeType(Usage))
            {
                result = "[TxAttribute] error, Unsupported attribute Description.";
            }
            result += Crypto.HexToVarBytes(Data);
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