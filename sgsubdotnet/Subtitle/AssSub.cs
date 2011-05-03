using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Subtitle
{
    public class AssSub
    {
        private AssHead _assHead;
        private AssLineParser _assParser;
        public BindingSource SubItems = new BindingSource();

        public AssHead DefaultAssHead;
        public string DefaultFormatLine;
        //Format: Marked, Start, End, Style, Name, MarginL, MarginR, MarginV, Effect, Text
        public string DefaultFormat;
        public string DefaultMarked;
        public string DefaultLayer;
        public double DefaultStart;
        public double DefaultEnd;
        public string DefaultStyle;
        public string DefaultName;
        public string DefaultActor;
        public int DefaultMarginL;
        public int DefaultMarginR;
        public int DefaultMarginV;
        public string DefaultEffect;

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
            _assHead = new AssHead();

            while (!iStream.EndOfStream)
            {
                line = iStream.ReadLine();
                if (line == null) throw (new Exception("Wrong ass file."));
                _assHead.AddLine(line);
                if (line.ToUpper().IndexOf("EVENTS") != -1)
                {
                    eventfound = true;
                    break;
                }
            }
            if (!eventfound) throw (new Exception("Wrong ass file."));
            line = iStream.ReadLine();
            _assParser = new AssLineParser(line);
            SubItems.Clear();
            while (!iStream.EndOfStream)
            {
                line = iStream.ReadLine();
                var item = _assParser.ParseLine(line);
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
            FileStream ofile = new FileStream(filename, FileMode.Create);
            StreamWriter oStream = new StreamWriter(ofile,encoding);
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
            _assHead.WriteTo(oStream);
            oStream.WriteLine(_assParser.FmtLine);
            foreach (object i in SubItems)
            {
                oStream.WriteLine(_assParser.FormatLine((AssItem)i));
            }
        }

        /// <summary>
        ///  读取无时间轴的翻译稿
        /// </summary>
        /// <param name="filename"></param>
        public void LoadText(string filename)
        {
            var ifile = new FileStream(filename, FileMode.Open);
            var istream = new StreamReader(ifile, Encoding.Default, true);
            
            LoadText(istream);
            istream.Close();
            ifile.Close();
        }

        /// <summary>
        ///  读取无时间轴的翻译稿
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="encoding"></param>
        public void LoadText(string filename, Encoding encoding)
        {
            var ifile = new FileStream(filename, FileMode.Open);
            var istream = new StreamReader(ifile, encoding);
            LoadText(istream);
            istream.Close();
            ifile.Close();
        }


        public void LoadText(StreamReader iStream)
        {
            _assHead = DefaultAssHead;
            _assParser = new AssLineParser(DefaultFormatLine);
            string line;
            SubItems.Clear();
            while (!iStream.EndOfStream)
            {
                line = iStream.ReadLine();
                AssItem item = new AssItem();
                //Format: Marked, Start, End, Style, Name, MarginL, MarginR, MarginV, Effect, Text
                item.Format = DefaultFormat;
                item.Layer = DefaultLayer;
                item.Marked = DefaultMarked;
                item.Start.TimeValue = DefaultStart;
                item.End.TimeValue = DefaultEnd;
                item.Style = DefaultStyle;
                item.Name = DefaultName;
                item.Actor = DefaultActor;
                item.MarginL = DefaultMarginL;
                item.MarginR = DefaultMarginR;
                item.MarginV = DefaultMarginV;
                item.Effect = DefaultEffect;
                item.Text = line;
                SubItems.Add(item);
            }
        }

        /// <summary>
        /// 为了在播放时能快速找到显示的字幕，定义这么一个索引。
        /// </summary>
        private List<AssItem>[] m_AssItemIndex;
        private double m_VideoLen;

        /// <summary>
        /// 建立索引
        /// </summary>
        /// <param name="length">视频长度</param>
        public void CreateIndex(double length)
        {
            if (length > 0)
            {
                m_VideoLen = length;
                m_AssItemIndex = new List<AssItem>[(int)Math.Ceiling(length) + 1];
                for (int i = 0; i < m_AssItemIndex.Length; i++) m_AssItemIndex[i] = new List<AssItem>();
                foreach (AssItem item in SubItems)
                {
                    int a = (int)Math.Floor(item.Start.TimeValue);
                    int b = (int)Math.Ceiling(item.End.TimeValue);
                    for (int i = a; i < b; i++)
                    {
                        if (i < m_AssItemIndex.Length)
                            m_AssItemIndex[i].Add(item);
                    }
                }
            }
            else
            {
                m_AssItemIndex = null;
            }
        }


        public void RefreshIndex()
        {
            if (m_AssItemIndex != null)
            {
                foreach (var t in m_AssItemIndex)
                {
                    t.Clear();
                }
                foreach (AssItem item in SubItems)
                {
                    int a = (int)Math.Floor(item.Start.TimeValue);
                    int b = (int)Math.Ceiling(item.End.TimeValue);
                    for (int i = a; i < b; i++)
                    {
                        if (i < m_AssItemIndex.Length)
                            m_AssItemIndex[i].Add(item);
                    }
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
            int olds = (int)Math.Floor(oldStart);
            int olde = (int)Math.Ceiling(oldEnd);

            int news = (int)Math.Floor(item.Start.TimeValue);
            int newe = (int)Math.Ceiling(item.End.TimeValue);
            int s = Math.Min(news, olds), e = Math.Max(newe, olde);
            if (m_AssItemIndex != null)
            {
                for (int i = s; i < e; i++)
                {
                    if (i < m_AssItemIndex.Length)
                    {
                        if (i < news || i >= newe)
                        {
                            if (m_AssItemIndex[i].Contains(item)) m_AssItemIndex[i].Remove(item);
                        }
                        else
                        {
                            if (!m_AssItemIndex[i].Contains(item)) m_AssItemIndex[i].Add(item);

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
            if (m_AssItemIndex != null && time <= m_VideoLen)
            {
                str =
                    m_AssItemIndex[(int) Math.Floor(time)]
                        .Where(
                            t =>
                            t.Start.TimeValue <= time && t.End.TimeValue >= time &&
                            t.End.TimeValue - t.Start.TimeValue > 0.1)
                        .Aggregate(str,
                                   (current, t) =>
                                   current + (t.Text + Environment.NewLine));
            }
            return str;
        }
    }
}
