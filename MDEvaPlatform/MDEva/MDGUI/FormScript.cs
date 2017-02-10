using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using MD.MDCommon;
using DMCommunication;
using GeneralRegConfigPlatform.GUI;
using System.Threading;
using System.Text.RegularExpressions;

namespace GeneralRegConfigPlatform.MDGUI
{
    public partial class FormScript : UserControl
    {
        DMDongle dg;

        public enum SCRIPT_COMMAND
        {
            SINGLE_WRITE,
            SINGLE_READ,
            BURST_WRITE,
            BURST_READ,
            SET_RSTB,
            SET_VALID,
            DELAY,
            None
        };

        public FormScript(DMDongle _uart)
        {
            InitializeComponent();
            dg = _uart;
        }

        //public FormScript()
        //{
        //    InitializeComponent();
        //}

        private void btn_Script_load_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openfiledlg = new OpenFileDialog();
                openfiledlg.Title = "Please choose the script file to be loaded...";
                openfiledlg.Filter = "Text file(*.txt)|*.txt";
                openfiledlg.RestoreDirectory = true;
                string filename = "";
                if (openfiledlg.ShowDialog() == DialogResult.OK)
                {
                    filename = openfiledlg.FileName;
                }
                else
                    return;

                StreamReader sr = new StreamReader(filename);
                this.richtxt_ScriptView.Text = sr.ReadToEnd();

