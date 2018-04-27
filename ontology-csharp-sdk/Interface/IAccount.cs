using Network;
namespace Interface
{
    interface IAccount
    {
        string createPrivateKey();
        string getPublicKey(string privatekey);
        string createONTID(string privatekey);
        string createAddressFromPublickKey(string publicKey);

        APIResult registerONTID(string ontid, string privatekey);
        APIResult transferFund(string name, string fromaddress, string toaddress, decimal value, string privatekey);
        APIResult registerClaim(string context, string metadata,string content, string type, string issuer, string privatekey);
    }
}
