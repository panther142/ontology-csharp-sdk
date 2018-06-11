using System.Collections.Generic;

namespace OntologyCSharpSDK.Wallet
{
    public class Wallet
    {
        public string name = "OntologyWallet";
        public string version = "1.0";
        public Scrypt scrypt = new Scrypt();
        public object extra = null;
        public IList<Identity> identities = new List<Identity>();
        public IList<Account> accounts = new List<Account>();

        public Wallet()
        {
            identities.Clear();
            identities.Add(new Identity { label = "NewIdentity", isDefault = true, @lock = false, extra = null, controls = new List<Control>() { new Control("6Pxxxxxxxxx", "1") }, ontid = "did:ont:AMxxxxxxxxxxxxxxxxxxxxxxx" });
            accounts.Add(new Account { address = "TA6xxxxxxxxxxxx", key = "6Pxxxxxxxxxxx",  hash = "sha256",  label="NewAccount", publicKey = "1202xxxxxxxxxxxxxx", signatureScheme = "SHA256withECDSA", passwordHash = "passwordhash"});
            scrypt = new Scrypt(16384, 8, 8);
        }

    }
}