                this.text_Srcipt_Name.Text = System.IO.Path.GetFileName(filename);
                sr.Close();
            }
            catch
            {
                MessageBox.Show("Load scripte file failed, please choose correct file!");
            }
        }

        private void btn_Script_Save_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog savefiledlg = new SaveFileDialog();
                savefiledlg.Title = "Saving script command...";
                savefiledlg.Filter = "Text file(*.txt)|*.txt";
                savefiledlg.RestoreDirectory = true;
                string filename = "";
                if (savefiledlg.ShowDialog() == DialogResult.OK)
                {
                    filename = savefiledlg.FileName;
                }
                else
                    return;

                //filename = Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf('\\') + 1) + "script demo.txt";

                StreamWriter sw = File.CreateText(filename);
                /* First line for description */
                //sw.WriteLine(String.Format("/* Script for DSM measurement of MDO???, CopyRight of InvenSense Inc. -- Saved time:{0} */",DateTime.Now.ToString()));
                /* script Data */
                sw.Write(this.richtxt_ScriptView.Text);

                sw.Close();
            }
            catch
            {
                MessageBox.Show("Save script file failed!");
            }
        }

        private void btn_Script_Excute_Click(object sender, EventArgs e)
        {
            try
            {
                byte addr = 0x00;
                byte value = 0x00;
                byte[] data;
                //byte[] reg = new byte[2];

                string[] AllCommands = this.richtxt_ScriptView.Text.Split('\n');

                string[] parameters;
                SCRIPT_COMMAND cmdType;
                for (int i = 0; i < AllCommands.Length; i++)
                {
                    cmdType = ScriptDecodeCommand(AllCommands[i], out parameters);
                    if (cmdType != SCRIPT_COMMAND.None)
                    {
                        switch (cmdType)
                        {
                            case SCRIPT_COMMAND.SINGLE_WRITE:
                                //I2CWrite_Single_OneWire(parameters[0], parameters[1]);
                                //reg[0] = Convert.ToByte(parameters[0], 16);
                                //reg[1] = Convert.ToByte(parameters[1], 16);
                                addr = Convert.ToByte(parameters[0], 16);
                                value = Convert.ToByte(parameters[1], 16);
                                dg.writeRegSingle( addr, value);
                                Thread.Sleep(200);
                                //dg.writeRegBlockSingle(reg, 1);
                                break;

                            case SCRIPT_COMMAND.SINGLE_READ:
                                //tempU32 = oneWrie_device.I2CRead_Single(dev_addr, uint.Parse(parameters[0], NumberStyles.HexNumber));
                                //if (tempU32 <= 0xFF)
                                //{
                                //    ScriptResult(AllCommands[i], true);
                                //    DisplayOperateMes("Reg 0x" + parameters[0] + " == " + tempU32.ToString("X2"));
                                //}
                                //else
                                //    ScriptResult(AllCommands[i], false);

                                break;

                            case SCRIPT_COMMAND.BURST_WRITE:
                                addr = Convert.ToByte(parameters[0], 16);
                                byte length = Convert.ToByte(parameters[1], 16);

                                data = new byte[parameters.Length - 2];
                                for (int j = 0; j < data.Length; j++)
                                    data[j] = Convert.ToByte(parameters[2 + j], 16);
                                dg.writeRegBurst(addr, data, length);
                                Thread.Sleep(200);
                                break;

                            case SCRIPT_COMMAND.BURST_READ:
                                //data = new uint[uint.Parse(parameters[1], NumberStyles.HexNumber)];
                                //tempU32 = oneWrie_device.I2CRead_Burst(dev_addr, uint.Parse(parameters[0], NumberStyles.HexNumber), uint.Parse(parameters[1], NumberStyles.HexNumber), data);
                                //if (tempU32 == 0)
                                //{
                                //    ScriptResult(AllCommands[i], true);
                                //    opMsg = "";
                                //    for (int j = 0; j < data.Length; )
                                //    {
                                //        opMsg += data[j++].ToString("X2") + "    ";
                                //        if (j % 10 == 0)
                                //            opMsg += "\r\n";
                                //    }
                                //    DisplayOperateMes(opMsg);
                                //}

                                break;

                            case SCRIPT_COMMAND.SET_RSTB:
                                value = Convert.ToByte(parameters[0], 16);
                                if (dg.IsOpen)
                                {
                                    if(value == 1)
                                        dg.setUserIO(DMDongle.USERIOGROUP.GROUP_A, DMDongle.USERIOPIN.USR_IO_1);
                                    else if(value == 0)
                                        dg.resetUserIO(DMDongle.USERIOGROUP.GROUP_A, DMDongle.USERIOPIN.USR_IO_1);
                                }
                                Thread.Sleep(200);
                                break;

                            case SCRIPT_COMMAND.SET_VALID:
                                value = Convert.ToByte(parameters[0], 16);
                                if (dg.IsOpen)
                                {
                                    if (value == 1)
                                        dg.setUserIO(DMDongle.USERIOGROUP.GROUP_A, DMDongle.USERIOPIN.USR_IO_0);
                                    else if(value == 0)
                                        dg.resetUserIO(DMDongle.USERIOGROUP.GROUP_A, DMDongle.USERIOPIN.USR_IO_0);
                                }
                                Thread.Sleep(200);
                                break;

                            case SCRIPT_COMMAND.DELAY:
                                UInt32 delay = Convert.ToUInt32(parameters[0]);
                                Thread.Sleep(value);
                                //Thread.Sleep(200);
                                break;

                            default:
                                break;
                        }
                    }
                }

            }
            catch
            {
                MessageBox.Show("Excute script failed, please check the device address and commands!");
            }
        }

        private SCRIPT_COMMAND ScriptDecodeCommand(string cmdAndParaStr, out string[] param)
        {
            SCRIPT_COMMAND ret = SCRIPT_COMMAND.None;
            string[] cmdDetail = cmdAndParaStr.Split(":;".ToCharArray());
            string[] tempParam;
            string tempMsg;
            param = null;

            switch (cmdDetail[0].Trim().ToUpper())
            {
                case "W":
                case "SW":
                case "WRITE":
                case "SINGLEWRITE":
                case "SINGLE_WRITE":
                    try
                    {
                        if (cmdDetail.Length > 1)
                        {
                            param = new string[2];
                            tempParam = cmdAndParaStr.Split(":;".ToCharArray());
                            //tempParam = cmdDetail[1].Split(",;".ToCharArray());
                            if (tempParam.Length >= 3)
                            {
                                for (int i = 0; i < 2; i++)
                                {
                                    tempMsg = tempParam[i + 1].Trim().ToUpper();

                                    tempMsg = tempMsg.Substring(tempMsg.LastIndexOf('X') + 1, tempMsg.Length - tempMsg.LastIndexOf('X') - 1);
                                    if (tempMsg.Length <= 3)
                                    {
                                        param[i] = uint.Parse(tempMsg, System.Globalization.NumberStyles.HexNumber).ToString("X2");
                                        //param[i] = tempMsg;
                                    }
                                    else
                                        throw new Exception("Invaild parameters");
                                }
                                ret = SCRIPT_COMMAND.SINGLE_WRITE;           /* the only way get vaild command and parameters */
                            }
                            else
                                throw new Exception("Invaild parameters");
                        }
                        else
                            throw new Exception("No parameters");
                    }
                    catch
                    {
                        //DisplayOperateMes(cmdAndParaStr + " --> with invaild parameters!", Color.Red);
                    }
                    break;

                case "SR":
                case "READ":
                case "SINGLEREAD":
                case "SINGLE_READ":
                    try
                    {
                        if (cmdDetail.Length > 1)
                        {
                            param = new string[1];
                            tempParam = cmdDetail[1].Split(",;".ToCharArray());
                            if (tempParam.Length >= 1)
                            {
                                tempMsg = tempParam[0].Trim().ToUpper();

                                tempMsg = tempMsg.Substring(tempMsg.LastIndexOf('X') + 1, tempMsg.Length - tempMsg.LastIndexOf('X') - 1);
                                if (tempMsg.Length <= 2)
                                {
                                    param[0] = uint.Parse(tempMsg, System.Globalization.NumberStyles.HexNumber).ToString("X2");
                                }
                                else
                                    throw new Exception("Invaild parameters");

                                ret = SCRIPT_COMMAND.SINGLE_READ;           /* the only way get vaild command and parameters */
                            }
                            else
                                throw new Exception("Invaild parameters");
                        }
                        else
                            throw new Exception("No parameters");
                    }
                    catch
                    {
                        //DisplayOperateMes(cmdAndParaStr + " --> with invaild/no parameters!", Color.Red);
                    }
                    break;

               

                case "BW":
                case "BURSTWRITE":
                case "BURST_WRITE":
                    try
                    {
                        if (cmdDetail.Length > 1)
                        {
                            uint paramCount;
                            //tempParam = cmdDetail[1].Split(",;".ToCharArray());
                            tempParam = cmdAndParaStr.Split(":;".ToCharArray());
                            if (tempParam.Length >= 3)  //At least 3 params
                            {
                                /* 1.Get the write count firstly */
                                //tempMsg = tempParam[1].Trim().ToUpper();
                                tempMsg = tempParam[2].Trim().ToUpper();
                                tempMsg = tempMsg.Substring(tempMsg.LastIndexOf('X') + 1, tempMsg.Length - tempMsg.LastIndexOf('X') - 1);
                                if (tempMsg.Length <= 2)
                                {
                                    paramCount = uint.Parse(tempMsg, System.Globalization.NumberStyles.HexNumber);
                                }
                                else
                                    throw new Exception("Invaild parameters");

                                /* 2.Initialize param array */
                                param = new string[paramCount + 2];

                                /* 3. Fill in params */
                                tempMsg = tempParam[1].Trim().ToUpper();
                                tempMsg = tempMsg.Substring(tempMsg.LastIndexOf('X') + 1, tempMsg.Length - tempMsg.LastIndexOf('X') - 1);
                                if (tempMsg.Length <= 2)
                                {
                                    param[0] = uint.Parse(tempMsg, System.Globalization.NumberStyles.HexNumber).ToString("X2");
                                }
                                else
                                    throw new Exception("Invaild parameters");

                                param[1] = paramCount.ToString("X2");

                                //tempMsg = tempParam[3].ToUpper().Substring(tempParam[3].ToUpper().IndexOf('X') - 1);
                                //tempParam = tempMsg.Split(' ');
                                //if (tempParam.Length >= paramCount)
                                //{
                                for (int i = 0; i < paramCount; i++)
                                {
                                    //tempMsg = tempParam[3+i].TrimStart("0X".ToCharArray());
                                    tempMsg = tempParam[3 + i].Trim().ToUpper();
                                    tempMsg = tempMsg.Substring(tempMsg.LastIndexOf('X') + 1, tempMsg.Length - tempMsg.LastIndexOf('X') - 1);
                                    if (tempMsg.Length <= 2)
                                    {
                                        param[2 + i] = uint.Parse(tempMsg, System.Globalization.NumberStyles.HexNumber).ToString("X2");
                                    }
                                    else
                                        throw new Exception("Invaild parameters");
                                }
                                //}
                                ret = SCRIPT_COMMAND.BURST_WRITE;           /* the only way get vaild command and parameters */
                            }
                            else
                                throw new Exception("Invaild parameters");
                        }
                        else
                            throw new Exception("No parameters");
                    }
                    catch
                    {
                        //DisplayOperateMes(cmdAndParaStr + " --> with invaild parameters!", Color.Red);
                    }
                    break;

                case "BR":
                case "BURSTREAD":
                case "BURST_READ":
                    try
                    {
                        if (cmdDetail.Length > 1)
                        {
                            param = new string[2];
                            tempParam = cmdAndParaStr.Split(":;".ToCharArray());
                            //tempParam = cmdDetail[1].Split(",;".ToCharArray());
                            if (tempParam.Length >= 3)
                            {
                                for (int i = 0; i < 2; i++)
                                {
                                    tempMsg = tempParam[i + 1].Trim().ToUpper();
                                    tempMsg = tempMsg.Substring(tempMsg.LastIndexOf('X') + 1, tempMsg.Length - tempMsg.LastIndexOf('X') - 1);
                                    if (tempMsg.Length <= 3)
                                    {
                                        param[i] = uint.Parse(tempMsg, System.Globalization.NumberStyles.HexNumber).ToString("X2");
                                    }
                                    else
                                        throw new Exception("Invaild parameters");
                                }
                                ret = SCRIPT_COMMAND.BURST_READ;           /* the only way get vaild command and parameters */
                            }
                            else
                                throw new Exception("Invaild parameters");
                        }
                        else
                            throw new Exception("No parameters");
                    }
                    catch
                    {
                        //DisplayOperateMes(cmdAndParaStr + " --> with invaild parameters!", Color.Red);
                    }
                    break;

                case "SETRSTB":
                case "SET_RSTB":
                    try
                    {
                        if (cmdDetail.Length > 1)
                        {
                            param = new string[1];
                            tempParam = cmdDetail[1].Split(",;".ToCharArray());

                            Regex regex = new Regex("\\d+");      /* Get the first number in a string. */
                            var m = regex.Match(tempParam[0]);
                            param[0] = uint.Parse(m.ToString(), System.Globalization.NumberStyles.Integer).ToString();

                            ret = SCRIPT_COMMAND.SET_RSTB;        /* the only way get vaild command and parameters */
                        }
                        else
                            throw new Exception("No parameters");
                    }
                    catch
                    {
                        //DisplayOperateMes(cmdAndParaStr + " --> with invaild parameters!", Color.Red);
                    }
                    break;

                case "SETDELAY":
                case "DELAY":
                    try
                    {
                        if (cmdDetail.Length > 1)
                        {
                            param = new string[1];
                            tempParam = cmdDetail[1].Split(",;".ToCharArray());

                            Regex regex = new Regex("\\d+");     /* Get the first number in a string. */
                            var m = regex.Match(tempParam[0]);
                            param[0] = uint.Parse(m.ToString(), System.Globalization.NumberStyles.Integer).ToString();

                            ret = SCRIPT_COMMAND.DELAY;           /* the only way get vaild command and parameters */
                        }
                        else
                            throw new Exception("No parameters");
                    }
                    catch
                    {
                        //DisplayOperateMes(cmdAndParaStr + " --> with invaild parameters!", Color.Red);
                    }
                    break;

                case "SETVALID":
                case "SET_VALID":
                    try
                    {
                        if (cmdDetail.Length > 1)
                        {
                            param = new string[1];
                            tempParam = cmdDetail[1].Split(",;".ToCharArray());

                            Regex regex = new Regex("\\d+");      /* Get the first number in a string. */
                            var m = regex.Match(tempParam[0]);
                            param[0] = uint.Parse(m.ToString(), System.Globalization.NumberStyles.Integer).ToString();

                            ret = SCRIPT_COMMAND.SET_VALID;        /* the only way get vaild command and parameters */
                        }
                        else
                            throw new Exception("No parameters");
                    }
                    catch
                    {
                        //DisplayOperateMes(cmdAndParaStr + " --> with invaild parameters!", Color.Red);
                    }
                    break;


                default:
                    break;
            }
            return ret;
        }
    }
}
