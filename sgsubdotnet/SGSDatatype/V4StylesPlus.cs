using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;

namespace SGSDatatype
{
    public class V4StylesPlus : ISection
    {
        private string[] _styleFormat;
        public readonly List<V4StyleP> StyleList;
        public V4StylesPlus()
        {
            StyleList = new List<V4StyleP>();
            _styleFormat = null;
        }

        public void AddLine(string line)
        {
            if (line[0] == ';') return;
            var spliterpos = line.IndexOf(':');
            if (spliterpos == -1) return;
            char[] separator = { ',' };
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
                    var v4StyleP = new V4StyleP();
                    var fields = line.Split(separator);
                    for (var i = 0; i < _styleFormat.Length; i++)
                    {
                        var field = v4StyleP.GetProperty(_styleFormat[i]);
                        if (i < fields.Length && field != null)
                            field.FromString(fields[i]);
                    }
                    StyleList.Add(v4StyleP);
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
            get { return "v4+ Styles"; }
        }
    }

    public class V4StyleP
    {
        public SSAString Name { get; set; }
        public SSAString Fontname { get; set; }
        public SSAInt Fontsize { get; set; }
        public SSAColour PrimaryColour { get; set; }
        public SSAColour SecondaryColour { get; set; }
        public SSAColour OutlineColor { get; set; }
        public SSAColour BackColour { get; set; }
        public SSABool Bold { get; set; }
        public SSABool Italic { get; set; }
        public SSABool Underline { get; set; }
        public SSABool Strikeout { get; set; }
        public SSADecimal ScaleX { get; set; }
        public SSADecimal ScaleY { get; set; }
        public SSAInt Spacing { get; set; }
        public SSADecimal Angle { get; set; }
        public SSAInt BorderStyle { get; set; }
        public SSAInt Outline { get; set; }
        public SSAInt Shadow { get; set; }
        public SSAInt MarginL { get; set; }
        public SSAInt MarginR { get; set; }
        public SSAInt MarginV { get; set; }
        public SSAString AlphaLevel { get; set; }
        public SSAString Encoding { get; set; }

        public V4StyleP()
        {
            Name = new SSAString();
            Fontname = new SSAString();
            Fontsize = new SSAInt();
            PrimaryColour = new SSAColour();
            SecondaryColour = new SSAColour();
            OutlineColor = new SSAColour();
            BackColour = new SSAColour();
            Bold = new SSABool();
            Italic = new SSABool();
            Underline=new SSABool();
            Strikeout = new SSABool();
            ScaleX = new SSADecimal();
            ScaleY = new SSADecimal();
            Spacing = new SSAInt();
            Angle = new SSADecimal();
            BorderStyle = new SSAInt();
            Outline = new SSAInt();
            Shadow = new SSAInt();
            MarginL = new SSAInt();
            MarginR = new SSAInt();
            MarginV = new SSAInt();
            AlphaLevel = new SSAString();
            Encoding = new SSAString();
        }

        public void SetProperty(string propertyName, ISSAField value)
        {
            var propertyInfo = typeof(V4StyleP).GetProperty(propertyName,
                                                            BindingFlags.Public | BindingFlags.IgnoreCase |
                                                            BindingFlags.Instance);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(this, value, null);
            }
        }
        public ISSAField GetProperty(string propertyName)
        {
            var propertyInfo = typeof(V4StyleP).GetProperty(propertyName,
                                                            BindingFlags.Public | BindingFlags.IgnoreCase |
                                                            BindingFlags.Instance);
            return propertyInfo != null ? (ISSAField) propertyInfo.GetValue(this, null) : null;
        }

    }



}
