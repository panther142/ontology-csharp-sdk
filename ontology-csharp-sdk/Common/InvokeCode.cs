namespace OntologyCSharpSDK.Common
{
    public class InvokeCode
    {
        public Fixed64 gasLimit { get; set; }
        public VmCode code { get; set; }
        public InvokeCode()
        {
            gasLimit = new Fixed64();
        }
        public string serialize()
        {
            var result = "";
            if (gasLimit != null)
            {
                result += gasLimit.serialize();
            }
            result += code.serialize();
            return result;
        }
    }
}