using System.Xml;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace SGSDatatype
{
    [DataContract(Name = "SGSConfig", Namespace = "SGSDatatype")]
    public class SGSConfig
    {
        private string _filename;

        public static string FFMpegPath;
        public static string AutosavePath;
        public static string ConfigPath;
        public static string DefaultCfgPath;
        public static string MPlayerPath;
        public static bool WithMPlayer;

        [DataMember]
        private string Version { get; set; }

        #region Timeline hotkeys


        /// <summary>
        /// 暂停、继续按键
        /// </summary>
        [DataMember]
        public Keys Pause { get; set; }

        /// <summary>
        /// 插入时间点按键（按下起始，抬起终止）
        /// </summary>
        [DataMember]
        public Keys AddTimePoint { get; set; }

        /// <summary>
        /// 插入单元格时间点按键（按下插入当前时间）
        /// </summary>
        [DataMember]
        public Keys AddCellTime { get; set; }

        /// <summary>
        /// 连续插入时间点（插入结束时间点和下一行开始时间点）
        /// </summary>
        [DataMember]
        public Keys AddContTimePoint { get; set; }

        /// <summary>
        /// 插入起始时间点按键
        /// </summary>
        [DataMember]
        public Keys AddStartTime { get; set; }

        /// <summary>
        /// 插入终止时间点按键
        /// </summary>
        [DataMember]
        public Keys AddEndTime { get; set; }

        /// <summary>
        /// 前进
        /// </summary>
        [DataMember]
        public Keys SeekForward { get; set; }

        /// <summary>
        /// 后退
        /// </summary>
        [DataMember]
        public Keys SeekBackword { get; set; }

        /// <summary>
        /// 跳至当前行
        /// </summary>
        [DataMember]
        public Keys GotoCurrent { get; set; }


        /// <summary>
        /// 跳至上一行
        /// </summary>
        [DataMember]
        public Keys GotoPrevious { get; set; }

        /// <summary>
        /// 进入编辑模式
        /// </summary>
        [DataMember]
        public Keys EnterEditMode { get; set; }

        /// <summary>
        /// 微调加
        /// </summary>
        [DataMember]
        public Keys MiniTrimPlus { get; set; }

        /// <summary>
        /// 微调减
        /// </summary>
        [DataMember]
        public Keys MiniTrimMinus { get; set; }

        [DataMember]
        public Keys SaveAss { get; set; }

        #endregion

        #region settings

        /// <summary>
        /// 布局名称
        /// </summary>
        [DataMember]
        public string LayoutName { get; set; }

        /// <summary>
        /// 选择播放器
        /// </summary>
        [DataMember]
        public PlayerType Player { get; set; }

        /// <summary>
        /// 起始时间点相对于按键时刻的偏移量（负为提前）（秒）
        /// </summary>
        [DataMember]
        public double StartOffset { get; set; }

        /// <summary>
        /// 终止时间点相对于按键时刻的偏移量（负为提前）（秒）
        /// </summary>
        [DataMember]
        public double EndOffset { get; set; }

        /// <summary>
        /// 快进、退的步长（秒）
        /// </summary>
        [DataMember]
        public double SeekStep { get; set; }

        /// <summary>
        /// 微调步长（秒）
        /// </summary>
        [DataMember]
        public double MinitrimStep { get; set; }


        /// <summary>
        /// 高亮的行定位于第几行
        /// </summary>
        [DataMember]
        public int SelectRowOffset { get; set; }


        [DataMember]
        public bool AutoOverlapCorrection { get; set; }

        [DataMember]
        public string TemplateName { get; set; }

        /// <summary>
        /// 自动保存周期 秒
        /// </summary>
        [DataMember]
        public int AutoSavePeriod { get; set; }

        /// <summary>
        /// 自动保存数据保留时间 小时
        /// </summary>
        [DataMember]
        public int AutoSaveLifeTime { get; set; }

        #endregion

        #region Syntax Highlighting

        /// <summary>
        /// 窗
        /// </summary>
        [DataMember]
        public char HolePlaceholder{ get; set; }

        /// <summary>
        /// 注释
        /// </summary>
        [DataMember]
        public char CommentMark{ get; set; }

        /// <summary>
        /// 存疑内容左
        /// </summary>
        [DataMember]
        public char UncertainLeftMark{ get; set; }

        /// <summary>
        /// 存疑内容右
        /// </summary>
        [DataMember]
        public char UncertainRightMark { get; set; }

        /// <summary>
        /// 字面标志
        /// </summary>
        [DataMember]
        public char LiteralLineMark { get; set; }

        /// <summary>
        /// 每行字数检查
        /// </summary>
        [DataMember]
        public int LineLength { get; set; }

        #endregion

        #region Translation Hotkeys

        [DataMember]
        public Keys PlayerFF { get; set; }

        [DataMember]
        public Keys PlayerRW { get; set; }

        [DataMember]
        public Keys PlayerTogglePause { get; set; }

        [DataMember]
        public Keys PlayerJumpto { get; set; }

        [DataMember]
        public Keys InsertTag { get; set; }


        [DataMember]
        public Keys PlayerFF2 { get; set; }

        [DataMember]
        public Keys PlayerRW2 { get; set; }

        [DataMember]
        public Keys PlayerTogglePause2 { get; set; }

        [DataMember]
        public Keys PlayerJumpto2 { get; set; }

        [DataMember]
        public Keys InsertTag2 { get; set; }


        [DataMember]
        public Keys SetEndTime { get; set; }

        #endregion

        /// <summary>
        /// Check weather the configuration object is compatible with this version.
        /// </summary>
        /// <param name="config">Configuration Object</param>
        /// <returns></returns>
        public bool Compatible(SGSConfig config)
        {
            return config.Version == Version;
        }

        public static SGSConfig FromFile(string filename)
        {
            var fs = new FileStream(filename,FileMode.Open,FileAccess.Read);

            XmlDictionaryReader reader =
                XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
            var ser = new DataContractSerializer(typeof(SGSConfig));

            var sgsCfgObject = (SGSConfig)ser.ReadObject(reader, true);
            reader.Close();
            fs.Close();
            sgsCfgObject._filename = filename;


            if (!WithMPlayer && sgsCfgObject.Player == PlayerType.MPlayer)
            {
                sgsCfgObject.Player = PlayerType.WMPlayer;
            }


            return sgsCfgObject;
        }

        public void Save(string filename)
        {
            var writer = new FileStream(filename, FileMode.Create);
            var ser = new DataContractSerializer(typeof(SGSConfig));
            ser.WriteObject(writer, this);
            writer.Close();
            _filename = filename;
        }
        public void Save()
        {
            if (_filename != null)
            {
                Save(_filename);
            }
        }
        
    }
    public enum PlayerType { DShowPlayer = 0, MDXPlayer = 1, WMPlayer = 2, MPlayer = 3 }

}
