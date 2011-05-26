using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SGSDatatype
{
    public interface ISSAField
    {
        void FromString(string str);
    }

    [DataContract(Name = "SSAColour", Namespace = "SGSDatatype")]
    public class SSAColour : ISSAField
    {
        [DataMember]
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

    [DataContract(Name = "SSABool", Namespace = "SGSDatatype")]
    public class SSABool : ISSAField
    {
        [DataMember]
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

    [DataContract(Name = "SSAString", Namespace = "SGSDatatype")]
    public class SSAString : ISSAField
    {
        [DataMember]
        public string Value = "";
        public void FromString(string str)
        {
            Value = str;
        }
        public override string ToString()
        {
            return Value;
        }
    }

    [DataContract(Name = "SSAInt", Namespace = "SGSDatatype")]
    public class SSAInt : ISSAField
    {
        [DataMember]
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
        public override string ToString()
        {
            return Value.ToString();
        }
    }

    [DataContract(Name = "SSADecimal", Namespace = "SGSDatatype")]
    public class SSADecimal:ISSAField
    {
        [DataMember]
        public decimal Value;
        public void FromString(string str)
        {
            if(decimal.TryParse(str,out Value)) return;
            Value = 0;
        }
        public override string ToString()
        {
            return Value.ToString();
        }
    }

    [DataContract(Name = "SSATime", Namespace = "SGSDatatype")]
    public class SSATime : ISSAField
    {
        [DataMember]
        public double Value;
        public void FromString(string str)
        {
            char[] separator = { ':' };
            var segs = str.Trim().Split(separator);
            double sum = 0;
            double multiply = 1;
            if (segs.Length > 4) //Error
            {
                Value = 0;
                return;
            }
            if (segs.Length == 4) //Style HH:MM:SS:xx
            {
                try
                {
                    for (int i = segs.Length - 1; i >= 0; i--)
                    {
                        sum += double.Parse(segs[i]) * multiply;
                        multiply *= 60;
                    }
                    Value = sum;
                }
                catch (Exception)
                {
                    Value = 0;
                }
            }
            //Style HH:MM:SS.xx or MM:SS.xx or SSSSS.xx

            try
            {
                for (int i = segs.Length - 1; i >= 0; i--)
                {
                    sum += double.Parse(segs[i]) * multiply;
                    multiply *= 60;
                }
                Value = sum;
            }
            catch (Exception)
            {
                Value = 0;
            }
        }
        public override string ToString()
        {
            double dsec = Math.Truncate(Value);
            double ms = Value - dsec;
            var sec = (int) dsec;
            int h = sec/3600;
            int mm = (sec%3600)/60;
            double ss = sec%60 + ms;
            return string.Format("{0}:{1}:{2}", h.ToString("D1"), mm.ToString("D2"), ss.ToString("00.00"));

        }
    }

    [DataContract(Name = "SSAMargin", Namespace = "SGSDatatype")]
    public class SSAMargin:ISSAField
    {
        [DataMember]
        public int Value;
        public void FromString(string str)
        {
            if (int.TryParse(str, out Value)) return;
            Value = 0;
        }
        public override string ToString()
        {
            return Value.ToString("D4");
        }
    }

    public enum SSAVersion{V4, V4Plus}
}
