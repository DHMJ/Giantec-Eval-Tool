using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Text.RegularExpressions;

namespace DMDongle
{
    public class comm
    {

        public bool IsOpen = false;
        public enum CommMode
        {
            SC = 1,
            OWCI = 2,
            I2C = 3
        };

        private byte DevAddr;
        private byte Pilot;
        private CommMode CM;
        private SerialPort com = new SerialPort();

        public bool dongleInit(string portname, CommMode cm, byte devaddr, byte pilot)
        {
            if (cm == CommMode.SC)
                CM = CommMode.SC;
            else if (cm == CommMode.OWCI)
                CM = CommMode.OWCI;
            else if (cm == CommMode.I2C)
                CM = CommMode.I2C;
            else
                return false;
            
            DevAddr = devaddr;
            Pilot = pilot;

            if (!IsOpen)
            {
                com.PortName = portname;
                com.BaudRate = 115200;
                com.DataBits = 8;
                com.Parity = Parity.None;
                com.StopBits = StopBits.One;

                try
                {
                    com.Open();
                    IsOpen = true;
                    //return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }

            byte[] WriteBuf = new byte[3];

            WriteBuf[0] = 0x5A;
            WriteBuf[1] = 0x04;
            WriteBuf[2] = DevAddr;
            com.Write(WriteBuf, 0, 3);

            uint timeOutCounter = 200;
            while (com.BytesToRead == 0 && timeOutCounter > 0)
            {
                timeOutCounter--;
                System.Threading.Thread.Sleep(10);
            }

            if (timeOutCounter != 0)
            {
                byte[] readBuf = new byte[com.BytesToRead];
                com.Read(readBuf, 0, com.BytesToRead);
                if (readBuf[0] != 0x5A || readBuf[1] != 0x04 || readBuf[2] != 0xCC)
                    return false;
            }
            else
                return false;

            WriteBuf[0] = 0x5A;
            WriteBuf[1] = 0x03;
            WriteBuf[2] = Pilot;
            com.Write(WriteBuf, 0, 3);

            timeOutCounter = 200;
            while (com.BytesToRead == 0 && timeOutCounter > 0)
            {
                timeOutCounter--;
                System.Threading.Thread.Sleep(10);
            }

            if (timeOutCounter != 0)
            {
                byte[] readBuf = new byte[com.BytesToRead];
                com.Read(readBuf, 0, com.BytesToRead);
                if (readBuf[0] != 0x5A || readBuf[1] != 0x03 || readBuf[2] != 0xCC)
                    return false;
            }
            else
                return false;

            return true;
        }

        public void dongleDeInit() 
        {
            com.Close();
            IsOpen = false;
        }

        public bool writeSingleReg(byte regaddr, byte regdata)
        {
            byte[] buf = new byte[5];
            buf[0] = 0x5A;
            buf[1] = 0x01;
            buf[2] = regaddr;
            buf[3] = 0x01;
            buf[4] = regdata;
            com.Write(buf, 0, 5);

            uint timeOutCounter = 200;
            while (com.BytesToRead == 0 && timeOutCounter > 0)
            {
                timeOutCounter--;
                System.Threading.Thread.Sleep(10);
            }

            if (timeOutCounter != 0)
            {
                byte[] readBuf = new byte[com.BytesToRead];
                com.Read(readBuf, 0, com.BytesToRead);
                if (readBuf[0] != 0x5A || readBuf[1] != 0x01 || readBuf[2] != 0xCC)
                    return false;
            }
            else
                return false;

            return true;
        }

        public byte readSingleReg(byte regaddr)
        {
            byte[] buf = new byte[5];
            buf[0] = 0x5A;
            buf[1] = 0x02;
            buf[2] = regaddr;
            buf[3] = 0x01;
            com.Write(buf, 0, 4);

            uint timeOutCounter = 200;
            while (com.BytesToRead == 0 && timeOutCounter > 0)
            {
                timeOutCounter--;
                System.Threading.Thread.Sleep(10);
            }

            if (timeOutCounter != 0 && com.BytesToRead != 0)
            {
                byte[] readBuf = new byte[com.BytesToRead];
                com.Read(readBuf, 0, com.BytesToRead);
                if (readBuf[0] != 0x5A || readBuf[1] != 0x02 || readBuf[2] != 0x02)
                    return 0x00;
                else
                    return readBuf[3];
            }
            else
                return 0x00;
        }

        public bool writeRegBurst(byte startregaddr, byte[] data, byte count)
        {
            byte[] buf = new byte[4 + count];
            buf[0] = 0x5A;
            buf[1] = 0x01;
            buf[2] = startregaddr;
            buf[3] = count;
            for (int i = 0; i < count; i++ )
                buf[4 + i] = data[i];
            com.Write(buf, 0, 4 + count);

            uint timeOutCounter = 200;
            while (com.BytesToRead == 0 && timeOutCounter > 0)
            {
                timeOutCounter--;
                System.Threading.Thread.Sleep(10);
            }

            if (timeOutCounter != 0)
            {
                byte[] readBuf = new byte[com.BytesToRead];
                com.Read(readBuf, 0, com.BytesToRead);
                if (readBuf[0] != 0x5A || readBuf[1] != 0x01 || readBuf[2] != 0xCC)
                    return false;
            }
            else
                return false;

            return true;
        }

        public bool readRegBurst(byte startregaddr, byte[] data, byte count)
        {
            byte[] buf = new byte[4];
            buf[0] = 0x5A;
            buf[1] = 0x02;
            buf[2] = startregaddr;
            buf[3] = count;

            com.Write(buf, 0, 4);

            uint timeOutCounter = 200;
            while (com.BytesToRead == 0 && timeOutCounter > 0)
            {
                timeOutCounter--;
                System.Threading.Thread.Sleep(10);
            }

            int validData = 0;
            if (count > (byte)(com.BytesToRead))
                validData = com.BytesToRead;
            else
                validData = count;

            if (timeOutCounter != 0 && com.BytesToRead != 0)
            {
                byte[] readBuf = new byte[com.BytesToRead];
                com.Read(readBuf, 0, com.BytesToRead);
                if (readBuf[0] != 0x5A || readBuf[1] != 0x02)
                    return false;
                else
                {
                    for (int i = 0; i < validData; i++)
                        data[i] = readBuf[i + 2];
                    return true;
                }
            }
            else
                return false;
        }
    }
}
