using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Runtime.Serialization;

namespace SGSDatatype
{
    [DataContract(Name = "SGSAutoSave", Namespace = "SGSDatatype")]
    public class SGSAutoSave
    {
        [DataMember]
        private List<AutoSaveRecord> _saveList;

        private string _filename;

        public SGSAutoSave()
        {
            _saveList = new List<AutoSaveRecord>();
            _filename = null;
        }

        public static SGSAutoSave Load(string filename)
        {
            var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);

            XmlDictionaryReader reader =
                XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
            var ser = new DataContractSerializer(typeof(SGSAutoSave));

            var autosaveobj = (SGSAutoSave)ser.ReadObject(reader, true);
            reader.Close();
            fs.Close();
            autosaveobj._filename = filename;
            return autosaveobj;
        }
        
        public void SaveHistory(AssSub sub)
        {
            var ms = new MemoryStream();
            var write = new StreamWriter(ms);
            sub.WriteAss(write);
            write.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(ms);
            var subcopy = new AssSub();
            subcopy.LoadAss(reader);
            _saveList.Add(new AutoSaveRecord(DateTime.Today, subcopy));

        }


        public void Save(string filename)
        {
            var writer = new FileStream(filename, FileMode.Create);
            var ser = new DataContractSerializer(typeof(SGSAutoSave));
            _filename = filename;
            ser.WriteObject(writer, this);
            writer.Close();
        }

        public void Save()
        {
            if(_filename != null) Save(_filename);
            else throw new Exception("Error.");
        }



    }

    [DataContract(Name = "SGSConfig", Namespace = "SGSControls")]
    internal class AutoSaveRecord
    {
        public AutoSaveRecord()
        {
            
        }
        public AutoSaveRecord(DateTime time, AssSub sub)
        {
            SaveDate = time;
            Subtitle = sub;
        }

        [DataMember]
        public DateTime SaveDate;
        [DataMember]
        public AssSub Subtitle;
    }
}
