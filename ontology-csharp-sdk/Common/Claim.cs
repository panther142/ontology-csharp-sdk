using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OntologyCSharpSDK.Common
{
    public class Claim
    {
        public string Context { get; set; }
        public string Id { get; set; }
        public JToken Content { get; set; }
        public JToken Metadata { get; set; }
        public Signature Signature { get; set; }
        public Claim(string context, string content, string metadata)
        {
            Context = context;
            Content = JToken.Parse(content);
            Metadata = JToken.Parse(metadata);

            var body = new JObject
            {
                ["Context"] = context,
                ["Content"] = JToken.Parse(content),
                ["Metadata"] = JToken.Parse(metadata)
            };
            var bodySerialised = JsonConvert.SerializeObject(body);
            Id = Crypto.SHA256ByteArray(Crypto.StringToByteArray(bodySerialised));
        }

        public string GetValue(string data)
        {
            var result = Crypto.StringToHexString(data);
            return result;
        }

        public Signature Sign(string privatekey)
        {
            var body = new JObject { ["Context"] = Context, ["Id"] = Id, ["Content"] = Content, ["Metadata"] = Metadata };

            var unsignedData = JsonConvert.SerializeObject(body);
            var signatureValue = Crypto.signData(unsignedData, SignDataType.String, privatekey);
            var sig = new Signature { Value = signatureValue };
            Signature = sig;
            return sig;
        }

    }
}