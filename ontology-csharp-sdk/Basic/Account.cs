using OntologyCSharpSDK.Interface;
using OntologyCSharpSDK.Network;
using System.Collections.Generic;
using OntologyCSharpSDK.Common;

namespace OntologyCSharpSDK.Basic
{
    internal class Account : IAccount
    {
        public static string Node { get; set; }

        public Account(string node)
        {
            Node = node;
        }

        public string createPrivateKey()
        {
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
            var hash = TransactionBuilder.GetHash(privatekey);
            var ontid = "did:ont:" + TransactionBuilder.U160ToAddress(hash);
            return ontid;
        }

        public NetworkResponse registerONTID(string ontid, string privatekey)
        {
            var tx = TransactionBuilder.BuildRegisterOntidTx(ontid, privatekey);
            var serialized = tx.serialize();
            IList<object> param = new List<object> { serialized };
            var result = NetworkHelper.SendNetworkRequest(Protocol.REST, "POST", Constants.REST_sendRawTransaction, param);
            return result;
        }

        public string createAddressFromPublickKey(string publicKey)
        {
            publicKey = "1202" + publicKey;
            var programHash = TransactionBuilder.GetSingleSigUInt160(publicKey);
            var address = TransactionBuilder.U160ToAddress(programHash);

            return address;
        }

        public NetworkResponse transferFund(string name, string fromaddress, string toaddress, decimal value, string privatekey)
        {
            var fromhexaddress = TransactionBuilder.AddresstTou160(fromaddress);
            var tohexaddress = TransactionBuilder.AddresstTou160(toaddress);
            var tx = TransactionBuilder.MakeTransferTransaction(name, fromhexaddress, tohexaddress, value.ToString(), privatekey);
            var serialized = tx.serialize();

            IList<object> param = new List<object> { serialized };
            var result = NetworkHelper.SendNetworkRequest(Protocol.REST, "POST", Constants.REST_sendRawTransaction, param);
            return result;
        }

        public NetworkResponse registerClaim(string context, string metadata,
            string content, string type, string issuer, string privatekey)
        {
            var claim = new Claim(context, content, metadata);
            var signed = claim.Sign(privatekey);
            var claimId = claim.Id;
            var path = Crypto.StringToHexString(claimId);
            var typeHex = Crypto.StringToHexString(type);

            var data = "{'Type':'" + type + "','Value':{'Context':'" + context + "','Issuer':'" + issuer + "'}}";

            var value = claim.GetValue(data);

            var tx = TransactionBuilder.BuildAddAttributeTx(path, value, typeHex, issuer, privatekey);

            var serialized = tx.serialize();
            IList<object> param = new List<object> { serialized };
            var result = NetworkHelper.SendNetworkRequest(Protocol.REST, "POST", Constants.REST_sendRawTransaction, param);
            return result;
        }

        public NetworkResponse addPublicKey(string ontid, string newPublickey, string publickey, string privatekey)
        {
            var tx = TransactionBuilder.BuildAddPublicKeyTx(ontid, newPublickey, publickey, privatekey);
            var serialized = tx.serialize();
            IList<object> param = new List<object> { serialized };
            var result = NetworkHelper.SendNetworkRequest(Protocol.REST, "POST", Constants.REST_sendRawTransaction, param);
            return result;
        }
    }
}
