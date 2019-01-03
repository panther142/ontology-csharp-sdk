using System.Collections.Generic;

namespace OntologyCSharpSDK.Common
{
    public class Transaction
    {
        public int type { get; set; }
        public int version { get; set; }
        public InvokeCode payload { get; set; }
        public string nonce { get; set; }

        public List<TransactionAttribute> txAttributes { get; set; }
        public List<Fee> fee { get; set; }
        public Fixed64 networkFee { get; set; }
        public List<Sig> sigs { get; set; }

        public Transaction()
        {
            networkFee = new Fixed64();
            txAttributes = new List<TransactionAttribute>();
            fee = new List<Fee>();
            sigs = new List<Sig>();
            type = 0xd1;
            version = 0x00;
        }

        public string getHash()
        {

            var data = serializeUnsignedData();

            // Console.WriteLine("serializeUnsignedData:" + data);

            var ProgramSha256 = Crypto.SHA256ByteArray(Crypto.HexStringToByteArray(data));
            var ProgramSha256_2 = Crypto.SHA256ByteArray(Crypto.HexStringToByteArray(ProgramSha256));

            return ProgramSha256_2;

        }

        public string serialize()
        {
            var unsigned = serializeUnsignedData();
            // Console.WriteLine("unsigned:" + unsigned);

            var signed = SerializeSignedData();
            // Console.WriteLine("signed:" + signed);
            return unsigned + signed;
        }

        public string serializeUnsignedData()
        {
            var result = "";
            result += Crypto.NumberToHex(version);
            result += Crypto.NumberToHex(type);
            //nonce 4bytes
            result += nonce;
            result += payload.serialize();

            //serialize transaction attributes
            result += Crypto.NumberToHex(txAttributes.Count);
            foreach (var t in txAttributes)
            {
                result += t.serialize();
            }
            result += Crypto.NumberToHex(fee.Count);
            foreach (var t in fee)
            {
                result += t.amount.serialize();
                result += t.payer.serialize();
            }

            if (networkFee != null)
            {
                result += networkFee.serialize();
            }

            return result;
        }

        public string SerializeSignedData()
        {
            var result = "";
            result += Crypto.NumberToHex(sigs.Count);
            foreach (var t in sigs)
            {
                result += t.serialize();
            }

            return result;
        }
    }
}