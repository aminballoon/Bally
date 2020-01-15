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
    class MyCommunication
    {
        // # Delegate
        public delegate void myDelegateMatch(MyCommunication client, string ADDR, string DATA);
        public delegate void myDelegateMyClient(MyCommunication client);

        public event myDelegateMatch eventOnLinkData;
        public event myDelegateMyClient eventOnDisconnected;

        // # Socket
        Socket mysock = null;
        int ID = 0;
        bool REGISTER = false;
        public string NAME = "New Client";

        // # Thread
        Thread ThreadCore = null;
        bool isCommunication = false;
        byte[] tcp_buffer = new byte[MyPX410.MyModule.TCP_BUFFER_SIZE];
        string receive_buffer = "";

        // # Initialize
        public MyCommunication(Socket sock)
        {
            Log("\n==================================\n" +
            "Initialize " + NAME +
            "\n==================================");

            mysock = sock;
            ID = ((IPEndPoint)mysock.RemoteEndPoint).Port;

            isCommunication = true;
        }

        // # Start Core
        public void Start()
        {
            Log("Create communication thread");
            ThreadCore = new Thread(new ThreadStart(MyCore));

            Log("Start thread " + ThreadCore.ManagedThreadId);
            ThreadCore.Start();
        }

        // # Core Thread
        void MyCore()
        {
            string buffer;
            int nbytes;
            string ADDR, DATA;

            Log("Start Communication Thread");

            // Send welcome message
            SendData("SERVER", "Connected!");

            while (isCommunication)
            {
                try
                {
                    //Log("Wait for incomming data ...");

                    nbytes = mysock.Receive(tcp_buffer);

                    //Log("Receive data " + nbytes + " bytes");

                    if (nbytes > 0)
                    {
                        buffer = System.Text.Encoding.UTF8.GetString(tcp_buffer, 0, nbytes);

                        //Log(mysock.RemoteEndPoint + " --> " + buffer);

                        // BEGIN DECODING
                        receive_buffer += buffer;
                        int i_start;

                        while (receive_buffer.Length >= 7)  // [1:2=3]
                        {
                            i_start = receive_buffer.IndexOf('[');
                            if (i_start != -1)
                            {
                                // trim start
                                receive_buffer = receive_buffer.Substring(i_start);
                            }
                            else
                            {
                                break;
                            }

                            int res = MyPX410.MyFunction.MyDecodePX4(receive_buffer, out ADDR, out DATA);
                            if (res > 0)
                            {
                                // drop current data
                                receive_buffer = receive_buffer.Substring(res);

                                OnMatch(ADDR, DATA);
                            }
                            else
                            {
                                Debug.WriteLine("Mismatch : " + receive_buffer);
                                break;
                            }
                        }
                        // END DECODING
                    }
                    else
                    {
                        Log(mysock.RemoteEndPoint + " shutting down sending and receiving");
                        mysock.Shutdown(SocketShutdown.Both);
                        Log(mysock.RemoteEndPoint + " closing connection");
                        mysock.Close();

                        eventOnDisconnected(this);
                        isCommunication = false;
                    }
                }
                catch (Exception e)
                {
                    Log("MyCore : " + e.Message, true);

                    Log(mysock.RemoteEndPoint + " force shutting down sending and receiving");
                    mysock.Shutdown(SocketShutdown.Both);
                    Log(mysock.RemoteEndPoint + " force closing connection");
                    mysock.Close();

                    eventOnDisconnected(this);
                    isCommunication = false;
                }
            }

            Log("End Communication Thread");
        }

        // # Socket Method

        void OnMatch(string ADDR, string DATA)
        {
            try
            {
                Debug.WriteLine("Match >" + ADDR + ":" + DATA);

                if (!REGISTER)
                {
                    if (DATA == "REG")
                    {
                        // to register module name (first receive)
                        REGISTER = true;
                        NAME = ADDR;

                        Log("Register as " + NAME);
                    }
                }
                else
                {
                    // transmit to another module
                    eventOnLinkData(this, ADDR, DATA);
                }
            }
            catch (Exception e)
            {
                Log("OnMatch : " + e.Message, true);
            }
        }

        public void SendData(string addr, string data)
        {
            try
            {
                if (!mysock.Connected)
                {
                    Log("Socket is disconnected");

                    return;
                }

                data = MyPX410.MyFunction.MyEncodePX4(addr, data);
                byte[] buff = buff = System.Text.Encoding.UTF8.GetBytes(data);
                mysock.Send(buff);
            }
            catch (Exception e)
            {
                Log("SendData : " + e.Message, true);
            }
        }

        // # General Method
        void Log(string text, bool isError = false)
        {
            Console.WriteLine(NAME + " > " + text);

            LOG.Write(NAME, text, isError);
        }
    }
}
