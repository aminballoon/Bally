using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using System.Net.Sockets;

namespace MyPX410
{

    #region DEFINE
    namespace DEFINE
    {

        namespace COM
        {

            namespace PXDATA
            {
                public static class SYSTEM
                {
                    public const string PROPERTY = "SYSTEM";
                }

                public enum VALUE
                {
                    PING
                }

            }

            namespace ACTION
            {

                public static class STATE
                {
                    public const string PROPERTY = "STATE";

                    public enum VALUE
                    {
                        NONE,
                        BUSY,
                        READY,
                        FREE,
                        FREEZE
                    }

                }
            }

        }

        public static class NAME
        {
            public const string HEAD = "HEAD";
            public const string ARM = "ARM";
            public const string WHEEL = "WHEEL";
            public const string MULTIMEDIA = "MULTIMEDIA";
            public const string REMOTE = "REMOTE";
            public const string EYE = "EYE";
            public const string COMMUNICATION = "COMMUNICATION";
            public const string LIGHT = "LIGHT";
        }

        namespace MAIN
        {

            namespace ACTION
            {

                public static class STATE
                {
                    public const string PROPERTY = "STATE";

                    public enum VALUE
                    {
                        NONE,
                        BUSY,
                        READY,
                        FREE,
                        FREEZE
                    }
                }

                public static class TEMPERATURE
                {

                    public const string PROPERTY = "TEMPERATURE";

                }

                public static class VOLTAGE
                {

                    public const string PROPERTY = "VOLTAGE";

                }

            }

            // # CLASS PACKAGE FOR COMMANDER-02
            public class VAR
            {
                public MyPX410.DEFINE.COM.ACTION.STATE.VALUE STATE;
                public float HIGHEST_TEMPERATURE, LOWEST_VOLTAGE;

                public List<ModulePackageBase> LIST_MODULE;

            }

            // # CLASS PACKAGE FOR ALL MODULE
            public class ModulePackageBase
            {
                public string NAME
                {
                    get;
                    set;
                }

                public string DATA
                {
                    get;
                    set;
                }

                public string PATH
                {
                    get;
                    set;
                }

                public string WORKING_DIRECTORY
                {
                    get;
                    set;
                }

                public string DecodeForValue(string data)
                {
                    foreach (string sp1 in this.DATA.Split(' '))
                    {
                        string[] sp2 = sp1.Split('=');

                        if (sp2[0] == data)
                        {
                            return sp2[1];
                        }
                    }

                    return "";
                }
            }

        }

        namespace ARM
        {

            namespace ACTION
            {

                public static class STATE
                {
                    public const string PROPERTY = "STATE";

                    public enum VALUE
                    {
                        NONE,
                        BUSY,
                        READY,
                        FREE,
                        FREEZE
                    }
                }

                public static class POSE
                {

                    public const string PROPERTY = "POSE";

                    public enum VALUE
                    {
                        NONE,
                        TEST_1,
                        TEST_2,
                        STANDBY,
                        SAWASDEE1,
                        SAWASDEE2,
                        BOKMHER_RIGHT_1,
                        BOKMHER_RIGHT_2,
                        BOKMHER_LEFT_1,
                        BOKMHER_LEFT_2,
                        JUBMHER_RIGHT,
                        JUBMHER_LEFT,
                        BOKMHER_GUM_BAE_RIGHT_1,
                        BOKMHER_GUM_BAE_RIGHT_2,
                        BOKMHER_GUM_BAE_LEFT_1,
                        BOKMHER_GUM_BAE_LEFT_2,
                        KAO_MA_LOEI_1,
                    }

                }

                public static class TEMPERATURE
                {

                    public const string PROPERTY = "TEMPERATURE";

                }

                public static class VOLTAGE
                {

                    public const string PROPERTY = "VOLTAGE";

                }

            }

