using System;
using System.Windows.Forms;

namespace SGSControls
{
    public partial class WaveFormViewer : UserControl
    {
        public WaveFormViewer()
        {
            InitializeComponent();
        }
        #region Private members
        private Subtitle.AssSub _currentSub;
        private bool _subLoaded;
        private int _currentRow = -1;
        #endregion

        #region Public Events
        public event EventHandler<WaveReader.WFMouseEventArgs> WaveFormMouseDown
        {
            add { waveScope.WSMouseDown += value; }
            remove { waveScope.WSMouseDown -= value; }
        }

        public event EventHandler BTNOpenAss
        {
            add { tsbtnOpenAss.Click += value; }
            remove { tsbtnOpenAss.Click -= value; }
        }
        public event EventHandler BTNOpenTxt
        {
            add { tsbtnOpenTxt.Click += value; }
            remove { tsbtnOpenTxt.Click -= value; }
        }
        public event EventHandler BTNOpenMedia
        {
            add { tsbtnOpenMedia.Click += value; }
            remove { tsbtnOpenMedia.Click -= value; }
        }
        public event EventHandler BTNSaveAss
        {
            add { tsbtnSaveASS.Click += value; }
            remove { tsbtnSaveASS.Click -= value; }
        }
        public event EventHandler<PlayerControlEventArgs> PlayerControl = null;
        #endregion
        #region Public Properties
        public Subtitle.AssSub CurrentSub
        {
            get { return _currentSub; }
            set
            {
                _currentSub = value;
                if (value != null && value.SubItems.Count > 0)
                {
                    _subLoaded = true;

                }
                else
                {
                    _subLoaded = false;
                }
            }
        }
        public double CurrentPosition
        {
            get { return waveScope.CurrentPosition; }
            set {
                waveScope.CurrentPosition = value;
                waveScope.Redraw();
            }
        }
        public int CurrentLineIndex
        {
            get
            {
                return _currentRow;
            }
            set
            {
                _currentRow = value;
                RefreshDisplay();
            }
        }
        public void RefreshDisplay()
        {
            if (_subLoaded)
            {
                Subtitle.AssItem item;
                var time = new Subtitle.AssTime();
                if (_currentRow > 0)
                {
                    item = (Subtitle.AssItem)(_currentSub.SubItems[_currentRow - 1]);
                    time.TimeValue = item.End.TimeValue - item.Start.TimeValue;
                    labelLastLine.Text = item.Text;
                    labelLastDuration.Text = (time.TimeValue >= 0) ? time.ToString() : "?:??:??.??";
                    waveScope.LastStart = item.Start.TimeValue;
                    waveScope.LastEnd = item.End.TimeValue;
                }
                else
                {
                    labelLastLine.Text = "";
                    labelLastDuration.Text = @"-:--:--.--";
                    waveScope.LastStart = 0;
                    waveScope.LastEnd = 0;
                }
                if (_currentRow >= 0 && _currentRow < _currentSub.SubItems.Count)
                {
                    item = (Subtitle.AssItem)(_currentSub.SubItems[_currentRow]);
                    time.TimeValue = item.End.TimeValue - item.Start.TimeValue;
                    labelThisLine.Text = item.Text;
                    labelThisDuration.Text = (time.TimeValue >= 0) ? time.ToString() : "?:??:??.??";
                    waveScope.Start = item.Start.TimeValue;
                    waveScope.End = item.End.TimeValue;
                }
                else
                {
                    labelThisLine.Text = "";
                    labelThisDuration.Text = @"-:--:--.--";
                    waveScope.Start = 0;
                    waveScope.End = 0;
                }
                if (_currentRow < _currentSub.SubItems.Count - 1)
                {
                    item = (Subtitle.AssItem)(_currentSub.SubItems[_currentRow + 1]);
                    time.TimeValue = item.End.TimeValue - item.Start.TimeValue;
                    labelNextLine.Text = item.Text;
                    labelNextDuration.Text = (time.TimeValue >= 0) ? time.ToString() : "?:??:??.??";
                }
                else
                {
                    labelNextLine.Text = "";
                    labelNextDuration.Text = @"-:--:--.--";
                }
                waveScope.Redraw();
            }
        }
        #endregion
        #region Public Methods
        public void GenerateFFTData(string filename)
        {
            if (PlayerControl != null)
            {
                var arg = new PlayerControlEventArgs(PlayerCommand.Pause);
                PlayerControl(this, arg);
            }
            WaveReader.WaveForm.FFmpegpath = FFMpegPath;
            WaveReader.WaveForm wf = WaveReader.WaveForm.ExtractWave(filename);
            waveScope.Wave = wf;
            waveScope.Redraw();
        }
        public string MediaFilename;
        public string FFMpegPath;
        #endregion

        private void tsbtnFFT_Click(object sender, EventArgs e)
        {
            if (MediaFilename != null)
            {
                GenerateFFTData(MediaFilename);
            }
        }
    }
}
