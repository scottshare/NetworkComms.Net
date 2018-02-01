using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using NetworkCommsDotNet.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCD.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            NetworkComms.ConnectionEstablishTimeoutMS = 10000000;

            NetworkComms.AppendGlobalIncomingPacketHandler<string>("Message", (packetHeader, connection, incomingString) =>
            {
                Console.WriteLine("\n  ... Incoming message from " + connection.ToString() + " saying '" + incomingString + "'.");
            });

            //Start listening for incoming 'TCP' connections.
            Connection.StartListening(ConnectionType.TCP, IPTools.ParseEndPointFromString(System.Configuration.ConfigurationManager.AppSettings["listenAddress"]));
            Console.ReadKey(true);
            NetworkComms.Shutdown();
        }
    }
}
