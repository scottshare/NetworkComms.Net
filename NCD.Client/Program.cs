using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using NetworkCommsDotNet.Connections.TCP;
using NetworkCommsDotNet.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
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
            TCPConnection.GetConnection(new ConnectionInfo(IPTools.ParseEndPointFromString(System.Configuration.ConfigurationManager.AppSettings["socketServer"]))).SendObject("Message", "hello!");

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
