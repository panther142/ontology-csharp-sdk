namespace Interface
{
    interface IAccount
    {
        string createPrivateKey();
        string getPublicKey(string privatekey);
        string createONTID(string privatekey);
    }
}
