using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;

namespace SGSDatatype
{

    [DataContract(Name = "SGSConfig", Namespace = "SGSDatatype")]
    class AssSub
    {
        [DataMember]
        public AssHead AssHead;

        [DataMember]
        public AssLineParser AssParser;

        [DataMember]
        public BindingSource SubItems = new BindingSource();


        /// <summary>
        /// 读取ass文件
        /// </summary>
        /// <param name="filename"></param>
        public void LoadAss(string filename)
        {
            var ifile = new FileStream(filename, FileMode.Open);
            var istream = new StreamReader(ifile, Encoding.Default, true);
            LoadAss(istream);
            istream.Close();
            ifile.Close();
        }

        /// <summary>
        /// 读取ass文件
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="encoding"></param>
        public void LoadAss(string filename, Encoding encoding)
        {
            var ifile = new FileStream(filename, FileMode.Open);
            var istream = new StreamReader(ifile,encoding);
            LoadAss(istream);
            istream.Close();
            ifile.Close();
        }

        /// <summary>
        /// 读取ass文件
        /// </summary>
        /// <param name="iStream"></param>
        public void LoadAss(StreamReader iStream)
        {
            string line;
            bool eventfound = false;
            AssHead = new AssHead();

            while (!iStream.EndOfStream)
            {
                line = iStream.ReadLine();
                if (line == null) throw (new Exception("Wrong ass file."));
                AssHead.AddLine(line);
                if (line.ToUpper().IndexOf("EVENTS") != -1)
                {
                    eventfound = true;
                    break;
                }
            }
            if (!eventfound) throw (new Exception("Wrong ass file."));
            line = iStream.ReadLine();
            AssParser = new AssLineParser(line);
            SubItems.Clear();
            while (!iStream.EndOfStream)
            {
                line = iStream.ReadLine();
                var item = AssParser.ParseLine(line);
                if (item != null) SubItems.Add(item);
            }
        }


        /// <summary>
        /// 保存ass文件
        /// </summary>
        /// <param name="filename"></param>
        public void WriteAss(string filename)
        {
            var ofile = new FileStream(filename, FileMode.Create);
            var oStream = new StreamWriter(ofile);
            WriteAss(oStream);
            oStream.Flush();
            ofile.Flush();
            oStream.Close();
            ofile.Close();
        }

        /// <summary>
        /// 保存ass文件
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="encoding"></param>
        public void WriteAss(string filename, Encoding encoding)
        {
            var ofile = new FileStream(filename, FileMode.Create);
            var oStream = new StreamWriter(ofile,encoding);
            WriteAss(oStream);
            oStream.Flush();
            ofile.Flush();
            oStream.Close();
            ofile.Close();
        }

        /// <summary>
        /// 保存ass文件
        /// </summary>
        /// <param name="oStream"></param>
        public void WriteAss(StreamWriter oStream)
        {
            AssHead.WriteTo(oStream);
            oStream.WriteLine(AssParser.FmtLine);
            foreach (object i in SubItems)
            {
                oStream.WriteLine(AssParser.FormatLine((AssItem)i));
            }
        }

        /// <summary>
        ///  读取无时间轴的翻译稿
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="config"></param>
        public void LoadText(string filename, SGSConfig config)
        {
            var ifile = new FileStream(filename, FileMode.Open);
            var istream = new StreamReader(ifile, Encoding.Default, true);

            LoadText(istream, config);
            istream.Close();
            ifile.Close();
        }

        /// <summary>
        ///  读取无时间轴的翻译稿
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="encoding"></param>
        /// <param name="config"></param>
        public void LoadText(string filename, Encoding encoding, SGSConfig config)
        {
            var ifile = new FileStream(filename, FileMode.Open);
            var istream = new StreamReader(ifile, encoding);
            LoadText(istream, config);
            istream.Close();
            ifile.Close();
        }


