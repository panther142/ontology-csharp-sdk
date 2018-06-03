using System.Collections.Generic;

namespace OntologyCSharpSDK.Wallet
{
    public class Identity
    {
        public string label = "";
        public string ontid = "";
        public bool isDefault = false;
        public bool @lock = false;
        public IList<Control> controls = new List<Control>();
        public object extra = null;
    }
}
