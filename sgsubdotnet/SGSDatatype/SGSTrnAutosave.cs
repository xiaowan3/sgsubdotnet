using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace SGS.Datatype
{
    public class SGSTrnAutosave
    {
        public readonly BindingSource AutoSaveFileBindingSource;
        public DateTime PreviousSaveTime { get; private set; }

        public SGSTrnAutosave()
        {
            AutoSaveFileBindingSource = new BindingSource();
            if (!Directory.Exists(SGSConfig.AutosavePath))
            {
                Directory.CreateDirectory(SGSConfig.AutosavePath);
            }
            PreviousSaveTime = DateTime.Now;
        }

        public void Load()
        {
            AutoSaveFileBindingSource.Clear();
            var savefiles = Directory.GetFiles(SGSConfig.AutosavePath, "*.tbk");
            var savefileList = new List<TrnSaveFileIndex>();
            foreach (var savefile in savefiles)
            {
                var autosaverec = TrnAutosaveRec.Fromfile(savefile);
                var item = new TrnSaveFileIndex(savefile, autosaverec.Filename, autosaverec.SaveDate);
                savefileList.Add(item);
            }
            savefileList.Sort(new Comparison<TrnSaveFileIndex>(TrnSaveFileIndex.Compare));
            foreach (TrnSaveFileIndex trnSaveFileIndex in savefileList)
            {
                AutoSaveFileBindingSource.Add(trnSaveFileIndex);
            }
        }

        public void SaveHistory(string text, string filename)
        {

            var autosaverec = new TrnAutosaveRec(DateTime.Now, text, filename);
            autosaverec.Save(string.Format("{0}\\{1}.tbk", SGSConfig.AutosavePath, Guid.NewGuid()));
            PreviousSaveTime = DateTime.Now;
        }
        /// <summary>
        /// Delete old autosave records.
        /// </summary>
        /// <param name="hours"></param>
        public void DeleteOld(int hours)
        {

            var savefiles = Directory.GetFiles(SGSConfig.AutosavePath, "*.tbk");

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

    [DataContract(Name = "TrnSaveFileIndex", Namespace = "SGSDatatype")]
    public class TrnSaveFileIndex
    {
        public TrnSaveFileIndex(string savefilename, string filename, DateTime saveTime)
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

        public static int Compare(TrnSaveFileIndex x, TrnSaveFileIndex y)
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
    }

    [DataContract(Name = "TrnAutosaveRec", Namespace = "SGSDatatype")]
    public class TrnAutosaveRec
    {

        public static TrnAutosaveRec Fromfile(string filename)
        {
            var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            var zipfs = new GZipStream(fs, CompressionMode.Decompress);
            var reader = XmlDictionaryReader.CreateTextReader(zipfs, new XmlDictionaryReaderQuotas());
            var ser = new DataContractSerializer(typeof(TrnAutosaveRec));

            var autosaverec = (TrnAutosaveRec)ser.ReadObject(reader, true);
            reader.Close();
            zipfs.Close();
            return autosaverec;

        }

        public void Save(string filename)
        {
            var fs = new FileStream(filename, FileMode.Create);
            var zipwriter = new GZipStream(fs, CompressionMode.Compress);
            var ser = new DataContractSerializer(typeof(TrnAutosaveRec));
            ser.WriteObject(zipwriter, this);
            zipwriter.Flush();
            fs.Flush();
            zipwriter.Close();
            fs.Close();
        }

        public TrnAutosaveRec(DateTime time, string text)
        {
            SaveDate = time;
            Text = text;
            Filename = "Unknown";
        }

        public TrnAutosaveRec(DateTime time, string text, string filename)
        {
            SaveDate = time;
            Text = text;
            Filename = filename;
        }


        [DataMember]
        public DateTime SaveDate;

        [DataMember]
        public string Filename;

        [DataMember]
        public string Text;
    }
}
