using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;

namespace SGS.Datatype
{
    public class SGSAutoSave
    {
        public readonly BindingSource AutoSaveFileBindingSource;
        public DateTime PreviousSaveTime { get; private set; }

        public SGSAutoSave()
        {
            AutoSaveFileBindingSource = new BindingSource();
            if (!Directory.Exists(SGSConfig.AutosavePath))
            {
                Directory.CreateDirectory(SGSConfig.AutosavePath);
            }
            PreviousSaveTime = DateTime.Now;
        }

        /// <summary>
        /// Generate autosave file list from autosave path.
        /// </summary>
        public void Load()
        {
            AutoSaveFileBindingSource.Clear();
            var savefiles = Directory.GetFiles(SGSConfig.AutosavePath, "*.save");
            var savefileList = new List<SaveFileIndex>();
            foreach (var savefile in savefiles)
            {
                var autosaverec = AutoSaveRecord.Fromfile(savefile);
                var item = new SaveFileIndex(savefile, autosaverec.Filename, autosaverec.SaveDate);
                savefileList.Add(item);
            }
            savefileList.Sort(new Comparison<SaveFileIndex>(SaveFileIndex.Compare));
            foreach (SaveFileIndex saveFileIndex in savefileList)
            {
                AutoSaveFileBindingSource.Add(saveFileIndex);
            }
        }

        public void SetSaveTime()
        {
            PreviousSaveTime = DateTime.Now;
        }

        /// <summary>
        /// Create a autosave record.
        /// </summary>
        /// <param name="sub">Subtitle</param>
        /// <param name="filename">Subtitle's file name</param>
        public void SaveHistory(SubStationAlpha sub, string filename)
        {

            var autosaverec = new AutoSaveRecord(DateTime.Now, sub, filename);
            autosaverec.Save(string.Format("{0}\\{1}.save", SGSConfig.AutosavePath, Guid.NewGuid()));
            PreviousSaveTime = DateTime.Now;
        }

        /// <summary>
        /// Delete old autosave records.
        /// </summary>
        /// <param name="hours"></param>
        public void DeleteOld(int hours)
        {

            var savefiles = Directory.GetFiles(SGSConfig.AutosavePath, "*.save");

            foreach (var savefile in savefiles)
                {
                    FileInfo fileinfo = new FileInfo(savefile);
                    var editedtime = fileinfo.LastWriteTime;
                    var offset = DateTime.Now.Subtract(editedtime);
                    if (offset.TotalHours > hours)
                    {
                        File.Delete(savefile);
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

        public static int Compare(SaveFileIndex x, SaveFileIndex y)
        {
            if (x == null)
            {
                if (y == null) return 0;
                return -1;
            }
            if (y == null) return 1;
            if (x.SaveTime.Subtract(y.SaveTime).TotalSeconds > 0) return 1;
            if (x.SaveTime.Subtract(y.SaveTime).TotalSeconds == 0) return 0;
            return -1;
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

        public AutoSaveRecord(DateTime time, SubStationAlpha sub)
        {
            SaveDate = time;
            Subtitle = sub;
            Filename = "Unknown";
        }

        public AutoSaveRecord(DateTime time, SubStationAlpha sub, string filename)
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
        public SubStationAlpha Subtitle;
    }

}
