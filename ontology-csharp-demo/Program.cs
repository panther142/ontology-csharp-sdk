﻿using OntologyCSharpSDK;
using System;

namespace ontology_csharp_demo
{
    class Program
    {

        // MAIN_NODE = "54.222.182.88";
        // TEST_NODE = "139.219.111.50";
        private static string node = "ws://192.168.4.5:20335";

        static OntologySDK OntSDK = new OntologySDK(node);
        static string TxHash = "5fcffe97e2c4d413b34cea985bf548bfce0ae3a0cbf2c9ec9e518388c0dd650a5fcffe97e2c4d413b34cea985bf548bfce0ae3a0cbf2c9ec9e518388c0dd650a";
        static string Address = "TA87tPxU1Zq8ANQfMnrGZTTs6X4UYFEjsw";
        static string BlockHash = "f0d2da3a971b2e51f8aebce06c37a9bb5253db39a28b1fd713b222928e2c439c";
        static string privatekey = "2d3747c9b5eba66e3b4f5b1491aa08720503fffca167b2b405af6f07c8eb108b";

        static void Main(string[] args)
        {
            WebsocketDemo();
            //RPCDemo();
            //RESTDemo();
            //CreateRegisterONTID();
            //TransferFund();
            //CreateAndRegisterClaim();
            //AddPublicKey();
            Console.WriteLine("\r\n\r\nPress any key..");
            Console.ReadKey();

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

        public static void RESTDemo()
        {
            Console.WriteLine("(REST) Block Generation Time: " + OntSDK.getBlockGenerationTime().ToString() + " seconds ");
            Console.WriteLine("(REST) Block Height: " + OntSDK.getBlockHeight().ToString());
            Console.WriteLine("(REST) ONT Balance: " + OntSDK.getAddressBalance(Address));
            Console.WriteLine("(REST) Node Count: " + OntSDK.getNodeCount());
            Console.WriteLine("(REST) Block Height by Tx Hash: " + OntSDK.getBlockHeightByTxHash(TxHash));
            Console.WriteLine("(REST) Block Json (int): " + OntSDK.getBlockJson(15));
            Console.WriteLine("(REST) Block Json (hash): " + OntSDK.getBlockJson(BlockHash));
            Console.WriteLine("(REST) Transaction Json by Tx Hash: " + OntSDK.getRawTransactionJson(TxHash));
        }

        public static void WebsocketDemo()
        {
            Console.WriteLine("(Websocket) Block Generation Time: " + OntSDK.getBlockGenerationTime().ToString() + " seconds ");
            Console.WriteLine("(Websocket) Block Height: " + OntSDK.getBlockHeight().ToString());
            Console.WriteLine("(Websocket) Block Json (int): " + OntSDK.getBlockJson(50));
            Console.WriteLine("(Websocket) Block Json (hash): " + OntSDK.getBlockJson(BlockHash));
            Console.WriteLine("(Websocket) Block hash by height: " + OntSDK.getBlockHashByHeight(50));
            Console.WriteLine("(Websocket) Get Address Balance: " + OntSDK.getAddressBalance("TA8HEr37yqME9RRrcCgTp9qZN2F1xdWPAz"));            
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
            Console.WriteLine("result:{0}", result.rawResponse);
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
            Console.WriteLine("result:{0}", result.rawResponse);
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

            var result = OntSDK.registerClaim(context, metaData, content, type, issuer_ontid, privatekey);
            Console.WriteLine("result:{0}", result.rawResponse);
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


            var ontid = OntSDK.createONTID(privatekey); // get exisitng ONTID by privatekey
            var publickey = OntSDK.getPublicKey(privatekey); // get existing public key by privatekey

            var privatekey2 = OntSDK.createPrivateKey(); // create new private key to generate a new publick key, it can be supplied (if unknown) without a new privatekey
            var publickey2 = OntSDK.getPublicKey(privatekey2); // get the new public key

            var result = OntSDK.addPublicKey(ontid, publickey2, publickey, privatekey);
            Console.WriteLine("result:{0}", result.rawResponse);
        }
    }

}

