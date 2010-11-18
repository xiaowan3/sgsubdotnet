using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subtitle
{
    public class AssHead
    {
        public List<string> m_HeadLines = new List<string>();

        public void AddLine(string line)
        {
            m_HeadLines.Add(line);
        }

        public void Clear()
        {
            m_HeadLines = new List<string>();
        }

        public override string ToString()
        {
            string str = "";
            foreach (string s in m_HeadLines)
                str += s;
            return str;
        }
    }
    public class AssItem
    {
        //Format: Marked, Start, End, Style, Name, MarginL, MarginR, MarginV, Effect, Text
        public string Format = "";

        public string Marked = "";

        public string Layer = "";

        public string StartTime
        {
            get
            {
                return Start.ToString();
            }
            set
            {
                Start = new AssTime(value);
            }
        }

        public string EndTime
        {
            get
            {
                return End.ToString();
            }
            set
            {
                End = new AssTime(value);
            }
        }

        public AssTime Start = new AssTime();

        public AssTime End = new AssTime();

        public string Style = "";

        public string Name = "";

        public int MarginL = 0;

        public int MarginR = 0;

        public int MarginV = 0;

        public string Effect = "";

        private string m_Text = "";
        public string Text
        {
            get { return m_Text; }
            set { m_Text = value; }
        }

        public AssItem()
        {
        }

    }

    public class AssTime
    {
        double timeValue;

        public double TimeValue
        {
            get
            {
                return timeValue;
            }
        }

        public AssTime()
        {
            timeValue = 0;
        }

        public AssTime(string time)
        {
            string[] s = new string[3];
            int c = 0;
            for (int i = 0; i < time.Length; i++)
            {
                if (Char.IsNumber(time[i]) || time[i] == '.')
                {
                    s[c] += time[i];
                }
                else
                {
                    if (time[i] == ':')
                        c++;
                }
            }
            timeValue = int.Parse(s[0]) * 3600 + int.Parse(s[1]) * 60 + float.Parse(s[2]);

        }

        public override string ToString()
        {
            int h, m,s,ms;
            int intTime = (int)Math.Round(timeValue * 100);
            h = intTime / 360000;
            intTime %= 360000;
            m = intTime / 6000;
            intTime %= 6000;
            s = intTime / 100;
            ms = intTime % 100;
            return h.ToString() + ":" + m.ToString("D2") + ":" + s.ToString("D2") + "." + ms.ToString("D2");
        }
    }
    //Format: Marked, Start, End, Style, Name, MarginL, MarginR, MarginV, Effect, Text

    public class AssLineParser
    {
        private char[] spliter = new char[12];
        private int formatpos = -1;
        private int markedpos = -1;
        private int startpos = -1;
        private int endpos = -1;
        private int stylepos = -1;
        private int namepos = -1;
        private int marginlpos = -1;
        private int marginrpos = -1;
        private int marginvpos = -1;
        private int effectpos = -1;
        private int textpos = -1;
        private int layerPos = -1;
        public AssLineParser(string format)
        {
            string seg = "";
            int pos = 0;
            int s = 0;
            format += "#";
            for(int i = 0;i<format.Length;i++)
            {
                if (Char.IsLetter(format[i])) seg += format[i];
                else
                {
                    if (!Char.IsSeparator(format[i]))
                    {
                        switch (seg.ToUpper())
                        {
                            case "FORMAT":
                                formatpos = pos;
                                break;
                            case "MARKED":
                                markedpos = pos;
                                break;
                            case "LAYER":
                                layerPos = pos;
                                break;
                            case "START":
                                startpos = pos;
                                break;
                            case "END":
                                endpos = pos;
                                break;
                            case "STYLE":
                                stylepos = pos;
                                break;
                            case "NAME":
                                namepos = pos;
                                break;
                            case "MARGINL":
                                marginlpos = pos;
                                break;
                            case "MARGINR":
                                marginrpos = pos;
                                break;
                            case "MARGINV":
                                marginvpos = pos;
                                break;
                            case "EFFECT":
                                effectpos = pos;
                                break;
                            case "TEXT":
                                textpos = pos;
                                break;
                            default:
                                break;
                        }
                        spliter[s] = format[i];
                        s++;
                        pos++;
                        seg = "";
                    }
                }
            }
        }

        public AssItem ParseLine(string line)
        {
            string[] segs = new string[12];
            int last = 0;
            int len;
            for (int i = 0; i < 12; i++)
            {
                if (spliter[i] != '#')
                {
                    len = line.IndexOf(spliter[i], last) - last;
                    segs[i] = line.Substring(last, len);
                    last = len + last + 1;
                }
                else
                {
                    segs[i] = line.Substring(last);
                    break;
                }

            }
            AssItem assitem = new AssItem();
            if (formatpos != -1) assitem.Format = segs[formatpos];
            if (markedpos != -1) assitem.Marked = segs[markedpos];
            if (layerPos != -1) assitem.Layer = segs[layerPos];
            if (startpos != -1) assitem.StartTime = segs[startpos];
            if (endpos != -1) assitem.EndTime = segs[endpos];
            if (stylepos != -1) assitem.Style = segs[stylepos];
            if (namepos != -1) assitem.Name = segs[namepos];
            if (marginlpos != -1) assitem.MarginL = int.Parse(segs[marginlpos]);
            if (marginrpos != -1) assitem.MarginR = int.Parse(segs[marginrpos]);
            if (marginvpos != -1) assitem.MarginV = int.Parse(segs[marginvpos]);
            if (effectpos != -1) assitem.Effect = segs[effectpos];
            if (textpos != -1) assitem.Text = segs[textpos];

            return assitem;
        }

    }


}
