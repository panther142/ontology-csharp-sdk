namespace Interface
{
    public interface IConnector
    {

        int getBlockGenerationTime();
        int getBlockHeight();
        string getONTBalance(string ONTAddress);
        int getNodeCount();
        int getBlockHeightByTxHash(string TxHash);

    }
}
