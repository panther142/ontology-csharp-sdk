using OntologyCSharpSDK;
using OntologyCSharpSDK.Interface;
using System;
using System.Threading.Tasks;

namespace ontology_csharp_demo
{
    class Program
    {
        // MAIN_NODE = "54.222.182.88";
        // TEST_NODE = "139.219.111.50";
        private static readonly string _node = "http://192.168.4.5:20336";

        static OntologySdk OntSDK = new OntologySdk(_node, ConnectionMethodFactory.ConnectionMethod.RPC);
        static string TxHash = "5fcffe97e2c4d413b34cea985bf548bfce0ae3a0cbf2c9ec9e518388c0dd650a5fcffe97e2c4d413b34cea985bf548bfce0ae3a0cbf2c9ec9e518388c0dd650a";
        static string Address = "TA87tPxU1Zq8ANQfMnrGZTTs6X4UYFEjsw";
        static string BlockHash = "f0d2da3a971b2e51f8aebce06c37a9bb5253db39a28b1fd713b222928e2c439c";
        static string privatekey = "2d3747c9b5eba66e3b4f5b1491aa08720503fffca167b2b405af6f07c8eb108b";

        static void Main(string[] args)
        {
            /************  Example to show Websocket Subcribe functionality
            ConsoleKeyInfo cki;
            var progress = new Progress<string>(ProgressUpdate);
            Task wsSubscribeTask = Task.Factory.StartNew(() => OntSDK.websocketSubscribe.SubscribeAsync(null, true, true, false, false, "ws://192.168.4.5:20335", progress));
            do
            {
                cki = Console.ReadKey();
            } while (cki.Key != ConsoleKey.Escape);
            wsSubscribeTask.Dispose(); 
            ****************/

            //QueryBlockchain();
            //CreateRegisterONTID();
            //TransferFund();
            //CreateAndRegisterClaim();
            //AddPublicKey();
            Console.WriteLine("\r\n\r\nPress any key..");
            Console.ReadKey();
        }

        //Query blockchain using chosen connection method (RPC, REST or Websocket)
        public static void QueryBlockchain()
        {
            try { Console.WriteLine("Connecting to blockchain via: " + OntSDK.ConnectionManager.GetType()); } catch { };
            try { Console.WriteLine("Block Generation Time: " + OntSDK.ConnectionManager.getBlockGenerationTime().ToString() + " seconds "); } catch { };
            try { Console.WriteLine("Block Height: " + OntSDK.ConnectionManager.getBlockHeight().ToString()); } catch { };
            try { Console.WriteLine("ONT Balance: " + OntSDK.ConnectionManager.getAddressBalance(Address)); } catch { };
            try { Console.WriteLine("Node Count: " + OntSDK.ConnectionManager.getNodeCount()); } catch { };
            try { Console.WriteLine("Block Height by Tx Hash: " + OntSDK.ConnectionManager.getBlockHeightByTxHash(TxHash)); } catch { };
            try { Console.WriteLine("Block Hex (int): " + OntSDK.ConnectionManager.getBlockHex(15)); } catch { };
            try { Console.WriteLine("Block Hex (hash): " + OntSDK.ConnectionManager.getBlockHex(BlockHash)); } catch { };
            try { Console.WriteLine("Block Json (int): " + OntSDK.ConnectionManager.getBlockJson(15)); } catch { };
            try { Console.WriteLine("Block Json (hash): " + OntSDK.ConnectionManager.getBlockJson(BlockHash)); } catch { };
            try { Console.WriteLine("Transaction Hex by Tx Hash: " + OntSDK.ConnectionManager.getRawTransactionHex(TxHash)); } catch { };
            try { Console.WriteLine("Transaction Json by Tx Hash: " + OntSDK.ConnectionManager.getRawTransactionJson(TxHash)); } catch { };
        }

        // create a new private key
        public static void CreatePrivateKey()
        {
            var privatekey = OntSDK.CreatePrivateKey();
            Console.WriteLine("privatekey:{0}", privatekey);
        }

        // create ONTID and register on blockchain
        public static void CreateRegisterONTID()
        {
            var ontid = OntSDK.CreateOntid(privatekey);
            Console.WriteLine("ontid:{0}", ontid);
            var result = OntSDK.RegisterOntid(ontid, privatekey);
            Console.WriteLine("result:{0}", result.RawResponse);
        }

        // get the public key from privatekey
        public static void GetPublicKey()
        {
            var publickey = OntSDK.GetPublicKey(privatekey);
            Console.WriteLine("publickey:{0}", publickey);
        }

        // get the address from privatekey
        public static void GetAddress()
        {
            var publickey = OntSDK.GetPublicKey(privatekey);

            var address = OntSDK.CreateAddressFromPublickKey(publickey);
            Console.WriteLine("address:{0}", address);
        }

        // transfer fund to another address, signed by privatekey
        public static void TransferFund()
        {
            var publickey = OntSDK.GetPublicKey(privatekey);
            var fromaddress = OntSDK.CreateAddressFromPublickKey(publickey);
            var toaddress = "TA5UZsbLNNw1kLQhtarvVMfoCVBe6ZDZGv";
            var result = OntSDK.TransferFund("ONT", fromaddress, toaddress, 5, privatekey);
            Console.WriteLine("result:{0}", result.RawResponse);
        }

        public static void CreateAndRegisterClaim()
        {
            var issuer_ontid = "YOUR ONTID generated by privatekey";
            var subject_ontid = "Target subject ONTID";

            // create the claim
            var context = "claim:standard0001";
            var metaData = @"{'CreateTime':'2017-01-01T22:01:20Z','Issuer':'" + issuer_ontid + "','Subject':'" + subject_ontid + "','Expires':'2019-01-01','Revocation':'RevocationList','Crl':'http://192.168.1.1/rev.crl'}";
            var content = @"{'name':'Michael','age':25}";
            var type = "JSON";

            var result = OntSDK.RegisterClaim(context, metaData, content, type, issuer_ontid, privatekey);
            Console.WriteLine("result:{0}", result.RawResponse);
        }

        public static void AddPublicKey()
        {
            /*
             * To add a new public key to existing ONTID, in addition to new public key value,
             * you also need to provide the existing ONTID and publickey generated by original privatekey
             * Currently there is a privacy concern to add any new PublicKey to someone'e ONTID(if existing privatekey is known)
             * There is a proposal to add additional privatekey for the new public key, so both old and new public key are fully authorised and owned by the same ONTID user
             * This method may get updated once Ontology team has provided an update to this Tx
             */


            var ontid = OntSDK.CreateOntid(privatekey); // get exisitng ONTID by privatekey
            var publickey = OntSDK.GetPublicKey(privatekey); // get existing public key by privatekey

            var privatekey2 = OntSDK.CreatePrivateKey(); // create new private key to generate a new publick key, it can be supplied (if unknown) without a new privatekey
            var publickey2 = OntSDK.GetPublicKey(privatekey2); // get the new public key

            var result = OntSDK.AddPublicKey(ontid, publickey2, publickey, privatekey);
            Console.WriteLine("result:{0}", result.RawResponse);
        }

        static void ProgressUpdate(string progress)
        {
            Console.WriteLine(progress);
        }

    }

}

