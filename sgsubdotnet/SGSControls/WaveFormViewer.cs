using System;
using System.Windows.Forms;
using SGSDatatype;
namespace SGSControls
{
    public partial class WaveFormViewer : UserControl
    {
        public WaveFormViewer()
        {
            InitializeComponent();
        }
        #region Private members
        private SubStationAlpha _currentSub;
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
        public new event EventHandler MouseEnter
        {
            add { waveScope.MouseEnter += value; }
            remove { waveScope.MouseEnter -= value; }
        }
        public new event EventHandler MouseLeave
        {
            add { waveScope.MouseLeave += value; }
            remove { waveScope.MouseLeave -= value; }
        }
        public event EventHandler<PlayerControlEventArgs> PlayerControl = null;
        #endregion
        #region Public Properties
        public SubStationAlpha CurrentSub
        {
            get { return _currentSub; }
            set
            {
                _currentSub = value;
                if (value != null && value.EventsSection.EventList.Count > 0)
                {
                    _subLoaded = true;

                }
                else
                {
                    _subLoaded = false;
                    labelLastLine.Text = "";
                    labelLastDuration.Text = @"-:--:--.--";
                    labelThisLine.Text = "";
                    labelThisDuration.Text = @"-:--:--.--";
                    labelNextLine.Text = "";
                    labelNextDuration.Text = @"-:--:--.--";
                    waveScope.Redraw();
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
                V4Event item;
                var time = new SSATime();
                if (_currentRow > 0)
                {
                    item = (V4Event)(_currentSub.EventsSection.EventList[_currentRow - 1]);
                    time.Value = item.End.Value - item.Start.Value;
                    labelLastLine.Text = item.Text.ToString();
                    labelLastDuration.Text = (time.Value >= 0) ? time.ToString() : "?:??:??.??";
                    waveScope.LastStart = item.Start.Value;
                    waveScope.LastEnd = item.End.Value;
                }
                else
                {
                    labelLastLine.Text = "";
                    labelLastDuration.Text = @"-:--:--.--";
                    waveScope.LastStart = 0;
                    waveScope.LastEnd = 0;
                }
                if (_currentRow >= 0 && _currentRow < _currentSub.EventsSection.EventList.Count)
                {
                    item = (V4Event)(_currentSub.EventsSection.EventList[_currentRow]);
                    time.Value = item.End.Value - item.Start.Value;
                    labelThisLine.Text = item.Text.ToString();
                    labelThisDuration.Text = (time.Value >= 0) ? time.ToString() : "?:??:??.??";
                    waveScope.Start = item.Start.Value;
                    waveScope.End = item.End.Value;
                }
                else
                {
                    labelThisLine.Text = "";
                    labelThisDuration.Text = @"-:--:--.--";
                    waveScope.Start = 0;
                    waveScope.End = 0;
                }
                if (_currentRow < _currentSub.EventsSection.EventList.Count - 1)
                {
                    item = (V4Event)(_currentSub.EventsSection.EventList[_currentRow + 1]);
                    time.Value = item.End.Value - item.Start.Value;
                    labelNextLine.Text = item.Text.ToString();
                    labelNextDuration.Text = (time.Value >= 0) ? time.ToString() : "?:??:??.??";
                }
                else
                {
                    labelNextLine.Text = "";
                    labelNextDuration.Text = @"-:--:--.--";
                }
                waveScope.Redraw();
            }
            else
            {
                labelLastLine.Text = "";
                labelLastDuration.Text = @"-:--:--.--";
                labelThisLine.Text = "";
                labelThisDuration.Text = @"-:--:--.--";
                labelNextLine.Text = "";
                labelNextDuration.Text = @"-:--:--.--";
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

            FFTForm fftForm = new FFTForm();
            fftForm.ExtractWave(filename);
            fftForm.ShowDialog();
            waveScope.Wave = fftForm.Waveform;
            waveScope.Redraw();
        }
        public string MediaFilename;
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
