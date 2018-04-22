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
    }
}
