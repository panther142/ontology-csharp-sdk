using System;
using OntologyCSharpSDK;

namespace ontology_csharp_examples
{
    class Program
    {
        protected static string privatekey = "2d3747c9b5eba66e3b4f5b1491aa08720503fffca167b2b405af6f07c8eb108b";
        protected static OntologySDK sdk = new OntologySDK();

        static void Main(string[] args)
        {
            QueryBlockchain();
            //CreatePrivateKey();
            //CreateRegisterONTID();
            //GetPublicKey();
            //GetAddress();
            //TransferFund();
        }

        public static void QueryBlockchain()
        {
            int ts = sdk.getBlockGenerationTime();
            Console.WriteLine("privatekey:{0}", privatekey);
        }




        // create a new private key
        public static void CreatePrivateKey()
        {
            var privatekey = sdk.createPrivateKey();
            Console.WriteLine("privatekey:{0}", privatekey);
        }

        // create ONTID and register on blockchain
        public static void CreateRegisterONTID()
        {
            var ontid = sdk.createONTID(privatekey);
            Console.WriteLine("ontid:{0}", ontid);
            var result = sdk.registerONTID(ontid, privatekey);
            Console.WriteLine("result:{0}", result.content);
        }

        // get the public key from privatekey
        public static void GetPublicKey()
        {
            var publickey = sdk.getPublicKey(privatekey);
            Console.WriteLine("publickey:{0}", publickey);
        }

        // get the address from privatekey
        public static void GetAddress()
        {
            var publickey = sdk.getPublicKey(privatekey);

            var address = sdk.createAddressFromPublickKey(publickey);
            Console.WriteLine("address:{0}", address);
        }

        // transfer fund to another address, signed by privatekey
        public static void TransferFund()
        {
            var publickey = sdk.getPublicKey(privatekey);
            var fromaddress = sdk.createAddressFromPublickKey(publickey);
            var toaddress = "TA5UZsbLNNw1kLQhtarvVMfoCVBe6ZDZGv";
            var result = sdk.transferFund("ONT", fromaddress, toaddress, 5, privatekey);
            Console.WriteLine("result:{0}", result.content);
        }
    }
}
