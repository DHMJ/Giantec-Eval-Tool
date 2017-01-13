using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DMDongle;

namespace Sc_Test
{
    public partial class Form1 : Form
    {
        DMDongle.comm com = new comm();

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            //DMDangle.comm.CommMode.SC;
            //DMDangle.comm.dangleInit();
            com.dongleInit("COM8", DMDongle.comm.CommMode.SC, 0x65, 10);
            richTextBoxLog.Text = "Init COM8 Success!";
        }

        private void buttonWrite_Click(object sender, EventArgs e)
        {
            com.writeSingleReg(0x30, 0xaa);
        }

        private void buttonRead_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[8];
            com.readRegBurst(0x39, data, 8);
            for (int i = 0; i < 8; i++ )
                richTextBoxLog.AppendText(data[i].ToString("X2") + " ");
        }
    }
}
