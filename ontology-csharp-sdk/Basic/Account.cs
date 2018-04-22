using Interface;
using Network;
using Common.Cryptology;
using Common.TransactionBuilder;
using Common.Net;

namespace Basic
{
    class Account : IAccount
    {
        public string createPrivateKey() {

            var bytes = Crypto.GetSecureRandomByteArray(32);
            var privatekey = Crypto.ByteArrayToHexString(bytes);
            return privatekey;
        }

        public string getPublicKey(string privatekey)
        {
            var publickey = TransactionBuilder.getPublicKey(privatekey);
            return publickey;
        }

        public string createONTID(string privatekey)
        {
            var hash = TransactionBuilder.getHash(privatekey);

            var ontid = "did:ont:" + TransactionBuilder.u160ToAddress(hash);

            return ontid;
        }

        public string createAddressFromPublickKey(string publicKey)
        {
            publicKey = "1202" + publicKey;
            string programHash = TransactionBuilder.getSingleSigUInt160(publicKey);
            string address = TransactionBuilder.u160ToAddress(programHash);

            return address;
       }

        public APIResult registerONTID(string ontid, string privatekey)
        {
            var tx = TransactionBuilder.buildRegisterOntidTx(ontid, privatekey);
            var serialized = tx.serialize();
            var param = TransactionBuilder.buildRestfulParam(serialized);
            var url = NetworkBuilder.getSendRawTxURL();
            var result = RESTrequests.sendRESTrequest(url, "POST", null, param);
            return result;
        }

    }
}
