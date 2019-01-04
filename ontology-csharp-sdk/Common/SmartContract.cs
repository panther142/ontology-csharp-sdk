using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using OntologyCSharpSDK.Common;

namespace OntologyCSharpSDK.Common
{
    public class State
    {
        public string version { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string value { get; set; }

        public State()
        {
            version = "00";
        }

        public string serialize()
        {
            var result = "";
            result += version;
            if (@from == null)
            {
                throw new Exception("[State.serialize], Invalid from address");
            }

            if (@from.Length != 40)
            {
                throw new Exception("[State.serialize], Invalid from address");
            }
            result += @from;
            if (to == null)
            {
                throw new Exception("[State.serialize], Invalid to address");
            }

            if (to.Length != 40)
            {
                throw new Exception("[State.serialize], Invalid to address");
            }
            result += to;
            var bn = Crypto.ByteArrayToHexString(BigInteger.Parse(value).ToByteArray());
            bn = bn.Length % 2 == 0 ? bn : '0' + bn;
            result += Crypto.HexToVarBytes(bn);

            return result;
        }
    }

    public class Transfers
    {
        public string version { get; set; }
        public List<State> states { get; set; }

        public Transfers()
        {
            version = "00";
        }

        public string serialize()
        {
            var result = "";
            result += version;
            result += Crypto.NumberToHex(states.Count);
            return states.Aggregate(result, (current, t) => current + t.serialize());
        }

    }

    public class Contract
    {
        public string version { get; set; }
        public string code { get; set; }
        public string address { get; set; }
        public string method { get; set; }
        public string args { get; set; }

        public Contract()
        {
            version = "00";
            code = "00";
        }
        public string serialize()
        {
            var result = "";
            result += version;
            result += code;
            result += address;
            result += Crypto.StringToVarBytes(method);
            result += Crypto.HexToVarBytes(args);

            return result;
        }
    }
}
