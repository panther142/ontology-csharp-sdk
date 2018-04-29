using Network;
using Network.NetworkHelper;

namespace Interface
{
    interface IAccount
    {
        string createPrivateKey();
        string getPublicKey(string privatekey);
        string createONTID(string privatekey);
        string createAddressFromPublickKey(string publicKey);
                
        NetworkResponse registerONTID(string ontid, string privatekey);
        NetworkResponse transferFund(string name, string fromaddress, string toaddress, decimal value, string privatekey);
        NetworkResponse registerClaim(string context, string metadata, string content, string type, string issuer, string privatekey);
    }
}
