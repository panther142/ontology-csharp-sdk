using System;
using System.Text;
using System.Linq;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Security;

namespace Common.Crypto
{
    public static class Crypto
    {
        private static X9ECParameters curve = SecNamedCurves.GetByName("secp256k1");
        private static ECDomainParameters domain = new ECDomainParameters(curve.Curve, curve.G, curve.N, curve.H);

        public static byte[] getPublicKeyByteArray(byte[] privateKey)
        {
            BigInteger d = new BigInteger(privateKey);
            ECPoint q = domain.G.Multiply(d);

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


        public static string EncodeByteArray(byte[] ba)
        {
            return Convert.ToBase64String(ba);
        }
    }
}
