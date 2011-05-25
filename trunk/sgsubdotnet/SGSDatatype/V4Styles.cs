using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;

namespace SGSDatatype
{
    public class V4Styles: ISection
    {
        private string[] _styleFormat;
        public readonly List<V4Style> StyleList;

        public V4Styles()
        {
            StyleList = new List<V4Style>();
            _styleFormat = null;
        }

        public void AddLine(string line)
        {
            if(line[0]==';') return;
            var spliterpos = line.IndexOf(':');
            if(spliterpos == -1) return;
            char[] separator = {','};
            var linefmt = line.Substring(0, spliterpos).ToUpper().Trim();
            line = line.Substring(spliterpos + 1);
            switch (linefmt)
            {
                case "FORMAT":
                    _styleFormat = line.Split(separator);
                    for (var i = 0; i < _styleFormat.Length; i++)
                    {
                        _styleFormat[i] = _styleFormat[i].Trim();
                    }
                    break;
                case "STYLE":
                    if (_styleFormat == null) throw new Exception("Style Section Error");
                    var v4Style = new V4Style();
                    var fields = line.Split(separator);
                    for (var i = 0; i < _styleFormat.Length; i++)
                    {
                        var field = (ISSAField) v4Style.GetProperty(_styleFormat[i]);
                        if (i < fields.Length && field != null)
                            field.FromString(fields[i]);
                    }
                    StyleList.Add(v4Style);
                    break;
                default:
                    return;
            }
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
        public SSAInt Fontsize { get; set; }
        public SSAColour PrimaryColour { get; set; }
        public SSAColour SecondaryColour { get; set; }
        public SSAColour TertiaryColour { get; set; }
        public SSAColour BackColour { get; set; }
        public SSABool Bold { get; set; }
        public SSABool Italic { get; set; }
        public SSAInt BorderStyle { get; set; }
        public SSAInt Outline { get; set; }
        public SSAInt Shadow { get; set; }
        public SSAInt Alignment { get; set; }
        public SSAInt MarginL { get; set; }
        public SSAInt MarginR { get; set; }
        public SSAInt MarginV { get; set; }
        public SSAString AlphaLevel { get; set; }
        public SSAString Encoding { get; set; }
        public V4Style()
        {
            Name = new SSAString();
            Fontname = new SSAString();
            Fontsize = new SSAInt();
            PrimaryColour = new SSAColour();
            SecondaryColour = new SSAColour();
            TertiaryColour = new SSAColour();
            BackColour = new SSAColour();
            Bold = new SSABool();
            Italic = new SSABool();
            BorderStyle = new SSAInt();
            Outline = new SSAInt();
            Shadow = new SSAInt();
            Alignment = new SSAInt();
            MarginL = new SSAInt();
            MarginR = new SSAInt();
            MarginV = new SSAInt();
            AlphaLevel = new SSAString();
            Encoding= new SSAString();
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
