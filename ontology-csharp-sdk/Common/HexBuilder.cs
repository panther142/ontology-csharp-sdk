using Common.Crypto;
using Merkator.BitCoin;

namespace Common.HexBuilder
{
    public static class HexBuilder
    {
        public static string getHash(string hex)
        {
            
            var sha256 = Crypto.Crypto.GenerateSHA256ByteArray(Crypto.Crypto.HexStringToByteArray(hex));
            var ripemd160 = Crypto.Crypto.GenerateRIPEMD160ByteArray(Crypto.Crypto.HexStringToByteArray(sha256));
            return ripemd160;
        }


        public static string u160ToAddress(string hash)
        {
            var data = "41" + hash;

            var ProgramSha256 = Crypto.Crypto.GenerateSHA256ByteArray(Crypto.Crypto.HexStringToByteArray(data));
            var ProgramSha256_2 = Crypto.Crypto.GenerateSHA256ByteArray(Crypto.Crypto.HexStringToByteArray(ProgramSha256));
            var ProgramSha256Buffer = Crypto.Crypto.HexStringToByteArray(ProgramSha256_2);

            var datas = data + ProgramSha256_2.Substring(0, 8).ToLower();


            return Base58Encoding.Encode(Crypto.Crypto.HexStringToByteArray(datas)); ;
        }


        public static string getSingleSigUInt160(string hash)
        {
            var PkSha256 = Crypto.Crypto.GenerateSHA256ByteArray(Crypto.Crypto.HexStringToByteArray(hash));
            var PkRipemd160 = Crypto.Crypto.GenerateRIPEMD160ByteArray(Crypto.Crypto.HexStringToByteArray(PkSha256));

            return "01" + PkRipemd160.Substring(2, PkRipemd160.Length - 2);
        }

    }
}