            // # CLASS PACKAGE FOR MODULE
            public class VAR
            {
                public MyPX410.DEFINE.COM.ACTION.STATE.VALUE STATE;
                public float HIGH_TEMPERATURE, LOW_VOLTAGE, HIGH_TEMPERATURE_ID, LOW_VOLTAGE_ID;
                public MyPX410.DEFINE.ARM.ACTION.POSE.VALUE POSE;
            }
        }

        namespace MULTIMEDIA
        {

            namespace ACTION
            {

                public static class STATE
                {
                    public const string PROPERTY = "STATE";

                    public enum VALUE
                    {
                        NONE,
                        BUSY,
                        READY,
                        FREE,
                        FREEZE
                    }
                }

                public static class AUDIO
                {

                    public const string PROPERTY = "AUDIO";

                }

                public static class LIGHT
                {

                    public const string PROPERTY = "LIGHT";

                    public enum VALUE
                    {
                        PATT1,
                        PATT2,
                        PATT3
                    }

                }

            }

            // # CLASS PACKAGE FOR MODULE
            public class VAR
            {
                public MyPX410.DEFINE.COM.ACTION.STATE.VALUE STATE;
                public string AUDIO, LIGHT;
            }

            public class MyAudio
            {

                string _ID;

                public string ID
                {
                    get
                    {
                        return _ID;
                    }

                    set
                    {
                        _ID = value;
                    }
                }

                string _PATH;

                public string PATH
                {
                    get
                    {
                        return _PATH;
                    }

                    set
                    {
                        _PATH = value;
                    }
                }

                public MyAudio(string id, string path)
                {
                    ID = id;
                    PATH = path;
                }

            }

        }

        namespace WHEEL
        {

            namespace ACTION
            {

                public static class STATE
                {
                    public const string PROPERTY = "STATE";

                    public enum VALUE
                    {
                        NONE,
                        BUSY,
                        READY,
                        FREE,
                        FREEZE
                    }
                }

                public static class SPEED_LEFT
                {
                    /*
					 * SPEED = <-100 to 100>
					 */
                    public const string PROPERTY = "SPEED_LEFT";

                }

                public static class SPEED_RIGHT
                {
                    /*
					 * SPEED = <-100 to 100>
					 */
                    public const string PROPERTY = "SPEED_RIGHT";

                }

                public static class MODE
                {
                    /*
                     * D = drive
                     * N = null, free
                     */
                    public const string PROPERTY = "MODE";

                    public enum VALUE
                    {
                        D,
                        N
                    }

                }

                public static class TEMPERATURE
                {

                    public const string PROPERTY = "TEMPERATURE";

                }

                public static class VOLTAGE
                {

                    public const string PROPERTY = "VOLTAGE";

                }

            }

            // # CLASS PACKAGE FOR MODULE
            public class VAR
            {
                public MyPX410.DEFINE.COM.ACTION.STATE.VALUE STATE;
                public float HIGH_TEMPERATURE, LOW_VOLTAGE, HIGH_TEMPERATURE_ID, LOW_VOLTAGE_ID;
                public int SPEED_LEFT, SPEED_RIGHT;
                public string MODE;
            }
        }

        namespace HEAD
        {

            namespace ACTION
            {

                public static class STATE
                {
                    public const string PROPERTY = "STATE";

                    public enum VALUE
                    {
                        NONE,
                        BUSY,
                        READY,
                        FREE,
                        FREEZE
                    }
                }

                public static class MONITOR
                {
                    /*
					 * LEFT = -1
					 * CENTER = 0
					 * RIGHT = 1
					 * Otherwise = STOP
					 */
                    public const string PROPERTY = "MONITOR";

                    public enum VALUE
                    {
                        STOP,
                        DOWN,
                        UP
                    }

                }

                public static class POSITION_X
                {

                    public const string PROPERTY = "POSITION_X";

                    public enum VALUE
                    {
                        ZERO = 0
                    }

                }

                public static class TEMPERATURE
                {

                    public const string PROPERTY = "TEMPERATURE";

                }

                public static class VOLTAGE
                {

                    public const string PROPERTY = "VOLTAGE";

                }

            }

