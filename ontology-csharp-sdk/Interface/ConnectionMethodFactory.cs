using OntologyCSharpSDK.ConnectionMethods;

namespace OntologyCSharpSDK.Interface
{
    public class ConnectionMethodFactory
    {
        public enum ConnectionMethod
        {
            RPC,
            REST,
            Websocket
        }

        public virtual IConnectionMethod setConnectionMethod(ConnectionMethod method)
        {
            IConnectionMethod connectionMethod = null;

            switch (method)

            {
                case ConnectionMethod.RPC:
                    connectionMethod = new RPC();
                    break;
                    
                case ConnectionMethod.REST:
                    connectionMethod = new REST();
                    break;

                case ConnectionMethod.Websocket:
                    connectionMethod = new Websocket();
                    break;
            }

            return connectionMethod;

        }

    }
}
