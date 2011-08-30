using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace SGSDatatype
{
    [DataContract(Name = "SubStationAlpha", Namespace = "SGSDatatype")]
    public class SubStationAlpha
    {
        [DataMember]
        public readonly SSAScriptInfo ScriptInfoSection;

        [DataMember]
        public readonly SSAStyles StylesSection;

        [DataMember]
        public readonly SSAEvents EventsSection;

        public SubStationAlpha()
        {
            ScriptInfoSection = new SSAScriptInfo();
            StylesSection = new SSAStyles();
            EventsSection = new SSAEvents();
        }
        public static SubStationAlpha Load(string filename)
        {
            var stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            var streamreader = new StreamReader(stream,Encoding.Default, true);
            return Load(streamreader);
        }

        public static SubStationAlpha Load(string filename, Encoding encoding)
        {
            var stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            var streamreader = new StreamReader(stream, encoding);
            return Load(streamreader);
        }

        private static SubStationAlpha Load(StreamReader reader)
        {
            int linenumber = 0;
            ISection currentSection = null;
            var ssa = new SubStationAlpha();

            do
            {
                var line = reader.ReadLine();
                if (line == null) continue;
                line = line.Trim();
                if (line.Length == 0) continue;
                if (line[0] == '[')
                {
                    var n = line.IndexOf(']');
                    if (n == -1 || n < 2) throw new Exception("Error in line " + linenumber);
                    var secname = line.Substring(1, n - 1).ToUpper();
                    switch (secname)
                    {
                        case "SCRIPT INFO":
                            currentSection = ssa.ScriptInfoSection;
                            break;
                        case "V4 STYLES":
                            if (ssa.ScriptInfoSection.Version != SSAVersion.V4) 
                                throw new Exception("Error Style in line " + linenumber);
                            currentSection = ssa.StylesSection;
                            break;
                        case "V4+ STYLES":
                            if (ssa.ScriptInfoSection.Version != SSAVersion.V4Plus) 
                                throw new Exception("Error Style in line " + linenumber);
                            currentSection = ssa.StylesSection;
                            break;
                        case "EVENTS":
                            currentSection = ssa.EventsSection;
                            break;
                        default:
                            break;

                    }
                    continue;
                }
                if (currentSection == null) throw new Exception("Error in line " + linenumber);
                try
                {
                    currentSection.ParseLine(line);
                }
                catch (Exception)
                {

                    throw new Exception("Error in line " + linenumber);
                }
                linenumber++;
            } while (!reader.EndOfStream);
            ssa.StylesSection.Version = ssa.ScriptInfoSection.Version;
            ssa.EventsSection.Version = ssa.ScriptInfoSection.Version;

            return ssa;
        }

        public static SubStationAlpha CreateFromTxt(string filename, string templateFile)
        {
            var tfile = new FileStream(templateFile, FileMode.Open, FileAccess.Read);
            var ssa = FromXml(tfile);
            var fstream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Read);
            var reader = new StreamReader(fstream, Encoding.Default, true);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                ssa.EventsSection.AppendNewLine(line);
            }
            return ssa;
        }
        public void Save(string filename, Encoding encoding)
        {
            var stream = new FileStream(filename,FileMode.Create,FileAccess.Write);
            var writer = new StreamWriter(stream,encoding);
            ScriptInfoSection.WriteTo(writer);
            StylesSection.WriteTo(writer);
            EventsSection.WriteTo(writer);
            writer.Flush();
            stream.Flush();
            stream.Close();
        }

        public static SubStationAlpha FromXml(Stream filestream)
        {
            var reader = XmlDictionaryReader.CreateTextReader(filestream, new XmlDictionaryReaderQuotas());
            var ser = new DataContractSerializer(typeof(SubStationAlpha));
            var ssa = (SubStationAlpha)ser.ReadObject(reader, true);
            reader.Close();
            return ssa;
        }
        public void WriteXml(Stream filestream)
        {
            var ser = new DataContractSerializer(typeof(SubStationAlpha));
            ser.WriteObject(filestream, this);
            filestream.Flush();
        }

        public V4Event CreateEmptyEvent(string text)
        {
            return EventsSection.NewLine(text);
        }
    }
}