            // # CLASS PACKAGE FOR MODULE
            public class VAR
            {
                public MyPX410.DEFINE.COM.ACTION.STATE.VALUE STATE;
                public float HIGH_TEMPERATURE, LOW_VOLTAGE, HIGH_TEMPERATURE_ID, LOW_VOLTAGE_ID;
                public int POSITION_X;
                public string MONITOR;
            }
        }

        namespace REMOTE
        {

            namespace ACTION
            {

                public static class STATE
                {
                    public const string PROPERTY = "STATE";

                    public enum VALUE
                    {
                        NONE,
                        BUSY,
                        READY,
                        FREE,
                        FREEZE
                    }
                }

            }

            // # CLASS PACKAGE FOR MODULE
            public class VAR
            {
                public MyPX410.DEFINE.COM.ACTION.STATE.VALUE STATE;
            }

        }

        namespace EYE
        {

            namespace ACTION
            {

                public static class STATE
                {
                    public const string PROPERTY = "STATE";

                    enum VALUE
                    {
                        NONE,
                        BUSY,
                        READY,
                        FREE,
                        FREEZE
                    };
                };

                public static class DETECTED
                {

                    public const string PROPERTY = "DETECTED";

                    public enum VALUE
                    {
                        NO = 0,
                        YES = 1
                    };

                };

                public static class POSITION
                {

                    public const string PROPERTY = "POSITION";

                };

                public static class MODE
                {

                    public const string PROPERTY = "MODE";

                    public enum VALUE
                    {
                        AUTOMATIC,
                        MANUAL
                    };

                };

            }

            // # CLASS PACKAGE FOR MODULE
            public class VAR
            {
                public MyPX410.DEFINE.COM.ACTION.STATE.VALUE STATE;
                public MyPX410.DEFINE.EYE.ACTION.DETECTED.VALUE DETECTED;
                public string POSITION;
                public ACTION.MODE.VALUE MODE;
            };
        }

        namespace COMMUNICATION
        {

            namespace ACTION
            {

                public static class STATE
                {
                    public const string PROPERTY = "STATE";

                    enum VALUE
                    {
                        NONE,
                        BUSY,
                        READY,
                        FREE,
                        FREEZE
                    };
                };

                public static class MODE
                {

                    public const string PROPERTY = "MODE";

                };

            }

            // # CLASS PACKAGE FOR MODULE
            public class VAR
            {
                public MyPX410.DEFINE.COM.ACTION.STATE.VALUE STATE;
                public string ORDER, DATA;
            };
        }

    }
    #endregion

    #region MODULE
    public class MyModule
    {
        /*
		 * # Protocol
		 *		[ADDR:DATA]0...[ADDR:DATA]n
		 * # ADDR
		 *		module name
		 * # DATA
		 *		PROPERTY0=VALUE0...PROPERTYn=VALUEn
		 */

        #region Event
        public delegate void myDelegateReceive(IAsyncResult ar, string data);
        public delegate void myDelegateMatch(string ADDR, string DATA);
        public delegate void myDelegateError(string title, string msg);
        public delegate void myDelegateVoid();
        public delegate void myDelegateBool(bool st);
        public delegate void myDelegateMessage(string msg);

        public event myDelegateBool eventOnConnected;
        public event myDelegateVoid eventOnDisconnected;
        public event myDelegateMatch eventOnMatch;
        public event myDelegateError eventOnError;
        //public event	myDelegateMessage	eventOnMessage;
        //public event	myDelegateVoid		eventOnShutdown;
        #endregion

        #region variables
        public const int TCP_BUFFER_SIZE = 1024;    // 1 KB
        protected Socket mysock = null;
        protected byte[] tcp_buffer = null;
        protected string receive_buffer = null;
        public string NAME = "NEW MODULE";

        protected string BUFF_MODULE_DATA,
                            BUFF_MODULE_DATA_PREV;
        public bool IS_REGISTER = false;
        #endregion

