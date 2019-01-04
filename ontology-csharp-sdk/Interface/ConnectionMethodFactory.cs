using System;
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

        public virtual IConnectionMethod SetConnectionMethod(ConnectionMethod method)
        {
            try
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
                    default:
                        throw new ArgumentOutOfRangeException(nameof(method), method, null);
                }

                return connectionMethod;
            }
            catch { throw; }
        }
    }
}
