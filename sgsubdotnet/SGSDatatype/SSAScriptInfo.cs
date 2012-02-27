using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SGS.Datatype
{
    [DataContract(Name = "SSAScriptInfo", Namespace = "SGSDatatype")]
    public class SSAScriptInfo : ISection
    {
        [DataMember]
        private readonly List<string> _scriptInfoLines;
        [DataMember]
        public SSAVersion Version { get; set; }
        public void ParseLine(string line)
        {
            _scriptInfoLines.Add(line);
            if(line[0]==';') return;
            int n;
            if ((n=line.IndexOf(':') )== -1) return;
            var sectionName = line.Substring(0, n);
            if (sectionName == "ScriptType")
            {
                var val = line.Substring(n + 1).Trim().ToUpper();
                switch (val)
                {
                    case "V4.00":
                        Version = SSAVersion.V4;
                        break;
                    case "V4.00+":
                        Version = SSAVersion.V4Plus;
                        break;
                    default:
                        throw new Exception("Unsupported Version");
                }
            }
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
