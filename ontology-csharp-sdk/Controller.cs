using Interface;
using ConnectorTypes;

namespace Controller
{
    public class Controller
    {
        public enum ConnectorType
        {
            RPC
        }

        public virtual IConnector Gateway(ConnectorType method)
        {
            IConnector ConnectorInterface = null;

            switch (method)

            {
                case ConnectorType.RPC:
                    ConnectorInterface = new RPC();
                    break;
            }

            return ConnectorInterface;

        }

    }
}
