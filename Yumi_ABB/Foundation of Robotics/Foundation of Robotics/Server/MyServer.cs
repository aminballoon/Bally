using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyPX410;
using System.Diagnostics;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class MyServer
    {
        // # Constant
        const string NAME = "SERVER";
        const string TITLE = "PX410 : " + NAME;

        // # Config
        static int CONFIG_PORT = 1150;

        // # Socket
        static Socket mysock = null;
        static bool isWaitConnection = false;
        static Mutex mutex_clientlist = new Mutex(false);

        // # Client
        static List<MyCommunication> CLIENT_LIST = null;

        // # Main
        static int Main(string[] args)
        {
            Console.Title = TITLE;

            Log("\n==================================\n" +
            "Initialize" + NAME +
            "\n==================================");

            Log("Listening port " + CONFIG_PORT);

            // # START ---
            #region Listening port
            try
            {
                mysock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IPEndPoint localendpoint = new IPEndPoint(IPAddress.Any, CONFIG_PORT);
                mysock.Bind(localendpoint);
                mysock.Listen(10);

                isWaitConnection = true;
                // client list
                CLIENT_LIST = new List<MyCommunication>();
            }
            catch (Exception e)
            {
                Log("Main : " + e.Message, true);
            }
            #endregion

            // # Loop wait connection
            #region Client Acception
            while (isWaitConnection)
            {
                try
                {
                    Console.Title = TITLE + "(" + CLIENT_LIST.Count + " client)";

                    Log("Wait new incomming connection ...");

                    // wait incomming connection then add to list
                    Socket accept = mysock.Accept();

                    Log("Accept new incomming connection ...");

                    if (mutex_clientlist.WaitOne())
                    {
                        //Log("Entered thread " + Thread.CurrentThread.ManagedThreadId);
                        Log("Add client to list");

                        CLIENT_LIST.Add(new MyCommunication(accept));

                        Log("Mapping client event");

                        CLIENT_LIST.Last().eventOnLinkData += MyServer_eventOnLinkData;
                        CLIENT_LIST.Last().eventOnDisconnected += MyServer_eventOnDisconnected;
                        CLIENT_LIST.Last().Start();

                        //Log("Leaving thread " + Thread.CurrentThread.ManagedThreadId);

                        mutex_clientlist.ReleaseMutex();
                    }
                    else
                    {
                        Log("Main : Can't enter to locked thread " + Thread.CurrentThread.ManagedThreadId, true);
                    }
                }
                catch (Exception e)
                {
                    Log("Main : " + e.Message, true);
                }
            }
            #endregion
            // # END ---
            Log("Server End");

            return 0;
        }

        // Event map on disconnected
        static void MyServer_eventOnDisconnected(MyCommunication client)
        {
            Log("MyServer_eventOnDisconnected : " + client.NAME);

            if (mutex_clientlist.WaitOne())
            {
                Log("Remove client from list");

                CLIENT_LIST.Remove(client);
                mutex_clientlist.ReleaseMutex();
            }
            else
            {
                Log("MyServer_eventOnDisconnected : Can't enter to locked thread " + Thread.CurrentThread.ManagedThreadId, true);
            }

            Console.Title = TITLE + "(" + CLIENT_LIST.Count + " client)";
        }

        // Event map on link data
        static void MyServer_eventOnLinkData(MyCommunication client, string ADDR, string DATA)
        {
            if (mutex_clientlist.WaitOne())
            {
                foreach (var dst in CLIENT_LIST)
                {
                    // find match destinetion
                    if (ADDR == dst.NAME)
                    {
                        // transmit data
                        dst.SendData(client.NAME, DATA);
                        //break;
                    }
                }
                mutex_clientlist.ReleaseMutex();
            }
            else
            {
                Log("MyServer_eventOnLinkData : Can't enter to locked thread " + Thread.CurrentThread.ManagedThreadId, true);
            }
        }

        // # General Method
        static void Log(string text, bool isError = false)
        {
            Console.WriteLine(NAME + " > " + text);

            LOG.Write(NAME, text, isError);
        }
    }
}