        public MyModule()
        {
            Debug.Write("# MyPX410 Module Initialize ...");

            tcp_buffer = new byte[TCP_BUFFER_SIZE];

            Debug.WriteLine("OK");
        }

        #region General method

        public void ConnectToServer(string host = "localhost", int port = 1150)
        {
            try
            {
                Debug.WriteLine("# Connecting to " + host + ":" + port);

                mysock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                mysock.Blocking = false;

                AsyncCallback ptr = new AsyncCallback(OnConnectToServer);
                mysock.BeginConnect(host, port, ptr, mysock);
            }
            catch (Exception e)
            {
                OnError("ConnectToServer", e);
            }
        }

        public void DisconnectFromServer()
        {
            try
            {
                if (mysock != null && mysock.Connected)
                {
                    Debug.WriteLine("# Disconnected");
                    Debug.WriteLine("# " + mysock.RemoteEndPoint + " shutting down sending and receiving");
                    mysock.Shutdown(SocketShutdown.Both);
                    Debug.WriteLine("# " + mysock.RemoteEndPoint + " closing connection");
                    mysock.Close();
                    OnDisconnectFromServer();
                }
            }
            catch (Exception e)
            {
                OnError("DisconnectFromServer", e);
            }
        }

        public void Send(string raw_data)
        {
            try
            {
                if (!mysock.Connected)
                    return;

                byte[] buff = System.Text.Encoding.UTF8.GetBytes(raw_data);

                Debug.WriteLine("# Send " + mysock.RemoteEndPoint + " <- " + raw_data);

                mysock.Send(buff);
            }
            catch (Exception e)
            {
                OnError("SendToServer", e);
            }
        }

        public void Send(string addr, string data)
        {
            data = MyPX410.MyFunction.MyEncodePX4(addr, data);
            Send(data);
        }

        protected void SetupReceive(Socket m_sock)
        {
            try
            {
                Array.Clear(tcp_buffer, 0, tcp_buffer.Length);
                AsyncCallback ptr = new AsyncCallback(OnReceiveFromServer);
                m_sock.BeginReceive(tcp_buffer, 0, tcp_buffer.Length, SocketFlags.None, ptr, m_sock);
            }
            catch (Exception e)
            {
                OnError("SetupReceive", e);
            }
        }

        protected void OnError(string title, string msg)
        {
            eventOnError(title, msg);
        }

        protected void OnError(string title, Exception e)
        {
            OnError(title, e.Message);
        }

        #endregion

        #region Event method
        void OnConnectToServer(IAsyncResult ar)
        {
            try
            {
                Debug.Write("# OnConnectToServer ...");

                Socket m_sock = (Socket)ar.AsyncState;

                Debug.WriteLine((m_sock.Connected ? "OK" : "FAIL"));

                if (m_sock.Connected)
                {
                    //Debug.WriteLine(m_sock.LocalEndPoint + " -> " + m_sock.RemoteEndPoint);

                    SetupReceive(m_sock);
                }
                eventOnConnected(m_sock.Connected);
            }
            catch (Exception e)
            {
                OnError("OnConnectToServer", e);
            }
        }

        void OnDisconnectFromServer()
        {
            Debug.WriteLine("# OnDisconnectFromServer");
            eventOnDisconnected();
        }

