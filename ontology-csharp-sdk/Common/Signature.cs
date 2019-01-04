namespace OntologyCSharpSDK.Common
{
    public class Signature
    {
        public string Format { get; set; }
        public string Algorithm { get; set; }
        public string Value { get; set; }
        public Signature()
        {
            Format = "pgp";
            Algorithm = "ECDSAwithSHA256";
        }
    }
}