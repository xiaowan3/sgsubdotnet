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
    public class SSAEvents
    {
        [DataMember]
        private string[] _eventFormat;
        [DataMember]
        public readonly BindingSource EventList;

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
                    var fields = line.Split(separator);
                    for (var i = 0; i < _eventFormat.Length; i++)
                    {
                        var field = v4Event.GetProperty(_eventFormat[i]);
                        if (i < fields.Length && field != null)
                            field.FromString(fields[i]);
                    }
                    EventList.Add(v4Event);
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

    [DataContract(Name = "V4Event", Namespace = "SGSDatatype")]
    class V4Event
    {
        [DataMember]
        public SSAString Marked { get; set; }

        [DataMember]
        public SSATime Start { get; set; }

        [DataMember]
        public SSATime End { get; set; }

        [DataMember]
        public SSAString Style { get; set; }

        [DataMember]
        public SSAString Name { get; set; }

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
            Start = new SSATime();
            End = new SSATime();
            Style = new SSAString();
            Name = new SSAString();
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