        void OnReceiveFromServer(IAsyncResult ar)
        {
            try
            {
                Debug.WriteLine("# OnReceiveFromServer");

                Socket m_sock = (Socket)ar.AsyncState;
                int nByte = m_sock.EndReceive(ar);

                if (nByte > 0)
                {
                    //Debug.WriteLine("# Receive from " + m_sock.RemoteEndPoint);

                    string data = System.Text.Encoding.UTF8.GetString(tcp_buffer, 0, nByte);
                    string ADDR, DATA;

                    Debug.WriteLine(data);

                    // BEGIN DECODING
                    receive_buffer += data;
                    int i_start;

                    while (receive_buffer.Length >= 7)  // [1:2=3]
                    {
                        i_start = receive_buffer.IndexOf('[');
                        if (i_start >= 0)
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

                            eventOnMatch(ADDR, DATA);
                        }
                        else
                        {
                            Debug.WriteLine("# !Mismatch " + receive_buffer);
                            break;
                        }
                    }
                    // END DECODING

                    SetupReceive(m_sock);
                }
                else
                {
                    if (mysock != null && mysock.Connected)
                    {
                        Debug.WriteLine("# shutting down sending and receiving");
                        mysock.Shutdown(SocketShutdown.Both);
                        Debug.WriteLine("# closing connection");
                        mysock.Close();

                        OnDisconnectFromServer();
                    }
                }
            }
            catch (Exception e)
            {
                if (mysock != null && mysock.Connected)
                {
                    Debug.WriteLine("# force shutting down sending and receiving");
                    mysock.Shutdown(SocketShutdown.Both);
                    Debug.WriteLine("# force closing connection");
                    mysock.Close();

                    OnDisconnectFromServer();
                }
                else if (e.Message == "An existing connection was forcibly closed by the remote host")
                {
                    OnDisconnectFromServer();
                }

            }
        }

