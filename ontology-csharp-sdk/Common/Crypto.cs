using System;
using System.Text;
using System.Linq;
using System.Security.Cryptography;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Security;
using Merkator.BitCoin;

namespace Common.Crypto
{
    public static class Crypto
    {
        private static readonly X9ECParameters curve = SecNamedCurves.GetByName("secp256r1");
        private static readonly ECDomainParameters domain = new ECDomainParameters(curve.Curve, curve.G, curve.N, curve.H);

        public static byte[] getPublicKeyByteArray(byte[] privateKey)
        {
            BigInteger d = new BigInteger(1,privateKey);
            Org.BouncyCastle.Math.EC.ECPoint q = domain.G.Multiply(d);

            var publicParams = new ECPublicKeyParameters(q, domain);
            return publicParams.Q.GetEncoded(true);
        }

        public static byte[] getSecureRandomByteArray(int size)
        {
            SecureRandom random = new SecureRandom();
            return random.GenerateSeed(size);
        }

        public static string ByteArrayToHexString(byte[] ba)
        {
            System.Text.StringBuilder hex = new System.Text.StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public static byte[] HexStringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
        

        public static string GenerateSHA256String(string inputString)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha256.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        public static string GenerateSHA256ByteArray(byte[] bytes)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] hash = sha256.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        public static string GenerateRIPEMD160String(string inputString)
        {
            // create a ripemd160 object
            RIPEMD160 r160 = RIPEMD160Managed.Create();
            // convert the string to byte
            byte[] myByte = System.Text.Encoding.ASCII.GetBytes(inputString);
            // compute the byte to RIPEMD160 hash
            byte[] encrypted = r160.ComputeHash(myByte);
            // create a new StringBuilder process the hash byte
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < encrypted.Length; i++)
            {
                sb.Append(encrypted[i].ToString("X2"));
            }
            // convert the StringBuilder to String and convert it to lower case and return it.
            return sb.ToString().ToLower();
        }

        public static string GenerateRIPEMD160ByteArray(byte[] bytes)
        {
            // create a ripemd160 object
            RIPEMD160 r160 = RIPEMD160Managed.Create();
            // compute the byte to RIPEMD160 hash
            byte[] encrypted = r160.ComputeHash(bytes);
            // create a new StringBuilder process the hash byte
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < encrypted.Length; i++)
            {
                sb.Append(encrypted[i].ToString("X2"));
            }
            // convert the StringBuilder to String and convert it to lower case and return it.
            return sb.ToString().ToLower();
        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }



    }
}