        public void LoadText(StreamReader iStream, SGSConfig config)
        {
            AssHead = config.DefaultAssHead;
            AssParser = new AssLineParser(config.DefaultFormatLine);
            SubItems.Clear();
            while (!iStream.EndOfStream)
            {
                string line = iStream.ReadLine();
                var item = new AssItem
                               {
                                   Format = config.DefaultFormat,
                                   Layer = config.DefaultLayer,
                                   Marked = config.DefaultMarked,
                                   Start = { TimeValue = config.DefaultStart },
                                   End = { TimeValue = config.DefaultEnd },
                                   Style = config.DefaultStyle,
                                   Name = config.DefaultName,
                                   Actor = config.DefaultActor,
                                   MarginL = config.DefaultMarginL,
                                   MarginR = config.DefaultMarginR,
                                   MarginV = config.DefaultMarginV,
                                   Effect = config.DefaultEffect,
                                   Text = line
                               };
                //Format: Marked, Start, End, Style, Name, MarginL, MarginR, MarginV, Effect, Text
                SubItems.Add(item);
            }
        }

        /// <summary>
        /// 为了在播放时能快速找到显示的字幕，定义这么一个索引。
        /// </summary>
        private List<AssItem>[] _mAssItemIndex;
        private double _mVideoLen;

        /// <summary>
        /// 建立索引
        /// </summary>
        /// <param name="length">视频长度</param>
        public void CreateIndex(double length)
        {
            if (length > 0)
            {
                _mVideoLen = length;
                _mAssItemIndex = new List<AssItem>[(int)Math.Ceiling(length) + 1];
                for (int i = 0; i < _mAssItemIndex.Length; i++) _mAssItemIndex[i] = new List<AssItem>();
                foreach (AssItem item in SubItems)
                {
                    var a = (int)Math.Floor(item.Start.TimeValue);
                    var b = (int)Math.Ceiling(item.End.TimeValue);
                    for (int i = a; i < b; i++)
                    {
                        if (i < _mAssItemIndex.Length)
                            _mAssItemIndex[i].Add(item);
                    }
                }
            }
            else
            {
                _mAssItemIndex = null;
            }
        }


        public void RefreshIndex()
        {
            if (_mAssItemIndex == null) return;

            foreach (List<AssItem> t in _mAssItemIndex)
            {
                t.Clear();
            }
            foreach (AssItem item in SubItems)
            {
                var a = (int)Math.Floor(item.Start.TimeValue);
                var b = (int)Math.Ceiling(item.End.TimeValue);
                for (int i = a; i < b; i++)
                {
                    if (i < _mAssItemIndex.Length)
                        _mAssItemIndex[i].Add(item);
                }
            }
        }
        /// <summary>
        /// 当Item的起始或终止时间改变时，修改Index
        /// </summary>
        /// <param name="item">被改变的Item</param>
        /// <param name="oldStart">原来的起始时间</param>
        /// <param name="oldEnd">原来的终止时间</param>
        public void ItemEdited(AssItem item, double oldStart, double oldEnd)
        {
            var olds = (int)Math.Floor(oldStart);
            var olde = (int)Math.Ceiling(oldEnd);

            var news = (int)Math.Floor(item.Start.TimeValue);
            var newe = (int)Math.Ceiling(item.End.TimeValue);
            int s = Math.Min(news, olds), e = Math.Max(newe, olde);
            if (_mAssItemIndex != null)
            {
                for (int i = s; i < e; i++)
                {
                    if (i < _mAssItemIndex.Length)
                    {
                        if (i < news || i >= newe)
                        {
                            if (_mAssItemIndex[i].Contains(item)) _mAssItemIndex[i].Remove(item);
                        }
                        else
                        {
                            if (!_mAssItemIndex[i].Contains(item)) _mAssItemIndex[i].Add(item);

                        }
                    }

                }
            }
        }

        /// <summary>
        /// 当间时刻显示的内容
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public string GetSubtitle(double time)
        {
            string str = "";
            if (_mAssItemIndex != null && time <= _mVideoLen)
            {
                str = _mAssItemIndex[(int) Math.Floor(time)].
                    Where(
                        t =>
                        t.Start.TimeValue <= time && t.End.TimeValue >= time &&
                        t.End.TimeValue - t.Start.TimeValue > 0.1).
                    Aggregate(str, (current, t) => current + (t.Text + Environment.NewLine));
            }
            return str;
        }
    }
}
