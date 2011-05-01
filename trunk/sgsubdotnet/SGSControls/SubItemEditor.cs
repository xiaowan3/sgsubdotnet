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
    public partial class SubItemEditor : UserControl
    {
        public static string FFMpegPath = null;
        public SubItemEditor()
        {
            InitializeComponent();
        }

        #region Events
        public event EventHandler<PlayerControlEventArgs> PlayerControl = null;
        public event EventHandler<EventArgs> ButtonClicked = null;
        #endregion
        string m_MediaFile;
        private DogEar dogEar = null;
        public Subtitle.AssSub CurrentSub = null;
        public int CurrentIndex { get; set; }
        public string MediaFile
        {
            get { return m_MediaFile; }
            set
            {
                m_MediaFile = value;
                dogEar = new DogEar(this, value, FFMpegPath);
                dogEar.Hanning_Duration = 0.09;
                dogEar.Hanning_Overlap = 0.4;
                dogEar.Delta_Divisor = 18;
                dogEar.CatCoef = 1.5;
                dogEar.RabbitCoef = 2.6;
            }
        }

        private void btnHumanear_Click(object sender, EventArgs e)
        {
            if (CurrentSub != null && dogEar != null && CurrentIndex >= 0 && CurrentIndex < CurrentSub.SubItems.Count)
            {
                Subtitle.AssItem item = (Subtitle.AssItem)(CurrentSub.SubItems[CurrentIndex]);
                double duration = item.End.TimeValue - item.Start.TimeValue;
                if (duration > 30)
                {
                    MessageBox.Show("囧，句子太长了，我会傲娇的。");
                }
                else if (duration > 0.1)
                {
                    if (PlayerControl != null) PlayerControl(this, new PlayerControlEventArgs(PlayerCommand.Pause));
                    dogEar.EarAClip(item.Start.TimeValue, duration, EarType.Human);
                }
            }
            if (ButtonClicked != null) ButtonClicked(this, new EventArgs());
        }

        private void btnDogear_Click(object sender, EventArgs e)
        {
            if (CurrentSub != null && dogEar != null && CurrentIndex >= 0 && CurrentIndex < CurrentSub.SubItems.Count)
            {
                Subtitle.AssItem item = (Subtitle.AssItem)(CurrentSub.SubItems[CurrentIndex]);
                double duration = item.End.TimeValue - item.Start.TimeValue;
                if (duration > 30)
                {
                    MessageBox.Show("囧，句子太长了，我会傲娇的。");
                }
                if (duration > 0.1)
                {
                    if (PlayerControl != null) PlayerControl(this, new PlayerControlEventArgs(PlayerCommand.Pause));
                    dogEar.EarAClip(item.Start.TimeValue, duration, EarType.Cat);
                }
            }
            if (ButtonClicked != null) ButtonClicked(this, new EventArgs());
        }

        private void btnRabbitear_Click(object sender, EventArgs e)
        {
            if (CurrentSub != null && dogEar != null && CurrentIndex >= 0 && CurrentIndex < CurrentSub.SubItems.Count)
            {
                Subtitle.AssItem item = (Subtitle.AssItem)(CurrentSub.SubItems[CurrentIndex]);
                if (item.End.TimeValue - item.Start.TimeValue > 0.1)
                {
                    if (PlayerControl != null) PlayerControl(this, new PlayerControlEventArgs(PlayerCommand.Pause));
                    dogEar.EarAClip(item.Start.TimeValue, item.End.TimeValue - item.Start.TimeValue, EarType.Rabbit);
                }
            }
            if (ButtonClicked != null) ButtonClicked(this, new EventArgs());
        }
    }
}
