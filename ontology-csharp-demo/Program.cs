using OntologyCSharpSDK;
using System;

namespace ontology_csharp_demo
{
    class Program
    {

        static OntologySDK OntSDK = new OntologySDK();
        static string TxHash = "5fcffe97e2c4d413b34cea985bf548bfce0ae3a0cbf2c9ec9e518388c0dd650a5fcffe97e2c4d413b34cea985bf548bfce0ae3a0cbf2c9ec9e518388c0dd650a";
        static string Address = "TA87tPxU1Zq8ANQfMnrGZTTs6X4UYFEjsw";
        static string BlockHash = "f0d2da3a971b2e51f8aebce06c37a9bb5253db39a28b1fd713b222928e2c439c";
        static string privatekey = "2d3747c9b5eba66e3b4f5b1491aa08720503fffca167b2b405af6f07c8eb108b";

        static void Main(string[] args)
        {

            RPCDemo();

        }

        //Query blockchain using RPC
        public static void RPCDemo()
        {

            Console.WriteLine("(RPC) Block Generation Time: " + OntSDK.getBlockGenerationTime().ToString() + " seconds ");
            Console.WriteLine("(RPC) Block Height: " + OntSDK.getBlockHeight().ToString());
            Console.WriteLine("(RPC) ONT Balance: " + OntSDK.getAddressBalance(Address));
            Console.WriteLine("(RPC) Node Count: " + OntSDK.getNodeCount());
            Console.WriteLine("(RPC) Block Height by Tx Hash: " + OntSDK.getBlockHeightByTxHash(TxHash));
            Console.WriteLine("(RPC) Block Hex (int): " + OntSDK.getBlockHex(15));
            Console.WriteLine("(RPC) Block Hex (hash): " + OntSDK.getBlockHex(BlockHash));
            Console.WriteLine("(RPC) Block Json (int): " + OntSDK.getBlockJson(15));
            Console.WriteLine("(RPC) Block Json (hash): " + OntSDK.getBlockJson(BlockHash));
            Console.WriteLine("(RPC) Transaction Hex by Tx Hash: " + OntSDK.getRawTransactionHex(TxHash));
            Console.WriteLine("(RPC) Transaction Json by Tx Hash: " + OntSDK.getRawTransactionJson(TxHash));

        }

        // create a new private key
        public static void CreatePrivateKey()
        {
            var privatekey = OntSDK.createPrivateKey();
            Console.WriteLine("privatekey:{0}", privatekey);
        }

        // create ONTID and register on blockchain
        public static void CreateRegisterONTID()
        {
            var ontid = OntSDK.createONTID(privatekey);
            Console.WriteLine("ontid:{0}", ontid);
            var result = OntSDK.registerONTID(ontid, privatekey);
            Console.WriteLine("result:{0}", result.content);
        }

        // get the public key from privatekey
        public static void GetPublicKey()
        {
            var publickey = OntSDK.getPublicKey(privatekey);
            Console.WriteLine("publickey:{0}", publickey);
        }

        // get the address from privatekey
        public static void GetAddress()
        {
            var publickey = OntSDK.getPublicKey(privatekey);

            var address = OntSDK.createAddressFromPublickKey(publickey);
            Console.WriteLine("address:{0}", address);
        }

        // transfer fund to another address, signed by privatekey
        public static void TransferFund()
        {
            var publickey = OntSDK.getPublicKey(privatekey);
            var fromaddress = OntSDK.createAddressFromPublickKey(publickey);
            var toaddress = "TA5UZsbLNNw1kLQhtarvVMfoCVBe6ZDZGv";
            var result = OntSDK.transferFund("ONT", fromaddress, toaddress, 5, privatekey);
            Console.WriteLine("result:{0}", result.content);
        }
    }

}

