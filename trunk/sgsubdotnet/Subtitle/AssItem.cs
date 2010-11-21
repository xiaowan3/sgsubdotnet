using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

namespace Subtitle
{
    /// <summary>
    /// 文件头
    /// </summary>
    [DataContract(Name ="SGSConfig",Namespace="Config")]
    public class AssHead
    {
        [DataMember()]
        public List<string> m_HeadLines = new List<string>();

        public void AddLine(string line)
        {
            m_HeadLines.Add(line);
        }

        public void Clear()
        {
            m_HeadLines = new List<string>();
        }

        public void WriteTo(StreamWriter oStream)
        {
            foreach (string s in m_HeadLines)
                oStream.WriteLine(s);
        }
    }

    /// <summary>
    /// 每一行字幕
    /// </summary>
    public class AssItem
    {
        //Format: Marked, Start, End, Style, Name, MarginL, MarginR, MarginV, Effect, Text
        //全是ass中每一行的东西
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

        public string Actor = "";

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
        public AssItem Clone()
        {
            AssItem ret = new AssItem();
            ret.Actor = (string)Actor.Clone();
            ret.Effect = (string)Effect.Clone();
            ret.End = new AssTime(End.ToString());
            ret.Format = (string)Format.Clone();
            ret.Layer = (string)Layer.Clone();
            ret.m_Text = (string)m_Text.Clone();
            ret.MarginL = MarginL;
            ret.MarginR = MarginR;
            ret.MarginV = MarginV;
            ret.Marked = (string)Marked.Clone();
            ret.Name = (string)Name.Clone();
            ret.Start = new AssTime(Start.ToString());
            ret.Style = (string)Style.Clone();
        
            return ret;
        }

    }

    /// <summary>
    /// 时间
    /// </summary>
    public class AssTime
    {
        double timeValue;

        public double TimeValue
        {
            get
            {
                return timeValue;
            }
            set
            {
                timeValue = value;
            }
        }

        public AssTime()
        {
            timeValue = 0;
        }

        public AssTime(string time)
        {
            string sub;
            int a = time.Length-1;
            int b = 0;
            timeValue = 0;
            float unit = 1;
            try
            {
                do
                {
                    b = time.LastIndexOf(':', a);
                    if (b != -1)
                    {
                        sub = time.Substring(b + 1, a - b);
                        a = b - 1;
                    }
                    else
                    {
                        sub = time.Substring(0, a + 1);
                    }
                    timeValue += float.Parse(sub) * unit;
                    unit *= 60;
                } while (b != -1);
            }
            catch (Exception e)
            {
                timeValue = 0;
            }

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

    /// <summary>
    /// 解析每一行
    /// </summary>
    public class AssLineParser
    {
        private string m_fmtline = "";
        public string FmtLine
        {
            get { return m_fmtline; }
        }
        private char[] spliter = new char[13];
        private int formatpos = -1;
        private int markedpos = -1;
        private int startpos = -1;
        private int endpos = -1;
        private int stylepos = -1;
        private int namepos = -1;
        private int actorpos = -1;
        private int marginLpos = -1;
        private int marginRpos = -1;
        private int marginVpos = -1;
        private int effectpos = -1;
        private int textpos = -1;
        private int layerPos = -1;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="format">ass [Event]中Format的那一行</param>
        public AssLineParser(string format)
        {
            string seg = "";
            int pos = 0;
            int s = 0;
            m_fmtline = format;
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
                            case "ACTOR":
                                actorpos = pos;
                                break;
                            case "MARGINL":
                                marginLpos = pos;
                                break;
                            case "MARGINR":
                                marginRpos = pos;
                                break;
                            case "MARGINV":
                                marginVpos = pos;
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

        /// <summary>
        /// 解析一行
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public AssItem ParseLine(string line)
        {
            string[] segs = new string[13];
            int last = 0;
            int len;
            string trimed = line.Trim();
            if (trimed.Length <= 1 || trimed[0] == ';')
                return null;
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
            if (actorpos != -1) assitem.Actor = segs[actorpos];
            if (marginLpos != -1) assitem.MarginL = int.Parse(segs[marginLpos]);
            if (marginRpos != -1) assitem.MarginR = int.Parse(segs[marginRpos]);
            if (marginVpos != -1) assitem.MarginV = int.Parse(segs[marginVpos]);
            if (effectpos != -1) assitem.Effect = segs[effectpos];
            if (textpos != -1) assitem.Text = segs[textpos];

            return assitem;
        }

        /// <summary>
        /// 生成一行的字符串
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public string FormatLine(AssItem line)
        {
            string str = "";
            string[] segs = new string[12];
            for (int i = 0; i < 12; i++) segs[i] = "";

            if (formatpos != -1) segs[formatpos] = line.Format;
            if (markedpos != -1) segs[markedpos] = line.Marked;
            if (layerPos != -1) segs[layerPos] = line.Layer;
            if (startpos != -1) segs[startpos] = line.StartTime;
            if (endpos != -1) segs[endpos] = line.EndTime;
            if (stylepos != -1) segs[stylepos] = line.Style;
            if (namepos != -1) segs[namepos] = line.Name;
            if (actorpos != -1) segs[actorpos] = line.Actor;
            if (marginLpos != -1) segs[marginLpos] = line.MarginL.ToString("D4");
            if (marginRpos != -1) segs[marginRpos] = line.MarginR.ToString("D4");
            if (marginVpos != -1) segs[marginVpos] = line.MarginV.ToString("D4");
            if (effectpos != -1) segs[effectpos] = line.Effect;
            if (textpos != -1) segs[textpos] = line.Text;
            for (int i = 0; i < 12; i++)
            {
                if (spliter[i] != '#')
                {
                    str += (segs[i] + spliter[i]);
                }
                else
                {
                    str += segs[i];
                    break;
                }
            }
            return str;
        }


    }


}
