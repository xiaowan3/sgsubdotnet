using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGS.Controls
{
    class SyntaxToken
    {
        public TokenType Type{ get; private set; }
        public string Text { get; private set; }
        public int Length
        { get; private set; }

        public string Rtf { get; private set; }
        public SyntaxToken(TokenType type, string text)
        {
            Text = text;
            Rtf = StrToRtf(text);
            Type = type;
            switch (type)
            {
                case TokenType.Text:
                    Length = text.Length;
                    break;
                case TokenType.Comment:
                    Length = 0;
                    break;
                case TokenType.Hole:
                    Length = text.Length;
                    break;
                case TokenType.Uncertain:
                    Length = text.Length - 2;
                    break;
                case TokenType.TimeTag:
                    Length = 0;
                    break;
                case TokenType.Literal:
                    Length = text.Length - 1;
                    break;
            }
        }



        ///   <summary>
        /// 将字符串转换成RTF编码
        ///   </summary>
        ///   <param   name= "str"> 字符串 </param>
        ///   <returns> 将字符串转换成纯ASCII的编码 </returns>
        private static string StrToRtf(string str)
        {

            int length = str.Length;
            const int z = (int)'z';
            var ret = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                char ch = str[i];

                switch (ch)
                {
                    case '\\':
                        ret.Append("\\\\");
                        break;
                    case '\n':
                        ret.Append("\\par ");
                        break;
                    default:
                        if (ch > z)
                        {
                            //   Gets   the   encoding   for   the   specified   code   page.
                            Encoding targetEncoding = Encoding.Default;

                            //   Gets   the   byte   representation   of   the   specified   string.
                            byte[] encodedChars = targetEncoding.GetBytes(str[i].ToString());

                            for (int j = 0; j < encodedChars.Length; j++)
                            {
                                string st = encodedChars[j].ToString();
                                ret.Append("\\'").Append(int.Parse(st).ToString("X"));
                            }
                        }
                        else
                        {
                            ret.Append(ch);
                        }
                        break;
                }
            }
            return ret.ToString();
        }

    }
    enum TokenType { Text, Hole, Uncertain, Comment, TimeTag, Literal }

    class TimeTag
    {
        public double StartTime;
        public double Duration;
        public static TimeTag TryParse(string text)
        {
            string[] segs = text.Split(',');
            if(segs.Length != 2) return null;
            string[] timesegs = segs[0].Split(':');
            if(timesegs.Length >3) return null;
            double time = 0;
            double multiple = 1;
            for(int i = timesegs.Length -1;i>=0;i--)
            {
                double value;
                if (!double.TryParse(timesegs[i], out value)) return null;
                time += value*multiple;
                multiple *= 60;
            }
            double dur;
            if(!double.TryParse(segs[1],out dur))return null;
            return new TimeTag
                       {
                           Duration = dur,
                           StartTime = time
                       };
        }
        public static string CreateTag(double time,double duration)
        {
            double dsec = Math.Truncate(time);
            double ms = time - dsec;
            var sec = (int)dsec;
            int h = sec / 3600;
            int mm = (sec % 3600) / 60;
            double ss = sec % 60 + ms;
            return string.Format("[{0}:{1}:{2},{3}]", h.ToString("D1"), mm.ToString("D2"), ss.ToString("00.00"),duration.ToString("00.0"));
        }
    }
}
