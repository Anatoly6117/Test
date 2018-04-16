using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using S7PROSIMLib;
//using System.Runtime.InteropServices;

namespace s7proSIM_Equador2484
{
    public partial class Form1 : Form
    {
        //public S7ProSimClass PS = new S7ProSimClass();
        public S7ProSim Ps = new S7ProSim();
        Int32 visota_leb1 = 1, visota_leb2 = 1, visota_leb3 = 1, visota_leb4 = 1, visota_leb5 = 1, visota_leb6 = 1;
        const byte low_kol_max = 60;
        Int16 low_kol1 = low_kol_max, low_kol2 = low_kol_max, low_kol3 = low_kol_max, low_kol4 = low_kol_max, low_kol5 = low_kol_max, low_kol6 = low_kol_max;//переменная замедления движения колонн
        const Int16 WeightLeb1_kg = 210, WeightLeb2_kg = 210, WeightLeb3_kg = 210, WeightLeb4_kg = 210, WeightLeb5_kg = 210, WeightLeb6_kg = 210;
        Int16 WL1_kg = 0, WL2_kg = 0, WL3_kg = 0, WL4_kg = 0, WL5_kg = 0, WL6_kg = 0;

        public Form1()
        {
            InitializeComponent();

            if (cb_set_minus0_2m.Checked)
            {
                mtb_ID62_122.Text = "-200";
                mtb_ID62.Text = mtb_ID62_122.Text; mtb_ID74.Text = mtb_ID62_122.Text; mtb_ID86.Text = mtb_ID62_122.Text;
                mtb_ID98.Text = mtb_ID62_122.Text; mtb_ID110.Text = mtb_ID62_122.Text; mtb_ID122.Text = mtb_ID62_122.Text;
                visota_leb1 = Convert.ToInt32(mtb_ID62_122.Text); Ps.WriteInputPoint(62, 0, visota_leb1); visota_leb2 = Convert.ToInt32(mtb_ID62_122.Text); Ps.WriteInputPoint(74, 0, visota_leb2);
                visota_leb3 = Convert.ToInt32(mtb_ID62_122.Text); Ps.WriteInputPoint(86, 0, Convert.ToInt32(visota_leb3)); visota_leb4 = Convert.ToInt32(mtb_ID62_122.Text); Ps.WriteInputPoint(98, 0, Convert.ToInt32(visota_leb4));
                visota_leb5 = Convert.ToInt32(mtb_ID62_122.Text); Ps.WriteInputPoint(110, 0, Convert.ToInt32(visota_leb5)); visota_leb6 = Convert.ToInt32(mtb_ID62_122.Text); Ps.WriteInputPoint(122, 0, Convert.ToInt32(visota_leb6));
            }
        }

        private void button_Connect_Click(object sender, EventArgs e)
        {
            Ps.Connect();
            //PS.BeginScanNotify();
            var cpuStateText = Ps.GetState();
            var scanModeText = Ps.GetScanMode().ToString();
            if (checkBox_Line.Checked == true & scanModeText == ScanModeConstants.SingleScan.ToString())
            {
                Ps.SetScanMode(ScanModeConstants.ContinuousScan);
                scanModeText = Ps.GetScanMode().ToString();
            }
            Label_CPUState.Text = cpuStateText;
            comboBoxCPUSTATE.Text = cpuStateText;
            labelScanMode.Text = scanModeText;
            comboBoxSCANMODE.Text = scanModeText;
        }

        private void comboBoxCPUSTATE_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s = comboBoxCPUSTATE.SelectedItem.ToString();
            Ps.SetState(s);
            Label_CPUState.Text = s;
        }

