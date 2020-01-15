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
using System.Windows.Shapes;
using System.Threading;
using System.Diagnostics;
namespace Simple_Client_A
{
    /// <summary>
    /// Interaction logic for Yumiko.xaml
    /// </summary>
    public partial class Yumiko : Window
    {
        struct ParameterRobot
        {
            public double x, y, z, q0, qx, qy, qz, q1, q2, q3, q4, q5, q6, q7, idCode, Rx, Ry, Rz, Arm;
        };
        struct Pick
        {
            public double sourX;
            public double sourY;
            public double sourZ;
            public double desX;
            public double desY;
            public double desZ;
            public double th;
        };
        double zPos = 260;
        double offZAtoB = 40;
        double ZPosDown = 235;
        Stopwatch watch;
        double offsetX = 300;
        ParameterRobot paraLeftArm;
        ParameterRobot paraRightArm;
        ParameterRobot WorkL;
        ParameterRobot WorkR;
        ParameterRobot preWorkR;
        ParameterRobot preWorkL;
        Pick pick_L;
        Pick pick_R;
        bool L = true;
        bool R = false;
        bool On = true;
        bool Off = false;
        bool selectArm = false;
        public event MyPX410.MyModule.myDelegateMessage sendDataToMainWinL;
        public event MyPX410.MyModule.myDelegateMessage sendDataToMainWinR;
        public event MyPX410.MyModule.myDelegateMessage sendDataToMainWinS;

        public Yumiko()
        {
            InitializeComponent();
            paraLeftArm.x = 0;
            paraLeftArm.y = 0;
            paraLeftArm.z = 0;
            paraLeftArm.q0 = 0;
            paraLeftArm.qx = 0;
            paraLeftArm.qy = 0;
            paraLeftArm.qz = 0;
            paraLeftArm.q1 = 0;
            paraLeftArm.q2 = 0;
            paraLeftArm.q3 = 0;
            paraLeftArm.q4 = 0;
            paraLeftArm.q5 = 0;
            paraLeftArm.q6 = 0;
            paraLeftArm.q7 = 0;
            paraRightArm.x = 0;
            paraRightArm.y = 0;
            paraRightArm.z = 0;
            paraRightArm.q0 = 0;
            paraRightArm.qx = 0;
            paraRightArm.qy = 0;
            paraRightArm.qz = 0;
            paraRightArm.q1 = 0;
            paraRightArm.q2 = 0;
            paraRightArm.q3 = 0;
            paraRightArm.q4 = 0;
            paraRightArm.q5 = 0;
            paraRightArm.q6 = 0;
            paraRightArm.q7 = 0;
            WorkL.x = 204;
            WorkL.y = 244;
            WorkL.z = 340;
            WorkL.q0 = 0.0037;
            WorkL.qx = -0.0343;
            WorkL.qy = -0.999;
            WorkL.qz = -0.0126;
            WorkR.x = 204;
            WorkR.y = -244;
            WorkR.z = 340;
            WorkR.q0 = 0.0059;
            WorkR.qx = -0.5477;
            WorkR.qy = 0.8225;
            WorkR.qz = -0.03198;

            preWorkR.x = 204;
            preWorkR.y = -244;
            preWorkR.z = 360;
            preWorkR.q0 = 0.0059;
            preWorkR.qx = -0.5477;
            preWorkR.qy = 0.8225;
            preWorkR.qz = -0.03198;

            nudJoint.Text = "1";
            nudPos.Text = "1";
            nudOri.Text = "0.05";
            posToText(selectArm);
            jointToText(selectArm);
            watch = new Stopwatch();
            paraLeftArm.Rz = Convert.ToDouble(tbRz.Text);
            paraRightArm.Rz = Convert.ToDouble(tbRz.Text);
        }

