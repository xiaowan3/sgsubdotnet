using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSDatatype
{
    public interface ISSAField
    {
        void FromString(string str);
    }

    public class SSAColour:ISSAField
    {
        private UInt32 _colorVal;
        public void FromString(string str)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }

    public class SSABool:ISSAField
    {
        public void FromString(string str)
        {
            throw new NotImplementedException();
        }
    }

    public class SSAString:ISSAField
    {
        public string Str;
        public void FromString(string str)
        {
            Str = str;
        }
    }
}
