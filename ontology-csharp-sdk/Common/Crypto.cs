using System;
using System.Text;
using System.Linq;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Common.Cryptology
{
    public static class Crypto
    {
        private static readonly X9ECParameters curve = SecNamedCurves.GetByName("secp256r1");
        private static readonly ECDomainParameters domain = new ECDomainParameters(curve.Curve, curve.G, curve.N, curve.H);

        public static byte[] getPublicKeyByteArray(byte[] privateKey)
        {
            Org.BouncyCastle.Math.BigInteger d = new Org.BouncyCastle.Math.BigInteger(1, privateKey);
            Org.BouncyCastle.Math.EC.ECPoint q = domain.G.Multiply(d);

            var publicParams = new ECPublicKeyParameters(q, domain);
            return publicParams.Q.GetEncoded(true);
        }

        public static string signData(string message, string privatekey)
        {
            var bytes = HexStringToByteArray(privatekey);
            var curve = SecNamedCurves.GetByName("secp256r1");
            var domain = new ECDomainParameters(curve.Curve, curve.G, curve.N, curve.H);
            var keyParameters = new ECPrivateKeyParameters(new Org.BouncyCastle.Math.BigInteger(1, bytes), domain);
            var signer = SignerUtilities.GetSigner("SHA-256withECDSA");
            signer.Init(true, keyParameters);
            var bytes_message = HexStringToByteArray(message);
            signer.BlockUpdate(bytes_message, 0, bytes_message.Length);
            var signature = signer.GenerateSignature();
            return ByteArrayToHexString(DSADERtoPlain(signature));
        }


        public static byte[] DSADERtoPlain(byte[] sig)
        {
            Asn1Sequence seq = (Asn1Sequence)Asn1Object.FromByteArray(sig);
            var seq0 = (DerInteger)seq[0];
            var seq1 = (DerInteger)seq[1];
            byte[] r = seq0.Value.ToByteArray();
            byte[] s = seq1.Value.ToByteArray();
            int ri = (r[0] == 0) ? 1 : 0;
            int rl = r.Length - ri;
            int si = (s[0] == 0) ? 1 : 0;
            int sl = s.Length - si;
            byte[] res;
            if (rl > sl)
            {
                res = new byte[rl * 2];
            }
            else
            {
                res = new byte[sl * 2];
            }

            Array.Copy(r, ri, res, res.Length / 2 - rl, rl);
            Array.Copy(s, si, res, res.Length - sl, sl);

            return res; // size: 64
        }

        public static byte[] SerializeLong2Dec(double value)
        {
            value *= 100;
            value = Math.Round(value, MidpointRounding.AwayFromZero);

            if (value < -999999999.0 || value > 999999999.0)
            {
                throw new ArgumentOutOfRangeException();
            }

            int value2 = (int)value;

            return BitConverter.GetBytes(value2);
        }

        public static byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            var bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static byte[] GetSecureRandomByteArray(int size)
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

        public static string ByteArrayToString(byte[] ba)
        {
            string str = Encoding.UTF8.GetString(ba);
            return str;
        }

        public static byte[] StringToByteArray(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            return bytes;
        }

        public static string StringToHexString(string str)
        {
            string hex = ByteArrayToHexString(StringToByteArray(str));
            return hex;
        }

        public static string HexStringToString(string hex)
        {
            string str = ByteArrayToString(HexStringToByteArray(hex));
            return str;
        }


        public static string SHA256String(string inputString)
        {
            var sha256 = System.Security.Cryptography.SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha256.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        public static string SHA256ByteArray(byte[] bytes)
        {
            var sha256 = System.Security.Cryptography.SHA256Managed.Create();
            byte[] hash = sha256.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        public static string RIPEMD160String(string inputString)
        {
            // create a ripemd160 object
            var r160 = System.Security.Cryptography.RIPEMD160Managed.Create();
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

        public static string RIPEMD160ByteArray(byte[] bytes)
        {
            // create a ripemd160 object
            var r160 = System.Security.Cryptography.RIPEMD160Managed.Create();
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

        public static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }

        public static int HexToInteger(dynamic hex)
        {
            return Convert.ToInt32(hex);
        }

        public static string IntegerToHex(int number)
        {
            return number.ToString("X4");
        }

        public static string BigintToHex(System.Numerics.BigInteger number)
        {
            return number.ToString("X");
        }

        public static string NumberToHex(Int64 num, int size = 1, bool littleEndian = false)
        {
            var hexstring = "";
            if (num < 0)
            {
            }
            else
            {
                if (size % 1 != 0)
                {

                }
                else
                {
                    size = size * 2;
                    hexstring = BigintToHex(num);

                    string repeat = new String('0', size);

                    hexstring = hexstring.Length % size == 0 ? hexstring : (repeat + hexstring).Substring(hexstring.Length);
                    if (littleEndian)
                    {
                        hexstring = reverseHex(hexstring);
                    }
                }

            }
            return hexstring.ToLower();
        }

        public static string NumberToVarInt(Int64 num)
        {
            if (num < 0xfd)
            {
                return NumberToHex(num);

            }
            else if (num <= 0xffff)
            {
                // uint16
                return "fd" + NumberToHex(num, 2, true);


            }
            else if (num <= 0xffffffff)
            {
                // uint32
                return "fe" + NumberToHex(num, 4, true);
            }
            else
            {
                // uint64
                return "ff" + NumberToHex(num, 8, true);
            }

        }

        public static string reverseHex(string hex)
        {
            var result = "";
            if (hex.Length % 2 != 0)
            {

            }
            else
            {
                for (var i = hex.Length - 2; i >= 0; i -= 2)
                {
                    result += hex.Substring(i, 2);

                }

            }
            return result;
        }



        public static string HexToVarBytes(string hex)
        {
            var result = "";
            result += NumberToVarInt(hex.Length / 2);
            result += hex;
            return result;
        }

        public static string StringToVarBytes(string str)
        {
            var result = "";

            var hex = StringToHexString(str);

            var hexLen = NumberToVarInt(hex.Length / 2);

            result += hexLen;

            result += hex;

            return result;
        }


    }
}
