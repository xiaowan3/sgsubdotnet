using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SGSDatatype
{
    public interface ISSAField
    {
        void FromString(string str);
    }

    public class SSAColour : ISSAField
    {
        private UInt32 _colorVal;
        public void FromString(string str)
        {
            var trimed = str.Trim().ToUpper();
            if (trimed.Substring(0, 2) != "&H")
            {
                _colorVal = 0;
                return;
            }
            try
            {
                _colorVal = UInt32.Parse(trimed.Substring(2), NumberStyles.HexNumber);
            }
            catch (Exception)
            {
                _colorVal = 0;
            }

        }

        public override string ToString()
        {
            var str = _colorVal.ToString("X8");
            return "&H" + str;
        }
    }

    public class SSABool : ISSAField
    {
        public bool Value;
        public void FromString(string str)
        {
            int val;
            if (int.TryParse(str, out val))
            {
                Value = val != 0;
                return;
            }
            Value = false;
        }
        public override string ToString()
        {
            return Value ? "-1" : "0";
        }
    }

    public class SSAString : ISSAField
    {
        public string Value;
        public void FromString(string str)
        {
            Value = str;
        }
        public override string ToString()
        {
            return Value;
        }
    }
    public class SSAInt : ISSAField
    {
        public int Value;
        public void FromString(string str)
        {
            int val;
            if (int.TryParse(str, out val))
            {
                Value = val;
                return;
            }

            Value = 0;
        }
    }
}
