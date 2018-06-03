using System.Collections.Generic;

namespace OntologyCSharpSDK.Wallet
{
    public class Wallet
    {
        private string name = "OntologyWallet";
        private string version = "1.0";
        private Scrypt scrypt = new Scrypt();
        private object extra = null;
        private IList<Identity> identities = new List<Identity>();
        private IList<Account> accounts = new List<Account>();

        public Wallet()
        {
            identities.Clear();
        }

    }
}
