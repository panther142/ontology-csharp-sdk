namespace OntologyCSharpSDK.Common
{
    public class Constants
    {

        #region REST URL's
        public const string REST_getBlockGenerationTime = "/api/v1/node/generateblocktime";
        public const string REST_getBlockHeight = "/api/v1/block/height";
        public const string REST_getBlockHeightByTxHash = "/api/v1/block/height/txhash"; //+hash
        public const string REST_getNodeCount = "/api/v1/node/connectioncount";
        public const string REST_getBlockByHeight = "/api/v1/block/details/height"; //+height 
        public const string REST_getBlockByHash = "/api/v1/block/details/hash"; //+hash
        public const string REST_getTransactionByHash = "/api/v1/transaction"; //+hash 
        public const string REST_getAddressBalance = "/api/v1/balance"; //+addr
        public const string REST_getContract = "/api/v1/contract"; //+hash
        public const string REST_getSmartCodeEventByHeight = "/api/v1/smartcode/event/transactions"; //+height
        public const string REST_getSmartCodeEventByTxHash = "/api/v1/smartcode/event/txhash"; //+hash
        public const string REST_getTransactionsInBlock = "/api/v1/block/transactions/height"; //+height
        public const string REST_getMerkleProof = "/api/v1/merkleproof";//+hash 
        public const string REST_getStorage = "/api/v1/storage"; //+hash + /key
        public const string REST_sendRawTransaction = "/api/v1/transaction?preExec=0";
        public const string REST_sendRawTransactionPreExec = "/api/v1/transaction?preExec=1";
        public const string REST_getGasPrice = "/api/v1/gasprice";
        public const string REST_getAllowance = "/api/v1/allowance"; //+asset + /from + /to
        #endregion


    }
}