        private void click_event(object sender, RoutedEventArgs e)
        {
            if (sender == btSetHomeL)
            {
                sendDataToMainWinL("96 #");
            }
            else if (sender == btSetHomeR)
            {
                sendDataToMainWinR("96 #");
            }
            else if (sender == btSetWorkL)
            {
                sendDataToMainWinL("97 #");
            }
            else if (sender == btSetWorkR)
            {
                sendDataToMainWinR("97 #");
            }//
            else if (sender == btUpdateR)
            {
                sendDataToMainWinR("3 #");
            }
            else if (sender == btTest)
            {
                pick_R.sourX = 250;
                pick_R.sourY = -100;
                pick_R.sourZ = 235;
                pick_R.desX = 450;
                pick_R.desY = -350;
                pick_R.desZ = 235;
                PickAtoB(R, pick_R);
            }
            else if (sender == btSetP1)
            {
                paraRightArm.x = 390;
                paraRightArm.y = 0;
                paraRightArm.z = 235;
                sentPosToYumi(R, paraRightArm);
            }
            else if (sender == btSetP2)
            {
                paraRightArm.x = 550;
                paraRightArm.y = 0;
                paraRightArm.z = 235;
                sentPosToYumi(R, paraRightArm);
            }
            else if (sender == btSetP2)
            {
                if (selectArm == R)
                {
                    paraRightArm.x = 300;
                    paraRightArm.y = -200;
                    paraRightArm.z = 235;
                    sentPosToYumi(R, paraRightArm);
                }
                else
                {
                    paraLeftArm.x = 300;
                    paraLeftArm.y = -200;
                    paraLeftArm.z = 235;
                    sentPosToYumi(R, paraRightArm);
                }

            }
            else if (sender == btTestTCP)
            {
                sendDataToMainWinS("[JAVISION:RETURN=1]");
            }
        }
        private void btSelectArm_Click(object sender, RoutedEventArgs e)
        {
            int x = 0;
            if (selectArm == R)
            {
                selectArm = L;
                btSelectArm.Content = "LeftArm";
                posToText(selectArm);
                jointToText(selectArm);
            }
            else
            {
                selectArm = R;
                btSelectArm.Content = "RightArm";
                posToText(selectArm);
                jointToText(selectArm);
            }
        }
        #region Function YUMI
        public void msgStatus(string mesg)
        {
            tbStatus.Text = tbStatus.Text + Environment.NewLine + " >> " + mesg;
        }


