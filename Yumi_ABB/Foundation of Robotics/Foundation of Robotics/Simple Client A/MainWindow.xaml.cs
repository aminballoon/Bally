using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

using System.Diagnostics;

namespace Simple_Client_A
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        MyModule module;
        MyYumiModule yumiL;
        MyYumiModule yumiR;
        Yumiko mywin = new Yumiko();
        Thread recivePassingValue;
        string _NAME_ = "YUMIKO";
        double offsetX = 390;
        double offsetZ = 215;
        bool isClosing = false;
        bool isClickStateYumiR = false;
        bool isClickStateYumiL = false;
        bool L = true;
        bool R = false;
        public MainWindow()
        {
            InitializeComponent();
            #region Initialize

            MyPX410.LOG.Write(_NAME_, "=== [START] ===");

            module = new MyModule();
            yumiL = new MyYumiModule();
            yumiR = new MyYumiModule();
            yumiL.NAME = "Yumi_L";
            yumiR.NAME = "Yumi_R";
            recivePassingValue = new Thread(new ThreadStart(_recivePassingValue));
            #endregion
            #region Event

            module.eventOnConnected += module_eventOnConnected;
            module.eventOnDisconnected += module_eventOnDisconnected;
            module.eventOnError += module_eventOnError;
            module.eventOnMatch += Module_eventOnMatch;
            module.eventOnUpdate += Module_eventOnUpdate;

            yumiR.eventOnConnected += YumiR_eventOnConnected;
            yumiR.eventOnDisconnected += YumiR_eventOnDisconnected;
            yumiR.eventOnError += YumiR_eventOnError;
            yumiR.eventOnMatch += YumiR_eventOnMatch;
            yumiR.eventOnUpdate += YumiR_eventOnUpdate;

            yumiL.eventOnConnected += YumiL_eventOnConnected;
            yumiL.eventOnDisconnected += YumiL_eventOnDisconnected;
            yumiL.eventOnError += YumiL_eventOnError;
            yumiL.eventOnMatch += YumiL_eventOnMatch;
            yumiL.eventOnUpdate += YumiL_eventOnUpdate;

            
            #endregion
            #region Start
            
            #endregion

            #region Yumiko
            mywin.Show();
            mywin.sendDataToMainWinL += Mywin_sendDataToMainWinL;
            mywin.sendDataToMainWinR += Mywin_sendDataToMainWinR;
            mywin.sendDataToMainWinS += Mywin_sendDataToMainWinS1;
            module.TH = 0.ToString();
            #endregion
        }

        private void Mywin_sendDataToMainWinS1(string msg)
        {
            module.Send(msg);
        }
        private void Mywin_sendDataToMainWinR(string msg)
        {
            yumiR.Send(msg);
        }
        private void Mywin_sendDataToMainWinL(string msg)
        {
            yumiL.Send(msg);
        }
        #region Event
        private void YumiR_eventOnConnected(bool st)
        {
            if (st)
            {
                yumiR.Log("Connected");
            }
            else
            {
                yumiR.Log("Connection fail");
            }
        }
        private void YumiR_eventOnDisconnected()
        {
            yumiR.IS_REGISTER = false;
            yumiR.Log("Disconnected");
            //isClosing = true;
            //MyShutdown();
        }
        private void YumiR_eventOnError(string title, string msg)
        {
            try
            {
                yumiR.Log(title + " : " + msg, true);

                if (msg == "An existing connection was forcibly closed by the remote host")
                {
                    MyShutdown();
                }
                else
                {
                    if (!isClosing)
                    {
                        MessageBox.Show(msg, title, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
        private void YumiR_eventOnMatch(string ADDR, string DATA)
        {
            Dispatcher.Invoke(delegate ()
            {
                disp_receive.Text += "[" + ADDR + ":" + DATA + "]" + Environment.NewLine;
                disp_receive.ScrollToEnd();
                string[] splitData = DATA.Split(' ');
                if (splitData[0] == "3")
                {
                    mywin.updataPos(R, splitData[1], splitData[2], splitData[3],
                        splitData[4], splitData[5], splitData[6], splitData[7]);
                }
                if (splitData[0] == "1")
                {
                    mywin.updataPos(R, splitData[1], splitData[2], splitData[3],
                        splitData[4], splitData[5], splitData[6], splitData[7]);
                }
                else if (splitData[0] == "4")
                {
                    mywin.updataJoint(R, splitData[1], splitData[2], splitData[3],
                        splitData[4], splitData[5], splitData[6], splitData[7]);
                }
                
            });
            
            
        }
        private void YumiR_eventOnUpdate()
        {
            Dispatcher.Invoke(delegate ()
            {
                disp_color.Content = "COLOR = " + module.COLOR;
                disp_position.Content = "POSITION = " + module.POSITION;
            });
        }
        private void YumiL_eventOnUpdate()
        {
            Dispatcher.Invoke(delegate ()
            {
                disp_color.Content = "COLOR = " + module.COLOR;
                disp_position.Content = "POSITION = " + module.POSITION;
            });
        }
        private void YumiL_eventOnMatch(string ADDR, string DATA)
        {
            Dispatcher.Invoke(delegate ()
            {
                disp_receive.Text += "[" + ADDR + ":" + DATA + "]" + Environment.NewLine;
                disp_receive.ScrollToEnd();
                string[] splitData = DATA.Split(' ');
                if (splitData[0] == "3")
                {
                    mywin.updataPos(L, splitData[1], splitData[2], splitData[3],
                        splitData[4], splitData[5], splitData[6], splitData[7]);
                }
                if (splitData[0] == "1")
                {
                    mywin.updataPos(L, splitData[1], splitData[2], splitData[3],
                        splitData[4], splitData[5], splitData[6], splitData[7]);
                }
                else if (splitData[0] == "4")
                {
                    mywin.updataJoint(L, splitData[1], splitData[2], splitData[3],
                        splitData[4], splitData[5], splitData[6], splitData[7]);
                }
            });
        }
        private void YumiL_eventOnError(string title, string msg)
        {
            try
            {
                yumiL.Log(title + " : " + msg, true);

                if (msg == "An existing connection was forcibly closed by the remote host")
                {
                    MyShutdown();
                }
                else
                {
                    if (!isClosing)
                    {
                        MessageBox.Show(msg, title, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
        private void YumiL_eventOnDisconnected()
        {
            yumiL.IS_REGISTER = false;
            yumiL.Log("Disconnected");
            //isClosing = true;
            //MyShutdown();
        }
        private void YumiL_eventOnConnected(bool st)
        {
            if (st)
            {
                yumiL.Log("Connected");
            }
            else
            {
                yumiL.Log("Connection fail");
            }
        }
        private void MyClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            module.Log("=== [CLOSING] ===");
            isClosing = true;
            module.MyCoreStop();
            //MyShutdown();
        }

        private void Module_eventOnUpdate()
        {
            Dispatcher.Invoke(delegate ()
            {
                disp_color.Content = "COLOR = " + module.COLOR;
                disp_position.Content = "POSITION = " + module.POSITION;
            });
        }
        private void Module_eventOnMatch(string ADDR, string DATA)
        {
            Dispatcher.Invoke(delegate ()
            {
                disp_receive.Text += "[" + ADDR + ":" + DATA + "]" + Environment.NewLine;
                disp_receive.ScrollToEnd();
                //string[] splitData = DATA.Split(' ');
                //if (splitData[0] == "5")
                //{
                //    mywin.goAtoB(R, Convert.ToDouble(splitData[1]), Convert.ToDouble(splitData[2]), Convert.ToDouble(splitData[3]), Convert.ToDouble(splitData[4]), Convert.ToDouble(splitData[5]), Convert.ToDouble(splitData[6]));
                //    //mywin.PickAtoB(R,);
                //}
                if (module.COM == "AtoB")
                {
                    if (module.SEL == "L")
                    {
                        mywin.goAtoB(L, Convert.ToDouble(module.X1) + offsetX, Convert.ToDouble(module.Y1), Convert.ToDouble(module.Z1) + offsetZ,
                            Convert.ToDouble(module.X2) + offsetX, Convert.ToDouble(module.Y2), Convert.ToDouble(module.Z2) + offsetZ,Convert.ToDouble(module.TH));
                    }
                    else if (module.SEL == "R")
                    {
                        mywin.goAtoB(R, Convert.ToDouble(module.X1) + offsetX, Convert.ToDouble(module.Y1), Convert.ToDouble(module.Z1) + offsetZ,
                            Convert.ToDouble(module.X2) + offsetX, Convert.ToDouble(module.Y2), Convert.ToDouble(module.Z2) + offsetZ, Convert.ToDouble(module.TH));
                    }
                    module.COM = "-";
                    //mywin.goAtoB(R, Convert.ToDouble(splitData[1]), Convert.ToDouble(splitData[2]), Convert.ToDouble(splitData[3]), Convert.ToDouble(splitData[4]), Convert.ToDouble(splitData[5]), Convert.ToDouble(splitData[6]));
                }
            });
            
        }
        void module_eventOnConnected(bool st)
        {
            if (st)
            {
                module.Log("Connected");
            }
            else
            {
                module.Log("Connection fail");
            }
        }
        void module_eventOnDisconnected()
        {
            module.IS_REGISTER = false;
            module.Log("Disconnected");
            //isClosing = true;
            //MyShutdown();
        }
        void module_eventOnError(string title, string msg)
        {
            try
            {
                module.Log(title + " : " + msg, true);

                if (msg == "An existing connection was forcibly closed by the remote host")
                {
                    MyShutdown();
                }
                else
                {
                    if (!isClosing)
                    {
                        MessageBox.Show(msg, title, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
        private void MyEvent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender == bt_connect)
                {
                    _NAME_ = in_name.Text.Trim();
                    module.NAME = _NAME_;
                    module.ConnectToServer("127.0.0.1",1150);

                }
                else if (sender == bt_disconnect)
                {

                    module.DisconnectFromServer();

                }
                else if (sender == bt_send)
                {
                    string data = in_send.Text.Trim();
                    module.Send(data);
                }
                else if (sender == btCon_LArm)
                {
                    if (isClickStateYumiL == false)
                    {
                        yumiL.ConnectToServer("127.0.0.1",5000);
                        //yumiL.ConnectToServer("192.168.125.1", 4000);
                        isClickStateYumiL = true;
                        btCon_LArm.Content = "DisLeftArm";
                    }
                    else
                    {
                        yumiL.DisconnectFromServer();
                        isClickStateYumiL = false;
                        btCon_LArm.Content = "ConLeftArm";
                    }
                }
                else if (sender == btCon_RArm)
                {
                    if (isClickStateYumiR == false)
                    {
                        yumiR.ConnectToServer("127.0.0.1", 5001);
                        //yumiR.ConnectToServer("192.168.125.1", 5000);
                        isClickStateYumiR = true;
                        btCon_RArm.Content = "DisRightArm";
                    }
                    else
                    {
                        yumiR.DisconnectFromServer();
                        isClickStateYumiR = false;
                        btCon_RArm.Content = "ConRightArm";
                    }
                }
            }
            catch (Exception e0)
            {
                module.Log("! MyEvtClick : " + e0.Message);
            }
        }

        #endregion

        #region Helper

        void MyShutdown()
        {
            try
            {
                Dispatcher.Invoke(delegate ()
                {
                    module.MyCoreStop();
                    module.DisconnectFromServer();

                    Application.Current.Shutdown();
                });
            }
            catch (Exception e)
            {
                module.Log("MyShutdown : " + e.Message, true);
            }
        }

        #endregion
        
        private void in_send_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string data = in_send.Text.Trim();
                send(data);
            }
        }
        public void send(string data)
        {
            if (cbSelect.Text == "Server")
            {
                module.Send(data);
            }
            else if (cbSelect.Text == "YumiR")
            {
                yumiR.Send(data);
            }
            else if (cbSelect.Text == "YumiL")
            {
                yumiL.Send(data);
            }
        }
        void _recivePassingValue()
        {
            if (true)
            {
                
            }
        }
    }
}
