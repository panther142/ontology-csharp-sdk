using Newtonsoft.Json.Linq;

namespace OntologyCSharpSDK.Common
{
    public class Body
    {
        public string Context { get; set; }
        public JToken Content { get; set; }
        public JToken Metadata { get; set; }
    }
}