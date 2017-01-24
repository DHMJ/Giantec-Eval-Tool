using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using DMCommunication;

namespace GeneralRegConfigPlatform.MDGUI
{
    public partial class FormDongle : Form
    {
        DMDongle myDongle;
        public FormDongle(DMDongle _myDongle)
        {
            InitializeComponent();
            GetSerialPorts();
            myDongle = _myDongle;
        }

        private void GetSerialPorts()
        {
            this.cbPortName.Items.Clear();

            string[] str = SerialPort.GetPortNames();
            if (str == null)
            {
                //本机没有串口！
                this.cbPortName.Items.Add("NULL");
                this.cbPortName.SelectedIndex = 0;
            }
            else
            {
                for (int i = 0; i < str.Length; i++)
                    this.cbPortName.Items.Add(str[i]);

                this.cbPortName.SelectedIndex = 0;
            }

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (myDongle.dongleInit(this.cbPortName.Text, DMDongle.VCPGROUP.SC, 0x65, 10))
            {
                MessageBox.Show("Connected");
            }
            else
            {
                MessageBox.Show("Connected Failed");
            }
        }
    }
}
