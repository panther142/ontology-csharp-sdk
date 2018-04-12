namespace Interface
{
    public interface IConnector
    {

        int getBlockGenerationTime();
        int getBlockHeight();
        string getAddressBalance(string ONTAddress);
        int getNodeCount();
        int getBlockHeightByTxHash(string TxHash);
        string getBlockHex(int blockHeight);
        string getBlockHex(string blockHash);
        string getBlockJson(int blockHeight);
        string getBlockJson(string blockHash);

    }
}
