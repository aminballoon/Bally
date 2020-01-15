using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Simple_Client_A
{
    class MyYumiModule : MyPX410.MyModule
    {
        // # Delegate
        public delegate void delPassData(string _text);
        public event myDelegateVoid eventOnUpdate;
        #region module var

        public string COLOR = "...";
        public string POSITION = "...";

        #endregion

        #region core var

        public bool CORE_ISWORKING;
        public Thread CORE_THREAD;

        #endregion

        public MyYumiModule()
        {

            NAME = "YUMI";

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

                if (ADDR == "SERVER")   // SERVER INIT
                {
                    Send(NAME, "REG");
                    IS_REGISTER = true;
                }
                else
                {

                    if (MyPX410.MyFunction.MyDecodeDATA(DATA, "COLOR", out VALUE))
                    {
                        COLOR = VALUE;

                        eventOnUpdate();
                    }

                    if (MyPX410.MyFunction.MyDecodeDATA(DATA, "POSITION", out VALUE))
                    {
                        POSITION = VALUE;

                        eventOnUpdate();
                    }

                }

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
