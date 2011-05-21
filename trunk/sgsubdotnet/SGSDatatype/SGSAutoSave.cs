using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;

namespace SGSDatatype
{
    public class SGSAutoSave
    {
        public readonly BindingSource AutoSaveFileBindingSource;
        private readonly string _savePath;


        public SGSAutoSave(string savePath)
        {
            AutoSaveFileBindingSource = new BindingSource();
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            _savePath = savePath;
        }

        public void Load()
        {
            AutoSaveFileBindingSource.Clear();
            var savefiles = Directory.GetFiles(_savePath, "*.save");
            foreach (var savefile in savefiles)
            {
                var autosaverec = AutoSaveRecord.Fromfile(savefile);
                var item = new SaveFileIndex(savefile, autosaverec.Filename, autosaverec.SaveDate);
                AutoSaveFileBindingSource.Add(item);
            }
        }
        
        public void SaveHistory(AssSub sub)
        {

            var savesub = new AutoSaveSubtitle(sub);
            var autosaverec = new AutoSaveRecord(DateTime.Now, savesub);
            autosaverec.Save(string.Format("{0}\\{1}.save", _savePath, Guid.NewGuid()));
        }

        /// <summary>
        /// Create a autosave record.
        /// </summary>
        /// <param name="sub">Subtitle</param>
        /// <param name="filename">Subtitle's file name</param>
        public void SaveHistory(AssSub sub,string filename)
        {

            var savesub = new AutoSaveSubtitle(sub);
            var autosaverec = new AutoSaveRecord(DateTime.Now, savesub, filename);
            autosaverec.Save(string.Format("{0}\\{1}.save", _savePath, Guid.NewGuid()));
        }

        /// <summary>
        /// Delete old autosave records.
        /// </summary>
        /// <param name="hours"></param>
        public void DeleteOld(int hours)
        {
            
            Load();
            foreach (SaveFileIndex saveFileIndex in AutoSaveFileBindingSource)
            {
                var offset = DateTime.Now.Subtract(saveFileIndex.SaveTime);
                if(offset.Hours > hours)
                {
                    File.Delete(saveFileIndex.SaveFile);
                }
            }
            Load();
        }
    }

    [DataContract(Name = "AutoSaveRecord", Namespace = "SGSDatatype")]
    public class SaveFileIndex
    {
        public SaveFileIndex(string savefilename,string filename, DateTime saveTime)
        {
            SaveFile = savefilename;
            Filename = filename;
            SaveTime = saveTime;
        }

        [DataMember]
        public string SaveFile { get; set; }

        [DataMember]
        public string Filename { get; set; }

        [DataMember]
        public DateTime SaveTime { get; set; }
    }

    [DataContract(Name = "AutoSaveRecord", Namespace = "SGSDatatype")]
    public class AutoSaveRecord
    {

        public static AutoSaveRecord Fromfile(string filename)
        {
            var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            var zipfs = new GZipStream(fs, CompressionMode.Decompress);
            var reader = XmlDictionaryReader.CreateTextReader(zipfs, new XmlDictionaryReaderQuotas());
            var ser = new DataContractSerializer(typeof(AutoSaveRecord));

            var autosaverec = (AutoSaveRecord)ser.ReadObject(reader, true);
            reader.Close();
            zipfs.Close();
            return autosaverec;

        }

        public void Save(string filename)
        {
            var fs = new FileStream(filename, FileMode.Create);
            var zipwriter = new GZipStream(fs, CompressionMode.Compress);
            var ser = new DataContractSerializer(typeof(AutoSaveRecord));
            ser.WriteObject(zipwriter, this);
            zipwriter.Flush();
            fs.Flush();
            zipwriter.Close();
            fs.Close();
        }

        public AutoSaveRecord(DateTime time, AutoSaveSubtitle sub)
        {
            SaveDate = time;
            Subtitle = sub;
            Filename = "Unknown";
        }

        public AutoSaveRecord(DateTime time, AutoSaveSubtitle sub,string filename)
        {
            SaveDate = time;
            Subtitle = sub;
            Filename = filename;
        }


        [DataMember]
        public DateTime SaveDate;

        [DataMember]
        public string Filename;
        
        [DataMember]
        public AutoSaveSubtitle Subtitle;
    }

    [DataContract(Name = "SGSConfig", Namespace = "AutoSaveSubtitle")]
    public class AutoSaveSubtitle
    {
        [DataMember]
        public AssHead AssHead;

        [DataMember]
        public AssLineParser AssParser;

        [DataMember]
        public List<AssItem> SubItems = new List<AssItem>();

        public AutoSaveSubtitle(AssSub assSub)
        {
            AssHead = assSub.AssHead;
            AssParser = assSub.AssParser;
            foreach (var item in assSub.SubItems)
            {
                SubItems.Add(((AssItem)item).Clone());
            }
        }

        public AssSub GetSub()
        {
            var sub = new AssSub
                             {
                                 AssHead = AssHead,
                                 AssParser = AssParser,
                                 SubItems = new BindingSource()
                             };
            foreach (var item in SubItems)
            {
                sub.SubItems.Add(item);
            }
            return sub;

        }
    }
}
