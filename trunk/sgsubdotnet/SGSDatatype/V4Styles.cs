using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SGSDatatype
{
    class V4Styles: ISection
    {
        public void AddLine(string line)
        {
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

    class V4Style
    {
        public string Name { get; set; }
        public string Fontname { get; set; }
        public int Fontsize;
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
        public string AlphaLevel { get; set; }
        public string Encoding { get; set; }
    }
    
     class SSAColour
     {
         private UInt32 _colorVal;
     }
}
