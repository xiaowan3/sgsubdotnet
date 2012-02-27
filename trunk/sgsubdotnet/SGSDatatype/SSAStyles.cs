using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Reflection;
using System.Windows.Forms;

namespace SGS.Datatype
{
    [DataContract(Name = "SSAStyles", Namespace = "SGSDatatype")]
    [KnownType(typeof(Style))]
    public class SSAStyles : ISection
    {
        [DataMember]
        private string[] _styleFormat;

        [DataMember]
        public readonly BindingSource StyleList;

        [DataMember]
        public SSAVersion Version { get; set; }

        public SSAStyles()
        {
            StyleList = new BindingSource();
            _styleFormat = null;
        }

        /// <summary>
        /// Parse a line
        /// </summary>
        /// <param name="line">Line</param>
        public void ParseLine(string line)
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
                            field.FromString(fields[i].Trim());
                    }
                    StyleList.Add(v4StyleP);
                    break;
                default:
                    return;
            }

        }

        public void WriteTo(StreamWriter streamWriter)
        {
            streamWriter.WriteLine(string.Format("[{0}]", SectionName));
            streamWriter.Write("Format: ");

            for (int i = 0; i < _styleFormat.Length - 1; i++)
            {
                streamWriter.Write("{0}, ", _styleFormat[i]);
            }
            streamWriter.WriteLine(_styleFormat[_styleFormat.Length - 1]);
            foreach (Style style in StyleList)
            {
                streamWriter.Write("Style: {0}", style.GetProperty(_styleFormat[0]));
                for (int i = 1; i < _styleFormat.Length; i++)
                {
                    streamWriter.Write(",{0}", style.GetProperty(_styleFormat[i]));
                }
                streamWriter.WriteLine();
            }
            streamWriter.Flush();
        }

        public string SectionName
        {
            get
            {
                string ver = "";
                switch (Version)
                {
                    case SSAVersion.V4:
                        ver = "v4 Styles";
                        break;
                    case SSAVersion.V4Plus:
                        ver = "v4+ Styles";
                        break;
                }
                return ver;
            }
        }
    }

    [DataContract(Name = "Style", Namespace = "SGSDatatype")]
    public class Style
    {

        [DataMember]
        public SSAString Name { get; set; }

        [DataMember]
        public SSAString Fontname { get; set; }

        [DataMember]
        public SSADecimal Fontsize { get; set; }

        [DataMember]
        public SSAColour PrimaryColour { get; set; }

        [DataMember]
        public SSAColour SecondaryColour { get; set; }

        [DataMember]
        public SSAColour OutlineColour { get; set; }

        [DataMember]
        public SSAColour TertiaryColour { get; set; }

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
        public SSADecimal Spacing { get; set; }

        [DataMember]
        public SSADecimal Angle { get; set; }

        [DataMember]
        public SSAInt BorderStyle { get; set; }

        [DataMember]
        public SSADecimal Outline { get; set; }

        [DataMember]
        public SSADecimal Shadow { get; set; }

        [DataMember]
        public SSAInt Alignment { get; set; }

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
            Fontsize = new SSADecimal();
            PrimaryColour = new SSAColour();
            SecondaryColour = new SSAColour();
            OutlineColour = new SSAColour();
            BackColour = new SSAColour();
            Bold = new SSABool();
            Italic = new SSABool();
            Underline=new SSABool();
            Strikeout = new SSABool();
            ScaleX = new SSADecimal();
            ScaleY = new SSADecimal();
            Spacing = new SSADecimal();
            Angle = new SSADecimal();
            BorderStyle = new SSAInt();
            Outline = new SSADecimal();
            Shadow = new SSADecimal();
            Alignment = new SSAInt();
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