        private void comboBoxSCANMODE_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s = comboBoxSCANMODE.SelectedItem.ToString();
            if (s == "Single Scan")
                Ps.SetScanMode(ScanModeConstants.SingleScan);
            else if (s == "Continuous Scan")
                Ps.SetScanMode(ScanModeConstants.ContinuousScan);
            labelScanMode.Text = s;
        }

        private void timer_Read_M_Tick(object sender, EventArgs e)
        {
            //object M0_0 = true; //initial value
            //PS.ReadFlagValue(0, 0, PointDataTypeConstants.S7_Bit, ref M0_0);
            //checkBox_M0_0.Checked = (bool)M0_0;
            object Qx_x = true; //initial value
            if (cb_CheckInputsKolonn.Checked)
            {
                // Контроль тормоза лебедки 1
                Ps.ReadOutputPoint(0, 0, PointDataTypeConstants.S7_Bit, ref Qx_x); //Прочитать состояние выхода Q0.0
                Ps.WriteInputPoint(6, 2, ref Qx_x); //Запись выхода Q0.0 во вход I6.2
                // Контроль тормоза лебедки 2
                Ps.ReadOutputPoint(0, 4, PointDataTypeConstants.S7_Bit, ref Qx_x); //Прочитать состояние выхода Q0.4
                Ps.WriteInputPoint(6, 6, ref Qx_x); //Запись выхода Q0.4 во вход I6.6
                // Контроль тормоза лебедки 3
                Ps.ReadOutputPoint(1, 0, PointDataTypeConstants.S7_Bit, ref Qx_x); //Прочитать состояние выхода Q1.0
                Ps.WriteInputPoint(7, 2, ref Qx_x); //Запись выхода Q1.0 во вход I7.2
                // Контроль тормоза лебедки 4
                Ps.ReadOutputPoint(1, 4, PointDataTypeConstants.S7_Bit, ref Qx_x); //Прочитать состояние выхода Q1.4
                Ps.WriteInputPoint(7, 6, ref Qx_x); //Запись выхода Q1.4 во вход I7.6
                // Контроль тормоза лебедки 5
                Ps.ReadOutputPoint(2, 0, PointDataTypeConstants.S7_Bit, ref Qx_x); //Прочитать состояние выхода Q2.0
                Ps.WriteInputPoint(8, 2, ref Qx_x); //Запись выхода Q2.0 во вход I8.2
                // Контроль тормоза лебедки 6
                Ps.ReadOutputPoint(2, 4, PointDataTypeConstants.S7_Bit, ref Qx_x); //Прочитать состояние выхода Q2.4
                Ps.WriteInputPoint(8, 6, ref Qx_x); //Запись выхода Q2.4 во вход I8.6

                // Контроль поворота колонны 1
                Ps.ReadOutputPoint(0, 1, PointDataTypeConstants.S7_Bit, ref Qx_x); //Прочитать состояние выхода Q0.1
                Ps.WriteInputPoint(0, 1, ref Qx_x); //Запись выхода Q0.1 во вход I0.1
                // Контроль поворота колонны 2
                Ps.ReadOutputPoint(0, 5, PointDataTypeConstants.S7_Bit, ref Qx_x); //Прочитать состояние выхода Q0.5
                Ps.WriteInputPoint(1, 1, ref Qx_x); //Запись выхода Q0.5 во вход I1.1
                // Контроль поворота колонны 3
                Ps.ReadOutputPoint(1, 1, PointDataTypeConstants.S7_Bit, ref Qx_x); //Прочитать состояние выхода Q1.1
                Ps.WriteInputPoint(2, 1, ref Qx_x); //Запись выхода Q1.1 во вход I2.1
                // Контроль поворота колонны 4
                Ps.ReadOutputPoint(1, 5, PointDataTypeConstants.S7_Bit, ref Qx_x); //Прочитать состояние выхода Q1.5
                Ps.WriteInputPoint(3, 1, ref Qx_x); //Запись выхода Q1.5 во вход I3.1
                // Контроль поворота колонны 5
                Ps.ReadOutputPoint(2, 1, PointDataTypeConstants.S7_Bit, ref Qx_x); //Прочитать состояние выхода Q2.1
                Ps.WriteInputPoint(4, 1, ref Qx_x); //Запись выхода Q2.1 во вход I4.1
                // Контроль поворота колонны 6
                Ps.ReadOutputPoint(2, 5, PointDataTypeConstants.S7_Bit, ref Qx_x); //Прочитать состояние выхода Q2.5
                Ps.WriteInputPoint(5, 1, ref Qx_x); //Запись выхода Q2.5 во вход I5.1

                // Контроль разворота колонны 1
                Ps.ReadOutputPoint(0, 2, PointDataTypeConstants.S7_Bit, ref Qx_x); //Прочитать состояние выхода Q0.2
                Ps.WriteInputPoint(0, 2, ref Qx_x); //Запись выхода Q0.2 во вход I0.2
                // Контроль разворота колонны 2
                Ps.ReadOutputPoint(0, 6, PointDataTypeConstants.S7_Bit, ref Qx_x); //Прочитать состояние выхода Q0.6
                Ps.WriteInputPoint(1, 2, ref Qx_x); //Запись выхода Q0.6 во вход I1.2
                // Контроль разворота колонны 3
                Ps.ReadOutputPoint(1, 2, PointDataTypeConstants.S7_Bit, ref Qx_x); //Прочитать состояние выхода Q1.2
                Ps.WriteInputPoint(2, 2, ref Qx_x); //Запись выхода Q1.2 во вход I2.2
                // Контроль разворота колонны 4
                Ps.ReadOutputPoint(1, 6, PointDataTypeConstants.S7_Bit, ref Qx_x); //Прочитать состояние выхода Q1.6
                Ps.WriteInputPoint(3, 2, ref Qx_x); //Запись выхода Q1.6 во вход I3.2
                // Контроль разворота колонны 5
                Ps.ReadOutputPoint(2, 2, PointDataTypeConstants.S7_Bit, ref Qx_x); //Прочитать состояние выхода Q2.2
                Ps.WriteInputPoint(4, 2, ref Qx_x); //Запись выхода Q2.2 во вход I4.2
                // Контроль разворота колонны 6
                Ps.ReadOutputPoint(2, 6, PointDataTypeConstants.S7_Bit, ref Qx_x); //Прочитать состояние выхода Q2.6
                Ps.WriteInputPoint(5, 2, ref Qx_x); //Запись выхода Q2.6 во вход I5.2

                // Контроль тормоза колонны 1
                Ps.ReadOutputPoint(0, 3, PointDataTypeConstants.S7_Bit, ref Qx_x); //Прочитать состояние выхода Q0.3
                Ps.WriteInputPoint(0, 4, ref Qx_x); //Запись выхода Q0.3 во вход I0.4
                // Контроль тормоза колонны 2
                Ps.ReadOutputPoint(0, 7, PointDataTypeConstants.S7_Bit, ref Qx_x); //Прочитать состояние выхода Q0.7
                Ps.WriteInputPoint(1, 4, ref Qx_x); //Запись выхода Q0.7 во вход I1.4
                // Контроль тормоза колонны 3
                Ps.ReadOutputPoint(1, 3, PointDataTypeConstants.S7_Bit, ref Qx_x); //Прочитать состояние выхода Q1.3
                Ps.WriteInputPoint(2, 4, ref Qx_x); //Запись выхода Q1.3 во вход I2.4
                // Контроль тормоза колонны 4
                Ps.ReadOutputPoint(1, 7, PointDataTypeConstants.S7_Bit, ref Qx_x); //Прочитать состояние выхода Q1.7
                Ps.WriteInputPoint(3, 4, ref Qx_x); //Запись выхода Q1.7 во вход I3.4
                // Контроль тормоза колонны 5
                Ps.ReadOutputPoint(2, 3, PointDataTypeConstants.S7_Bit, ref Qx_x); //Прочитать состояние выхода Q2.3
                Ps.WriteInputPoint(4, 4, ref Qx_x); //Запись выхода Q2.3 во вход I4.4
                // Контроль тормоза колонны 6
                Ps.ReadOutputPoint(2, 7, PointDataTypeConstants.S7_Bit, ref Qx_x); //Прочитать состояние выхода Q2.7
                Ps.WriteInputPoint(5, 4, ref Qx_x); //Запись выхода Q2.7 во вход I5.4
            }
            if (cb_CheckInputsLebedok.Checked)
            {
                // Передача команды пуска привода лебедки 1 в PZD1.OUT из слова управления в слово состояние бита тормоза PZD3.IN
                Ps.ReadOutputPoint(135, 2, PointDataTypeConstants.S7_Bit, ref Qx_x); //Прочитать состояние выхода Q135.2
                Ps.WriteInputPoint(138, 0, ref Qx_x); //Запись выхода Q135.2 во вход I138.0
                // Передача команды пуска привода лебедки 2 в PZD1.OUT из слова управления в слово состояние бита тормоза PZD3.IN
                Ps.ReadOutputPoint(141, 2, PointDataTypeConstants.S7_Bit, ref Qx_x); //Прочитать состояние выхода Q141.2
                Ps.WriteInputPoint(144, 0, ref Qx_x); //Запись выхода Q141.2 во вход I144.0
                // Передача команды пуска привода лебедки 3 в PZD1.OUT из слова управления в слово состояние бита тормоза PZD3.IN
                Ps.ReadOutputPoint(147, 2, PointDataTypeConstants.S7_Bit, ref Qx_x); //Прочитать состояние выхода Q147.2
                Ps.WriteInputPoint(150, 0, ref Qx_x); //Запись выхода Q147.2 во вход I150.0
                // Передача команды пуска привода лебедки 4 в PZD1.OUT из слова управления в слово состояние бита тормоза PZD3.IN
                Ps.ReadOutputPoint(153, 2, PointDataTypeConstants.S7_Bit, ref Qx_x); //Прочитать состояние выхода Q153.2
                Ps.WriteInputPoint(156, 0, ref Qx_x); //Запись выхода Q153.2 во вход I156.0
                // Передача команды пуска привода лебедки 5 в PZD1.OUT из слова управления в слово состояние бита тормоза PZD3.IN
                Ps.ReadOutputPoint(159, 2, PointDataTypeConstants.S7_Bit, ref Qx_x); //Прочитать состояние выхода Q159.2
                Ps.WriteInputPoint(162, 0, ref Qx_x); //Запись выхода Q159.2 во вход I162.0
                // Передача команды пуска привода лебедки 6 в PZD1.OUT из слова управления в слово состояние бита тормоза PZD3.IN
                Ps.ReadOutputPoint(165, 2, PointDataTypeConstants.S7_Bit, ref Qx_x); //Прочитать состояние выхода Q165.2
                Ps.WriteInputPoint(168, 0, ref Qx_x); //Запись выхода Q165.2 во вход I168.0
            }
            if (cb_motion.Checked)//включен контроль движением
            {
                //Чтение выходов управления движением колонн
                object povorot_kol1 = true, povorot_kol2 = true, povorot_kol3 = true, povorot_kol4 = true, povorot_kol5 = true, povorot_kol6 = true;
                object razvorot_kol1 = true, razvorot_kol2 = true, razvorot_kol3 = true, razvorot_kol4 = true, razvorot_kol5 = true, razvorot_kol6 = true;
                Ps.ReadOutputPoint(0, 1, PointDataTypeConstants.S7_Bit, ref povorot_kol1); Ps.ReadOutputPoint(0, 2, PointDataTypeConstants.S7_Bit, ref razvorot_kol1);
                Ps.ReadOutputPoint(0, 5, PointDataTypeConstants.S7_Bit, ref povorot_kol2); Ps.ReadOutputPoint(0, 6, PointDataTypeConstants.S7_Bit, ref razvorot_kol2);
                Ps.ReadOutputPoint(1, 1, PointDataTypeConstants.S7_Bit, ref povorot_kol3); Ps.ReadOutputPoint(1, 2, PointDataTypeConstants.S7_Bit, ref razvorot_kol3);
                Ps.ReadOutputPoint(1, 5, PointDataTypeConstants.S7_Bit, ref povorot_kol4); Ps.ReadOutputPoint(1, 6, PointDataTypeConstants.S7_Bit, ref razvorot_kol4);
                Ps.ReadOutputPoint(2, 1, PointDataTypeConstants.S7_Bit, ref povorot_kol5); Ps.ReadOutputPoint(2, 2, PointDataTypeConstants.S7_Bit, ref razvorot_kol5);
                Ps.ReadOutputPoint(2, 5, PointDataTypeConstants.S7_Bit, ref povorot_kol6); Ps.ReadOutputPoint(2, 6, PointDataTypeConstants.S7_Bit, ref razvorot_kol6);
                //Колонна 1
                    if ((bool)povorot_kol1) {   Ps.WriteInputPoint(0, 6, true);//контактор разворота колонны 1
                                                if (low_kol1 <= 0)              {   low_kol1 = 0;   Ps.WriteInputPoint(0, 5, false);        }
                                                else low_kol1--;                                                                                }
                    if ((bool)razvorot_kol1){   Ps.WriteInputPoint(0, 5, true);//контактор разворота колонны 1
                                                if (low_kol1 >= low_kol_max)    {   low_kol1 = low_kol_max; Ps.WriteInputPoint(0, 6, false);}
                                                else low_kol1++;                                                                                }
                 //Колонна 2
                    if ((bool)povorot_kol2) {   Ps.WriteInputPoint(1, 6, true);//контактор разворота колонны 2
                                                if (low_kol2 <= 0) { low_kol2 = 0; Ps.WriteInputPoint(1, 5, false);                         }
                                                else low_kol2--;                                                                                }
                    if ((bool)razvorot_kol2){   Ps.WriteInputPoint(1, 5, true);//контактор разворота колонны 2
                                                if (low_kol2 >= low_kol_max) { low_kol2 = low_kol_max; Ps.WriteInputPoint(1, 6, false);     }
                                                else low_kol2++;                                                                                }
                //Колонна 3
                    if ((bool)povorot_kol3) {   Ps.WriteInputPoint(2, 6, true);//контактор разворота колонны 3
                                                if (low_kol3 <= 0) { low_kol3 = 0; Ps.WriteInputPoint(2, 5, false);                         }
                                                else low_kol3--;                                                                                }
                    if ((bool)razvorot_kol3){   Ps.WriteInputPoint(2, 5, true);//контактор разворота колонны 3
                                                if (low_kol3 >= low_kol_max) { low_kol3 = low_kol_max; Ps.WriteInputPoint(2, 6, false);     }
                                                else low_kol3++;                                                                                }
                //Колонна 4
                    if ((bool)povorot_kol4) {   Ps.WriteInputPoint(3, 6, true);//контактор разворота колонны 4
                                                if (low_kol4 <= 0) { low_kol4 = 0; Ps.WriteInputPoint(3, 5, false);                         }
                                                else low_kol4--;                                                                                }
                    if ((bool)razvorot_kol4){   Ps.WriteInputPoint(3, 5, true);//контактор разворота колонны 4
                                                if (low_kol4 >= low_kol_max) { low_kol4 = low_kol_max; Ps.WriteInputPoint(3, 6, false);     }
                                                else low_kol4++;                                                                                }
                //Колонна 5
                    if ((bool)povorot_kol5) {   Ps.WriteInputPoint(4, 6, true);//контактор разворота колонны 5
                                                if (low_kol5 <= 0) { low_kol5 = 0; Ps.WriteInputPoint(4, 5, false);                         }
                                                else low_kol5--;                                                                                }
                    if ((bool)razvorot_kol5){   Ps.WriteInputPoint(4, 5, true);//контактор разворота колонны 5
                                                if (low_kol5 >= low_kol_max) { low_kol5 = low_kol_max; Ps.WriteInputPoint(4, 6, false);     }
                                                else low_kol5++;                                                                                }
                //Колонна 6
                    if ((bool)povorot_kol6) {   Ps.WriteInputPoint(5, 6, true);//контактор разворота колонны 6
                                                if (low_kol6 <= 0) { low_kol6 = 0; Ps.WriteInputPoint(5, 5, false);                         }
                                                else low_kol6--;                                                                                }
                    if ((bool)razvorot_kol6){   Ps.WriteInputPoint(5, 5, true);//контактор разворота колонны 6
                                                if (low_kol6 >= low_kol_max) { low_kol6 = low_kol_max; Ps.WriteInputPoint(5, 6, false);     }
                                                else low_kol6++;                                                                                }
                //Чтение выходов управления движением лебедок
                object motion_leb1 = true, motion_leb2 = true, motion_leb3 = true, motion_leb4 = true, motion_leb5 = true, motion_leb6 = true;
                object speed_leb1 = true, speed_leb2 = true, speed_leb3 = true, speed_leb4 = true, speed_leb5 = true, speed_leb6 = true;
                Ps.ReadOutputPoint(135, 2, PointDataTypeConstants.S7_Bit, ref motion_leb1); Ps.ReadOutputPoint(141, 2, PointDataTypeConstants.S7_Bit, ref motion_leb2);
                Ps.ReadOutputPoint(147, 2, PointDataTypeConstants.S7_Bit, ref motion_leb3); Ps.ReadOutputPoint(153, 2, PointDataTypeConstants.S7_Bit, ref motion_leb4);
                Ps.ReadOutputPoint(159, 2, PointDataTypeConstants.S7_Bit, ref motion_leb5); Ps.ReadOutputPoint(165, 2, PointDataTypeConstants.S7_Bit, ref motion_leb6);
                Ps.ReadOutputPoint(136, 0, PointDataTypeConstants.S7_Word, ref speed_leb1); Ps.ReadOutputPoint(142, 0, PointDataTypeConstants.S7_Word, ref speed_leb2);
                Ps.ReadOutputPoint(148, 0, PointDataTypeConstants.S7_Word, ref speed_leb3); Ps.ReadOutputPoint(154, 0, PointDataTypeConstants.S7_Word, ref speed_leb4);
                Ps.ReadOutputPoint(160, 0, PointDataTypeConstants.S7_Word, ref speed_leb5); Ps.ReadOutputPoint(166, 0, PointDataTypeConstants.S7_Word, ref speed_leb6);
                bool UP1 = false, DOWN1 = false, UP2 = false, DOWN2 = false, UP3 = false, DOWN3 = false, UP4 = false, DOWN4 = false, UP5 = false, DOWN5 = false, UP6 = false, DOWN6 = false;
                if ((bool)motion_leb1)
                {
                    if ((visota_leb1 >= 10600) && ((Int16)speed_leb1 > 0)) visota_leb1 = 10600;
                    else if ((visota_leb1 < -600) && ((Int16)speed_leb1 < 0)) visota_leb1 = -600;
                    else if ((Int16)speed_leb1 > 0) { visota_leb1 += (Int16)speed_leb1 / 1000; UP1 = true; DOWN1 = false; }
                    else if ((Int16)speed_leb1 < 0) { visota_leb1 += (Int16)speed_leb1 / 1000; UP1 = false; DOWN1 = true; }
                    Ps.WriteInputPoint(62, 0, visota_leb1);
                }
                if ((bool)motion_leb2)
                {
                    if ((visota_leb2 >= 10600) && ((Int16)speed_leb2 > 0)) visota_leb2 = 10600;
                    else if ((visota_leb2 < -600) && ((Int16)speed_leb2 < 0)) visota_leb2 = -600;
                    else if ((Int16)speed_leb2 > 0) { visota_leb2 += (Int16)speed_leb2 / 1000; UP2 = true; DOWN2 = false; }
                    else if ((Int16)speed_leb2 < 0) { visota_leb2 += (Int16)speed_leb2 / 1000; UP2 = false; DOWN2 = true; }
                    Ps.WriteInputPoint(74, 0, visota_leb2);
                }
                if ((bool)motion_leb3)
                {
                    if ((visota_leb3 >= 10600) && ((Int16)speed_leb3 > 0)) visota_leb3 = 10600;
                    else if ((visota_leb3 < -600) && ((Int16)speed_leb3 < 0)) visota_leb3 = -600;
                    else if ((Int16)speed_leb3 > 0) { visota_leb3 += (Int16)speed_leb3 / 1000; UP3 = true; DOWN3 = false; }
                    else if ((Int16)speed_leb3 < 0) { visota_leb3 += (Int16)speed_leb3 / 1000; UP3 = false; DOWN3 = true; }
                    Ps.WriteInputPoint(86, 0, visota_leb3);
                }
                if ((bool)motion_leb4)
                {
                    if ((visota_leb4 >= 10600) && ((Int16)speed_leb4 > 0)) visota_leb4 = 10600;
                    else if ((visota_leb4 < -600) && ((Int16)speed_leb4 < 0)) visota_leb4 = -600;
                    else if ((Int16)speed_leb4 > 0) { visota_leb4 += (Int16)speed_leb4 / 1000; UP4 = true; DOWN4 = false; }
                    else if ((Int16)speed_leb4 < 0) { visota_leb4 += (Int16)speed_leb4 / 1000; UP4 = false; DOWN4 = true; }
                    Ps.WriteInputPoint(98, 0, visota_leb4);
                }
                if ((bool)motion_leb5)
                {
                    if ((visota_leb5 >= 10600) && ((Int16)speed_leb5 > 0)) visota_leb5 = 10600;
                    else if ((visota_leb5 < -600) && ((Int16)speed_leb5 < 0)) visota_leb5 = -600;
                    else if ((Int16)speed_leb5 > 0) { visota_leb5 += (Int16)speed_leb5 / 1000; UP5 = true; DOWN5 = false; }
                    else if ((Int16)speed_leb5 < 0) { visota_leb5 += (Int16)speed_leb5 / 1000; UP5 = false; DOWN5 = true; }
                    Ps.WriteInputPoint(110, 0, visota_leb5);
                }
                if ((bool)motion_leb6)
                {
                    if ((visota_leb6 >= 10600) && ((Int16)speed_leb6 > 0)) visota_leb6 = 10600;
                    else if ((visota_leb6 < -600) && ((Int16)speed_leb6 < 0)) visota_leb6 = -600;
                    else if ((Int16)speed_leb6 > 0) { visota_leb6 += (Int16)speed_leb6 / 1000; UP6 = true; DOWN6 = false; }
                    else if ((Int16)speed_leb6 < 0) { visota_leb6 += (Int16)speed_leb6 / 1000; UP6 = false; DOWN6 = true; }
                    Ps.WriteInputPoint(122, 0, visota_leb6);
                }
                if (UP1 && (WL1_kg < WeightLeb1_kg)) { WL1_kg++; } if (UP2 && (WL2_kg < WeightLeb2_kg)) { WL2_kg++; } if (UP3 && (WL3_kg < WeightLeb3_kg)) { WL3_kg++; }
                if (UP4 && (WL4_kg < WeightLeb4_kg)) { WL4_kg++; } if (UP5 && (WL5_kg < WeightLeb5_kg)) { WL5_kg++; } if (UP6 && (WL6_kg < WeightLeb6_kg)) { WL6_kg++; }
                if (DOWN1 && (WL1_kg > 0)) { WL1_kg--; } if (DOWN2 && (WL2_kg > 0)) { WL2_kg--; } if (DOWN3 && (WL3_kg > 0)) { WL3_kg--; }
                if (DOWN4 && (WL4_kg > 0)) { WL4_kg--; } if (DOWN5 && (WL5_kg > 0)) { WL5_kg--; } if (DOWN6 && (WL6_kg > 0)) { WL6_kg--; }
                Ps.WriteInputPoint(16, 0, WL1_kg); Ps.WriteInputPoint(24, 0, WL2_kg); Ps.WriteInputPoint(32, 0, WL3_kg);
                Ps.WriteInputPoint(40, 0, WL4_kg); Ps.WriteInputPoint(48, 0, WL5_kg); Ps.WriteInputPoint(56, 0, WL6_kg);
            }
        }

        private void cb_Xx_x_CheckedChanged(object sender, EventArgs e)
        {
            object Xx_x = ((CheckBox) sender).Checked; //checkBox_I0_0.Checked;
            string textT = ((CheckBox) sender).Text;
            int intTindex = textT.IndexOf(".", StringComparison.Ordinal);
            textBox1.Text = textT;
            string charsCompare = textT.Substring(0, 2);
            switch (charsCompare)
            {
                case "ID":
                    break;
                case "IW":
                    break;
                case "IB":
                    break;
                case "MD":
                    break;
                case "MW":
                    break;
                case "MB":
                    break;
                default:
                    string charCompare = textT.Substring(0, 1);
                    switch (charCompare)
                    {
                        case "I":
                            int intT1 = Convert.ToInt16(textT.Substring(1, intTindex - 1));
                            int intT2 = Convert.ToInt16(textT.Substring(intTindex + 1, 1));
                            Ps.WriteInputPoint(intT1, intT2, ref Xx_x);
                            break;
                        case "M":
                            break;
                    }
                    break;
            }
        }

        private void cb_I0_0_CheckedChanged(object sender, EventArgs e)
        {
            //object I0_0 = checkBox_I0_0.Checked;
            object I0_0 = ((CheckBox) sender).Checked; //checkBox_I0_0.Checked;
            Ps.WriteInputPoint(0, 0, ref I0_0);
        }

        private void checkBox_I0_1_CheckedChanged(object sender, EventArgs e)
        {
            object I0_1 = checkBox_I0_1.Checked;
            Ps.WriteInputPoint(0, 1, ref I0_1);
        }

        private void checkBox_I0_2_CheckedChanged(object sender, EventArgs e)
        {
            object I0_2 = checkBox_I0_2.Checked;
            Ps.WriteInputPoint(0, 2, ref I0_2);
        }

        private void checkBox_I0_3_CheckedChanged(object sender, EventArgs e)
        {
            object I0_3 = checkBox_I0_3.Checked;
            Ps.WriteInputPoint(0, 3, ref I0_3);
        }

        private void checkBox_I0_4_CheckedChanged(object sender, EventArgs e)
        {
            object I0_4 = checkBox_I0_4.Checked;
            Ps.WriteInputPoint(0, 4, ref I0_4);
        }

        private void checkBox_I0_5_CheckedChanged(object sender, EventArgs e)
        {
            object I0_5 = checkBox_I0_5.Checked;
            Ps.WriteInputPoint(0, 5, ref I0_5);
        }

        private void checkBox_I0_6_CheckedChanged(object sender, EventArgs e)
        {
            object I0_6 = checkBox_I0_6.Checked;
            Ps.WriteInputPoint(0, 6, ref I0_6);
        }

        private void checkBox_I0_7_CheckedChanged(object sender, EventArgs e)
        {
            object I0_7 = checkBox_I0_7.Checked;
            Ps.WriteInputPoint(0, 7, ref I0_7);
        }

        private void checkBox_I1_0_CheckedChanged(object sender, EventArgs e)
        {
            object I1_0 = checkBox_I1_0.Checked;
            Ps.WriteInputPoint(1, 0, ref I1_0);
        }

        private void checkBox_I1_1_CheckedChanged(object sender, EventArgs e)
        {
            object I1_1 = checkBox_I1_1.Checked;
            Ps.WriteInputPoint(1, 1, ref I1_1);
        }

        private void checkBox_I1_2_CheckedChanged(object sender, EventArgs e)
        {
            object I1_2 = checkBox_I1_2.Checked;
            Ps.WriteInputPoint(1, 2, ref I1_2);
        }

        private void checkBox_I1_3_CheckedChanged(object sender, EventArgs e)
        {
            object I1_3 = checkBox_I1_3.Checked;
            Ps.WriteInputPoint(1, 3, ref I1_3);
        }

        private void checkBox_I1_4_CheckedChanged(object sender, EventArgs e)
        {
            object I1_4 = checkBox_I1_4.Checked;
            Ps.WriteInputPoint(1, 4, ref I1_4);
        }

        private void checkBox_I1_5_CheckedChanged(object sender, EventArgs e)
        {
            object I1_5 = checkBox_I1_5.Checked;
            Ps.WriteInputPoint(1, 5, ref I1_5);
        }

        private void checkBox_I1_6_CheckedChanged(object sender, EventArgs e)
        {
            object I1_6 = checkBox_I1_6.Checked;
            Ps.WriteInputPoint(1, 6, ref I1_6);
        }

        private void checkBox_I1_7_CheckedChanged(object sender, EventArgs e)
        {
            object I1_7 = checkBox_I1_7.Checked;
            Ps.WriteInputPoint(1, 7, ref I1_7);
        }

        private void checkBox_I2_0_CheckedChanged(object sender, EventArgs e)
        {
            object I2_0 = checkBox_I2_0.Checked;
            Ps.WriteInputPoint(2, 0, ref I2_0);
        }

        private void checkBox_I2_1_CheckedChanged(object sender, EventArgs e)
        {
            object I2_1 = checkBox_I2_1.Checked;
            Ps.WriteInputPoint(2, 1, ref I2_1);
        }

        private void checkBox_I2_2_CheckedChanged(object sender, EventArgs e)
        {
            object I2_2 = checkBox_I2_2.Checked;
            Ps.WriteInputPoint(2, 2, ref I2_2);
        }

        private void checkBox_I2_3_CheckedChanged(object sender, EventArgs e)
        {
            object I2_3 = checkBox_I2_3.Checked;
            Ps.WriteInputPoint(2, 3, ref I2_3);
        }

        private void checkBox_I2_4_CheckedChanged(object sender, EventArgs e)
        {
            object I2_4 = checkBox_I2_4.Checked;
            Ps.WriteInputPoint(2, 4, ref I2_4);
        }

        private void checkBox_I2_5_CheckedChanged(object sender, EventArgs e)
        {
            object I2_5 = checkBox_I2_5.Checked;
            Ps.WriteInputPoint(2, 5, ref I2_5);
        }

        private void checkBox_I2_6_CheckedChanged(object sender, EventArgs e)
        {
            object I2_6 = checkBox_I2_6.Checked;
            Ps.WriteInputPoint(2, 6, ref I2_6);
        }

        private void checkBox_I2_7_CheckedChanged(object sender, EventArgs e)
        {
            object I2_7 = checkBox_I2_7.Checked;
            Ps.WriteInputPoint(2, 7, ref I2_7);
        }

        private void checkBox_I3_0_CheckedChanged(object sender, EventArgs e)
        {
            object I3_0 = checkBox_I3_0.Checked;
            Ps.WriteInputPoint(3, 0, ref I3_0);
        }

        private void checkBox_I3_1_CheckedChanged(object sender, EventArgs e)
        {
            object I3_1 = checkBox_I3_1.Checked;
            Ps.WriteInputPoint(3, 1, ref I3_1);
        }

        private void checkBox_I3_2_CheckedChanged(object sender, EventArgs e)
        {
            object I3_2 = checkBox_I3_2.Checked;
            Ps.WriteInputPoint(3, 2, ref I3_2);
        }

        private void checkBox_I3_3_CheckedChanged(object sender, EventArgs e)
        {
            object I3_3 = checkBox_I3_3.Checked;
            Ps.WriteInputPoint(3, 3, ref I3_3);
        }

        private void checkBox_I3_4_CheckedChanged(object sender, EventArgs e)
        {
            object I3_4 = checkBox_I3_4.Checked;
            Ps.WriteInputPoint(3, 4, ref I3_4);
        }

        private void checkBox_I3_5_CheckedChanged(object sender, EventArgs e)
        {
            object I3_5 = checkBox_I3_5.Checked;
            Ps.WriteInputPoint(3, 5, ref I3_5);
        }

        private void checkBox_I3_6_CheckedChanged(object sender, EventArgs e)
        {
            object I3_6 = checkBox_I3_6.Checked;
            Ps.WriteInputPoint(3, 6, ref I3_6);
        }

        private void checkBox_I3_7_CheckedChanged(object sender, EventArgs e)
        {
            object I3_7 = checkBox_I3_7.Checked;
            Ps.WriteInputPoint(3, 7, ref I3_7);
        }

        private void checkBox_I4_0_CheckedChanged(object sender, EventArgs e)
        {
            object I4_0 = checkBox_I4_0.Checked;
            Ps.WriteInputPoint(4, 0, ref I4_0);
        }

        private void checkBox_I4_1_CheckedChanged(object sender, EventArgs e)
        {
            object I4_1 = checkBox_I4_1.Checked;
            Ps.WriteInputPoint(4, 1, ref I4_1);
        }

        private void checkBox_I4_2_CheckedChanged(object sender, EventArgs e)
        {
            object I4_2 = checkBox_I4_2.Checked;
            Ps.WriteInputPoint(4, 2, ref I4_2);
        }

        private void checkBox_I4_3_CheckedChanged(object sender, EventArgs e)
        {
            object I4_3 = checkBox_I4_3.Checked;
            Ps.WriteInputPoint(4, 3, ref I4_3);
        }

        private void checkBox_I4_4_CheckedChanged(object sender, EventArgs e)
        {
            object I4_4 = checkBox_I4_4.Checked;
            Ps.WriteInputPoint(4, 4, ref I4_4);
        }

        private void checkBox_I4_5_CheckedChanged(object sender, EventArgs e)
        {
            object I4_5 = checkBox_I4_5.Checked;
            Ps.WriteInputPoint(4, 5, ref I4_5);
        }

        private void checkBox_I4_6_CheckedChanged(object sender, EventArgs e)
        {
            object I4_6 = checkBox_I4_6.Checked;
            Ps.WriteInputPoint(4, 6, ref I4_6);
        }

        private void checkBox_I4_7_CheckedChanged(object sender, EventArgs e)
        {
            object I4_7 = checkBox_I4_7.Checked;
            Ps.WriteInputPoint(4, 7, ref I4_7);
        }

        private void checkBox_I5_0_CheckedChanged(object sender, EventArgs e)
        {
            object I5_0 = checkBox_I5_0.Checked;
            Ps.WriteInputPoint(5, 0, ref I5_0);
        }

        private void checkBox_I5_1_CheckedChanged(object sender, EventArgs e)
        {
            object I5_1 = checkBox_I5_1.Checked;
            Ps.WriteInputPoint(5, 1, ref I5_1);
        }

        private void checkBox_I5_2_CheckedChanged(object sender, EventArgs e)
        {
            object I5_2 = checkBox_I5_2.Checked;
            Ps.WriteInputPoint(5, 2, ref I5_2);
        }

        private void checkBox_I5_3_CheckedChanged(object sender, EventArgs e)
        {
            object I5_3 = checkBox_I5_3.Checked;
            Ps.WriteInputPoint(5, 3, ref I5_3);
        }

        private void checkBox_I5_4_CheckedChanged(object sender, EventArgs e)
        {
            object I5_4 = checkBox_I5_4.Checked;
            Ps.WriteInputPoint(5, 4, ref I5_4);
        }

        private void checkBox_I5_5_CheckedChanged(object sender, EventArgs e)
        {
            object I5_5 = checkBox_I5_5.Checked;
            Ps.WriteInputPoint(5, 5, ref I5_5);
        }

        private void checkBox_I5_6_CheckedChanged(object sender, EventArgs e)
        {
            object I5_6 = checkBox_I5_6.Checked;
            Ps.WriteInputPoint(5, 6, ref I5_6);
        }

        private void checkBox_I5_7_CheckedChanged(object sender, EventArgs e)
        {
            object I5_7 = checkBox_I5_7.Checked;
            Ps.WriteInputPoint(5, 7, ref I5_7);
        }

        private void checkBox_I6_0_CheckedChanged(object sender, EventArgs e)
        {
            object I6_0 = checkBox_I6_0.Checked;
            Ps.WriteInputPoint(6, 0, ref I6_0);
        }

        private void checkBox_I6_1_CheckedChanged(object sender, EventArgs e)
        {
            object I6_1 = checkBox_I6_1.Checked;
            Ps.WriteInputPoint(6, 1, ref I6_1);
        }

        private void checkBox_I6_2_CheckedChanged(object sender, EventArgs e)
        {
            object I6_2 = checkBox_I6_2.Checked;
            Ps.WriteInputPoint(6, 2, ref I6_2);
        }

        private void checkBox_I6_3_CheckedChanged(object sender, EventArgs e)
        {
            object I6_3 = checkBox_I6_3.Checked;
            Ps.WriteInputPoint(6, 3, ref I6_3);
        }

        private void checkBox_I6_4_CheckedChanged(object sender, EventArgs e)
        {
            object I6_4 = checkBox_I6_4.Checked;
            Ps.WriteInputPoint(6, 4, ref I6_4);
        }

        private void checkBox_I6_5_CheckedChanged(object sender, EventArgs e)
        {
            object I6_5 = checkBox_I6_5.Checked;
            Ps.WriteInputPoint(6, 5, ref I6_5);
        }

        private void checkBox_I6_6_CheckedChanged(object sender, EventArgs e)
        {
            object I6_6 = checkBox_I6_6.Checked;
            Ps.WriteInputPoint(6, 6, ref I6_6);
        }

        private void checkBox_I6_7_CheckedChanged(object sender, EventArgs e)
        {
            object I6_7 = checkBox_I6_7.Checked;
            Ps.WriteInputPoint(6, 7, ref I6_7);
        }

        private void checkBox_I7_0_CheckedChanged(object sender, EventArgs e)
        {
            object I7_0 = checkBox_I7_0.Checked;
            Ps.WriteInputPoint(7, 0, ref I7_0);
        }

        private void checkBox_I7_1_CheckedChanged(object sender, EventArgs e)
        {
            object I7_1 = checkBox_I7_1.Checked;
            Ps.WriteInputPoint(7, 1, ref I7_1);
        }

        private void checkBox_I7_2_CheckedChanged(object sender, EventArgs e)
        {
            object I7_2 = checkBox_I7_2.Checked;
            Ps.WriteInputPoint(7, 2, ref I7_2);
        }

        private void checkBox_I7_3_CheckedChanged(object sender, EventArgs e)
        {
            object I7_3 = checkBox_I7_3.Checked;
            Ps.WriteInputPoint(7, 3, ref I7_3);
        }

        private void checkBox_I7_4_CheckedChanged(object sender, EventArgs e)
        {
            object I7_4 = checkBox_I7_4.Checked;
            Ps.WriteInputPoint(7, 4, ref I7_4);
        }

        private void checkBox_I7_5_CheckedChanged(object sender, EventArgs e)
        {
            object I7_5 = checkBox_I7_5.Checked;
            Ps.WriteInputPoint(7, 5, ref I7_5);
        }

        private void checkBox_I7_6_CheckedChanged(object sender, EventArgs e)
        {
            object I7_6 = checkBox_I7_6.Checked;
            Ps.WriteInputPoint(7, 6, ref I7_6);
        }

        private void checkBox_I7_7_CheckedChanged(object sender, EventArgs e)
        {
            object I7_7 = checkBox_I7_7.Checked;
            Ps.WriteInputPoint(7, 7, ref I7_7);
        }

        private void checkBox_I8_0_CheckedChanged(object sender, EventArgs e)
        {
            object I8_0 = checkBox_I8_0.Checked;
            Ps.WriteInputPoint(8, 0, ref I8_0);
        }

        private void checkBox_I8_1_CheckedChanged(object sender, EventArgs e)
        {
            object I8_1 = checkBox_I8_1.Checked;
            Ps.WriteInputPoint(8, 1, ref I8_1);
        }

        private void checkBox_I8_2_CheckedChanged(object sender, EventArgs e)
        {
            object I8_2 = checkBox_I8_2.Checked;
            Ps.WriteInputPoint(8, 2, ref I8_2);
        }

        private void checkBox_I8_3_CheckedChanged(object sender, EventArgs e)
        {
            object I8_3 = checkBox_I8_3.Checked;
            Ps.WriteInputPoint(8, 3, ref I8_3);
        }

        private void checkBox_I8_4_CheckedChanged(object sender, EventArgs e)
        {
            object I8_4 = checkBox_I8_4.Checked;
            Ps.WriteInputPoint(8, 4, ref I8_4);
        }

        private void checkBox_I8_5_CheckedChanged(object sender, EventArgs e)
        {
            object I8_5 = checkBox_I8_5.Checked;
            Ps.WriteInputPoint(8, 5, ref I8_5);
        }

        private void checkBox_I8_6_CheckedChanged(object sender, EventArgs e)
        {
            object I8_6 = checkBox_I8_6.Checked;
            Ps.WriteInputPoint(8, 6, ref I8_6);
        }

        private void checkBox_I8_7_CheckedChanged(object sender, EventArgs e)
        {
            object I8_7 = checkBox_I8_7.Checked;
            Ps.WriteInputPoint(8, 7, ref I8_7);
        }

        private void checkBox_I9_0_CheckedChanged(object sender, EventArgs e)
        {
            object I9_0 = checkBox_I9_0.Checked;
            Ps.WriteInputPoint(9, 0, ref I9_0);
        }

        private void checkBox_I9_1_CheckedChanged(object sender, EventArgs e)
        {
            object I9_1 = checkBox_I9_1.Checked;
            Ps.WriteInputPoint(9, 1, ref I9_1);
        }

        private void checkBox_I9_2_CheckedChanged(object sender, EventArgs e)
        {
            object I9_2 = checkBox_I9_2.Checked;
            Ps.WriteInputPoint(9, 2, ref I9_2);
        }

        private void checkBox_I9_3_CheckedChanged(object sender, EventArgs e)
        {
            object I9_3 = checkBox_I9_3.Checked;
            Ps.WriteInputPoint(9, 3, ref I9_3);
        }

        private void checkBox_I9_4_CheckedChanged(object sender, EventArgs e)
        {
            object I9_4 = checkBox_I9_4.Checked;
            Ps.WriteInputPoint(9, 4, ref I9_4);
        }

        private void checkBox_I9_5_CheckedChanged(object sender, EventArgs e)
        {
            object I9_5 = checkBox_I9_5.Checked;
            Ps.WriteInputPoint(9, 5, ref I9_5);
        }

        private void checkBox_I9_6_CheckedChanged(object sender, EventArgs e)
        {
            object I9_6 = checkBox_I9_6.Checked;
            Ps.WriteInputPoint(9, 6, ref I9_6);
        }

        private void checkBox_I9_7_CheckedChanged(object sender, EventArgs e)
        {
            object I9_7 = checkBox_I9_7.Checked;
            Ps.WriteInputPoint(9, 7, ref I9_7);
        }

        private void checkBox_M0_0_CheckedChanged(object sender, EventArgs e)
        {
            object M0_0 = checkBox_M0_0.Checked;
            Ps.WriteFlagValue(0, 0, ref M0_0);
        }

        private void timer_Read_Q_Tick(object sender, EventArgs e)
        {
            object Q0_0 = true; //initial value
            object Q0_1 = true; //initial value
            object Q0_2 = true; //initial value
            object Q0_3 = true; //initial value
            object Q0_4 = true; //initial value
            object Q0_5 = true; //initial value
            object Q0_6 = true; //initial value
            object Q0_7 = true; //initial value
            object Q1_0 = true; //initial value
            object Q1_1 = true; //initial value
            object Q1_2 = true; //initial value
            object Q1_3 = true; //initial value
            object Q1_4 = true; //initial value
            object Q1_5 = true; //initial value
            object Q1_6 = true; //initial value
            object Q1_7 = true; //initial value
            object Q2_0 = true; //initial value
            object Q2_1 = true; //initial value
            object Q2_2 = true; //initial value
            object Q2_3 = true; //initial value
            object Q2_4 = true; //initial value
            object Q2_5 = true; //initial value
            object Q2_6 = true; //initial value
            object Q2_7 = true; //initial value
            object Q3_0 = true; //initial value
            object Q3_1 = true; //initial value
            object Q3_2 = true; //initial value
            object Q3_3 = true; //initial value
            object Q3_4 = true; //initial value
            object Q3_5 = true; //initial value
            object Q3_6 = true; //initial value
            object Q3_7 = true; //initial value
            object M0_0 = true; //initial value
            object M0_1 = true; //initial value
            object M0_2 = true; //initial value
            object M0_3 = true; //initial value
            object M0_4 = true; //initial value
            object M0_5 = true; //initial value
            object M0_6 = true; //initial value
            object M0_7 = true; //initial value
            object M1_0 = true; //initial value
            object M1_1 = true; //initial value
            object M1_2 = true; //initial value
            object M1_3 = true; //initial value
            object M1_4 = true; //initial value
            object M1_5 = true; //initial value
            object M1_6 = true; //initial value
            object M1_7 = true; //initial value
            object M2_0 = true; //initial value
            object M2_1 = true; //initial value
            object M2_2 = true; //initial value
            object M2_3 = true; //initial value
            object M2_4 = true; //initial value
            object M2_5 = true; //initial value
            object M2_6 = true; //initial value
            object M2_7 = true; //initial value
            object M3_0 = true; //initial value
            object M3_1 = true; //initial value
            object M3_2 = true; //initial value
            object M3_3 = true; //initial value
            object M3_4 = true; //initial value
            object M3_5 = true; //initial value
            object M3_6 = true; //initial value
            object M3_7 = true; //initial value
            object M4_0 = true; //initial value
            object M4_1 = true; //initial value
            object M4_2 = true; //initial value
            object M4_3 = true; //initial value
            object M4_4 = true; //initial value
            object M4_5 = true; //initial value
            object M4_6 = true; //initial value
            object M4_7 = true; //initial value
            object M5_0 = true; //initial value
            object M5_1 = true; //initial value
            object M5_2 = true; //initial value
            object M5_3 = true; //initial value
            object M5_4 = true; //initial value
            object M5_5 = true; //initial value
            object M5_6 = true; //initial value
            object M5_7 = true; //initial value
            object M6_0 = true; //initial value
            object M6_1 = true; //initial value
            object M6_2 = true; //initial value
            object M6_3 = true; //initial value
            object M6_4 = true; //initial value
            object M6_5 = true; //initial value
            object M6_6 = true; //initial value
            object M6_7 = true; //initial value
            object M7_0 = true; //initial value
            object M7_1 = true; //initial value
            object M7_2 = true; //initial value
            object M7_3 = true; //initial value
            object M7_4 = true; //initial value
            object M7_5 = true; //initial value
            object M7_6 = true; //initial value
            object M7_7 = true; //initial value
            object M8_0 = true; //initial value
            object M8_1 = true; //initial value
            object M8_2 = true; //initial value
            object M8_3 = true; //initial value
            object M8_4 = true; //initial value
            object M8_5 = true; //initial value
            object M8_6 = true; //initial value
            object M8_7 = true; //initial value
            object M9_0 = true; //initial value
            object M9_1 = true; //initial value
            object M9_2 = true; //initial value
            object M9_3 = true; //initial value
            object M9_4 = true; //initial value
            object M9_5 = true; //initial value
            object M9_6 = true; //initial value
            object M9_7 = true; //initial value
            object DB1_DBX0_0 = false;//initial value
            Ps.ReadOutputPoint(0, 0, PointDataTypeConstants.S7_Bit, ref Q0_0);
            Ps.ReadOutputPoint(0, 1, PointDataTypeConstants.S7_Bit, ref Q0_1);
            Ps.ReadOutputPoint(0, 2, PointDataTypeConstants.S7_Bit, ref Q0_2);
            Ps.ReadOutputPoint(0, 3, PointDataTypeConstants.S7_Bit, ref Q0_3);
            Ps.ReadOutputPoint(0, 4, PointDataTypeConstants.S7_Bit, ref Q0_4);
            Ps.ReadOutputPoint(0, 5, PointDataTypeConstants.S7_Bit, ref Q0_5);
            Ps.ReadOutputPoint(0, 6, PointDataTypeConstants.S7_Bit, ref Q0_6);
            Ps.ReadOutputPoint(0, 7, PointDataTypeConstants.S7_Bit, ref Q0_7);
            Ps.ReadOutputPoint(1, 0, PointDataTypeConstants.S7_Bit, ref Q1_0);
            Ps.ReadOutputPoint(1, 1, PointDataTypeConstants.S7_Bit, ref Q1_1);
            Ps.ReadOutputPoint(1, 2, PointDataTypeConstants.S7_Bit, ref Q1_2);
            Ps.ReadOutputPoint(1, 3, PointDataTypeConstants.S7_Bit, ref Q1_3);
            Ps.ReadOutputPoint(1, 4, PointDataTypeConstants.S7_Bit, ref Q1_4);
            Ps.ReadOutputPoint(1, 5, PointDataTypeConstants.S7_Bit, ref Q1_5);
            Ps.ReadOutputPoint(1, 6, PointDataTypeConstants.S7_Bit, ref Q1_6);
            Ps.ReadOutputPoint(1, 7, PointDataTypeConstants.S7_Bit, ref Q1_7);
            Ps.ReadOutputPoint(2, 0, PointDataTypeConstants.S7_Bit, ref Q2_0);
            Ps.ReadOutputPoint(2, 1, PointDataTypeConstants.S7_Bit, ref Q2_1);
            Ps.ReadOutputPoint(2, 2, PointDataTypeConstants.S7_Bit, ref Q2_2);
            Ps.ReadOutputPoint(2, 3, PointDataTypeConstants.S7_Bit, ref Q2_3);
            Ps.ReadOutputPoint(2, 4, PointDataTypeConstants.S7_Bit, ref Q2_4);
            Ps.ReadOutputPoint(2, 5, PointDataTypeConstants.S7_Bit, ref Q2_5);
            Ps.ReadOutputPoint(2, 6, PointDataTypeConstants.S7_Bit, ref Q2_6);
            Ps.ReadOutputPoint(2, 7, PointDataTypeConstants.S7_Bit, ref Q2_7);
            Ps.ReadOutputPoint(3, 0, PointDataTypeConstants.S7_Bit, ref Q3_0);
            Ps.ReadOutputPoint(3, 1, PointDataTypeConstants.S7_Bit, ref Q3_1);
            Ps.ReadOutputPoint(3, 2, PointDataTypeConstants.S7_Bit, ref Q3_2);
            Ps.ReadOutputPoint(3, 3, PointDataTypeConstants.S7_Bit, ref Q3_3);
            Ps.ReadOutputPoint(3, 4, PointDataTypeConstants.S7_Bit, ref Q3_4);
            Ps.ReadOutputPoint(3, 5, PointDataTypeConstants.S7_Bit, ref Q3_5);
            Ps.ReadOutputPoint(3, 6, PointDataTypeConstants.S7_Bit, ref Q3_6);
            Ps.ReadOutputPoint(3, 7, PointDataTypeConstants.S7_Bit, ref Q3_7);
            Ps.ReadFlagValue(0, 0, PointDataTypeConstants.S7_Bit, ref M0_0);
            Ps.ReadFlagValue(0, 1, PointDataTypeConstants.S7_Bit, ref M0_1);
            Ps.ReadFlagValue(0, 2, PointDataTypeConstants.S7_Bit, ref M0_2);
            Ps.ReadFlagValue(0, 3, PointDataTypeConstants.S7_Bit, ref M0_3);
            Ps.ReadFlagValue(0, 4, PointDataTypeConstants.S7_Bit, ref M0_4);
            Ps.ReadFlagValue(0, 5, PointDataTypeConstants.S7_Bit, ref M0_5);
            Ps.ReadFlagValue(0, 6, PointDataTypeConstants.S7_Bit, ref M0_6);
            Ps.ReadFlagValue(0, 7, PointDataTypeConstants.S7_Bit, ref M0_7);
            Ps.ReadFlagValue(1, 0, PointDataTypeConstants.S7_Bit, ref M1_0);
            Ps.ReadFlagValue(1, 1, PointDataTypeConstants.S7_Bit, ref M1_1);
            Ps.ReadFlagValue(1, 2, PointDataTypeConstants.S7_Bit, ref M1_2);
            Ps.ReadFlagValue(1, 3, PointDataTypeConstants.S7_Bit, ref M1_3);
            Ps.ReadFlagValue(1, 4, PointDataTypeConstants.S7_Bit, ref M1_4);
            Ps.ReadFlagValue(1, 5, PointDataTypeConstants.S7_Bit, ref M1_5);
            Ps.ReadFlagValue(1, 6, PointDataTypeConstants.S7_Bit, ref M1_6);
            Ps.ReadFlagValue(1, 7, PointDataTypeConstants.S7_Bit, ref M1_7);
            Ps.ReadFlagValue(2, 0, PointDataTypeConstants.S7_Bit, ref M2_0);
            Ps.ReadFlagValue(2, 1, PointDataTypeConstants.S7_Bit, ref M2_1);
            Ps.ReadFlagValue(2, 2, PointDataTypeConstants.S7_Bit, ref M2_2);
            Ps.ReadFlagValue(2, 3, PointDataTypeConstants.S7_Bit, ref M2_3);
            Ps.ReadFlagValue(2, 4, PointDataTypeConstants.S7_Bit, ref M2_4);
            Ps.ReadFlagValue(2, 5, PointDataTypeConstants.S7_Bit, ref M2_5);
            Ps.ReadFlagValue(2, 6, PointDataTypeConstants.S7_Bit, ref M2_6);
            Ps.ReadFlagValue(2, 7, PointDataTypeConstants.S7_Bit, ref M2_7);
            Ps.ReadFlagValue(3, 0, PointDataTypeConstants.S7_Bit, ref M3_0);
            Ps.ReadFlagValue(3, 1, PointDataTypeConstants.S7_Bit, ref M3_1);
            Ps.ReadFlagValue(3, 2, PointDataTypeConstants.S7_Bit, ref M3_2);
            Ps.ReadFlagValue(3, 3, PointDataTypeConstants.S7_Bit, ref M3_3);
            Ps.ReadFlagValue(3, 4, PointDataTypeConstants.S7_Bit, ref M3_4);
            Ps.ReadFlagValue(3, 5, PointDataTypeConstants.S7_Bit, ref M3_5);
            Ps.ReadFlagValue(3, 6, PointDataTypeConstants.S7_Bit, ref M3_6);
            Ps.ReadFlagValue(3, 7, PointDataTypeConstants.S7_Bit, ref M3_7);
            Ps.ReadFlagValue(4, 0, PointDataTypeConstants.S7_Bit, ref M4_0);
            Ps.ReadFlagValue(4, 1, PointDataTypeConstants.S7_Bit, ref M4_1);
            Ps.ReadFlagValue(4, 2, PointDataTypeConstants.S7_Bit, ref M4_2);
            Ps.ReadFlagValue(4, 3, PointDataTypeConstants.S7_Bit, ref M4_3);
            Ps.ReadFlagValue(4, 4, PointDataTypeConstants.S7_Bit, ref M4_4);
            Ps.ReadFlagValue(4, 5, PointDataTypeConstants.S7_Bit, ref M4_5);
            Ps.ReadFlagValue(4, 6, PointDataTypeConstants.S7_Bit, ref M4_6);
            Ps.ReadFlagValue(4, 7, PointDataTypeConstants.S7_Bit, ref M4_7);
            Ps.ReadFlagValue(5, 0, PointDataTypeConstants.S7_Bit, ref M5_0);
            Ps.ReadFlagValue(5, 1, PointDataTypeConstants.S7_Bit, ref M5_1);
            Ps.ReadFlagValue(5, 2, PointDataTypeConstants.S7_Bit, ref M5_2);
            Ps.ReadFlagValue(5, 3, PointDataTypeConstants.S7_Bit, ref M5_3);
            Ps.ReadFlagValue(5, 4, PointDataTypeConstants.S7_Bit, ref M5_4);
            Ps.ReadFlagValue(5, 5, PointDataTypeConstants.S7_Bit, ref M5_5);
            Ps.ReadFlagValue(5, 6, PointDataTypeConstants.S7_Bit, ref M5_6);
            Ps.ReadFlagValue(5, 7, PointDataTypeConstants.S7_Bit, ref M5_7);
            Ps.ReadFlagValue(6, 0, PointDataTypeConstants.S7_Bit, ref M6_0);
            Ps.ReadFlagValue(6, 1, PointDataTypeConstants.S7_Bit, ref M6_1);
            Ps.ReadFlagValue(6, 2, PointDataTypeConstants.S7_Bit, ref M6_2);
            Ps.ReadFlagValue(6, 3, PointDataTypeConstants.S7_Bit, ref M6_3);
            Ps.ReadFlagValue(6, 4, PointDataTypeConstants.S7_Bit, ref M6_4);
            Ps.ReadFlagValue(6, 5, PointDataTypeConstants.S7_Bit, ref M6_5);
            Ps.ReadFlagValue(6, 6, PointDataTypeConstants.S7_Bit, ref M6_6);
            Ps.ReadFlagValue(6, 7, PointDataTypeConstants.S7_Bit, ref M6_7);
            Ps.ReadFlagValue(7, 0, PointDataTypeConstants.S7_Bit, ref M7_0);
            Ps.ReadFlagValue(7, 1, PointDataTypeConstants.S7_Bit, ref M7_1);
            Ps.ReadFlagValue(7, 2, PointDataTypeConstants.S7_Bit, ref M7_2);
            Ps.ReadFlagValue(7, 3, PointDataTypeConstants.S7_Bit, ref M7_3);
            Ps.ReadFlagValue(7, 4, PointDataTypeConstants.S7_Bit, ref M7_4);
            Ps.ReadFlagValue(7, 5, PointDataTypeConstants.S7_Bit, ref M7_5);
            Ps.ReadFlagValue(7, 6, PointDataTypeConstants.S7_Bit, ref M7_6);
            Ps.ReadFlagValue(7, 7, PointDataTypeConstants.S7_Bit, ref M7_7);
            Ps.ReadFlagValue(8, 0, PointDataTypeConstants.S7_Bit, ref M8_0);
            Ps.ReadFlagValue(8, 1, PointDataTypeConstants.S7_Bit, ref M8_1);
            Ps.ReadFlagValue(8, 2, PointDataTypeConstants.S7_Bit, ref M8_2);
            Ps.ReadFlagValue(8, 3, PointDataTypeConstants.S7_Bit, ref M8_3);
            Ps.ReadFlagValue(8, 4, PointDataTypeConstants.S7_Bit, ref M8_4);
            Ps.ReadFlagValue(8, 5, PointDataTypeConstants.S7_Bit, ref M8_5);
            Ps.ReadFlagValue(8, 6, PointDataTypeConstants.S7_Bit, ref M8_6);
            Ps.ReadFlagValue(8, 7, PointDataTypeConstants.S7_Bit, ref M8_7);
            Ps.ReadFlagValue(9, 0, PointDataTypeConstants.S7_Bit, ref M9_0);
            Ps.ReadFlagValue(9, 1, PointDataTypeConstants.S7_Bit, ref M9_1);
            Ps.ReadFlagValue(9, 2, PointDataTypeConstants.S7_Bit, ref M9_2);
            Ps.ReadFlagValue(9, 3, PointDataTypeConstants.S7_Bit, ref M9_3);
            Ps.ReadFlagValue(9, 4, PointDataTypeConstants.S7_Bit, ref M9_4);
            Ps.ReadFlagValue(9, 5, PointDataTypeConstants.S7_Bit, ref M9_5);
            Ps.ReadFlagValue(9, 6, PointDataTypeConstants.S7_Bit, ref M9_6);
            Ps.ReadFlagValue(9, 7, PointDataTypeConstants.S7_Bit, ref M9_7);
            Ps.ReadDataBlockValue(1, 0, 0, PointDataTypeConstants.S7_Bit, ref DB1_DBX0_0);
            checkBox_Q0_0.Checked = (bool)Q0_0;
            checkBox_Q0_1.Checked = (bool)Q0_1;
            checkBox_Q0_2.Checked = (bool)Q0_2;
            checkBox_Q0_3.Checked = (bool)Q0_3;
            checkBox_Q0_4.Checked = (bool)Q0_4;
            checkBox_Q0_5.Checked = (bool)Q0_5;
            checkBox_Q0_6.Checked = (bool)Q0_6;
            checkBox_Q0_7.Checked = (bool)Q0_7;
            checkBox_Q1_0.Checked = (bool)Q1_0;
            checkBox_Q1_1.Checked = (bool)Q1_1;
            checkBox_Q1_2.Checked = (bool)Q1_2;
            checkBox_Q1_3.Checked = (bool)Q1_3;
            checkBox_Q1_4.Checked = (bool)Q1_4;
            checkBox_Q1_5.Checked = (bool)Q1_5;
            checkBox_Q1_6.Checked = (bool)Q1_6;
            checkBox_Q1_7.Checked = (bool)Q1_7;
            checkBox_Q2_0.Checked = (bool)Q2_0;
            checkBox_Q2_1.Checked = (bool)Q2_1;
            checkBox_Q2_2.Checked = (bool)Q2_2;
            checkBox_Q2_3.Checked = (bool)Q2_3;
            checkBox_Q2_4.Checked = (bool)Q2_4;
            checkBox_Q2_5.Checked = (bool)Q2_5;
            checkBox_Q2_6.Checked = (bool)Q2_6;
            checkBox_Q2_7.Checked = (bool)Q2_7;
            checkBox_Q3_0.Checked = (bool)Q3_0;
            checkBox_Q3_1.Checked = (bool)Q3_1;
            checkBox_Q3_2.Checked = (bool)Q3_2;
            checkBox_Q3_3.Checked = (bool)Q3_3;
            checkBox_Q3_4.Checked = (bool)Q3_4;
            checkBox_Q3_5.Checked = (bool)Q3_5;
            checkBox_Q3_6.Checked = (bool)Q3_6;
            checkBox_Q3_7.Checked = (bool)Q3_7;
            checkBox_M0_0.Checked = (bool)M0_0;
            checkBox_M0_1.Checked = (bool)M0_1;
            checkBox_M0_2.Checked = (bool)M0_2;
            checkBoxDB1_DBX0_0.Checked = (bool)DB1_DBX0_0;
        }



        private void checkBox_M0_2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox_M0_1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void mtb_ID14_TextChanged(object sender, EventArgs e)
        {

/*            object Xx_x = ((CheckBox)sender).Checked; //checkBox_I0_0.Checked;
            string textT = ((CheckBox)sender).Text;
            int intTindex = textT.IndexOf(".", StringComparison.Ordinal);
            textBox1.Text = textT;
            string charsCompare = textT.Substring(0, 2);
            switch (charsCompare)
            {
                case "ID":
                    break;
                case "IW":
                    break;
                case "IB":
                    break;
                case "MD":
                    break;
                case "MW":
                    break;
                case "MB":
                    break;
                default:
                    string charCompare = textT.Substring(0, 1);
                    switch (charCompare)
                    {
                        case "I":
                            int intT1 = Convert.ToInt16(textT.Substring(1, intTindex - 1));
                            int intT2 = Convert.ToInt16(textT.Substring(intTindex + 1, 1));
                            Ps.WriteInputPoint(intT1, intT2, ref Xx_x);
                            break;
                        case "M":
                            break;
                    }
                    break;
            }
*/



            //object ID14 = Convert.ToDecimal(mtb_ID14.Text);
            //Ps.WriteInputPoint(14, 0, ref ID14);
            //Ps.WriteInputImage(14,ID14);
        }

        private void mtb_IDx_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            string textT = ((MaskedTextBox)sender).Name;
            int intTindex = textT.IndexOf("_", StringComparison.Ordinal);
            string charsCompare = textT.Substring(intTindex+1, 2);
            string numberCompare = textT.Substring(intTindex + 3);
            switch (charsCompare)
            {
                case "ID":
                    object idx = Convert.ToInt32(((MaskedTextBox)sender).Text, 10);
                    Ps.WriteInputPoint(Convert.ToInt16(numberCompare), 0, ref idx);
                    break;
                case "IW":
                    break;
                case "IB":
                    break;
                case "MD":
                    break;
                case "MW":
                    break;
                case "MB":
                    break;
            }
        }

        private void MainPage_Click(object sender, EventArgs e)
        {

        }

        private void btn_Setup_Click(object sender, EventArgs e)
        {
            Ps.WriteInputPoint(0, 0, true);Ps.WriteInputPoint(0, 3, true);Ps.WriteInputPoint(0, 5, true);
            Ps.WriteInputPoint(1, 0, true);Ps.WriteInputPoint(1, 3, true);Ps.WriteInputPoint(1, 5, true);
            Ps.WriteInputPoint(2, 0, true);Ps.WriteInputPoint(2, 3, true);Ps.WriteInputPoint(2, 5, true);
            Ps.WriteInputPoint(3, 0, true);Ps.WriteInputPoint(3, 3, true);Ps.WriteInputPoint(3, 5, true);
            Ps.WriteInputPoint(4, 0, true);Ps.WriteInputPoint(4, 3, true);Ps.WriteInputPoint(4, 5, true);
            Ps.WriteInputPoint(5, 0, true);Ps.WriteInputPoint(5, 3, true);Ps.WriteInputPoint(5, 5, true);

            Ps.WriteInputPoint(6, 0, true); Ps.WriteInputPoint(6, 1, true); Ps.WriteInputPoint(6, 3, true);Ps.WriteInputPoint(6, 4, true); Ps.WriteInputPoint(6, 5, true); Ps.WriteInputPoint(6, 7, true);
            Ps.WriteInputPoint(7, 0, true); Ps.WriteInputPoint(7, 1, true); Ps.WriteInputPoint(7, 3, true); Ps.WriteInputPoint(7, 4, true); Ps.WriteInputPoint(7, 5, true); Ps.WriteInputPoint(7, 7, true);
            Ps.WriteInputPoint(8, 0, true); Ps.WriteInputPoint(8, 1, true); Ps.WriteInputPoint(8, 3, true); Ps.WriteInputPoint(8, 4, true); Ps.WriteInputPoint(8, 5, true); Ps.WriteInputPoint(8, 7, true);

            Ps.WriteInputPoint(9, 0, true); Ps.WriteInputPoint(9, 1, true); Ps.WriteInputPoint(9, 2, true); Ps.WriteInputPoint(9, 3, true); Ps.WriteInputPoint(9, 4, true); Ps.WriteInputPoint(9, 5, true);
            Ps.WriteInputPoint(10, 0, true); Ps.WriteInputPoint(10, 4, true); Ps.WriteInputPoint(10, 5, true);
        }

        private bool Stp;
        private void button1_Click(object sender, EventArgs e)
        {
            Stp = !Stp;
            Ps.WriteInputPoint(10,6,Stp);
            if (Stp) button1.Text = "Выбран ПУ1";
            else button1.Text = "Выбран ШУ";
        }

        private void rBtn1_CheckedChanged(object sender, EventArgs e)
        {
            Ps.WriteInputPoint(10, 1, true); Ps.WriteInputPoint(10, 2, false); Ps.WriteInputPoint(10, 3, false);
        }

        private void rBtn2_CheckedChanged(object sender, EventArgs e)
        {
            Ps.WriteInputPoint(10, 1, false); Ps.WriteInputPoint(10, 2, true); Ps.WriteInputPoint(10, 3, false);
        }

        private void rBtn3_CheckedChanged(object sender, EventArgs e)
        {
            Ps.WriteInputPoint(10, 1, false); Ps.WriteInputPoint(10, 2, false); Ps.WriteInputPoint(10, 3, true);
        }

        private void mtb_ID62_Validated(object sender, EventArgs e)
        {
            visota_leb1 = Convert.ToInt32(mtb_ID62.Text);
            Ps.WriteInputPoint(62, 0, visota_leb1);
        }

        private void mtb_ID74_Validated(object sender, EventArgs e)
        {
            visota_leb2 = Convert.ToInt32(mtb_ID74.Text);
            Ps.WriteInputPoint(74, 0, visota_leb2);
        }

        private void mtb_ID86_Validated(object sender, EventArgs e)
        {
            visota_leb3 = Convert.ToInt32(mtb_ID86.Text);
            Ps.WriteInputPoint(86, 0, visota_leb3);
        }

        private void mtb_ID98_Validated(object sender, EventArgs e)
        {
            visota_leb4 = Convert.ToInt32(mtb_ID98.Text);
            Ps.WriteInputPoint(98, 0, visota_leb4);
        }

        private void mtb_ID110_Validated(object sender, EventArgs e)
        {
            visota_leb5 = Convert.ToInt32(mtb_ID110.Text);
            Ps.WriteInputPoint(110, 0, visota_leb5);
        }

        private void mtb_ID122_Validated(object sender, EventArgs e)
        {
            visota_leb6 = Convert.ToInt32(mtb_ID122.Text);
            Ps.WriteInputPoint(122, 0, visota_leb6);
        }

        private void mtb_ID62_122_Validated(object sender, EventArgs e)
        {
            mtb_ID62.Text = mtb_ID62_122.Text; mtb_ID74.Text = mtb_ID62_122.Text; mtb_ID86.Text = mtb_ID62_122.Text;
            mtb_ID98.Text = mtb_ID62_122.Text; mtb_ID110.Text = mtb_ID62_122.Text; mtb_ID122.Text = mtb_ID62_122.Text;
            visota_leb1 = Convert.ToInt32(mtb_ID62_122.Text); Ps.WriteInputPoint(62, 0, visota_leb1); visota_leb2 = Convert.ToInt32(mtb_ID62_122.Text); Ps.WriteInputPoint(74, 0, visota_leb2);
            visota_leb3 = Convert.ToInt32(mtb_ID62_122.Text); Ps.WriteInputPoint(86, 0, Convert.ToInt32(visota_leb3)); visota_leb4 = Convert.ToInt32(mtb_ID62_122.Text); Ps.WriteInputPoint(98, 0, Convert.ToInt32(visota_leb4));
            visota_leb5 = Convert.ToInt32(mtb_ID62_122.Text); Ps.WriteInputPoint(110, 0, Convert.ToInt32(visota_leb5)); visota_leb6 = Convert.ToInt32(mtb_ID62_122.Text); Ps.WriteInputPoint(122, 0, Convert.ToInt32(visota_leb6));
        }

        private void mtb_IW16_56_Validated(object sender, EventArgs e)
        {
            Int16 vesPlatf = (short)Convert.ToInt16(mtb_IW16_56.Text);
            //Int16 vesLeb = (short)(vesPlatf/6);
            WL1_kg = (short)(vesPlatf / 6); WL2_kg = WL1_kg; WL3_kg = WL1_kg; WL4_kg = WL1_kg; WL5_kg = WL1_kg;
            //Int16 vesLeb6 = (short)(vesPlatf - vesLeb*5);
            WL6_kg = (short)(vesPlatf - WL1_kg * 5);
            string leb = WL1_kg.ToString();
            string leb6 = WL6_kg.ToString();
            mtb_IW16.Text = leb; mtb_IW24.Text = leb; mtb_IW32.Text = leb;
            mtb_IW40.Text = leb; mtb_IW48.Text = leb; mtb_IW56.Text = leb6;
            Ps.WriteInputPoint(16, 0, WL1_kg); Ps.WriteInputPoint(24, 0, WL2_kg);
            Ps.WriteInputPoint(32, 0, WL3_kg); Ps.WriteInputPoint(40, 0, WL4_kg);
            Ps.WriteInputPoint(48, 0, WL5_kg); Ps.WriteInputPoint(56, 0, WL6_kg);
        }

        private void mtb_IW16_Validated(object sender, EventArgs e)
        {
            WL1_kg = Convert.ToInt16(mtb_IW16.Text);
            Ps.WriteInputPoint(16, 0, WL1_kg);
        }

        private void mtb_IW24_Validated(object sender, EventArgs e)
        {
            WL2_kg = Convert.ToInt16(mtb_IW24.Text);
            Ps.WriteInputPoint(24, 0, WL2_kg);
        }

        private void mtb_IW32_Validated(object sender, EventArgs e)
        {
            WL3_kg = Convert.ToInt16(mtb_IW32.Text);
            Ps.WriteInputPoint(32, 0, WL3_kg);
        }

        private void mtb_IW40_Validated(object sender, EventArgs e)
        {
            WL4_kg = Convert.ToInt16(mtb_IW40.Text);
            Ps.WriteInputPoint(40, 0, WL4_kg);
        }

        private void mtb_IW48_Validated(object sender, EventArgs e)
        {
            WL5_kg = Convert.ToInt16(mtb_IW48.Text);
            Ps.WriteInputPoint(48, 0, WL5_kg);
        }

        private void mtb_ID56_Validated(object sender, EventArgs e)
        {
            WL6_kg = Convert.ToInt16(mtb_IW56.Text);
            Ps.WriteInputPoint(56, 0, WL6_kg);
        }

        private void cb_1_Only_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_1_Only.Checked) Ps.WriteFlagValue(13, 7, true);
            else Ps.WriteFlagValue(13, 7, false);
        }

        private void btnRL_Click(object sender, EventArgs e)
        {
            Ps.WriteInputPoint(138, 2, true); Ps.WriteInputPoint(138, 3, true);//Привод 1
            Ps.WriteInputPoint(144, 2, true); Ps.WriteInputPoint(144, 3, true);//Привод 2
            Ps.WriteInputPoint(150, 2, true); Ps.WriteInputPoint(150, 3, true);//Привод 3
            Ps.WriteInputPoint(156, 2, true); Ps.WriteInputPoint(156, 3, true);//Привод 4
            Ps.WriteInputPoint(162, 2, true); Ps.WriteInputPoint(162, 3, true);//Привод 5
            Ps.WriteInputPoint(168, 2, true); Ps.WriteInputPoint(168, 3, true);//Привод 6
        }

      

    

      

      

 

  



       






    }
}
