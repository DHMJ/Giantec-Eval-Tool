using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Text.RegularExpressions;

namespace DMCommunication
{
    public class DMDongle
    {
        //public variables
        public bool IsOpen = false;

        public enum VCPGROUP
        {
            BASE    = 0x00,
            GPIO    = 0x10,
            SC      = 0x20,
            ADC     = 0x30,
            OWCI    = 0x40,
            I2C     = 0x50,
            SPI     = 0x60   
        };
        public enum USERIOGROUP
        {
            GROUP_A = 0x00,
            GROUP_B = 0x10,
            GROUP_C = 0x20,
            GROUP_D = 0x30,
            GROUP_E = 0x40
        };
        public enum USERIOPIN
        {
            USR_IO_0 = 0x01,
            USR_IO_1 = 0x02,
            USR_IO_2 = 0x04,
            USR_IO_3 = 0x08,
            USR_IO_4 = 0x10,
            USR_IO_5 = 0x20,
            USR_IO_6 = 0x40,
            USR_IO_7 = 0x80
        }

        //private variables
        private byte DevAddr;
        private byte Pilot;
        private byte commMode;

        private SerialPort uart = new SerialPort();

        //----------------------DONGLE FUNCTIONS--------------------------------
        public bool dongleInit(string portname, VCPGROUP vg, byte devaddr, byte pilot)
        {
            if (!IsOpen)
            {
                uart.PortName = portname;
                uart.BaudRate = 115200;
                uart.DataBits = 8;
                uart.Parity = Parity.None;
                uart.StopBits = StopBits.One;

                try
                {
                    uart.Open();
                    IsOpen = true;
                    commInit(vg, devaddr, pilot);
                    //return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            return true;
        }

        public void dongleDeInit()
        {
            uart.Close();
            IsOpen = false;
        }

        //----------------------DUT CTL METHOD  FUNCTIONS-----------------------
        public bool commInit(VCPGROUP vg, byte devaddr, byte pilot)
        {
            if (vg == VCPGROUP.SC)
                commMode = (byte)VCPGROUP.SC;
            else if (vg == VCPGROUP.OWCI)
                commMode = (byte)VCPGROUP.OWCI;
            else if (vg == VCPGROUP.I2C)
                commMode = (byte)VCPGROUP.I2C;
            else if (vg == VCPGROUP.SPI)
                commMode = (byte)VCPGROUP.SPI;
            else
                return false;

            DevAddr = devaddr;
            Pilot = pilot;
            byte[] WriteBuf = new byte[4];

            WriteBuf[0] = 0x5A;
            WriteBuf[1] = (byte)commMode;
            WriteBuf[2] = 0x06;
            WriteBuf[3] = DevAddr;
            uart.Write(WriteBuf, 0, 4);

            uint timeOutCounter = 200;
            while (uart.BytesToRead == 0 && timeOutCounter > 0)
            {
                timeOutCounter--;
                System.Threading.Thread.Sleep(10);
            }

            if (timeOutCounter != 0)
            {
                byte[] readBuf = new byte[uart.BytesToRead];
                uart.Read(readBuf, 0, uart.BytesToRead);
                if (readBuf[0] != 0xA5 || readBuf[1] != (byte)commMode || readBuf[2] != 0x06 || readBuf[3] != 0xCC)
                    return false;
            }
            else
                return false;

            WriteBuf[0] = 0x5A;
            WriteBuf[1] = (byte)commMode;
            WriteBuf[2] = 0x05;
            WriteBuf[3] = Pilot;
            uart.Write(WriteBuf, 0, 4);

            timeOutCounter = 200;
            while (uart.BytesToRead == 0 && timeOutCounter > 0)
            {
                timeOutCounter--;
                System.Threading.Thread.Sleep(10);
            }

            if (timeOutCounter != 0)
            {
                byte[] readBuf = new byte[uart.BytesToRead];
                uart.Read(readBuf, 0, uart.BytesToRead);
                if (readBuf[0] != 0xA5 || readBuf[1] != (byte)commMode || readBuf[2] != 0x05 || readBuf[3] != 0xCC)
                    return false;
            }
            else
                return false;

            return true;
        }

        public bool writeRegSingle(byte regAddr, byte regData)
        {
            return writeRegBlockSingle(new byte[] { regAddr, regData }, 0x1);
        }

        public bool writeRegBlockSingle(byte[] data, int count)
        {
            byte[] buf = new byte[count * 2 + 4];
            buf[0] = 0x5A;
            buf[1] = commMode;
            buf[2] = 0x01;         //writesingle
            buf[3] = (byte)count;      //reg addr
            for (byte i = 0; i < count; i++)
            {
                buf[4 + i*2] = data[i*2 + 0];      //length = 1
                buf[5 + i*2] = data[i*2 + 1];      //reg value
            }

            uart.Write(buf, 0, count * 2 + 4);

            uint timeOutCounter = 200;
            while (uart.BytesToRead == 0 && timeOutCounter > 0)
            {
                timeOutCounter--;
                System.Threading.Thread.Sleep(10);
            }

            if (timeOutCounter != 0)
            {
                byte[] readBuf = new byte[uart.BytesToRead];
                uart.Read(readBuf, 0, uart.BytesToRead);
                if (readBuf[0] != 0xA5 || readBuf[1] != (byte)commMode || readBuf[2] != 0x01 || readBuf[3] != 0xCC)
                    return false;
            }
            else
                return false;

            return true;
        }

        public bool readRegSingle(byte regAddr, out byte regData)
        {
            byte[] boockData = new byte[1];
            regData = 0;
            bool ret = readRegBlockSingle(new byte[] { regAddr }, boockData, 1);
            if(ret == true)
                regData = boockData[0];

            return ret;
        }

        public bool readRegBlockSingle(byte[] addr, byte[] data, int count)
        {
            byte[] buf = new byte[4 + count];
            buf[0] = 0x5A;
            buf[1] = commMode;
            buf[2] = 0x02;
            buf[3] = (byte)count;
            for(byte i = 0; i < count; i++)
                buf[4 + i] = addr[i];
            //buf[5] = regaddr;


            uart.Write(buf, 0, 4 + count);

            uint timeOutCounter = 200;
            while (uart.BytesToRead == 0 && timeOutCounter > 0)
            {
                timeOutCounter--;
                System.Threading.Thread.Sleep(10);
            }

            if (timeOutCounter != 0 && uart.BytesToRead != 0)
            {
                byte[] readBuf = new byte[uart.BytesToRead];
                uart.Read(readBuf, 0, uart.BytesToRead);
                if (readBuf[0] != 0xA5 || readBuf[1] != (byte)commMode || readBuf[2] != 0x02)
                    return false;
                else
                {
                    for (int i = 0; i < count; i++)
                        data[i] = readBuf[i + 4];
                    return true;
                }
            }
            else
                return false;
        }

        public bool writeRegBurst(byte startregaddr, byte[] data, int count)
        {
            byte[] buf = new byte[5 + count];
            buf[0] = 0x5A;
            buf[1] = commMode;
            buf[2] = 0x03;
            buf[3] = startregaddr;
            buf[4] = (byte)count;

            for (int i = 0; i < count; i++)
                buf[5 + i] = data[i];

            uart.Write(buf, 0, 5 + count);

            uint timeOutCounter = 200;
            while (uart.BytesToRead == 0 && timeOutCounter > 0)
            {
                timeOutCounter--;
                System.Threading.Thread.Sleep(10);
            }

            if (timeOutCounter != 0)
            {
                byte[] readBuf = new byte[uart.BytesToRead];
                uart.Read(readBuf, 0, uart.BytesToRead);
                if (readBuf[0] != 0xA5 || readBuf[1] != (byte)commMode || readBuf[2] != 0x03 || readBuf[3] != 0xCC)
                    return false;
            }
            else
                return false;

            return true;
        }

        public bool readRegBurst(byte startregaddr, byte[] data, int count)
        {
            byte[] buf = new byte[5];
            buf[0] = 0x5A;
            buf[1] = commMode;
            buf[2] = 0x04;
            buf[3] = startregaddr;
            buf[4] = (byte)count;

            uart.Write(buf, 0, 5);

            uint timeOutCounter = 200;
            while (uart.BytesToRead == 0 && timeOutCounter > 0)
            {
                timeOutCounter--;
                System.Threading.Thread.Sleep(10);
            }

            int validData = 0;
            if (count > (byte)(uart.BytesToRead))
                validData = uart.BytesToRead;
            else
                validData = count;

            if (timeOutCounter != 0 && uart.BytesToRead != 0)
            {
                byte[] readBuf = new byte[uart.BytesToRead];
                uart.Read(readBuf, 0, uart.BytesToRead);
                if (readBuf[0] != 0xA5 || readBuf[1] != (byte)commMode || readBuf[2] != 0x04)
                    return false;
                else
                {
                    for (int i = 0; i < validData; i++)
                        data[i] = readBuf[i + 3];
                    return true;
                }
            }
            else
                return false;
        }

        //----------------------USER IO FUNCTIONS--------------------------------
        public bool setUserIO(USERIOGROUP group, USERIOPIN pin )
        {
            byte[] buf = new byte[5];
            buf[0] = 0x5A;
            buf[1] = (byte)VCPGROUP.GPIO;
            buf[2] = 0x01;              //set io
            buf[3] = (byte)group;       //group
            buf[4] = (byte)pin;         //pin
            uart.Write(buf, 0, 5);

            uint timeOutCounter = 200;
            while (uart.BytesToRead == 0 && timeOutCounter > 0)
            {
                timeOutCounter--;
                System.Threading.Thread.Sleep(10);
            }

            if (timeOutCounter != 0)
            {
                byte[] readBuf = new byte[uart.BytesToRead];
                uart.Read(readBuf, 0, uart.BytesToRead);
                if (readBuf[0] != 0xA5 || readBuf[1] != (byte)VCPGROUP.GPIO || readBuf[2] != 0x01 || readBuf[3] != 0xCC)
                    return false;
            }
            else
                return false;

            return true;
        }

        public bool resetUserIO( USERIOGROUP group, USERIOPIN pin )
        {
            byte[] buf = new byte[5];
            buf[0] = 0x5A;
            buf[1] = (byte)VCPGROUP.GPIO;
            buf[2] = 0x02;              //reset io
            buf[3] = (byte)group;       //group
            buf[4] = (byte)pin;         //pin
            uart.Write(buf, 0, 5);

            uint timeOutCounter = 200;
            while (uart.BytesToRead == 0 && timeOutCounter > 0)
            {
                timeOutCounter--;
                System.Threading.Thread.Sleep(10);
            }

            if (timeOutCounter != 0)
            {
                byte[] readBuf = new byte[uart.BytesToRead];
                uart.Read(readBuf, 0, uart.BytesToRead);
                if (readBuf[0] != 0xA5 || readBuf[1] != (byte)VCPGROUP.GPIO || readBuf[2] != 0x02 || readBuf[3] != 0xCC)
                    return false;
            }
            else
                return false;

            return true;
        }

        public bool toggleUserIO(USERIOGROUP group, USERIOPIN pin)
        {
            byte[] buf = new byte[6];
            buf[0] = 0x5A;
            buf[1] = (byte)VCPGROUP.GPIO;
            buf[2] = 0x03;         //toggle io
            buf[3] = (byte)group;      //group
            buf[4] = (byte)pin;         //pin
            uart.Write(buf, 0, 5);

            uint timeOutCounter = 200;
            while (uart.BytesToRead == 0 && timeOutCounter > 0)
            {
                timeOutCounter--;
                System.Threading.Thread.Sleep(10);
            }

            if (timeOutCounter != 0)
            {
                byte[] readBuf = new byte[uart.BytesToRead];
                uart.Read(readBuf, 0, uart.BytesToRead);
                if (readBuf[0] != 0xA5 || readBuf[1] != (byte)VCPGROUP.GPIO || readBuf[2] != 0x03 || readBuf[3] != 0xCC)
                    return false;
            }
            else
                return false;

            return true;
        }

        public byte readUserIO(USERIOGROUP group, USERIOPIN pin)
        {
            byte[] buf = new byte[6];
            buf[0] = 0x5A;
            buf[1] = (byte)VCPGROUP.GPIO;
            buf[2] = 0x04;         //toggle io
            buf[3] = (byte)group;      //group
            buf[4] = (byte)pin;         //pin
            uart.Write(buf, 0, 5);

            uint timeOutCounter = 200;
            while (uart.BytesToRead == 0 && timeOutCounter > 0)
            {
                timeOutCounter--;
                System.Threading.Thread.Sleep(10);
            }

            if (timeOutCounter != 0)
            {
                byte[] readBuf = new byte[uart.BytesToRead];
                uart.Read(readBuf, 0, uart.BytesToRead);
                if (readBuf[0] != 0xA5 || readBuf[1] != (byte)VCPGROUP.GPIO || readBuf[2] != 0x04)
                    return 0xFF;
                else
                    return readBuf[3];
            }
            else
                return 0xFF;

            //return readBuf[3];
        }

        //------------------------BASE FUNCTIONS---------------------------------
        public byte readSotFlag( )
        {
            byte[] buf = new byte[6];
            buf[0] = 0x5A;
            buf[1] = (byte)VCPGROUP.BASE;
            buf[2] = 0x01;         //toggle io

            uart.Write(buf, 0, 3);

            uint timeOutCounter = 200;
            while (uart.BytesToRead == 0 && timeOutCounter > 0)
            {
                timeOutCounter--;
                System.Threading.Thread.Sleep(10);
            }

            if (timeOutCounter != 0)
            {
                byte[] readBuf = new byte[uart.BytesToRead];
                uart.Read(readBuf, 0, uart.BytesToRead);
                if (readBuf[0] != 0xA5 || readBuf[1] != (byte)VCPGROUP.BASE || readBuf[2] != 0x01)
                    return 0xFF;
                else
                    return readBuf[3];
            }
            else
                return 0xFF;

            //return readBuf[3];
        }


        public bool clearSotFlag()
        {
            byte[] buf = new byte[6];
            buf[0] = 0x5A;
            buf[1] = (byte)VCPGROUP.BASE;
            buf[2] = 0x02;         //toggle io

            uart.Write(buf, 0, 3);

            uint timeOutCounter = 200;
            while (uart.BytesToRead == 0 && timeOutCounter > 0)
            {
                timeOutCounter--;
                System.Threading.Thread.Sleep(10);
            }

            if (timeOutCounter != 0)
            {
                byte[] readBuf = new byte[uart.BytesToRead];
                uart.Read(readBuf, 0, uart.BytesToRead);
                if (readBuf[0] != 0xA5 || readBuf[1] != (byte)VCPGROUP.BASE || readBuf[2] != 0x02 || readBuf[3] != 0xCC)
                    return false;
                else
                    return true;
            }
            else
                return false;

            //return readBuf[3];
        }


        public bool setBin(byte bin )
        {
            byte[] buf = new byte[6];
            buf[0] = 0x5A;
            buf[1] = (byte)VCPGROUP.BASE;
            buf[2] = (byte)(0x02 + bin);         //toggle io

            uart.Write(buf, 0, 3);

            uint timeOutCounter = 200;
            while (uart.BytesToRead == 0 && timeOutCounter > 0)
            {
                timeOutCounter--;
                System.Threading.Thread.Sleep(10);
            }

            if (timeOutCounter != 0)
            {
                byte[] readBuf = new byte[uart.BytesToRead];
                uart.Read(readBuf, 0, uart.BytesToRead);
                if (readBuf[0] != 0xA5 || readBuf[1] != (byte)VCPGROUP.BASE || readBuf[2] != (byte)(0x02 + bin) || readBuf[3] != 0xCC)
                    return false;
                else
                    return true;
            }
            else
                return false;

        }

    }
}
