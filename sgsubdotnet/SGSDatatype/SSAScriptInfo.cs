using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SGSDatatype
{
    public class SSAScriptInfo : ISection
    {
        [DataMember]
        private readonly List<string> _scriptInfoLines;
        [DataMember]
        public SSAVersion Version { get; set; }
        public void AddLine(string line)
        {
            _scriptInfoLines.Add(line);
        }

        public void WriteTo(StreamWriter streamWriter)
        {
            streamWriter.WriteLine("[Script Info]");
            foreach (string scriptInfoLine in _scriptInfoLines)
            {
                streamWriter.WriteLine(scriptInfoLine);
            }
            streamWriter.Flush();
        }

        public string SectionName
        {
            get { return "Script Info"; }
        }
        public SSAScriptInfo()
        {
            _scriptInfoLines = new List<string>();
        }
    }
}
