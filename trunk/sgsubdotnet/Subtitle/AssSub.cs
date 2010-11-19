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
        private AssHead m_DefaultHead;
        private AssLineParser m_AssParser;
        public BindingSource SubItems = new BindingSource();

        public void LoadAss(string filename)
        {
            FileStream ifile = new FileStream(filename, FileMode.Open);
            StreamReader istream = new StreamReader(ifile);
            LoadAss(istream);
            istream.Close();
            ifile.Close();
        }
        public void LoadAss(string filename, Encoding encoding)
        {
            FileStream ifile = new FileStream(filename, FileMode.Open);
            StreamReader istream = new StreamReader(ifile,encoding);
            LoadAss(istream);
            istream.Close();
            ifile.Close();
        }

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

        public void WriteAss(StreamWriter oStream)
        {
            m_AssHead.WriteTo(oStream);
            oStream.WriteLine(m_AssParser.FmtLine);
            foreach (object i in SubItems)
            {
                oStream.WriteLine(m_AssParser.FormatLine((AssItem)i));
            }
        }

        public void LoadText(string filename)
        {
        }
        public void LoadText(string filename, Encoding encoding)
        {
        }


        private List<AssItem>[] m_Track;
        public void CreateTrack(double length)
        {
            m_Track = new List<AssItem>[(int)Math.Ceiling(length)];
            for (int i = 0; i < m_Track.Length; i++) m_Track[i] = new List<AssItem>();
            foreach (AssItem item in SubItems)
            {
                int a = (int)Math.Floor(item.Start.TimeValue);
                int b = (int)Math.Ceiling(item.End.TimeValue);
                for (int i = a; i < b; i++)
                {
                    if (i < m_Track.Length)
                        m_Track[i].Add(item);
                }
            }
        }

        public void ItemEdited(AssItem item, double oldStart, double oldEnd)
        {
            int olds = (int)Math.Floor(oldStart);
            int olde = (int)Math.Ceiling(oldEnd);

            int news = (int)Math.Floor(item.Start.TimeValue);
            int newe = (int)Math.Ceiling(item.End.TimeValue);
            int s = Math.Min(news, olds), e = Math.Max(newe, olde);

            for (int i = s; i < e; i++)
            {
                if (i < m_Track.Length)
                {
                    if (i < news || i >= newe)
                    {
                        if (m_Track[i].Contains(item)) m_Track[i].Remove(item);
                    }
                    else
                    {
                        if (!m_Track[i].Contains(item)) m_Track[i].Add(item);

                    }
                }

            }
        }

        public string GetSubtitle(double time)
        {
            string str = "";
            if (m_Track != null)
            {
                foreach (AssItem t in m_Track[(int)Math.Floor(time)])
                {
                    if (t.Start.TimeValue <= time && t.End.TimeValue >= time && t.End.TimeValue - t.Start.TimeValue > 0.1)
                        str += t.Text + Environment.NewLine;
                }
            }
            return str;
        }
    }
}
