using Interface;
using ConnectorTypes;

namespace OntologyCSharpSDK.NetController
{
    public class Controller
    {
        public enum ConnectorType
        {
            RPC,
            REST,
            Websocket
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

                case ConnectorType.Websocket:
                    ConnectorInterface = new Websocket();
                    break;
            }

            return ConnectorInterface;

        }

    }
}
