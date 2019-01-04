using System;
using System.Numerics;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OntologyCSharpSDK.Common
{
    public static class TransactionBuilder
    {
        private static readonly string _ontContract = "ff00000000000000000000000000000000000001";
        private static string _ongContract = "ff00000000000000000000000000000000000002";

        public static string getHash(string hex)
        {

            var sha256 = Crypto.SHA256ByteArray(Crypto.HexStringToByteArray(hex));
            var ripemd160 = Crypto.RIPEMD160ByteArray(Crypto.HexStringToByteArray(sha256));
            return ripemd160;
        }


        public static string u160ToAddress(string hash)
        {
            var data = "17" + hash;

            var programSha256 = Crypto.SHA256ByteArray(Crypto.HexStringToByteArray(data));
            var programSha2562 = Crypto.SHA256ByteArray(Crypto.HexStringToByteArray(programSha256));
            var programSha256Buffer = Crypto.HexStringToByteArray(programSha2562);

            var datas = data + programSha2562.Substring(0, 8).ToLower();

            return Base58Encoding.Encode(Crypto.HexStringToByteArray(datas)); ;
        }

        public static string AddresstTou160(string addressencoded)
        {
            var result = "";
            var decoded = Base58Encoding.Decode(addressencoded);

            var programhash = Crypto.ByteArrayToHexString(decoded).Substring(2, 40);
            var add58 = u160ToAddress(programhash);
            result = add58 != addressencoded ? "" : programhash;

            return result;
        }

        public static string getSingleSigUInt160(string hash)
        {
            var pkSha256 = Crypto.SHA256ByteArray(Crypto.HexStringToByteArray(hash));
            var pkRipemd160 = Crypto.RIPEMD160ByteArray(Crypto.HexStringToByteArray(pkSha256));
            return "01" + pkRipemd160.Substring(2, pkRipemd160.Length - 2);
        }

        public static string getPublicKey(string privatekey)
        {
            var bytes = Crypto.HexStringToByteArray(privatekey);
            var bytes_pub = Crypto.getPublicKeyByteArray(bytes);

            var publickey = Crypto.ByteArrayToHexString(bytes_pub);
            return publickey;
        }

        public static Transaction buildAddAttributeTx(string path, string value, string type, string ontid, string privatekey)
        {
            var publickey = getPublicKey(privatekey);
            publickey = "1202" + publickey;

            if (ontid.Substring(0, 3) == "did")
            {
                ontid = Crypto.StringToHexString(ontid);
            }

            var f = AbiModels.GetFunction("AddAttribute");

            var parameters = JArray.Parse(f["parameters"].ToString());

            for (var i = 0; i < parameters.Count; i++)
            {
                var parameter = parameters[i];
                if (parameter["name"].ToString() == "ontId")
                {
                    parameter["value"] = ontid;
                }
                if (parameter["name"].ToString() == "path")
                {
                    parameter["value"] = path;
                }
                if (parameter["name"].ToString() == "type")
                {
                    parameter["value"] = type;
                }
                if (parameter["name"].ToString() == "value")
                {
                    parameter["value"] = value;
                }
                if (parameter["name"].ToString() == "publicKey")
                {
                    parameter["value"] = publickey;
                }
                parameters[i] = parameter;
            }
            f["parameters"] = parameters;

            var hash = AbiModels.GetHash();

            var tx = makeInvokeTransaction(f, hash, privatekey);

            return tx;

        }

        public static Transaction BuildAddPublicKeyTx(string ontid, string new_publickey, string sender, string privatekey)
        {
            new_publickey = "1202" + new_publickey;
            sender = "1202" + sender;

            if (ontid.Substring(0, 3) == "did")
            {
                ontid = Crypto.StringToHexString(ontid);
            }

            var f = AbiModels.GetFunction("AddKey");

            var parameters = JArray.Parse(f["parameters"].ToString());

            for (var i = 0; i < parameters.Count; i++)
            {
                var parameter = parameters[i];
                if (parameter["name"].ToString() == "ontId")
                {
                    parameter["value"] = ontid;
                }
                if (parameter["name"].ToString() == "newPublicKey")
                {
                    parameter["value"] = new_publickey;
                }
                if (parameter["name"].ToString() == "sender")
                {
                    parameter["value"] = sender;
                }
                parameters[i] = parameter;
            }
            f["parameters"] = parameters;

            var hash = AbiModels.GetHash();

            var tx = makeInvokeTransaction(f, hash, privatekey);

            return tx;

        }

        public static Transaction MakeTransferTransaction(string tokentype, string fromaddress, string toaddress, string value, string privatekey)
        {
            var state = new State { @from = fromaddress, to = toaddress };

            var valueToSend = BigInteger.Parse(value).ToString();
            state.value = valueToSend;

            var transfer = new Transfers();
            var states = new List<State> { state };
            transfer.states = states;

            var contract = new Contract { address = _ontContract, method = "transfer", args = transfer.serialize() };

            var tx = new Transaction
            {
                version = 0x00,
                type = Crypto.HexToInteger(TxType.Invoke),
                nonce = Crypto.ByteArrayToHexString(Crypto.GetSecureRandomByteArray(4))
            };

            //inovke
            var code = "";
            //TODO: change with token type

            code += contract.serialize();
            var vmcode = new VmCode { code = code, vmType = VmType.NativeVM };
            var invokeCode = new InvokeCode { code = vmcode };
            tx.payload = invokeCode;

            if (privatekey != null)
            {
                tx = signTransaction(tx, privatekey);
            }

            return tx;

        }

        public static Transaction BuildRegisterOntidTx(string ontid, string privatekey)
        {
            var publickey = getPublicKey(privatekey);
            publickey = "1202" + publickey;

            if (ontid.Substring(0, 3) == "did")
            {
                ontid = Crypto.StringToHexString(ontid);
            }

            var f = AbiModels.GetFunction("RegIdWithPublicKey");

            var parameters = JArray.Parse(f["parameters"].ToString());

            for (var i = 0; i < parameters.Count; i++)
            {
                var parameter = parameters[i];
                if (parameter["name"].ToString() == "ontId")
                {
                    parameter["value"] = ontid;
                }
                if (parameter["name"].ToString() == "publicKey")
                {
                    parameter["value"] = publickey;
                }
                parameters[i] = parameter;
            }
            f["parameters"] = parameters;

            var hash = AbiModels.GetHash();

            var tx = makeInvokeTransaction(f, hash, privatekey);

            return tx;
        }

        public static Transaction makeInvokeTransaction(JToken func, string hash, string privatekey)
        {
            var tx = new Transaction
            {
                type = Crypto.HexToInteger(TxType.Invoke),
                version = 0x00,
                nonce = Crypto.ByteArrayToHexString(Crypto.GetSecureRandomByteArray(4))
            };

            var funcname = Crypto.StringToHexString(func["name"].ToString());


            var parameters = JArray.Parse(func["parameters"].ToString());

            var payload = makeInvokeCode(funcname, parameters, hash, VmType.NEOVM);
            tx.payload = payload;

            if (privatekey != null)
            {
                tx = signTransaction(tx, privatekey);
            }

            return tx;
        }

        public static Transaction signTransaction(Transaction tx, string privatekey, SignatureSchema schema = SignatureSchema.SHA256withECDSA)
        {


            var publickey = getPublicKey(privatekey);
            var type = 0x12;
            var pk = new PubKey(KeyType.PK_ECDSA, publickey);
            var hash = tx.getHash();

            var signed = Crypto.signData(hash, signDataType.Hex, privatekey);
            var sig = new Sig();
            var s = Crypto.NumberToHex((int)schema);
            signed = s + signed;
            sig.M = 1;

            var pubKeys = new List<PubKey>();
            var signData = new List<string>();
            pubKeys.Add(pk);
            signData.Add(signed);
            sig.pubKeys = pubKeys;
            sig.sigData = signData;

            tx.sigs.Add(sig);

            return tx;
        }

        public static InvokeCode makeInvokeCode(string funcname, JArray parameters, string hash, VmType vmtype = VmType.NativeVM)
        {
            var invokeCode = new InvokeCode();
            var vmCode = new VmCode();
            var args = buildSmartContractParam(funcname, parameters);
            //Console.WriteLine("code:" + code);

            var contract = new Contract { address = hash, args = args, method = "" };

            var code = contract.serialize();
            code = Crypto.NumberToHex(Crypto.HexToInteger(OPCODE.APPCALL)) + code;

            vmCode.code = code;
            vmCode.vmType = vmtype;
            invokeCode.code = vmCode;

            return invokeCode;
        }

        public static string buildSmartContractParam(string funcname, JArray parameters)
        {
            var result = "";

            for (var i = parameters.Count - 1; i > -1; i--)
            {
                var type = parameters[i]["type"].ToString();
                switch (type)
                {
                    case "Boolean":
                        result += pushBool(bool.Parse(parameters[i]["value"].ToString()));
                        break;
                    case "Number":
                        result += pushInt(int.Parse(parameters[i]["value"].ToString()));
                        break;
                    case "String":
                    {
                        var v = Crypto.StringToHexString(parameters[i]["value"].ToString());
                        result += pushHexString(v);
                        break;
                    }
                    case "ByteArray":
                        result += pushHexString(parameters[i]["value"].ToString());
                        break;
                }
            }
            //to work with vm
            long paramsLen = 0;
            if (parameters.Count == 0)
            {
                result += "00";
                paramsLen = 1;
            }
            else
            {
                paramsLen = parameters.Count;
            }
            var paramsLenR = Crypto.NumberToHex(paramsLen + 0x50);
            result += paramsLenR;

            var paramsEnd = "c1";
            result += paramsEnd;
            result += Crypto.HexToVarBytes(funcname);
            return result;
        }

        public static string pushBool(bool param)
        {
            var result = "";

            if (param)
            {
                result += OPCODE.PUSHT;
            }
            else
            {
                result += OPCODE.PUSHF;
            }

            return result;

        }

        public static string pushInt(int param)
        {
            var result = "";

            switch (param)
            {
                case -1:
                    result += OPCODE.PUSHM1;
                    break;
                case 0:
                    result += OPCODE.PUSH0;
                    break;
                default:
                {
                    if (param > 0 && param < 16)
                    {
                        var num = Crypto.HexToInteger(OPCODE.PUSH1) - 1 + param;
                        result += Crypto.NumberToHex(num);
                    }
                    else
                    {
                        result += Crypto.NumberToVarInt(param);
                    }

                    break;
                }
            }

            return result;

        }

        public static string pushHexString(string param)
        {
            var result = "";
            var len = param.Length / 2;

            if (len < Crypto.HexToInteger(OPCODE.PUSHBYTES75))
            {
                result += Crypto.NumberToHex(len);
            }
            else if (len < 0x100)
            {
                result += Crypto.NumberToHex(Crypto.HexToInteger(OPCODE.PUSHDATA1));
                result += Crypto.NumberToHex(len);
            }
            else if (len < 0x10000)
            {
                result += Crypto.NumberToHex(Crypto.HexToInteger(OPCODE.PUSHDATA2));
                result += Crypto.NumberToHex(len, 2, true);
            }
            else
            {
                result += Crypto.NumberToHex(Crypto.HexToInteger(OPCODE.PUSHDATA4));
                result += Crypto.NumberToHex(len, 4, true);
            }
            result += param;
            return result;

        }

        public static string BuildRestfulParam(string data)
        {
            var payload = new SendRawTransactionPayload { Action = "sendrawtransaction", Version = "1.0.0", Data = data };
            var result = JsonConvert.SerializeObject(payload);
            return result;
        }
    }
}
