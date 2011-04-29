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
                dogEar.Hanning_Duration = 0.2;
                dogEar.Hanning_Overlap = 0.3;
                dogEar.Delta_Divisor = 6;
                dogEar.CatCoef = 1.5;
                dogEar.RabbitCoef = 2.6;
            }
        }

        private void btnHumanear_Click(object sender, EventArgs e)
        {
            if (CurrentSub != null && dogEar != null && CurrentIndex >= 0 && CurrentIndex < CurrentSub.SubItems.Count)
            {
                Subtitle.AssItem item = (Subtitle.AssItem)(CurrentSub.SubItems[CurrentIndex]);
                if (item.End.TimeValue - item.Start.TimeValue > 0.1)
                {
                    dogEar.EarAClip(item.Start.TimeValue, item.End.TimeValue - item.Start.TimeValue, EarType.Human);
                }
            }
        }

        private void btnDogear_Click(object sender, EventArgs e)
        {
            if (CurrentSub != null && dogEar != null && CurrentIndex >= 0 && CurrentIndex < CurrentSub.SubItems.Count)
            {
                Subtitle.AssItem item = (Subtitle.AssItem)(CurrentSub.SubItems[CurrentIndex]);
                if (item.End.TimeValue - item.Start.TimeValue > 0.1)
                {
                    dogEar.EarAClip(item.Start.TimeValue, item.End.TimeValue - item.Start.TimeValue, EarType.Cat);
                }
            }
        }
    }
}
