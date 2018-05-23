using System;
using System.Numerics;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OntologyCSharpSDK.Common
{
    public static class TransactionBuilder
    {
        private static string ONT_CONTRACT = "ff00000000000000000000000000000000000001";
        private static string ONG_CONTRACT = "ff00000000000000000000000000000000000002";

        public static string getHash(string hex)
        {

            var sha256 = Crypto.SHA256ByteArray(Crypto.HexStringToByteArray(hex));
            var ripemd160 = Crypto.RIPEMD160ByteArray(Crypto.HexStringToByteArray(sha256));
            return ripemd160;
        }


        public static string u160ToAddress(string hash)
        {
            var data = "41" + hash;

            var ProgramSha256 = Crypto.SHA256ByteArray(Crypto.HexStringToByteArray(data));
            var ProgramSha256_2 = Crypto.SHA256ByteArray(Crypto.HexStringToByteArray(ProgramSha256));
            var ProgramSha256Buffer = Crypto.HexStringToByteArray(ProgramSha256_2);

            var datas = data + ProgramSha256_2.Substring(0, 8).ToLower();

            return Base58Encoding.Encode(Crypto.HexStringToByteArray(datas)); ;
        }

        public static string AddresstTou160(string addressencoded)
        {
            var result = "";
            var decoded = Base58Encoding.Decode(addressencoded);

            var programhash = Crypto.ByteArrayToHexString(decoded).Substring(2, 40);
            var add58 = u160ToAddress(programhash);
            if (add58 != addressencoded)
            {
                result = "";
            }
            else
            {
                result = programhash;
            }

            return result;
        }

        public static string getSingleSigUInt160(string hash)
        {
            var PkSha256 = Crypto.SHA256ByteArray(Crypto.HexStringToByteArray(hash));
            var PkRipemd160 = Crypto.RIPEMD160ByteArray(Crypto.HexStringToByteArray(PkSha256));

            return "01" + PkRipemd160.Substring(2, PkRipemd160.Length - 2);
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

            JToken f = abiModels.GetFunction("AddAttribute");

            var parameters = JArray.Parse(f["parameters"].ToString());

            for (int i = 0; i < parameters.Count; i++)
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

            var hash = abiModels.GetHash();

            var tx = makeInvokeTransaction(f, hash, privatekey);

            return tx;

        }

        public static Transaction buildAddPublicKeyTx(string ontid, string new_publickey, string sender, string privatekey)
        {
            new_publickey = "1202" + new_publickey;
            sender = "1202" + sender;

            if (ontid.Substring(0, 3) == "did")
            {
                ontid = Crypto.StringToHexString(ontid);
            }

            JToken f = abiModels.GetFunction("AddKey");

            var parameters = JArray.Parse(f["parameters"].ToString());

            for (int i = 0; i < parameters.Count; i++)
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

            var hash = abiModels.GetHash();

            var tx = makeInvokeTransaction(f, hash, privatekey);

            return tx;

        }

        public static Transaction makeTransferTransaction(string tokentype, string fromaddress, string toaddress, string value, string privatekey)
        {
            var state = new State();
            state.from = fromaddress;
            state.to = toaddress;

            var valueToSend = BigInteger.Parse(value).ToString();
            state.value = valueToSend;

            var transfer = new Transfers();
            var states = new List<State>();
            states.Add(state);
            transfer.states = states;

            var contract = new Contract();
            contract.address = ONT_CONTRACT;
            contract.method = "transfer";
            contract.args = transfer.serialize();

            var tx = new Transaction();
            tx.version = 0x00;
            tx.type = Crypto.HexToInteger(TxType.Invoke);
            tx.nonce = Crypto.ByteArrayToHexString(Crypto.GetSecureRandomByteArray(4));

            //inovke
            var code = "";
            //TODO: change with token type

            code += contract.serialize();
            var vmcode = new VmCode();
            vmcode.code = code;
            vmcode.vmType = VmType.NativeVM;
            var invokeCode = new InvokeCode();
            invokeCode.code = vmcode;
            tx.payload = invokeCode;

            if (privatekey != null)
            {
                tx = signTransaction(tx, privatekey);
            }

            return tx;

        }

        public static Transaction buildRegisterOntidTx(string ontid, string privatekey)
        {
            var publickey = getPublicKey(privatekey);
            publickey = "1202" + publickey;

            if (ontid.Substring(0, 3) == "did")
            {
                ontid = Crypto.StringToHexString(ontid);
            }

            JToken f = abiModels.GetFunction("RegIdWithPublicKey");

            var parameters = JArray.Parse(f["parameters"].ToString());

            for (int i = 0; i < parameters.Count; i++)
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

            var hash = abiModels.GetHash();

            var tx = makeInvokeTransaction(f, hash, privatekey);

            return tx;
        }

        public static Transaction makeInvokeTransaction(JToken func, string hash, string privatekey)
        {
            var tx = new Transaction();
            tx.type = Crypto.HexToInteger(TxType.Invoke);
            tx.version = 0x00;
            tx.nonce = Crypto.ByteArrayToHexString(Crypto.GetSecureRandomByteArray(4));

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

            var contract = new Contract();
            contract.address = hash;
            contract.args = args;
            contract.method = "";

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

            for (int i = parameters.Count - 1; i > -1; i--)
            {
                var type = parameters[i]["type"].ToString();
                if (type == "Boolean")
                {
                    result += pushBool(bool.Parse(parameters[i]["value"].ToString()));
                }
                else if (type == "Number")
                {
                    result += pushInt(int.Parse(parameters[i]["value"].ToString()));
                }
                else if (type == "String")
                {
                    var v = Crypto.StringToHexString(parameters[i]["value"].ToString());
                    result += pushHexString(v);
                }
                else if (type == "ByteArray")
                {
                    result += pushHexString(parameters[i]["value"].ToString());
                }

            }
            //to work with vm
            Int64 paramsLen = 0;
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

            if (param == -1)
            {
                result += OPCODE.PUSHM1;
            }
            else if (param == 0)
            {
                result += OPCODE.PUSH0;
            }
            else if (param > 0 && param < 16)
            {
                var num = Crypto.HexToInteger(OPCODE.PUSH1) - 1 + param;
                result += Crypto.NumberToHex(num);
            }
            else
            {
                result += Crypto.NumberToVarInt(param);
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

        public static string buildRestfulParam(string data)
        {
            var payload = new SendRawTransactionPayload();

            payload.Action = "sendrawtransaction";
            payload.Version = "1.0.0";
            payload.Data = data;

            var result = JsonConvert.SerializeObject(payload);

            return result;
        }

    }


    public class PubKey
    {
        public KeyType type { get; set; }
        public string publicKey { get; set; }
        public PubKey(KeyType type, string publicKey)
        {
            this.type = type;
            this.publicKey = publicKey;
        }
        public string serialize()
        {
            var result = "";
            result += "12"; //ecdsa
            result += "02"; //p256
            result += this.publicKey;
            var length = Crypto.NumberToHex(result.Length / 2);
            return length + result;
        }
    }
    public class InvokeCode
    {
        public Fixed64 gasLimit { get; set; }
        public VmCode code { get; set; }
        public InvokeCode()
        {
            gasLimit = new Fixed64();
        }
        public string serialize()
        {
            var result = "";
            if (this.gasLimit != null)
            {
                result += this.gasLimit.serialize();
            }
            result += this.code.serialize();
            return result;
        }
    }
    public class VmCode
    {
        public string code { get; set; }
        public VmType vmType { get; set; }

        public string serialize()
        {
            var result = "";
            result += Crypto.NumberToHex(Crypto.HexToInteger(this.vmType));
            result += Crypto.HexToVarBytes(this.code);
            return result;
        }
    }
    public class Uint160
    {
        public byte[] value { get; set; }
        public string serialize()
        {
            var hex = Crypto.ByteArrayToHexString(this.value);
            return Crypto.HexToVarBytes(hex);
        }
    }
    public class Fixed64
    {
        public string value { get; set; }

        public Fixed64()
        {
            value = "0000000000000000";
        }

        public string serialize()
        {
            return this.value;
        }

    }
    public class Transaction
    {
        public int type { get; set; }
        public int version { get; set; }
        public InvokeCode payload { get; set; }
        public string nonce { get; set; }

        public List<TransactionAttribute> txAttributes { get; set; }
        public List<Fee> fee { get; set; }
        public Fixed64 networkFee { get; set; }
        public List<Sig> sigs { get; set; }



        public Transaction()
        {
            networkFee = new Fixed64();
            txAttributes = new List<TransactionAttribute>();
            fee = new List<Fee>();
            sigs = new List<Sig>();
            type = 0xd1;
            version = 0x00;
        }

        public string getHash()
        {

            var data = this.serializeUnsignedData();

            // Console.WriteLine("serializeUnsignedData:" + data);

            var ProgramSha256 = Crypto.SHA256ByteArray(Crypto.HexStringToByteArray(data));
            var ProgramSha256_2 = Crypto.SHA256ByteArray(Crypto.HexStringToByteArray(ProgramSha256));

            return ProgramSha256_2;

        }

        public string serialize()
        {
            var unsigned = this.serializeUnsignedData();
            // Console.WriteLine("unsigned:" + unsigned);

            var signed = this.serializeSignedData();
            // Console.WriteLine("signed:" + signed);
            return unsigned + signed;
        }

        public string serializeUnsignedData()
        {
            var result = "";
            result += Crypto.NumberToHex(this.version);
            result += Crypto.NumberToHex(this.type);
            //nonce 4bytes
            result += this.nonce;
            result += this.payload.serialize();

            //serialize transaction attributes
            result += Crypto.NumberToHex(this.txAttributes.Count);
            for (var i = 0; i < this.txAttributes.Count; i++)
            {
                result += this.txAttributes[i].serialize();
            }
            result += Crypto.NumberToHex(this.fee.Count);
            for (var i = 0; i < this.fee.Count; i++)
            {
                result += this.fee[i].amount.serialize();
                result += this.fee[i].payer.serialize();
            }

            if (this.networkFee != null)
            {
                result += this.networkFee.serialize();
            }

            return result;
        }

        public string serializeSignedData()
        {
            var result = "";
            result += Crypto.NumberToHex(this.sigs.Count);
            for (var i = 0; i < this.sigs.Count; i++)
            {
                result += this.sigs[i].serialize();
            }

            return result;
        }
    }
    public class TransactionAttribute
    {
        public TransactionAttributeUsage usage { get; set; }
        public string data { get; set; }
        public string size { get; set; }

        public string serialize()
        {
            var result = "";
            result = Crypto.NumberToHex(Crypto.HexToInteger(this.usage));
            if (!isValidAttributeType(this.usage))
            {
                result = "[TxAttribute] error, Unsupported attribute Description.";
            }
            result += Crypto.HexToVarBytes(this.data);
            return result;
        }

        public bool isValidAttributeType(TransactionAttributeUsage usage)
        {
            return usage == TransactionAttributeUsage.Nonce || usage == TransactionAttributeUsage.Script
       || usage == TransactionAttributeUsage.Description || usage == TransactionAttributeUsage.DescriptionUrl;
        }
    }
    public class Fee
    {
        public Fixed64 amount { get; set; }
        public Uint160 payer { get; set; }
    }
    public class Sig
    {
        public List<PubKey> pubKeys { get; set; }
        public int M { get; set; }
        public List<string> sigData { get; set; }

        public string serialize()
        {
            var result = "";
            result += Crypto.NumberToHex(this.pubKeys.Count);
            for (var i = 0; i < this.pubKeys.Count; i++)
            {
                result += this.pubKeys[i].serialize();
            }
            result += Crypto.NumberToHex(this.M);

            result += Crypto.NumberToHex(this.sigData.Count);
            for (var i = 0; i < this.sigData.Count; i++)
            {
                result += Crypto.HexToVarBytes(this.sigData[i]);
            }
            return result;
        }
    }
    public class SendRawTransactionPayload
    {
        public string Action { get; set; }
        public string Version { get; set; }
        public string Data { get; set; }
    }
    public class Claim
    {
        public string Context { get; set; }
        public string Id { get; set; }
        public JToken Content { get; set; }
        public JToken Metadata { get; set; }
        public Signature Signature { get; set; }
        public Claim(string context, string content, string metadata)
        {
            this.Context = context;
            this.Content = JToken.Parse(content);
            this.Metadata = JToken.Parse(metadata);

            var body = new JObject();
            body["Context"] = context;
            body["Content"] = JToken.Parse(content);
            body["Metadata"] = JToken.Parse(metadata);
            var body_serialised = JsonConvert.SerializeObject(body);
            this.Id = Crypto.SHA256ByteArray(Crypto.StringToByteArray(body_serialised)).ToString();
        }

        public string GetValue(string data)
        {
            var result = Crypto.StringToHexString(data).ToString();
            return result;
        }


        public Signature sign(string privatekey)
        {
            var body = new JObject();
            body["Context"] = this.Context;
            body["Id"] = this.Id;
            body["Content"] = this.Content;
            body["Metadata"] = this.Metadata;

            var unsignedData = JsonConvert.SerializeObject(body);
            var signatureValue = Crypto.signData(unsignedData, signDataType.String, privatekey);
            var sig = new Signature();
            sig.Value = signatureValue;
            this.Signature = sig;
            return sig;
        }

    }

    public class Body
    {
        public string Context { get; set; }
        public JToken Content { get; set; }
        public JToken Metadata { get; set; }
    }
    public class Metadata
    {
        public string CreateTime { get; set; }
        public string Issuer { get; set; }
        public string Subject { get; set; }
        public string Expires { get; set; }
        public string Revocation { get; set; }
        public string Crl { get; set; }
    }
    public class Signature
    {
        public string Format { get; set; }
        public string Algorithm { get; set; }
        public string Value { get; set; }
        public Signature()
        {
            this.Format = "pgp";
            this.Algorithm = "ECDSAwithSHA256";
        }
    }
}
