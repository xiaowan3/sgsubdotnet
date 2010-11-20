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
        private AssHead m_AssHead;
        private AssLineParser m_AssParser;
        public BindingSource SubItems = new BindingSource();

        public AssHead DefaultAssHead;
        public string DefaultFormat;

        /// <summary>
        /// 读取ass文件
        /// </summary>
        /// <param name="filename"></param>
        public void LoadAss(string filename)
        {
            FileStream ifile = new FileStream(filename, FileMode.Open);
            StreamReader istream = new StreamReader(ifile);
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
            FileStream ifile = new FileStream(filename, FileMode.Open);
            StreamReader istream = new StreamReader(ifile,encoding);
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
            m_AssHead = new AssHead();

            while (!iStream.EndOfStream)
            {
                line = iStream.ReadLine();
                m_AssHead.AddLine(line);
                if (line.ToUpper().IndexOf("EVENTS") != -1)
                {
                    eventfound = true;
                    break;
                }
            }
            if (!eventfound) throw (new Exception("Wrong ass file."));
            line = iStream.ReadLine();
            m_AssParser = new AssLineParser(line);
            SubItems.Clear();
            while (!iStream.EndOfStream)
            {
                line = iStream.ReadLine();
                AssItem item = m_AssParser.ParseLine(line);
                if (item != null) SubItems.Add(item);
            }
        }


        /// <summary>
        /// 保存ass文件
        /// </summary>
        /// <param name="filename"></param>
        public void WriteAss(string filename)
        {
            FileStream ofile = new FileStream(filename, FileMode.OpenOrCreate);
            StreamWriter oStream = new StreamWriter(ofile);
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
            FileStream ofile = new FileStream(filename, FileMode.OpenOrCreate);
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
            m_AssHead.WriteTo(oStream);
            oStream.WriteLine(m_AssParser.FmtLine);
            foreach (object i in SubItems)
            {
                oStream.WriteLine(m_AssParser.FormatLine((AssItem)i));
            }
        }

        /// <summary>
        ///  读取无时间轴的翻译稿
        /// </summary>
        /// <param name="filename"></param>
        public void LoadText(string filename)
        {
        }

        /// <summary>
        ///  读取无时间轴的翻译稿
        /// </summary>
        /// <param name="filename"></param>
        public void LoadText(string filename, Encoding encoding)
        {
        }

        /// <summary>
        /// 为了在播放时能快速找到显示的字幕，定义这么一个索引。
        /// </summary>
        private List<AssItem>[] m_AssItemIndex;


        /// <summary>
        /// 建立索引
        /// </summary>
        /// <param name="length">视频长度</param>
        public void CreateIndex(double length)
        {
            m_AssItemIndex = new List<AssItem>[(int)Math.Ceiling(length)];
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

        /// <summary>
        /// 当间时刻显示的内容
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public string GetSubtitle(double time)
        {
            string str = "";
            if (m_AssItemIndex != null)
            {
                foreach (AssItem t in m_AssItemIndex[(int)Math.Floor(time)])
                {
                    if (t.Start.TimeValue <= time && t.End.TimeValue >= time && t.End.TimeValue - t.Start.TimeValue > 0.1)
                        str += t.Text + Environment.NewLine;
                }
            }
            return str;
        }
    }
}
