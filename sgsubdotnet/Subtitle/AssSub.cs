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
            line = iStream.ReadLine();
            m_AssParser = new AssLineParser(line);
            while (!iStream.EndOfStream)
            {
                line = iStream.ReadLine();
                SubItems.Add(m_AssParser.ParseLine(line));
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
