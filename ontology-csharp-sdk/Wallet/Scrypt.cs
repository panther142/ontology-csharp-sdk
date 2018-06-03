namespace OntologyCSharpSDK.Wallet
{
    public class Scrypt
    {
        private int n = 16384;
        private int r = 8;
        private int p = 8;

        public Scrypt()
        {
        }

        public Scrypt(int n, int r, int p)
        {
            this.n = n;
            this.r = r;
            this.p = p;
        }
    }
}
