using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subtitle
{
    public class AssItem
    {
        //Format: Marked, Start, End, Style, Name, MarginL, MarginR, MarginV, Effect, Text
        public string Format;

        public string Marked;

        public string StartTime
        {
            get
            {
                return "";
            }
            set
            {
            }
        }

        public string EndTime
        {
            get
            {
                return "";
            }
            set
            {
            }
        }

        public AssTime Start;

        public AssTime End;

        public string Style;

        public string Name;

        public int MarginL;

        public int MarginR;

        public int MarginV;

        public string Effect;

        public string Text;

        public AssItem()
        {
        }

    }

    public class AssTime
    {
        public AssTime()
        {
        }

        public AssTime(string Time)
        {
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
    //Format: Marked, Start, End, Style, Name, MarginL, MarginR, MarginV, Effect, Text

    class AssLineParser
    {
        private string[] spliter = new string[10];
        private int formatpos;
        private int markedpos;
        private int startpos;
        private int endpos;
        private int stylepos;
        private int namepos;
        private int marginlpos;
        private int marginrpos;
        private int marginvpos;
        private int effectpos;
        private int textpos;

    }


}
