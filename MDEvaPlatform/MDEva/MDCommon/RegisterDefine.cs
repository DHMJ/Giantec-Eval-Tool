using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace MD.MDCommon
{
    [Serializable]
    public class BitField
    {
        uint mask;        
        /// <summary>
        /// Initialize bit field and set default bit field value.
        /// </summary>
        /// <param name="bits">Contain bits start and end infomation</param>
        /// <param name="name">the name of this bit field</param>
        /// <param name="description">bit field description, include each bit value description</param>
        /// <param name="value">default register value</param>
        public BitField(string _bits, int byteCountOfReg, string name, string description, string regValue)
        {
            // Set name firstly is good for init when register init. Can update bf later.
            bfName = name;

            // if no bits info inputed
            bits = _bits.Trim("[]".ToCharArray());
            if (bits == null || bits.Length == 0 || name == "")
                return;

            // Get byte count of this bf
            string[] bits_inByte = _bits.Trim().Replace("\r\n", ",").Split(',');
            if(bits_inByte == null || bits_inByte.Length == 0)
                return;

            byteCount = bits_inByte.Length;
            bfValue_byte = new byte[byteCount];

            // calc end bit and start bit index
            string[] bits_ix;
            int bracketIx1, bracketIx2;
            byteCountOfReg = (byteCountOfReg > 4) ? 4 : byteCountOfReg;         // if byteCountOfReg > 4, will just use 4 instead
            if (byteCount == 1)     // this bf length is less than 8bits
            {
                // Normal registger, each reg contains 8 bits value, bits should like [x:y] or [x]
                if (bits_inByte.Length <= 5)
                {
                    bits_ix = bits_inByte[0].Trim("[]".ToCharArray()).Split(':');
                    int.TryParse(bits_ix[0], out endBit);
                    int.TryParse(bits_ix[bits_ix.Length - 1], out startBit);    // if length is 1([x]), then startbit = endBit;if 2([x:y]), then they are different
                }
                // 1 reg contains more than 1 bytes, bits should like bytex[y:z] or bytex[y]
                else
                {
                    bracketIx1 = bits_inByte[0].IndexOf("[");
                    bracketIx2 = bits_inByte[0].IndexOf("]");
                    int _byteIx = int.Parse(bits_inByte[0].Substring(bracketIx1 - 1, 1));

                    bits_ix = bits_inByte[0].Substring(bracketIx1, bracketIx2 - bracketIx1 + 1).Trim("[]".ToCharArray()).Split(':');
                    int.TryParse(bits_ix[0], out endBit);
                    int.TryParse(bits_ix[bits_ix.Length - 1], out startBit);
                    endBit += (byteCountOfReg - _byteIx - 1) * 8;
                    startBit += (byteCountOfReg - _byteIx - 1) * 8;
                }
            }
            else  // some case like bytex1[y1:z1], bytex2[y2:z2]...
            {
                // Calc end bit
                bracketIx1 = bits_inByte[0].IndexOf("[");
                bracketIx2 = bits_inByte[0].IndexOf("]");
                int _byteIx = int.Parse(bits_inByte[0].Substring(bracketIx1 - 1, 1));

                bits_ix = bits_inByte[0].Substring(bracketIx1, bracketIx2 - bracketIx1 + 1).Trim("[]".ToCharArray()).Split(':');
                int.TryParse(bits_ix[0], out endBit);
                endBit += (byteCountOfReg - _byteIx - 1) * 8;

                // Calc start bit
                bracketIx1 = bits_inByte[bits_inByte.Length - 1].IndexOf("[");
                bracketIx2 = bits_inByte[bits_inByte.Length - 1].IndexOf("]");
                _byteIx = int.Parse(bits_inByte[bits_inByte.Length - 1].Substring(bracketIx1 - 1, 1));

                bits_ix = bits_inByte[bits_inByte.Length - 1].Substring(bracketIx1, bracketIx2 - bracketIx1 + 1).Trim("[]".ToCharArray()).Split(':');
                int.TryParse(bits_ix[bits_ix.Length - 1], out startBit);
                startBit += (byteCountOfReg - _byteIx - 1) * 8;
            }

            // Calc bitLength            
            bitLength = endBit - startBit + 1;

            // Get bf description
            bfDesc = description;

            // calc mask based on startBit and bitLength
            mask = maskGen(startBit, bitLength);

            uint.TryParse(regValue.Replace("0x",""), System.Globalization.NumberStyles.HexNumber, null, out bfValue);
            bfValue = (bfValue & mask) >> startBit;

            //bitValueMeaning = new string[(int)Math.Pow(2, bitLength)];
            //Console.WriteLine(BFValueInRegValue.ToString("X2"));        
        }

        public void InitiBF(string _bits, int byteCountOfReg, string name, string description, string regValue)
        {
            // Set name firstly is good for init when register init. Can update bf later.
            bfName = name;

            // if no bits info inputed
            bits = _bits.Trim("[]".ToCharArray());
            if (bits == null || bits.Length == 0 || name == "")
                return;

            // Get byte count of this bf
            string[] bits_inByte = _bits.Trim().Replace(" ", "").Replace("\r\n", ",").Split(',');
            if(bits_inByte == null || bits_inByte.Length == 0)
                return;

            byteCount = bits_inByte.Length;
            bfValue_byte = new byte[byteCount];

            // calc end bit and start bit index
            string[] bits_ix;
            int bracketIx1, bracketIx2;
            byteCountOfReg = (byteCountOfReg > 4) ? 4 : byteCountOfReg;         // if byteCountOfReg > 4, will just use 4 instead
            if (byteCount == 1)     // this bf length is less than 8bits
            {
                // Normal registger, each reg contains 8 bits value, bits should like [x:y] or [x]
                if (bits_inByte.Length <= 5)
                {
                    bits_ix = bits_inByte[0].Trim("[]".ToCharArray()).Split(':');
                    int.TryParse(bits_ix[0], out endBit);
                    int.TryParse(bits_ix[bits_ix.Length - 1], out startBit);    // if length is 1([x]), then startbit = endBit;if 2([x:y]), then they are different
                }
                // 1 reg contains more than 1 bytes, bits should like bytex[y:z] or bytex[y]
                else
                {
                    bracketIx1 = bits_inByte[0].IndexOf("[");
                    bracketIx2 = bits_inByte[0].IndexOf("]");
                    int _byteIx = int.Parse(bits_inByte[0].Substring(bracketIx1 - 1, 1));

                    bits_ix = bits_inByte[0].Substring(bracketIx1, bracketIx2 - bracketIx1 + 1).Trim("[]".ToCharArray()).Split(':');
                    int.TryParse(bits_ix[0], out endBit);
                    int.TryParse(bits_ix[bits_ix.Length - 1], out startBit);
                    endBit += (byteCountOfReg - _byteIx - 1) * 8;
                    startBit += (byteCountOfReg - _byteIx - 1) * 8;
                }
            }
            else  // some case like bytex1[y1:z1], bytex2[y2:z2]...
            {
                // Calc end bit
                bracketIx1 = bits_inByte[0].IndexOf("[");
                bracketIx2 = bits_inByte[0].IndexOf("]");
                int _byteIx = int.Parse(bits_inByte[0].Substring(bracketIx1 - 1, 1));

                bits_ix = bits_inByte[0].Substring(bracketIx1, bracketIx2 - bracketIx1 + 1).Trim("[]".ToCharArray()).Split(':');
                int.TryParse(bits_ix[0], out endBit);
                endBit += (byteCountOfReg - _byteIx - 1) * 8;

                // Calc start bit
                bracketIx1 = bits_inByte[bits_inByte.Length - 1].IndexOf("[");
                bracketIx2 = bits_inByte[bits_inByte.Length - 1].IndexOf("]");
                _byteIx = int.Parse(bits_inByte[bits_inByte.Length - 1].Substring(bracketIx1 - 1, 1));

                bits_ix = bits_inByte[bits_inByte.Length - 1].Substring(bracketIx1, bracketIx2 - bracketIx1 + 1).Trim("[]".ToCharArray()).Split(':');
                int.TryParse(bits_ix[bits_ix.Length - 1], out startBit);
                startBit += (byteCountOfReg - _byteIx - 1) * 8;
            }

            // Calc bitLength            
            bitLength = endBit - startBit + 1;

            // Get bf description
            bfDesc = description;

            // calc mask based on startBit and bitLength
            mask = maskGen(startBit, bitLength);

            uint.TryParse(regValue.Replace("0x",""), System.Globalization.NumberStyles.HexNumber, null, out bfValue);
            bfValue = (bfValue & mask) >> startBit;

            //bitValueMeaning = new string[(int)Math.Pow(2, bitLength)];
            //Console.WriteLine(BFValueInRegValue.ToString("X2"));
        }

        private uint maskGen(int _startIx, int _len)
        {
            uint mask = ((uint)Math.Pow(2, _len) - 1) << _startIx;
            return mask;
        }

        public uint BFMask
        {
            get { return this.mask; }
        }

        private string bits;
        public string BITs
        {
            get { return this.bits; }
        }

        private int startBit;
        public int StartBit
        {
            get { return startBit; }
        }

        private int endBit;
        public int EndBit
        {
            get { return endBit; }
        }

        private int bitLength;
        public int BitLength
        {
            get { return bitLength; }
        }

        private int byteCount = 1;
        public int ByteCount
        {
            get { return this.byteCount; }
        }

        // This value just should in bf value on GUI, not the value in register
        private uint bfValue;
        public uint BFValue
        { 
            get { return bfValue; }
            // Due to set the bif field value with reg value, mask is needed
            set { bfValue = value; } 
        }

        private byte[] bfValue_byte;
        public byte[] BFValue_byte
        {
            get 
            {
                uint tempValue = BFValueInRegValue;
                uint byteMask = 0xFF;
                for (int ix = 0; ix < byteCount; ix++)
                {
                    bfValue_byte[byteCount - ix - 1] = (byte)((tempValue >> ((startBit / 8 + ix) * 8)) & byteMask);
                }

                return this.bfValue_byte; 
            }
        }

        public uint BFMaxValue
        {
            get { return (uint)Math.Pow(2, bitLength) - 1; }
        }

        // This is the value in the register, which means added offset in bfValue
        public uint BFValueInRegValue
        {
            get { return bfValue << startBit; }
            set { bfValue = (value & mask) >> startBit; } 
        }

        private string bfName;
        public string BFName
        {
            get { return bfName; }
        }

        private string bfDesc;
        public string BFDesc
        {
            get { return bfDesc; }
        }

        //private string[] bitValueMeaning;
        //public string[] BitValueMeaning
        //{
        //    get { return bitValueMeaning; }
        //}
    }

    [Serializable]
    public class Register
    {
        /// <summary>
        /// One Register(8 bits)
        /// </summary>
        /// <param name="_regGroup">the group name.</param>
        /// <param name="_regName">Register name.</param>
        /// <param name="_regAddress">Register address.</param>
        /// <param name="_rw"> register access propery.</param>
        /// <param name="_defaultValue">Defalut Value. </param>
        /// <param name="_paras">Bit field names array, LSB first.
        /// 3 bf example:bit7 - bit0: bf_2, bf_1, bf_0 -> _paras[0] = bf_0.name ... _paras[2] = bf_2.name</param>
        public Register(string _regGroup, string _regName, string _regAddress, RWProperty _rw, string _defaultValue, params object[] _paras)
        {
            this.groupName = _regGroup;
            this.regName = _regName;
            this.regAddr = byte.Parse(_regAddress.Replace("0x",""), System.Globalization.NumberStyles.HexNumber);
            this.rwPro = _rw;
            this.regValue = byte.Parse(_defaultValue.Replace("0x", ""), System.Globalization.NumberStyles.HexNumber);
            this.paras = _paras;

            bfList.Clear();
            for(int ix = 0; ix < _paras.Length; ix++)
            {
                bfList.Add(new BitField("", this.byteCount, (string)_paras[ix], "", ""));
            }
        }

        /// <summary>
        /// One Register, more than 8bits.
        /// </summary>
        /// <param name="_regGroup">the group name.</param>
        /// <param name="_regName">Register name.</param>
        /// <param name="_regAddress">Register address.</param>
        /// <param name="_rw"> register access propery.</param>
        /// <param name="_byteCount">how many bytes will belong this register.</param>
        /// <param name="_defaultValue">Defalut Value. </param>
        /// <param name="_paras">Bit field names array, LSB first.
        /// 3 bf example:bit7 - bit0: bf_2, bf_1, bf_0 -> _paras[0] = bf_0.name ... _paras[2] = bf_2.name</param>
        public Register(string _regGroup, string _regName, string _regAddress, RWProperty _rw, string _byteCount, string _defaultValue, params object[] _paras)
        {
            this.groupName = _regGroup;
            this.regName = _regName;
            this.regAddr = byte.Parse(_regAddress.Replace("0x", ""), System.Globalization.NumberStyles.HexNumber);
            this.rwPro = _rw;
            int.TryParse(_byteCount, out this.byteCount);
            regValue_byte = new byte[byteCount];

            this.regValue = 0 ;
            UInt32.TryParse(_defaultValue.Replace("0x", ""), System.Globalization.NumberStyles.HexNumber, null, out regValue);
            this.paras = _paras;

            bfList.Clear();
            for (int ix = 0; ix < _paras.Length; ix++)
            {
                bfList.Add(new BitField("", this.byteCount, (string)_paras[ix], "", ""));
            }
        }

        private List<BitField> bfList = new List<BitField> { };

        private int byteCount = 1;
        public int ByteCount
        {
            get { return this.byteCount; }
        }

        private string groupName;
        public string GroupName
        {
            set { this.groupName = value; }
            get { return groupName; }
        }

        private string regName;
        public string RegName
        {
            get { return regName; }
        }

        private byte regAddr;
        public byte RegAddress
        {
            get { return regAddr; }
        }

        private RWProperty rwPro;
        public RWProperty RWPro
        {
            get { return rwPro; }
        }

        private object[] paras;

        public bool UseByteArray
        {
            get { return byteCount > 1 ? true : false; }
        }

        public bool DisplayRegValueCell
        {
            get { return byteCount > 4 ? true : false; }
        }

        private UInt32 regValue = 0;
        public UInt32 RegValue
        {
            get { return regValue; }
            set
            {
                if (this.regValue != value && byteCount <= 4)
                {
                    this.regValue = value;
                    UpdateBFValue();
                }
            }
        }

        // save register value as byte array, MSB first(e.g. bit24-31 save in regValue_byte[0])
        private byte[] regValue_byte;
        public byte[] ByteValue
        {
            get 
            {
                uint byteMask = 0xFF;
                if (byteCount <= 4)
                {
                    for (int ix = 0; ix < byteCount; ix++)
                    {
                        regValue_byte[byteCount - ix - 1] = (byte)((regValue >> (ix * 8)) & byteMask);
                    }
                }
                else
                {
                    int ix_dest = 0;
                    for (int ix_bf = 0; ix_bf < BFCount; ix_bf++)
                    {
                        for (int ix = 0; ix < bfList[ix_bf].ByteCount; ix++)
                        {
                            regValue_byte[ix_dest++] = bfList[ix].BFValue_byte[ix];
                        }
                    }
                }
                return regValue_byte; 
            }
        }

        public int BFCount
        {
            get { return this.bfList.Count; }
        }

        public uint RegMaxValue
        {
            get 
            {
                if (byteCount == 1)
                    return byte.MaxValue;
                else if (byteCount == 2)
                    return UInt16.MaxValue;
                else if (byteCount == 4)
                    return UInt32.MaxValue;
                else
                    return 0;
            }
        }

        public BitField this[string name]
        {
            get
            {
                if(bfList.Count > 0)
                {
                    foreach (BitField bf in bfList)
                    {
                        if (bf.BFName.ToLower() == name.ToLower())
                            return bf;
                    }
                }
                return null;
            }
        }

        public BitField this[int ix]
        {
            get
            {
                if (bfList.Count > ix)
                {
                    return bfList[ix];
                }
                return null;
            }
        } 

        #region Methods
        public UInt32 GetBFValue(string _bfName)
        {
           return this[_bfName].BFValue;
        }

        public UInt32 GetBFValue(int _bfIx)
       {
           return bfList[_bfIx].BFValue;
       }
                
        private void UpdateBFValue()
        {
            // Can just update bf value with reg value, will mask inside BitField property
            foreach (BitField bf in bfList)
            {
                bf.BFValueInRegValue = this.regValue;
            }
        }

        private void UpdataRegValue()
        {
            UInt32 temp = 0;
            for (int ix = 0; ix < bfList.Count; ix++)
            {
                temp += bfList[ix].BFValueInRegValue;
            }
            this.regValue = temp;
        }

        public void SetBFValue(string _bfName, string _bfValue)
        {
            uint temp = regValue;
            BitField tempBF = this[_bfName];
            //tempBF.BFValue = _bfValue;
            tempBF.BFValue = uint.Parse(_bfValue, System.Globalization.NumberStyles.HexNumber);

            if (byteCount <= 4)
            {
                temp &= ~tempBF.BFMask;
                temp |= tempBF.BFValueInRegValue;

                this.regValue = temp;
            }
            else
            {
                // do nothing, because all data will store in byte array
            }
        }

        public bool Contain(string _bfName)
        {
            bool ret = false;
            foreach (BitField bf in bfList)
            {
                if (bf.BFName.ToUpper() == _bfName.ToUpper())
                    return true;
            }
            return ret;
        }

        #endregion Methods
    }

    [Obsolete]
    public class Register_backup
    {
        /// <summary>
        /// One Register(8 bits)
        /// </summary>
        /// <param name="_regName">This register's name.</param>
        /// <param name="_regAddress">This register's address.</param>
        /// <param name="_paras">All of the units, which make up the this register.And the params
        /// add from bit0 to bit7.
        /// Format:(p1+p2)->string p1_name,int p1_bitsCount,string p2_name,int p2_bitsCount</param>
        public Register_backup( string _regName,int _regAddress, params object[] _paras)
        {
            this.regName = _regName;
            this.paras = _paras;
            this.regAddr = _regAddress;
            SeparateParamsToUnits();
        }

        private string regName;
        public string RegName
        {
            get { return regName; }
        }

        private int regAddr;
        public int RegAddress
        {
            get { return regAddr; }
        }

        private object[] paras;

        private byte regValue = 0;
        public byte RegValue
        {
            get { return regValue; }
            set
            {
                if (this.regValue != value)
                {
                    this.regValue = value;
                    SeparateParamsToUnits();
                }
            }
        }

        private struct UnitInReg
        {
            public string unitName;
            public int unitValue;
            public int startIndex;
            public int bitsCount;
            public object controllor;
        }

        #region Methods
        public void BindingWith(ref int value, string _uintName)
        {
            value = (int)ValuesTable[_uintName];
        }

        private Hashtable ValuesTable = new Hashtable();

        //private Hashtable UnitsValue
        //{
        //    get { return ValuesTable; }
        //}

        public int GetUnitValue(string _unitName)
        {
            int temp;
            try
            {
                temp = (int)ValuesTable[_unitName];
            }
            catch
            {
                Console.WriteLine("GetUnitValue {0} from Reg{1} failed.", _unitName, RegAddress.ToString("X"));
                return -1;
            }
            return temp;
        }

        public bool SetUnitValue(string _unitName, int _value)
        {
            int temp = (int)ValuesTable[_unitName];
            if (temp != _value)
                try
                {
                    ValuesTable[_unitName] = _value;
                    UpdataRegValue();
                }
                catch
                {
                    return false;
                }
            return true;
        }

        // Format:(p1+p2)->string p1_name,int p1_bits,string p2_name,int p2_bits
        private void SeparateParamsToUnits()
        {
            ValuesTable.Clear();
            int startIndex = 0;
            for (int i = 0; i < (int)(paras.Length / 2); i++)
            {
                ValuesTable.Add((string)paras[i * 2], CalcUnitValue(startIndex,(int)paras[i * 2 + 1]));   //The firstly unit start at bit0.
                startIndex += (int)paras[i * 2 + 1];    //The next unit start index.
            }
        }

        /// <summary>
        /// Set register value by each unit's value.
        /// </summary>
        private void UpdataRegValue()
        {
            int startIndex = 0;
            int temp = 0;
            for (int i = 0; i < (int)(paras.Length / 2); i++)
            {
                //Get unit name by paras array, and get each unit value from ValuesTable, which just updated.
                temp += CalcRegValue(startIndex, (int)ValuesTable[(string)paras[i * 2]]);
                startIndex += (int)paras[i * 2 + 1];
            }
            //for (int i = 0; i < units.Count; i++)
            //{
            //    temp += CalcRegValue(units[i].startIndex, units[i].unitValue);
            //}
            this.regValue = (byte)temp;
        }

        /// <summary>
        /// Calculate each unit value by register value.
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="bitsCount"></param>
        /// <returns></returns>
        private int CalcUnitValue(int startIndex, int bitsCount)
        {
            int unitValue = 0;
            int flag = (int)(Math.Pow(2, (startIndex + bitsCount)) - Math.Pow(2, startIndex));
            unitValue = (regValue & flag) >> startIndex;

            return unitValue;
        }

        /// <summary>
        /// Calculate register value by each unit value.
        /// </summary>
        /// <returns></returns>
        private int CalcRegValue(int startIndex, int value)
        {
            return (value << startIndex);
        }
        //public void SetRegValue(byte _val)
        //{
        //    this.regValue = _val;
        //}

        //public byte GetRegValue()
        //{
        //    return this.regValue;
        //}
        #endregion
    }

    [Serializable]
    public class RegisterMap
    {
        private uint devAddress = 0;
        private List<Register> RegistersList = new List<Register> { };
        private List<string> RegGroupList = new List<string> { };
        public RegisterMap()
        {
            RegGroupList.Clear();
            RegistersList.Clear();
        }

        public uint DevAddr
        {
            set { this.devAddress = value; }
            get { return this.devAddress; }
        }

        public void AddGroup(string _groupName)
        {
            if(!RegGroupList.Contains(_groupName))
                this.RegGroupList.Add(_groupName);
        }

        public int GroupCount
        {
            get { return this.RegGroupList.Count; }
        }

        public string GetGroupName(int _ix)
        {
            return this.RegGroupList[_ix];
        }

        public void Add(Register _reg)
        {
            this.RegistersList.Add(_reg);
        }

        public void Remove(Register _reg)
        {
            this.RegistersList.Remove(_reg);
        }

        public void RemoveAt(int index)
        {
            this.RegistersList.RemoveAt(index);
        }

        public void Clear()
        {
            this.RegistersList.Clear();
        }

        public int Count()
        {
            return this.RegistersList.Count;
        }

        /// <summary>
        /// Get the register by register's address.
        /// </summary>
        /// <param name="_regAddress">Int type register address.</param>
        /// <returns></returns>
        public Register this[int _regAddress]
        {
            get
            {
                foreach (Register reg in RegistersList)
                {
                    if (reg.RegAddress == _regAddress)
                        return reg;
                }
                return null;
            }
        }

        /// <summary>
        /// Get the register by register's name.
        /// </summary>
        /// <param name="_regName">string type register name.</param>
        /// <returns></returns>
        public Register this[string _regName]
        {
            get
            {
                foreach (Register reg in RegistersList)
                {
                    if (reg.RegName.ToLower() == _regName.ToLower())
                        return reg;
                }
                return null;
            }
        }

        public List<Register> RegList
        {
            get { return this.RegistersList; }
        }
    }

    public class RegTap
    { }
}
