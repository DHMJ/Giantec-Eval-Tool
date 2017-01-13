using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;

namespace MD.MDCommon
{
    public class RegisterProperty:INotifyPropertyChanged
    {
        private uint _index;
        private uint _regAddress;
        private string _regData;
        private bool _ifRead;
        private bool _ifWrite;
        //private string _Read;
        private uint maxData = 0xFF;
        private uint minData = 0x00;
        private RWProperty _rw;
        public event PropertyChangedEventHandler PropertyChanged;

        public RegisterProperty(uint index, uint regAddr, string regVal,
            RWProperty rw, bool ifRead, bool ifwrite)
        {
            this._index = index;
            this._regAddress = regAddr;
            this._regData = regVal;
            this._rw = rw;
            this._ifRead = ifRead;
            this._ifWrite = ifwrite;
            //_Read = "Read";
        }


        public event System.EventHandler GainChanged;
        protected virtual void OnGainChanged()
        {
            System.EventArgs e = new System.EventArgs();
            if (GainChanged != null) GainChanged(this, e);

        }

        public event System.EventHandler RegAddrChanged;
        protected virtual void OnRegAddrChanged()
        {
            System.EventArgs e = new System.EventArgs();
            if (RegAddrChanged != null) RegAddrChanged(this, e);

        }

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #region Register Properties
        public uint Index
        {
            get
            {
                return _index;
            }
            set
            {
                _index = (uint)value;
                NotifyPropertyChanged("Index");
            }

        }

        public uint RegAddr
        {
            get
            {
                return (uint)_regAddress;
            }
            set
            {
                if (value < 0xFF && value >= 0)
                {
                    _regAddress = (uint)value;
                    NotifyPropertyChanged("RegAddr");
                }
            }
        }

        public string RegData
        {
            get
            {
                return _regData;
            }
            set
            {
                if (UInt32.Parse(value, System.Globalization.NumberStyles.HexNumber) <= 0xFF && UInt32.Parse(value, System.Globalization.NumberStyles.HexNumber) >= 0)
                {
                    _regData = value.ToUpper();
                    NotifyPropertyChanged("RegData");
                }
            }
        }

        public RWProperty RW
        {
            get
            {
                return _rw;
            }
            set
            {
                _rw = value;
                NotifyPropertyChanged("RW");
            }
        }

        public bool ifRead
        {
            get
            {
                return _ifRead;
            }
            set
            {
                _ifRead = value;
                NotifyPropertyChanged("ifRead");
            }
        }

        public bool ifWrite
        {
            get
            {
                return _ifWrite;
            }
            set
            {
                _ifWrite = value;
                NotifyPropertyChanged("ifWrite");
            }
        }

        //public string Test
        //{
        //    get
        //    {
        //        return _Read;
        //    }
        //    set
        //    {
        //        _Read = value;
        //        NotifyPropertyChanged("Test");
        //    }
        //}
        #endregion
    }

    public enum RWProperty
    {
        RW,
        R,
        W
    }

}
