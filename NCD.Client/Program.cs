using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using NetworkCommsDotNet.Connections.TCP;
using NetworkCommsDotNet.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NCD.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            NetworkComms.ConnectionEstablishTimeoutMS = 10000000;

            //Send a message which will attempt to connect to local
            var conn = TCPConnection.GetConnection(new ConnectionInfo(IPTools.ParseEndPointFromString(System.Configuration.ConfigurationManager.AppSettings["socketServer"])));
            conn.AppendIncomingPacketHandler<string>("Message", (packetHeader, connection, incomingString) =>
            {
                Console.WriteLine("\n  ... Incoming message from " + connection.ToString() + " saying '" + incomingString + "'.");
            });
            conn.SendObject("Message", "hello!");

            while (true)
            {
                string content = Console.ReadLine();
                if (content.Equals("exit"))
                    break;
                TCPConnection.GetConnection(new ConnectionInfo(IPTools.ParseEndPointFromString(System.Configuration.ConfigurationManager.AppSettings["socketServer"]))).SendObject("Message", content);
            }
            Console.ReadKey(true);
        }
    }
}
