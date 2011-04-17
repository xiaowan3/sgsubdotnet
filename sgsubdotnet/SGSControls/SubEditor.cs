using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SGSControls
{
    public partial class SubEditor : UserControl
    {
        public SubEditor()
        {
            InitializeComponent();
        }
        #region Private Members

        /// <summary>
        /// 字幕内容
        /// </summary>
        private Subtitle.AssSub m_CurrentSub = new Subtitle.AssSub();
        private Config.SGSConfig m_Config;
        #endregion

        #region Events
        public event EventHandler<SeekEventArgs> Seek = null;
        public event EventHandler<TimeEditEventArgs> TimeEdit = null;
        #endregion

        #region Methods
        public void EditBeginTime(int LineNumber, double Value)
        {
        }
        public void EditEndTime(int LineNumber, double Value)
        {
        }
        #endregion
    }

    public class SeekEventArgs : EventArgs
    {
        public double SeekOffset;
        public SeekDir SeekDirection;
        public SeekEventArgs(SeekMethod seekdir, double seekoff)
        {
            SeekOffset = seekoff;
            SeekDirection = seekdir;
        }
    }
    public enum SeekDir { Begin, CurrentPos };

    public class TimeEditEventArgs : EventArgs
    {
        public TimeType EditTime;
        public double TimeValue;
        public TimeEditEventArgs(TimeType editTime, doube timevalue)
        {
            EditTime = editTime;
            TimeValue = timevalue;
        }
    }
    public enum TimeType { BeginTime, EndTime };
}
