using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;

namespace SGSDatatype
{
    class V4Styles: ISection
    {
        private string[] _styleFormat;
        public void AddLine(string line)
        {
            var spliterpos = line.IndexOf(':');
            char[] separator = {','};
            var linefmt = line.Substring(0, spliterpos).ToUpper().Trim();
            line = line.Substring(spliterpos + 1);
            switch (linefmt)
            {
                case "FORMAT":
                    _styleFormat = line.Split(separator);
                    break;
                case "STYLE":
                    if (_styleFormat == null) throw new Exception("Style Section Error");

                    break;
                default:
                    return;
            }
            throw new NotImplementedException();
        }

        public void WriteTo(Stream stream)
        {
            throw new NotImplementedException();
        }

        public void WriteTo(Stream stream, Encoding encoding)
        {
            throw new NotImplementedException();
        }

        public string SectionName
        {
            get { return "v4 Styles"; }
        }
    }

    public class V4Style
    {

        public SSAString Name { get; set; }
        public SSAString Fontname { get; set; }
        public int Fontsize { get; set; }
        public SSAColour PrimaryColour { get; set; }
        public SSAColour SecondaryColour { get; set; }
        public SSAColour TertiaryColour { get; set; }
        public SSAColour BackColour { get; set; }
        public bool Bold { get; set; }
        public bool Italic { get; set; }
        public int BorderStyle { get; set; }
        public int Outline { get; set; }
        public int Shadow { get; set; }
        public int Alignment { get; set; }
        public int MarginL { get; set; }
        public int MarginR { get; set; }
        public int MarginV { get; set; }
        public SSAString AlphaLevel { get; set; }
        public SSAString Encoding { get; set; }
        public V4Style()
        {
            Name = new SSAString();
            Fontname = new SSAString();
        }
        
        public void SetProperty(string propertyName,object value)
        {
            var propertyInfo = typeof (V4Style).GetProperty(propertyName,
                                                            BindingFlags.Public | BindingFlags.IgnoreCase |
                                                            BindingFlags.Instance);
            if(propertyInfo != null)
            {
                propertyInfo.SetValue(this,value,null);
            }
        }
        public object GetProperty(string propertyName)
        {
            var propertyInfo = typeof (V4Style).GetProperty(propertyName,
                                                            BindingFlags.Public | BindingFlags.IgnoreCase |
                                                            BindingFlags.Instance);
            return propertyInfo != null ? propertyInfo.GetValue(this, null) : null;
        }
    }
    
}