        void sentJoint(bool sel,ParameterRobot _para)
        {
            _para.idCode = 2;
            string msgg;
            msgg = _para.idCode.ToString();
            msgg = msgg + " " + _para.q1.ToString();//string.Format(tbX.ToString(), 08.1f);
            msgg = msgg + " " + _para.q2.ToString();//string.Format(tbY.ToString(), 08.1f);
            msgg = msgg + " " + _para.q3.ToString();//string.Format(tbZ.ToString(), 08.1f);
            msgg = msgg + " " + _para.q4.ToString();//string.Format(tbQ0.ToString(), 08.5f);
            msgg = msgg + " " + _para.q5.ToString();//string.Format(tbQx.ToString(), 08.5f);
            msgg = msgg + " " + _para.q6.ToString();
            msgg = msgg + " " + _para.q7.ToString() + " #";
            if (sel == R)
            {
                sendDataToMainWinL(msgg);
                //msgStatus(msgg);
            }
            else if(sel == L)
            {
                sendDataToMainWinR(msgg);
                //msgStatus(msgg);
            }
        }
        void sentPosToYumi(bool sel,ParameterRobot _para)
        {
            string msgg;
            //_para.idCode = 1;
            _para.idCode = 93;
            msgg = _para.idCode.ToString();//string.Format(idCode.ToString(), .3d);
            msgg = msgg + " " + _para.x.ToString();//string.Format(tbX.ToString(), 08.1f);
            msgg = msgg + " " + _para.y.ToString();//string.Format(tbY.ToString(), 08.1f);
            msgg = msgg + " " + _para.z.ToString();//string.Format(tbZ.ToString(), 08.1f);
                                                   //msgg = msgg + " " + _para.q0.ToString();//string.Format(tbQ0.ToString(), 08.5f);
                                                   //msgg = msgg + " " + _para.qx.ToString();//string.Format(tbQx.ToString(), 08.5f);
                                                   //msgg = msgg + " " + _para.qy.ToString();//string.Format(tbQy.ToString(), 08.5f);
                                                   //msgg = msgg + " " + _para.qz.ToString() + " #";//string.Format(tbQz.ToString(), 08.5f) + " #";
            msgg = msgg + " " + _para.Rz.ToString() + " #";
            if (sel == R)
            {
                sendDataToMainWinR(msgg);
                //msgStatus(msgg);
            }
            else if(sel == L)
            {
                sendDataToMainWinL(msgg);
                //msgStatus(msgg);
            }
        }
        public void updataPos(bool sel, string p1, string p2, string p3, string p4,
            string p5, string p6, string p7)
        {
            if (sel == L)
            {
                paraLeftArm.x = Convert.ToDouble(p1);
                paraLeftArm.y = Convert.ToDouble(p2);
                paraLeftArm.z = Convert.ToDouble(p3);
                paraLeftArm.q0 = Convert.ToDouble(p4);
                paraLeftArm.qx = Convert.ToDouble(p5);
                paraLeftArm.qy = Convert.ToDouble(p6);
                paraLeftArm.qz = Convert.ToDouble(p7);
            }
            else if (sel == R)
            {
                paraRightArm.x = Convert.ToDouble(p1);
                paraRightArm.y = Convert.ToDouble(p2);
                paraRightArm.z = Convert.ToDouble(p3);
                paraRightArm.q0 = Convert.ToDouble(p4);
                paraRightArm.qx = Convert.ToDouble(p5);
                paraRightArm.qy = Convert.ToDouble(p6);
                paraRightArm.qz = Convert.ToDouble(p7);
            }
            //posToText(sel);
        }
        public void updataJoint(bool sel, string p1, string p2, string p3, string p4,
            string p5, string p6, string p7)
        {
            if (sel == L)
            {
                paraLeftArm.q1 = Convert.ToDouble(p1);
                paraLeftArm.q2 = Convert.ToDouble(p2);
                paraLeftArm.q3 = Convert.ToDouble(p3);
                paraLeftArm.q4 = Convert.ToDouble(p4);
                paraLeftArm.q5 = Convert.ToDouble(p5);
                paraLeftArm.q6 = Convert.ToDouble(p6);
                paraLeftArm.q7 = Convert.ToDouble(p7);
            }
            else if (sel == R)
            {
                paraRightArm.q1 = Convert.ToDouble(p1);
                paraRightArm.q2 = Convert.ToDouble(p2);
                paraRightArm.q3 = Convert.ToDouble(p3);
                paraRightArm.q4 = Convert.ToDouble(p4);
                paraRightArm.q5 = Convert.ToDouble(p5);
                paraRightArm.q6 = Convert.ToDouble(p6);
                paraRightArm.q7 = Convert.ToDouble(p7);
            }
            //jointToText(sel);
        }
        void posToText(bool sel)
        {
            if (sel == L)
            {
                tbX.Text = paraLeftArm.x.ToString();
                tbY.Text = paraLeftArm.y.ToString();
                tbZ.Text = paraLeftArm.z.ToString();
                tbQ0.Text = paraLeftArm.q0.ToString();
                tbQx.Text = paraLeftArm.qx.ToString();
                tbQy.Text = paraLeftArm.qy.ToString();
                tbQz.Text = paraLeftArm.qz.ToString();
            }
            else if (sel == R)
            {
                tbX.Text = paraRightArm.x.ToString();
                tbY.Text = paraRightArm.y.ToString();
                tbZ.Text = paraRightArm.z.ToString();
                tbQ0.Text = paraRightArm.q0.ToString();
                tbQx.Text = paraRightArm.qx.ToString();
                tbQy.Text = paraRightArm.qy.ToString();
                tbQz.Text = paraRightArm.qz.ToString();
            }
        }
        void jointToText(bool sel)
        {
            if (sel == L)
            {
                tbQ1.Text = paraLeftArm.q1.ToString();
                tbQ2.Text = paraLeftArm.q2.ToString();
                tbQ3.Text = paraLeftArm.q3.ToString();
                tbQ4.Text = paraLeftArm.q4.ToString();
                tbQ5.Text = paraLeftArm.q5.ToString();
                tbQ6.Text = paraLeftArm.q6.ToString();
                tbQ7.Text = paraLeftArm.q7.ToString();
            }
            else if (sel == R)
            {
                tbQ1.Text = paraRightArm.q1.ToString();
                tbQ2.Text = paraRightArm.q2.ToString();
                tbQ3.Text = paraRightArm.q3.ToString();
                tbQ4.Text = paraRightArm.q4.ToString();
                tbQ5.Text = paraRightArm.q5.ToString();
                tbQ6.Text = paraRightArm.q6.ToString();
                tbQ7.Text = paraRightArm.q7.ToString();
            }
        }
        void textToPos(bool sel)
        {
            if (sel == L)
            {
                paraLeftArm.x = Convert.ToDouble(tbX.Text);
                paraLeftArm.y = Convert.ToDouble(tbY.Text);
                paraLeftArm.z = Convert.ToDouble(tbZ.Text);
                paraLeftArm.q0 = Convert.ToDouble(tbQ0.Text);
                paraLeftArm.qx = Convert.ToDouble(tbQx.Text);
                paraLeftArm.qy = Convert.ToDouble(tbQy.Text);
                paraLeftArm.qz = Convert.ToDouble(tbQx.Text);
            }
            else if (sel == R)
            {
                paraRightArm.x = Convert.ToDouble(tbX.Text);
                paraRightArm.y = Convert.ToDouble(tbY.Text);
                paraRightArm.z = Convert.ToDouble(tbZ.Text);
                paraRightArm.q0 = Convert.ToDouble(tbQ0.Text);
                paraRightArm.qx = Convert.ToDouble(tbQx.Text);
                paraRightArm.qy = Convert.ToDouble(tbQy.Text);
                paraRightArm.qz = Convert.ToDouble(tbQx.Text);
            }
        }
        void textToJoint(bool sel)
        {
            if (sel == L)
            {
                paraLeftArm.q1 = Convert.ToDouble(tbQ1.Text);
                paraLeftArm.q2 = Convert.ToDouble(tbQ2.Text);
                paraLeftArm.q3 = Convert.ToDouble(tbQ3.Text);
                paraLeftArm.q4 = Convert.ToDouble(tbQ4.Text);
                paraLeftArm.q5 = Convert.ToDouble(tbQ5.Text);
                paraLeftArm.q6 = Convert.ToDouble(tbQ6.Text);
                paraLeftArm.q7 = Convert.ToDouble(tbQ7.Text);
            }
            else if (sel == R)
            {
                paraRightArm.q1 = Convert.ToDouble(tbQ1.Text);
                paraRightArm.q2 = Convert.ToDouble(tbQ2.Text);
                paraRightArm.q3 = Convert.ToDouble(tbQ3.Text);
                paraRightArm.q4 = Convert.ToDouble(tbQ4.Text);
                paraRightArm.q5 = Convert.ToDouble(tbQ5.Text);
                paraRightArm.q6 = Convert.ToDouble(tbQ6.Text);
                paraRightArm.q7 = Convert.ToDouble(tbQ7.Text);
            }

        }
        private void fnGoPos(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            if (bt.Name == "btGoPos")
            {
                if (selectArm == L)
                {
                    textToPos(L);
                    sentPosToYumi(L, paraLeftArm);
                }
                else
                {
                    textToPos(R);
                    sentPosToYumi(R,paraRightArm);
                }
            }
            string[] splBt = bt.Content.ToString().Split(' ');
            //msgStatus(splBt[0]);
            if (selectArm == L)
            {
                switch (splBt[0])
                {
                    case "X":
                        if (splBt[1] == "++")
                        {
                            paraLeftArm.x = paraLeftArm.x + Convert.ToDouble(nudPos.Text);
                        }
                        else if (splBt[1] == "--")
                        {
                            paraLeftArm.x = paraLeftArm.x - Convert.ToDouble(nudPos.Text);
                        }
                        break;
                    case "Y":
                        if (splBt[1] == "++")
                        {
                            paraLeftArm.y = paraLeftArm.y + Convert.ToDouble(nudPos.Text);
                        }
                        else if (splBt[1] == "--")
                        {
                            paraLeftArm.y = paraLeftArm.y - Convert.ToDouble(nudPos.Text);
                        }
                        break;
                    case "Z":
                        if (splBt[1] == "++")
                        {
                            paraLeftArm.z = paraLeftArm.z + Convert.ToDouble(nudPos.Text);
                        }
                        else if (splBt[1] == "--")
                        {
                            paraLeftArm.z = paraLeftArm.z - Convert.ToDouble(nudPos.Text);
                        }
                        break;
                    case "Q0":
                        if (splBt[1] == "++")
                        {
                            paraLeftArm.q0 = paraLeftArm.q0 + Convert.ToDouble(nudOri.Text);
                        }
                        else if (splBt[1] == "--")
                        {
                            paraLeftArm.q0 = paraLeftArm.q0 - Convert.ToDouble(nudOri.Text);
                        }
                        break;
                    case "Qx":
                        if (splBt[1] == "++")
                        {
                            paraLeftArm.qx = paraLeftArm.qx + Convert.ToDouble(nudOri.Text);
                        }
                        else if (splBt[1] == "--")
                        {
                            paraLeftArm.qx = paraLeftArm.qx - Convert.ToDouble(nudOri.Text);
                        }
                        break;
                    case "Qy":
                        if (splBt[1] == "++")
                        {
                            paraLeftArm.qy = paraLeftArm.qy + Convert.ToDouble(nudOri.Text);
                        }
                        else if (splBt[1] == "--")
                        {
                            paraLeftArm.qy = paraLeftArm.qy - Convert.ToDouble(nudOri.Text);
                        }
                        break;
                    case "Qz":
                        if (splBt[1] == "++")
                        {
                            paraLeftArm.qz = paraLeftArm.qz + Convert.ToDouble(nudOri.Text);
                        }
                        else if (splBt[1] == "--")
                        {
                            paraLeftArm.qz = paraLeftArm.qz - Convert.ToDouble(nudOri.Text);
                        }
                        break;
                }
                posToText(L);
                //textToPos(L);
                sentPosToYumi(L, paraLeftArm);
            }
            else if (selectArm == R)
            {
                switch (splBt[0])
                {
                    case "X":
                        if (splBt[1] == "++")
                        {
                            paraRightArm.x = paraRightArm.x + Convert.ToDouble(nudPos.Text);
                        }
                        else if (splBt[1] == "--")
                        {
                            paraRightArm.x = paraRightArm.x - Convert.ToDouble(nudPos.Text);
                        }
                        break;
                    case "Y":
                        if (splBt[1] == "++")
                        {
                            paraRightArm.y = paraRightArm.y + Convert.ToDouble(nudPos.Text);
                        }
                        else if (splBt[1] == "--")
                        {
                            paraRightArm.y = paraRightArm.y - Convert.ToDouble(nudPos.Text);
                        }
                        break;
                    case "Z":
                        if (splBt[1] == "++")
                        {
                            paraRightArm.z = paraRightArm.z + Convert.ToDouble(nudPos.Text);
                        }
                        else if (splBt[1] == "--")
                        {
                            paraRightArm.z = paraRightArm.z - Convert.ToDouble(nudPos.Text);
                        }
                        break;
                    case "Q0":
                        if (splBt[1] == "++")
                        {
                            paraRightArm.q0 = paraRightArm.q0 + Convert.ToDouble(nudOri.Text);
                        }
                        else if (splBt[1] == "--")
                        {
                            paraRightArm.q0 = paraRightArm.q0 - Convert.ToDouble(nudOri.Text);
                        }
                        break;
                    case "Qx":
                        if (splBt[1] == "++")
                        {
                            paraRightArm.qx = paraRightArm.qx + Convert.ToDouble(nudOri.Text);
                        }
                        else if (splBt[1] == "--")
                        {
                            paraRightArm.qx = paraRightArm.qx - Convert.ToDouble(nudOri.Text);
                        }
                        break;
                    case "Qy":
                        if (splBt[1] == "++")
                        {
                            paraRightArm.qy = paraRightArm.qy + Convert.ToDouble(nudOri.Text);
                        }
                        else if (splBt[1] == "--")
                        {
                            paraRightArm.qy = paraRightArm.qy - Convert.ToDouble(nudOri.Text);
                        }
                        break;
                    case "Qz":
                        if (splBt[1] == "++")
                        {
                            paraRightArm.qz = paraRightArm.qz + Convert.ToDouble(nudOri.Text);
                        }
                        else if (splBt[1] == "--")
                        {
                            paraRightArm.qz = paraRightArm.qz - Convert.ToDouble(nudOri.Text);
                        }
                        break;
                }
                posToText(R);
                //textToPos(R);
                sentPosToYumi(R,paraRightArm);
            }
        }
        private void fnGoJoint(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;

            if (bt.Name == "btGoJoint")
            {
                if (selectArm == L)
                {
                    textToJoint(L);
                    sentJoint(L, paraLeftArm);
                }
                else
                {
                    textToJoint(R);
                    sentJoint(R,paraRightArm);
                }
            }
            else
            {
                string[] splBt = bt.Content.ToString().Split('q');
                //msgStatus(splBt[1]);
                string[] op = splBt[1].Split(' ');
                if (selectArm == L)
                {
                    switch (op[0])
                    {
                        case "1":
                            if (op[1] == "++")
                            {
                                paraLeftArm.q1 = paraLeftArm.q1 + Convert.ToDouble(nudJoint.Text);
                            }
                            else if (op[1] == "--")
                            {
                                paraLeftArm.q1 = paraLeftArm.q1 - Convert.ToDouble(nudJoint.Text);
                            }
                            break;
                        case "2":
                            if (op[1] == "++")
                            {
                                paraLeftArm.q2 = paraLeftArm.q2 + Convert.ToDouble(nudJoint.Text);
                            }
                            else if (op[1] == "--")
                            {
                                paraLeftArm.q2 = paraLeftArm.q2 - Convert.ToDouble(nudJoint.Text);
                            }
                            break;
                        case "3":
                            if (op[1] == "++")
                            {
                                paraLeftArm.q3 = paraLeftArm.q3 + Convert.ToDouble(nudJoint.Text);
                            }
                            else if (op[1] == "--")
                            {
                                paraLeftArm.q3 = paraLeftArm.q3 - Convert.ToDouble(nudJoint.Text);
                            }
                            break;
                        case "4":
                            if (op[1] == "++")
                            {
                                paraLeftArm.q4 = paraLeftArm.q4 + Convert.ToDouble(nudJoint.Text);
                            }
                            else if (op[1] == "--")
                            {
                                paraLeftArm.q4 = paraLeftArm.q4 - Convert.ToDouble(nudJoint.Text);
                            }
                            break;
                        case "5":
                            if (op[1] == "++")
                            {
                                paraLeftArm.q5 = paraLeftArm.q5 + Convert.ToDouble(nudJoint.Text);
                            }
                            else if (op[1] == "--")
                            {
                                paraLeftArm.q5 = paraLeftArm.q5 - Convert.ToDouble(nudJoint.Text);
                            }
                            break;
                        case "6":
                            if (op[1] == "++")
                            {
                                paraLeftArm.q6 = paraLeftArm.q6 + Convert.ToDouble(nudJoint.Text);
                            }
                            else if (op[1] == "--")
                            {
                                paraLeftArm.q6 = paraLeftArm.q6 - Convert.ToDouble(nudJoint.Text);
                            }
                            break;
                        case "7":
                            if (op[1] == "++")
                            {
                                paraLeftArm.q7 = paraLeftArm.q7 + Convert.ToDouble(nudJoint.Text);
                            }
                            else if (op[1] == "--")
                            {
                                paraLeftArm.q7 = paraLeftArm.q7 - Convert.ToDouble(nudJoint.Text);
                            }
                            break;
                    }
                    jointToText(L);
                    sentJoint(L, paraLeftArm);
                }
                else if (selectArm == R)
                {
                    switch (op[0])
                    {
                        case "1":
                            if (op[1] == "++")
                            {
                                paraRightArm.q1 = paraRightArm.q1 + Convert.ToDouble(nudJoint.Text);
                            }
                            else if (op[1] == "--")
                            {
                                paraRightArm.q1 = paraRightArm.q1 - Convert.ToDouble(nudJoint.Text);
                            }
                            break;
                        case "2":
                            if (op[1] == "++")
                            {
                                paraRightArm.q2 = paraRightArm.q2 + Convert.ToDouble(nudJoint.Text);
                            }
                            else if (op[1] == "--")
                            {
                                paraRightArm.q2 = paraRightArm.q2 - Convert.ToDouble(nudJoint.Text);
                            }
                            break;
                        case "3":
                            if (op[1] == "++")
                            {
                                paraRightArm.q3 = paraRightArm.q3 + Convert.ToDouble(nudJoint.Text);
                            }
                            else if (op[1] == "--")
                            {
                                paraRightArm.q3 = paraRightArm.q3 - Convert.ToDouble(nudJoint.Text);
                            }
                            break;
                        case "4":
                            if (op[1] == "++")
                            {
                                paraRightArm.q4 = paraRightArm.q4 + Convert.ToDouble(nudJoint.Text);
                            }
                            else if (op[1] == "--")
                            {
                                paraRightArm.q4 = paraRightArm.q4 - Convert.ToDouble(nudJoint.Text);
                            }
                            break;
                        case "5":
                            if (op[1] == "++")
                            {
                                paraRightArm.q5 = paraRightArm.q5 + Convert.ToDouble(nudJoint.Text);
                            }
                            else if (op[1] == "--")
                            {
                                paraRightArm.q5 = paraRightArm.q5 - Convert.ToDouble(nudJoint.Text);
                            }
                            break;
                        case "6":
                            if (op[1] == "++")
                            {
                                paraRightArm.q6 = paraRightArm.q6 + Convert.ToDouble(nudJoint.Text);
                            }
                            else if (op[1] == "--")
                            {
                                paraRightArm.q6 = paraRightArm.q6 - Convert.ToDouble(nudJoint.Text);
                            }
                            break;
                        case "7":
                            if (op[1] == "++")
                            {
                                paraRightArm.q7 = paraRightArm.q7 + Convert.ToDouble(nudJoint.Text);
                            }
                            else if (op[1] == "--")
                            {
                                paraRightArm.q7 = paraRightArm.q7 - Convert.ToDouble(nudJoint.Text);
                            }
                            break;
                    }
                    jointToText(R);
                    sentJoint(R,paraRightArm);
                }
            }
        }

