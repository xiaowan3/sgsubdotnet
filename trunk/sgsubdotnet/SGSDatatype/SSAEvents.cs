using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization;

namespace SGSDatatype
{
    [DataContract(Name = "SSAEvents", Namespace = "SGSDatatype")]
    [KnownType(typeof(V4Event))]
    public class SSAEvents : ISection
    {
        [DataMember]
        private string[] _eventFormat;
        [DataMember]
        public readonly BindingSource EventList;

        [DataMember]
        public SSAVersion Version { get; set; }

        public SSAEvents()
        {
            EventList = new BindingSource();
            _eventFormat = null;
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
                    _eventFormat = line.Split(separator);
                    for (var i = 0; i < _eventFormat.Length; i++)
                    {
                        _eventFormat[i] = _eventFormat[i].Trim();
                    }
                    break;
                case "DIALOGUE":
                    if (_eventFormat == null) throw new Exception("Event Section Error");
                    var v4Event = new V4Event();
                   // var fields = line.Split(separator);
                    for (var i = 0; i < _eventFormat.Length; i++)
                    {
                        var comma = line.IndexOf(',');
                        if (comma == -1 && _eventFormat[i].ToUpper() != "TEXT") return; //Last field in a line is not Text
                        var field = v4Event.GetProperty(_eventFormat[i]);
                        if (_eventFormat[i].ToUpper() == "TEXT")
                        {
                            field = v4Event.GetProperty(_eventFormat[i]);
                            if (field != null) field.FromString(line);
                            break;
                        }
                        var f = line.Substring(0, comma);
                        if (field != null) field.FromString(f);
                        line = line.Substring(comma + 1);

                    }
                    EventList.Add(v4Event);
                    break;
                default:
                    return;
            }
        }

        public void WriteTo(StreamWriter streamWriter)
        {
            streamWriter.WriteLine(string.Format("[{0}]", SectionName));
            streamWriter.Write("Format: ");

            for (int i = 0; i < _eventFormat.Length - 1; i++)
            {
                streamWriter.Write("{0}, ", _eventFormat[i]);
            }
            streamWriter.WriteLine(_eventFormat[_eventFormat.Length - 1]);
            foreach (V4Event v4Event in EventList)
            {
                streamWriter.Write("Dialogue: {0}", v4Event.GetProperty(_eventFormat[0]));
                for (int i = 1; i < _eventFormat.Length; i++)
                {
                    streamWriter.Write(",{0}", v4Event.GetProperty(_eventFormat[i]));
                }
                streamWriter.WriteLine();
            }
            streamWriter.Flush();
        }

        public string SectionName
        {
            get { return "Events"; }
        }


    }

    [DataContract(Name = "V4Event", Namespace = "SGSDatatype")]
    public class V4Event
    {
        [DataMember]
        public SSAString Marked { get; set; }

        [DataMember]
        public SSAInt Layer { get; set; }

        [DataMember]
        public SSATime Start { get; set; }

        [DataMember]
        public SSATime End { get; set; }

        [DataMember]
        public SSAString Style { get; set; }

        [DataMember]
        public SSAString Name { get; set; }

        [DataMember]
        public SSAString Actor { get; set; }

        [DataMember]
        public SSAMargin MarginL { get; set; }

        [DataMember]
        public SSAMargin MarginR { get; set; }

        [DataMember]
        public SSAMargin MarginV { get; set; }

        [DataMember]
        public SSAString Effect { get; set; }

        [DataMember]
        public SSAString Text { get; set; }

        public V4Event()
        {
            Marked = new SSAString();
            Layer = new SSAInt();
            Start = new SSATime();
            End = new SSATime();
            Style = new SSAString();
            Name = new SSAString();
            Actor = new SSAString();
            MarginL = new SSAMargin();
            MarginR = new SSAMargin();
            MarginV = new SSAMargin();
            Effect = new SSAString();
            Text = new SSAString();
        }

        public void SetProperty(string propertyName, ISSAField value)
        {
            var propertyInfo = typeof(V4Event).GetProperty(propertyName,
                                                            BindingFlags.Public | BindingFlags.IgnoreCase |
                                                            BindingFlags.Instance);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(this, value, null);
            }
        }
        public ISSAField GetProperty(string propertyName)
        {
            var propertyInfo = typeof(V4Event).GetProperty(propertyName,
                                                            BindingFlags.Public | BindingFlags.IgnoreCase |
                                                            BindingFlags.Instance);
            return propertyInfo != null ? (ISSAField) propertyInfo.GetValue(this, null) : null;
        }
    }
}
