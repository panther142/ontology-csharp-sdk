using System;
using System.Numerics;
using System.Collections.Generic;
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
            this.version = "00";
        }

        public string serialize()
        {
            var result = "";
            result += this.version;
            if (this.from == null)
            {
                throw new Exception("[State.serialize], Invalid from address");
            }
            else
            {
                if (this.from.Length != 40)
                {
                    throw new Exception("[State.serialize], Invalid from address");
                }
            }
            result += this.from;
            if (this.to == null)
            {
                throw new Exception("[State.serialize], Invalid to address");
            }
            else
            {
                if (this.to.Length != 40)
                {
                    throw new Exception("[State.serialize], Invalid to address");
                }
            }
            result += this.to;
            var bn = Crypto.ByteArrayToHexString(BigInteger.Parse(this.value).ToByteArray());
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
            this.version = "00";
        }

        public string serialize()
        {
            string result = "";
            result += this.version;
            result += Crypto.NumberToHex(this.states.Count);
            for (var i = 0; i < this.states.Count; i++)
            {
                result += this.states[i].serialize();
            }
            return result;
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
            this.version = "00";
            this.code = "00";
        }
        public string serialize()
        {
            var result = "";
            result += this.version;
            result += this.code;
            result += this.address;
            result += Crypto.StringToVarBytes(this.method);
            result += Crypto.HexToVarBytes(this.args);

            return result;
        }
    }
}
