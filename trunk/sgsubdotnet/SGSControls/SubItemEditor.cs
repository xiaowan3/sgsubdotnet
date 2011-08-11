using System;
using System.Windows.Forms;
using SGSDatatype;

namespace SGSControls
{
    public partial class SubItemEditor : UserControl
    {
     //   public static string FFMpegPath;
        public SubItemEditor()
        {
            InitializeComponent();
        }

        #region Events
        public event EventHandler<PlayerControlEventArgs> PlayerControl = null;
        public event EventHandler<EventArgs> ButtonClicked = null;
        #endregion
        string _mediaFile;
        private WSOLA _wsola;
        public SubStationAlpha CurrentSub;
        public int CurrentIndex { private get; set; }
        public string MediaFile
        {
            set
            {
                _mediaFile = value;
                _wsola = new WSOLA(this, value)
                              {
                                  Hanning_Duration = 0.09,
                                  Hanning_Overlap = 0.4,
                                  Delta_Divisor = 18,
                                  CatCoef = 1.5,
                                  RabbitCoef = 2.6
                              };
            }
        }

        private void btnHumanear_Click(object sender, EventArgs e)
        {
            if (CurrentSub != null && _wsola != null && CurrentIndex >= 0 && 
                CurrentIndex < CurrentSub.EventsSection.EventList.Count)
            {
                var item = (V4Event)(CurrentSub.EventsSection.EventList[CurrentIndex]);
                double duration = item.End.Value - item.Start.Value;
                if (duration > 30)
                {
                    MessageBox.Show(@"囧，句子太长了，我会傲娇的。");
                }
                else if (duration > 0.1)
                {
                    if (PlayerControl != null) PlayerControl(this, new PlayerControlEventArgs(PlayerCommand.Pause));
                    _wsola.EarAClip(item.Start.Value, duration, EarType.Human);
                }
            }
            if (ButtonClicked != null) ButtonClicked(this, new EventArgs());
        }

        private void btnDogear_Click(object sender, EventArgs e)
        {
            if (CurrentSub != null && _wsola != null && CurrentIndex >= 0 && 
                CurrentIndex < CurrentSub.EventsSection.EventList.Count)
            {
                var item = (V4Event)(CurrentSub.EventsSection.EventList[CurrentIndex]);
                double duration = item.End.Value - item.Start.Value;
                if (duration > 30)
                {
                    MessageBox.Show(@"囧，句子太长了，我会傲娇的。");
                }
                if (duration > 0.1)
                {
                    if (PlayerControl != null) PlayerControl(this, new PlayerControlEventArgs(PlayerCommand.Pause));
                    _wsola.EarAClip(item.Start.Value, duration, EarType.Cat);
                }
            }
            if (ButtonClicked != null) ButtonClicked(this, new EventArgs());
        }

        private void btnRabbitear_Click(object sender, EventArgs e)
        {
            if (CurrentSub != null && _wsola != null && CurrentIndex >= 0 && CurrentIndex < CurrentSub.EventsSection.EventList.Count)
            {
                var item = (V4Event)(CurrentSub.EventsSection.EventList[CurrentIndex]);
                if (item.End.Value - item.Start.Value > 0.1)
                {
                    if (PlayerControl != null) PlayerControl(this, new PlayerControlEventArgs(PlayerCommand.Pause));
                    _wsola.EarAClip(item.Start.Value, item.End.Value - item.Start.Value, EarType.Rabbit);
                }
            }
            if (ButtonClicked != null) ButtonClicked(this, new EventArgs());
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (CurrentSub != null && CurrentIndex >= 0 && CurrentIndex < CurrentSub.EventsSection.EventList.Count)
            {
                var item = (V4Event)(CurrentSub.EventsSection.EventList[CurrentIndex]);
                double duration = item.End.Value - item.Start.Value;
                if (duration < 1) return;
                var saveFiledlg = new SaveFileDialog
                                      {
                                          Filter =
                                              @"MP3 File (*.mp3)|*.mp3||",
                                          AddExtension = true,
                                          DefaultExt = "mp3"
                                      };
                if (saveFiledlg.ShowDialog() == DialogResult.OK)
                {
                    AudioFileIO.ExportAudioClip(_mediaFile, item.Start.Value.ToString("F1"), duration.ToString("F1"),
                                                "160k", saveFiledlg.FileName);
                }

            }
        }

    }
}
