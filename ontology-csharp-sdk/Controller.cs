using Interface;
using ConnectorTypes;

namespace OntologyCSharpSDK
{
    public class Controller
    {
        public enum ConnectorType
        {
            RPC,
            REST
        }

        public virtual IConnector Gateway(ConnectorType method)
        {
            IConnector ConnectorInterface = null;

            switch (method)

            {
                case ConnectorType.RPC:
                    ConnectorInterface = new RPC();
                    break;


                case ConnectorType.REST:
                    ConnectorInterface = new REST();
                    break;

            }

            return ConnectorInterface;

        }

    }
}
