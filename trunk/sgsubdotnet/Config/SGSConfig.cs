using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Config
{
    public class SGSConfig
    {
        public double StartOffset { get; set; }

        public double EndOffset { get; set; }

        public double SeekStep { get; set; }

        public int SelectRowOffset { get; set; }

        public Keys Pause { get; set; }

        public Keys AddTimePoint { get; set; }

        public Keys AddStartTime { get; set; }

        public Keys AddEndTime { get; set; }

        public Keys SeekForward { get; set; }

        public Keys SeekBackword { get; set; }

        public SGSConfig()
        {
            Pause = Keys.Space;
            AddTimePoint = Keys.A;
            SeekBackword = Keys.Q;
            SelectRowOffset = 2;

        }
        
    }
}
