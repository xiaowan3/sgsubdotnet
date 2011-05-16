using System;
using System.Windows.Forms;
using SGSDatatype;

namespace SGSControls
{
    public partial class SubItemEditor : UserControl
    {
        public static string FFMpegPath;
        public SubItemEditor()
        {
            InitializeComponent();
        }

        #region Events
        public event EventHandler<PlayerControlEventArgs> PlayerControl = null;
        public event EventHandler<EventArgs> ButtonClicked = null;
        #endregion
        string _mediaFile;
        private DogEar _dogEar;
        public AssSub CurrentSub;
        public int CurrentIndex { get; set; }
        public string MediaFile
        {
            get { return _mediaFile; }
            set
            {
                _mediaFile = value;
                _dogEar = new DogEar(this, value, FFMpegPath)
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
            if (CurrentSub != null && _dogEar != null && CurrentIndex >= 0 && CurrentIndex < CurrentSub.SubItems.Count)
            {
                var item = (AssItem)(CurrentSub.SubItems[CurrentIndex]);
                double duration = item.End.TimeValue - item.Start.TimeValue;
                if (duration > 30)
                {
                    MessageBox.Show(@"囧，句子太长了，我会傲娇的。");
                }
                else if (duration > 0.1)
                {
                    if (PlayerControl != null) PlayerControl(this, new PlayerControlEventArgs(PlayerCommand.Pause));
                    _dogEar.EarAClip(item.Start.TimeValue, duration, EarType.Human);
                }
            }
            if (ButtonClicked != null) ButtonClicked(this, new EventArgs());
        }

        private void btnDogear_Click(object sender, EventArgs e)
        {
            if (CurrentSub != null && _dogEar != null && CurrentIndex >= 0 && CurrentIndex < CurrentSub.SubItems.Count)
            {
                var item = (AssItem)(CurrentSub.SubItems[CurrentIndex]);
                double duration = item.End.TimeValue - item.Start.TimeValue;
                if (duration > 30)
                {
                    MessageBox.Show(@"囧，句子太长了，我会傲娇的。");
                }
                if (duration > 0.1)
                {
                    if (PlayerControl != null) PlayerControl(this, new PlayerControlEventArgs(PlayerCommand.Pause));
                    _dogEar.EarAClip(item.Start.TimeValue, duration, EarType.Cat);
                }
            }
            if (ButtonClicked != null) ButtonClicked(this, new EventArgs());
        }

        private void btnRabbitear_Click(object sender, EventArgs e)
        {
            if (CurrentSub != null && _dogEar != null && CurrentIndex >= 0 && CurrentIndex < CurrentSub.SubItems.Count)
            {
                var item = (AssItem)(CurrentSub.SubItems[CurrentIndex]);
                if (item.End.TimeValue - item.Start.TimeValue > 0.1)
                {
                    if (PlayerControl != null) PlayerControl(this, new PlayerControlEventArgs(PlayerCommand.Pause));
                    _dogEar.EarAClip(item.Start.TimeValue, item.End.TimeValue - item.Start.TimeValue, EarType.Rabbit);
                }
            }
            if (ButtonClicked != null) ButtonClicked(this, new EventArgs());
        }
    }
}