        void gripper(bool sel,bool state)
        {
            if (sel == L)
            {
                if (state == On)
                {
                    sendDataToMainWinL("95 #");
                }
                else if (state == Off)
                {
                    sendDataToMainWinL("94 #");
                }
            }
            else if (sel == R)
            {
                if (state == On)
                {
                    sendDataToMainWinR("95 #");
                }
                else if (state == Off)
                {
                    sendDataToMainWinR("94 #");
                }
            }

        }
        #endregion
        #region Function Manipulator
        //void PickAtoB(bool sel,double sourX, double sourY, double sourZ, double desX, double desY, double desZ)
        public void goAtoB(bool sel, double _sourX, double _sourY, double _sourZ, double _desX, double _desY, double _desZ,double _th)
        {
            if (sel == L)
            {
                pick_L.sourX = _sourX;
                pick_L.sourY = _sourY;
                pick_L.sourZ = _sourZ;
                pick_L.desX = _desX;
                pick_L.desY = _desY;
                pick_L.desZ = _desZ;
                pick_L.th = _th;
                PickAtoB(L, pick_L);
            }
            else if (sel == R)
            {
                pick_R.sourX = _sourX;
                pick_R.sourY = _sourY;
                pick_R.sourZ = _sourZ;
                pick_R.desX = _desX;
                pick_R.desY = _desY;
                pick_R.desZ = _desZ;
                pick_R.th = _th;
                PickAtoB(R, pick_R);
            }
        }
        void PickAtoB(bool sel,Pick _p)
        {

            if (sel == L)
            {
                sentPosToYumi(L, WorkL);
                Thread.Sleep(500);
                updateParameter(L, _p.sourX, _p.sourY, _p.sourZ + offZAtoB);
                paraLeftArm.Rz = 0;
                sentPosToYumi(L, paraLeftArm);
                Thread.Sleep(500);
                gripper(L, On);
                Thread.Sleep(500);
                updateParameter(L, _p.sourX, _p.sourY, _p.sourZ);
                Thread.Sleep(100);
                sentPosToYumi(L, paraLeftArm);
                Thread.Sleep(500);
                updateParameter(L, _p.sourX, _p.sourY, _p.desZ + offZAtoB);
                paraLeftArm.Rz = _p.th;
                sentPosToYumi(L, paraLeftArm);
                Thread.Sleep(500);
                updateParameter(L, _p.desX, _p.desY, _p.desZ + offZAtoB);
                sentPosToYumi(L, paraLeftArm);
                Thread.Sleep(500);
                updateParameter(L, _p.desX, _p.desY, _p.desZ);
                sentPosToYumi(L, paraLeftArm);
                Thread.Sleep(1000);
                gripper(L, Off);
                Thread.Sleep(2000);
                updateParameter(L, _p.desX, _p.desY, _p.desZ + offZAtoB);
                sentPosToYumi(L, paraLeftArm);
                Thread.Sleep(500);
                paraLeftArm.Rz = 0;
                //sentPosToYumi(R, preWorkR);
                //Thread.Sleep(500);
                sentPosToYumi(L, WorkL);
                Thread.Sleep(500);
                sendDataToMainWinS("[JAVISION:RETURN=1]");
                //sendDataToMainWinS("[TASKOYAKI:RETURN=1]");

            }
            else if (sel == R)
            {
                sentPosToYumi(R, WorkR);
                Thread.Sleep(500);
                //sendDataToMainWinR("3 #");

                //Thread.Sleep(100);
                updateParameter(R, _p.sourX, _p.sourY, _p.sourZ+offZAtoB);
                paraRightArm.Rz = 0;
                sentPosToYumi(R, paraRightArm);
                Thread.Sleep(500);
                gripper(R, On);
                Thread.Sleep(500);
                updateParameter(R, _p.sourX, _p.sourY, _p.sourZ);
                Thread.Sleep(100);
                sentPosToYumi(R, paraRightArm);
                Thread.Sleep(500);
                updateParameter(R, _p.sourX, _p.sourY, _p.desZ + offZAtoB);
                paraRightArm.Rz = _p.th;
                sentPosToYumi(R, paraRightArm);
                Thread.Sleep(500);
                updateParameter(R, _p.desX, _p.desY, _p.desZ + offZAtoB);
                sentPosToYumi(R, paraRightArm);
                Thread.Sleep(500);
                updateParameter(R, _p.desX, _p.desY, _p.desZ);
                sentPosToYumi(R,paraRightArm);
                Thread.Sleep(1000);
                gripper(R, Off);
                Thread.Sleep(2000);
                updateParameter(R, _p.desX, _p.desY, _p.desZ + offZAtoB);
                sentPosToYumi(R, paraRightArm);
                Thread.Sleep(500);
                paraRightArm.Rz = 0;

                //sentPosToYumi(R, preWorkR);
                //Thread.Sleep(500);
                sentPosToYumi(R, WorkR);
                Thread.Sleep(500);
                sendDataToMainWinS("[JAVISION:RETURN=1]");
                //sendDataToMainWinS("[TASKOYAKI:RETURN=1]");
            }

        }
        void updateParameter(bool sel, double x, double y, double z, double q0, double qx, double qy, double qz)
        {
            if (sel == L)
            {
                paraLeftArm.x = x;
                paraLeftArm.y = y;
                paraLeftArm.z = z;
                paraLeftArm.q0 = q0;
                paraLeftArm.qx = qx;
                paraLeftArm.qy = qy;
                paraLeftArm.qz = qz;
            }
            else if (sel == R)
            {
                paraRightArm.x = x;
                paraRightArm.y = y;
                paraRightArm.z = z;
                paraRightArm.q0 = q0;
                paraRightArm.qx = qx;
                paraRightArm.qy = qy;
                paraRightArm.qz = qz;
            }
        }
        void updateParameter(bool sel, double x, double y, double z)
        {
            if (sel == L)
            {
                paraLeftArm.x = x;
                paraLeftArm.y = y;
                paraLeftArm.z = z;
            }
            else if (sel == R)
            {
                paraRightArm.x = x;
                paraRightArm.y = y;
                paraRightArm.z = z;
            }
        }
        #endregion
    }
}
