using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Reflection;
using System.Windows.Forms;

namespace SGSDatatype
{
    [DataContract(Name = "V4StylesPlus", Namespace = "SGSDatatype")]
    [KnownType(typeof(Style))]
    public class SSAStyles : ISection
    {
        [DataMember]
        private string[] _styleFormat;

        [DataMember]
        public readonly BindingSource StyleList;
        public SSAStyles()
        {
            StyleList = new BindingSource();
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
                    var v4StyleP = new Style();
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

    public class Style
    {

        [DataMember]
        public SSAString Name { get; set; }

        [DataMember]
        public SSAString Fontname { get; set; }

        [DataMember]
        public SSAInt Fontsize { get; set; }

        [DataMember]
        public SSAColour PrimaryColour { get; set; }

        [DataMember]
        public SSAColour SecondaryColour { get; set; }

        [DataMember]
        public SSAColour OutlineColor { get; set; }

        public SSAColour TertiaryColour
        {
            get { return OutlineColor; }
            set { OutlineColor = value; }
        }

        [DataMember]
        public SSAColour BackColour { get; set; }

        [DataMember]
        public SSABool Bold { get; set; }

        [DataMember]
        public SSABool Italic { get; set; }

        [DataMember]
        public SSABool Underline { get; set; }

        [DataMember]
        public SSABool Strikeout { get; set; }

        [DataMember]
        public SSADecimal ScaleX { get; set; }

        [DataMember]
        public SSADecimal ScaleY { get; set; }

        [DataMember]
        public SSAInt Spacing { get; set; }

        [DataMember]
        public SSADecimal Angle { get; set; }

        [DataMember]
        public SSAInt BorderStyle { get; set; }

        [DataMember]
        public SSAInt Outline { get; set; }

        [DataMember]
        public SSAInt Shadow { get; set; }

        [DataMember]
        public SSAInt MarginL { get; set; }

        [DataMember]
        public SSAInt MarginR { get; set; }

        [DataMember]
        public SSAInt MarginV { get; set; }

        [DataMember]
        public SSAString AlphaLevel { get; set; }

        [DataMember]
        public SSAString Encoding { get; set; }

        public Style()
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
            var propertyInfo = typeof(Style).GetProperty(propertyName,
                                                            BindingFlags.Public | BindingFlags.IgnoreCase |
                                                            BindingFlags.Instance);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(this, value, null);
            }
        }
        public ISSAField GetProperty(string propertyName)
        {
            var propertyInfo = typeof(Style).GetProperty(propertyName,
                                                            BindingFlags.Public | BindingFlags.IgnoreCase |
                                                            BindingFlags.Instance);
            return propertyInfo != null ? (ISSAField) propertyInfo.GetValue(this, null) : null;
        }

    }



}
