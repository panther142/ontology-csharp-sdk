using Newtonsoft.Json.Linq;
using Interface;
using Network;
using System.Collections.Generic;
using Org.BouncyCastle.Security;
using System.Text;
using Common.Helpers;
using Common.Crypto;
using Common.HexBuilder;

namespace Basic
{
    class Account : IAccount
    {
        public string createPrivateKey() {

            var bytes = Crypto.getSecureRandomByteArray(32);
            var privatekey = Crypto.ByteArrayToHexString(bytes);
            return privatekey;
        }

        public string getPublicKey(string privatekey)
        {
            var bytes = Crypto.HexStringToByteArray(privatekey);
            var bytes_pub = Crypto.getPublicKeyByteArray(bytes);

            var publickey = Crypto.ByteArrayToHexString(bytes_pub);
            return publickey;
        }

        public string createONTID(string privatekey)
        {
            var hash = HexBuilder.getHash(privatekey);

            var ontid = "did:ont:" + HexBuilder.u160ToAddress(hash);

            return ontid;
        }

        public string createAddressFromPublickKey(string publicKey)
        {
            publicKey = "1202" + publicKey;

            string programHash = HexBuilder.getSingleSigUInt160(publicKey);
            string address = HexBuilder.u160ToAddress(programHash);

            return address;

        }

    }
}
