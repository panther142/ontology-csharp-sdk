using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ontology_csharp_sdk.Common
{
    public class Constants
    {

        #region REST URL's
        const string RESTurl_getBlockGenerationTime = "/api/v1/node/generateblocktime";
        const string RESTurl_getBlockHeight = "/api/v1/block/height";
        const string RESTurl_getBlockHeightByTxHash = "/api/v1/block/height/txhash/"; //+hash
        const string RESTurl_getNodeCount = "/api/v1/node/connectioncount";
        const string RESTurl_getBlockByHeight = " /api/v1/block/details/height/"; //+height 
        const string RESTurl_getBlockByHash = " /api/v1/block/details/hash/"; //+hash
        const string RESTurl_getTransactionByHash = " /api/v1/transaction/"; //+hash 
        const string RESTurl_getAddressBalance = "/api/v1/balance/"; //+addr
        const string RESTurl_getContract = " /api/v1/contract/"; //+hash
        const string RESTurl_getSmartCodeEventByHeight = "/api/v1/smartcode/event/transactions/"; //+height
        const string RESTful_getSmartCodeEventByTxHash = "/api/v1/smartcode/event/txhash/"; //+hash
        const string RESTurl_getTransactionsInBlock = "/api/v1/block/transactions/height/"; //+height
        const string RESTurl_getMerkleProof = "/api/v1/merkleproof/";//+hash 
        const string RESTurl_getStorage = "/api/v1/storage/"; //+hash + /key
        const string RESTurl_sendRawTransaction = "/api/v1/transaction";
        #endregion


    }
}
