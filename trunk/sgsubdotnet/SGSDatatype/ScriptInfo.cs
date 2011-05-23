using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SGSDatatype
{
    class ScriptInfo : ISection
    {
        private readonly List<string> _scriptInfoLines;
        public void AddLine(string line)
        {
            _scriptInfoLines.Add(line);
        }

        public void WriteTo(Stream stream)
        {
            WriteTo(stream,Encoding.Unicode);
        }

        public void WriteTo(Stream stream, Encoding encoding)
        {
            var writer = new StreamWriter(stream,encoding);
            foreach (string scriptInfoLine in _scriptInfoLines)
            {
                writer.WriteLine(scriptInfoLine);
            }
            writer.Flush();
        }

        public string SectionName
        {
            get { return "Script Info"; }
        }
        public ScriptInfo()
        {
            _scriptInfoLines = new List<string>();
        }
    }
}
