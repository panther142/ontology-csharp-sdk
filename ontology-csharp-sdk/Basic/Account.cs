using Newtonsoft.Json.Linq;
using Interface;
using Network;
using System.Collections.Generic;
using Org.BouncyCastle.Security;
using Common.Helpers;

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

        
    }
}
