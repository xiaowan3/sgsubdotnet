﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace SGSDatatype
{
    /// <summary>
    /// 文件头
    /// </summary>
    [DataContract(Name = "AssHead", Namespace = "SGSDatatype")]
    public class AssHead
    {
        [DataMember]
        public List<string> HeadLines = new List<string>();

        public void AddLine(string line)
        {
            HeadLines.Add(line);
        }

        public void Clear()
        {
            HeadLines = new List<string>();
        }

        public void WriteTo(StreamWriter oStream)
        {
            foreach (string s in HeadLines)
                oStream.WriteLine(s);
        }
    }

    /// <summary>
    /// 每一行字幕
    /// </summary>
    [DataContract(Name = "AssItem", Namespace = "SGSDatatype")]
    public class AssItem
    {
        //Format: Marked, Start, End, Style, Name, MarginL, MarginR, MarginV, Effect, Text
        //全是ass中每一行的东西
        [DataMember]
        public string Format = "";

        [DataMember]
        public string Marked = "";

        [DataMember]
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

        [DataMember]
        public AssTime Start = new AssTime();

        [DataMember]
        public AssTime End = new AssTime();

        [DataMember]
        public string Style = "";

        [DataMember]
        public string Name = "";

        [DataMember]
        public string Actor = "";

        [DataMember]
        public int MarginL = 0;

        [DataMember]
        public int MarginR = 0;

        [DataMember]
        public int MarginV = 0;

        [DataMember]
        public string Effect = "";

        private string _mText = "";
       
        [DataMember]
        public string Text
        {
            get { return _mText; }
            set { _mText = value; }
        }

        public AssItem()
        {
        }
        public AssItem Clone()
        {
            var ret = new AssItem
                          {
                              Actor = (string) Actor.Clone(),
                              Effect = (string) Effect.Clone(),
                              End = new AssTime(End.ToString()),
                              Format = (string) Format.Clone(),
                              Layer = (string) Layer.Clone(),
                              _mText = (string) _mText.Clone(),
                              MarginL = MarginL,
                              MarginR = MarginR,
                              MarginV = MarginV,
                              Marked = (string) Marked.Clone(),
                              Name = (string) Name.Clone(),
                              Start = new AssTime(Start.ToString()),
                              Style = (string) Style.Clone()
                          };

            return ret;
        }

    }

    /// <summary>
    /// 时间
    /// </summary>
    [DataContract(Name = "AssTime", Namespace = "SGSDatatype")]
    public class AssTime
    {
        [DataMember]
        public double TimeValue { get; set; }

        public AssTime()
        {
            TimeValue = 0;
        }

        public AssTime(string time)
        {
            if(time == null)
            {
                TimeValue = 0;
                return;
            }
            int a = time.Length-1;
            TimeValue = 0;
            float unit = 1;
            try
            {
                int b;
                do
                {
                    b = time.LastIndexOf(':', a);
                    string sub;
                    if (b != -1)
                    {
                        sub = time.Substring(b + 1, a - b);
                        a = b - 1;
                    }
                    else
                    {
                        sub = time.Substring(0, a + 1);
                    }
                    TimeValue += float.Parse(sub) * unit;
                    unit *= 60;
                } while (b != -1);
            }
            catch (Exception)
            {
                TimeValue = 0;
            }

        }

        public override string ToString()
        {
            var intTime = (int)Math.Round(TimeValue * 100);
            int h = intTime / 360000;
            intTime %= 360000;
            int m = intTime / 6000;
            intTime %= 6000;
            int s = intTime / 100;
            int ms = intTime % 100;
            return string.Format("{0}:{1}:{2}.{3}", h, m.ToString("D2"), s.ToString("D2"), ms.ToString("D2"));
        }


    }
    //Format: Marked, Start, End, Style, Name, MarginL, MarginR, MarginV, Effect, Text

    /// <summary>
    /// 解析每一行
    /// </summary>
    [DataContract(Name = "AssLineParser", Namespace = "SGSDatatype")]
    public class AssLineParser
    {
        [DataMember]
        private string m_fmtline = "";
        public string FmtLine
        {
            get { return m_fmtline; }
        }
        [DataMember]
        private char[] spliter = new char[13];
        [DataMember]
        private int formatpos = -1;
        [DataMember]
        private int markedpos = -1;
        [DataMember]
        private int startpos = -1;
        [DataMember]
        private int endpos = -1;
        [DataMember]
        private int stylepos = -1;
        [DataMember]
        private int namepos = -1;
        [DataMember]
        private int actorpos = -1;
        [DataMember]
        private int marginLpos = -1;
        [DataMember]
        private int marginRpos = -1;
        [DataMember]
        private int marginVpos = -1;
        [DataMember]
        private int effectpos = -1;
        [DataMember]
        private int textpos = -1;
        [DataMember]
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
            foreach (char t in format)
            {
                if (Char.IsLetter(t)) seg += t;
                else
                {
                    if (!Char.IsSeparator(t))
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
                        spliter[s] = t;
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
            var segs = new string[13];
            int last = 0;
            string trimed = line.Trim();
            if (trimed.Length <= 1 || trimed[0] == ';')
                return null;
            for (int i = 0; i < 12; i++)
            {
                if (spliter[i] != '#')
                {
                    int len = line.IndexOf(spliter[i], last) - last;
                    segs[i] = line.Substring(last, len);
                    last = len + last + 1;
                }
                else
                {
                    segs[i] = line.Substring(last);
                    break;
                }

            }
            var assitem = new AssItem();
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
            var segs = new string[12];
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