        #endregion

    }
    #endregion

    #region EXT CLASS

    public class MyFunction
    {
        public static bool FindSerialPort(string name, out string port_result)
        {
            port_result = "";

            try
            {
                System.Management.ManagementObjectSearcher searcher = new System.Management.ManagementObjectSearcher("root\\WMI", "SELECT * FROM MSSerial_PortName");
                foreach (var ptr in searcher.Get())
                {
                    Debug.WriteLine(ptr);
                    Debug.WriteLine("\t" + ptr["InstanceName"] + " : " + ptr["PortName"]);

                    if (ptr["InstanceName"].ToString().Contains(name))
                    {
                        Debug.WriteLine("MATCH : " + ptr["InstanceName"] + " => " + ptr["PortName"]);
                        port_result = ptr["PortName"].ToString();

                        return true;
                    }
                }

                port_result = "Now found " + name;
                return false;
            }
            catch (Exception e)
            {
                port_result = e.Message;
                return false;
            }
        }

        public static void MyConvertPolar2Catesian(int radius, double degree, out int x, out int y)
        {
            double rad = degree * Math.PI / 180.0;
            x = (int)(radius * Math.Cos(rad));
            y = (int)(radius * Math.Sin(rad));
        }

        public static string MyEncodePX4(string ADDR, string DATA)
        {
            return "[" + ADDR + ":" + DATA + "]";
        }

        public static int MyDecodePX4(string SRC, out string ADDR, out string DATA)
        {
            int res = -1;
            int i_addr, i_data, i_end;

            i_addr = SRC.IndexOf('[');
            i_data = SRC.IndexOf(':', i_addr + 1);
            i_end = SRC.IndexOf(']', i_data + 1);

            if (i_addr != -1 && i_data != -1 && i_end != -1)
            {
                ADDR = SRC.Substring(i_addr + 1, i_data - i_addr - 1);
                DATA = SRC.Substring(i_data + 1, i_end - i_data - 1);
                res = ADDR.Length + DATA.Length + 3;
            }
            else
            {
                ADDR = "";
                DATA = "";
            }

            return res;
        }

        public static bool MyDecodeDATA(string SRC, string PROPERTY, out string VALUE)
        {
            string[] CMD = SRC.Split(' ');
            VALUE = "";
            foreach (var item in CMD)
            {
                string[] CMD2 = item.Split('=');
                if (PROPERTY == CMD2[0])
                {
                    VALUE = CMD2[1];
                    return true;
                }
            }

            return false;
        }

        public static MyPX410.DEFINE.COM.ACTION.STATE.VALUE MyConvertStringToState(string STATE)
        {
            if (STATE == MyPX410.DEFINE.COM.ACTION.STATE.VALUE.READY.ToString())
            {
                return MyPX410.DEFINE.COM.ACTION.STATE.VALUE.READY;
            }
            else if (STATE == MyPX410.DEFINE.COM.ACTION.STATE.VALUE.BUSY.ToString())
            {
                return MyPX410.DEFINE.COM.ACTION.STATE.VALUE.BUSY;
            }
            else if (STATE == MyPX410.DEFINE.COM.ACTION.STATE.VALUE.FREE.ToString())
            {
                return MyPX410.DEFINE.COM.ACTION.STATE.VALUE.FREE;
            }
            else if (STATE == MyPX410.DEFINE.COM.ACTION.STATE.VALUE.FREEZE.ToString())
            {
                return MyPX410.DEFINE.COM.ACTION.STATE.VALUE.FREEZE;
            }

            return MyPX410.DEFINE.COM.ACTION.STATE.VALUE.NONE;
        }
    }

    public class MyOnMessageThread
    {
        MyModule.myDelegateMessage ptr;
        System.Threading.Thread thread;
        string msg;

        public MyOnMessageThread(MyModule.myDelegateMessage ptr, string msg)
        {
            this.msg = msg;
            this.ptr = ptr;

            thread = new System.Threading.Thread(new System.Threading.ThreadStart(core));
            thread.Name = "MyOnMessageThread";
            thread.Start();
        }

        void core()
        {
            ptr(msg);
        }
    }

    public class MyOnErrorThread
    {
        MyModule.myDelegateError ptr;
        System.Threading.Thread thread;
        string title, msg;

        public MyOnErrorThread(MyModule.myDelegateError ptr, string title, string msg)
        {
            this.title = title;
            this.msg = msg;
            this.ptr = ptr;

            thread = new System.Threading.Thread(new System.Threading.ThreadStart(core));
            thread.Name = "myDelegateError";
            thread.Start();
        }

        void core()
        {
            ptr(title, msg);
        }
    }

    #endregion

    #region LOG
    public class LOG
    {
        //
        const string DIRECTORY_NAME = "MODULE.LOG";

        static System.Threading.Mutex mutex_file = new System.Threading.Mutex(false);

        // LOG
        public static void Write(string ModuleName, string text, bool isError = false)
        {
            try
            {
                string path = Environment.CurrentDirectory + @"\" + DIRECTORY_NAME + @"\" + DateTime.Now.ToString("yyyy-MM-dd") + @"\" + ModuleName;
                string buff;

                if (mutex_file.WaitOne())
                {
                    System.IO.Directory.CreateDirectory(path);

                    using (var sw = new System.IO.StreamWriter(path + @"\log.txt", true))
                    {
                        buff = TimeStamp() + " > " + text;
                        buff = buff.Replace("\n", Environment.NewLine);

                        //Debug.WriteLine(buff);
                        sw.WriteLine(buff);
                    }

                    mutex_file.ReleaseMutex();
                }
                else
                {
                    Debug.WriteLine("! Thread " + System.Threading.Thread.CurrentThread.ManagedThreadId + " Mutex can't enter file process");
                }

                if (!isError)
                {
                    return;
                }

                System.Media.SystemSounds.Asterisk.Play();

                if (mutex_file.WaitOne())
                {
                    using (var sw = new System.IO.StreamWriter(path + @"\log.error.txt"))
                    {
                        buff = TimeStamp() + " > " + text;

                        //Debug.WriteLine("!!! " + buff);
                        sw.WriteLine(buff);
                    }
                }
                else
                {
                    Debug.WriteLine("! Thread " + System.Threading.Thread.CurrentThread.ManagedThreadId + " Mutex can't enter file process");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("! " + e.Message);

                mutex_file.ReleaseMutex();

                Write(ModuleName, text, isError);
            }
        }

        //
        static string TimeStamp()
        {
            return DateTime.Now.ToString();
        }
    }

    public class ThreadWriteLog
    {
        string DATA;

        public ThreadWriteLog(string data)
        {
            DATA = data;
        }

        void Core()
        {
            try
            {

            }
            catch (Exception e)
            {
                ;
            }
        }
    }
    #endregion

}
