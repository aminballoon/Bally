using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using System.Threading;

namespace Simple_Client_A
{
    class MyModule : MyPX410.MyModule
    {

        // # Delegate

        public event myDelegateVoid eventOnUpdate;

        #region module var
        Stopwatch watch;
        public string X1 = "";
        public string X2 = "";
        public string Y1 = "";
        public string Y2 = "";
        public string Z1 = "";
        public string Z2 = "";
        public string TH = "";
        public string COM = "";
        public string SEL = "";


        public string COLOR = "...";
        public string POSITION = "...";

        #endregion

        #region core var

        public bool CORE_ISWORKING;
        public Thread CORE_THREAD;

        #endregion

        public MyModule()
        {
            watch = new Stopwatch();
            NAME = "CLIENT_A";

            #region Initialize

            Log("Initialize");

            #endregion

            #region Event

            eventOnMatch += MyModule_eventOnMatch;

            #endregion

            #region Start

            MyCoreStart();

            #endregion

        }

        #region Event

        void MyModule_eventOnMatch(string ADDR, string DATA)
        {

            try
            {

                #region Decode

                string VALUE;
                //Debug.WriteLine("Match >" + ADDR + "->" + DATA);

                watch.Restart();

                if (ADDR == "SERVER")   // SERVER INIT
                {
                    Send(NAME, "REG");
                    IS_REGISTER = true;
                }
                else
                {
                    if (MyPX410.MyFunction.MyDecodeDATA(DATA, "COM", out VALUE))
                    {
                        COM = VALUE;

                        eventOnUpdate();
                    }
                    if (MyPX410.MyFunction.MyDecodeDATA(DATA, "SEL", out VALUE))
                    {
                        SEL = VALUE;

                        eventOnUpdate();
                    }
                    if (MyPX410.MyFunction.MyDecodeDATA(DATA, "X1", out VALUE))
                    {
                        X1 = VALUE;

                        eventOnUpdate();
                    }
                    if (MyPX410.MyFunction.MyDecodeDATA(DATA, "X2", out VALUE))
                    {
                        X2 = VALUE;

                        eventOnUpdate();
                    }
                    if (MyPX410.MyFunction.MyDecodeDATA(DATA, "Y1", out VALUE))
                    {
                        Y1 = VALUE;

                        eventOnUpdate();
                    }
                    if (MyPX410.MyFunction.MyDecodeDATA(DATA, "Y2", out VALUE))
                    {
                        Y2 = VALUE;

                        eventOnUpdate();
                    }
                    if (MyPX410.MyFunction.MyDecodeDATA(DATA, "Z1", out VALUE))
                    {
                        Z1 = VALUE;

                        eventOnUpdate();
                    }
                    if (MyPX410.MyFunction.MyDecodeDATA(DATA, "Z2", out VALUE))
                    {
                        Z2 = VALUE;

                        eventOnUpdate();
                    }
                    if (MyPX410.MyFunction.MyDecodeDATA(DATA, "TH", out VALUE))
                    {
                        TH = VALUE;

                        eventOnUpdate();
                    }
                    if (MyPX410.MyFunction.MyDecodeDATA(DATA, "POSITION", out VALUE))
                    {
                        POSITION = VALUE;

                        eventOnUpdate();
                    }

                }

                //Debug.WriteLine("latency=" + watch.ElapsedMilliseconds + "ms");

                #endregion

            }
            catch (Exception e)
            {
                Log("MyModule_eventOnMatch : " + e.Message, true);
            }

        }

        #endregion

        #region Helper

        #region Core

        public void MyCoreStart()
        {
            Log("START CORE");

            CORE_ISWORKING = false;

            CORE_THREAD = new Thread(new ThreadStart(MyCore));
            CORE_THREAD.Name = "MY CORE";
            CORE_THREAD.Start();
        }

        public void MyCoreStop()
        {
            Log("STOP CORE");

            CORE_ISWORKING = false;
        }

        void MyCore()
        {

            // BEGIN CORE
            try
            {
                Log("[Core Multimedia BEGIN]");

                //string port = "NONE";

                #region Initialize USB
                //Log("Find serial port : A1017WGBA");
                //if(MyPX410.MyFunction.FindSerialPort("A1017WGBA", out port))
                //{
                //	Log("Opening port : " + port);
                //	Serial_Light = new System.IO.Ports.SerialPort(port, 115200);
                //	Serial_Light.Open();

                //	if(Serial_Light.IsOpen == false)
                //	{
                //		Log("Open port " + port + " [FAIL]");
                //		OnError("MyCore", "Open port " + port + " [FAIL]");

                //		return;
                //	}
                //}
                //else
                //{
                //	Log("Not found USB!");
                //	OnError("MyCore", "Not found USB!");

                //	return;
                //}
                #endregion

                #region Core

                Log("Core LOOP");
                CORE_ISWORKING = true;
                int n = 0;
                while (CORE_ISWORKING == true)
                {

                    //Log("Thread loop " + (n++));
                    Thread.Sleep(1000);

                }

                #endregion

            }
            catch (Exception e)
            {
                OnError("MyCore", e.Message);
            }
            finally
            {
                Log("[Core Multimedia END]");
            }
            // END CORE

        }

        #endregion
        
        #region Etc

        // # General Method
        public void Log(string text, bool isError = false)
        {
            Console.WriteLine(NAME + " > " + text);

            MyPX410.LOG.Write(NAME, text, isError);
        }
        
        #endregion

        #endregion

    }
}
