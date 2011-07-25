using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSControls
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
    enum TokenType{Text,Hole,Uncertain,Comment}
}
