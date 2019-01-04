using System.Collections.Generic;

namespace OntologyCSharpSDK.Common
{
    public class Sig
    {
        public List<PubKey> pubKeys { get; set; }
        public int M { get; set; }
        public List<string> sigData { get; set; }

        public string serialize()
        {
            var result = "";
            result += Crypto.NumberToHex(pubKeys.Count);
            foreach (var t in pubKeys)
            {
                result += t.Serialize();
            }
            result += Crypto.NumberToHex(M);

            result += Crypto.NumberToHex(sigData.Count);
            foreach (var t in sigData)
            {
                result += Crypto.HexToVarBytes(t);
            }
            return result;
        }
    }
}