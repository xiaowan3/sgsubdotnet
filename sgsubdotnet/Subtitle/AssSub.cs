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
            while (!iStream.EndOfStream)
            {
                line = iStream.ReadLine();
                SubItems.Add(m_AssParser.ParseLine(line));
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
    }
}
