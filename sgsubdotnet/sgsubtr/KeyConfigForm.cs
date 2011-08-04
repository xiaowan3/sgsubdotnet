using System;
using System.Windows.Forms;
using SGSDatatype;

namespace sgsubtr
{
    public partial class KeyConfigForm : Form
    {
        public KeyConfigForm(SGSConfig config)
        {
            InitializeComponent();
            _config = config;

            textTRW.AssiciateKey = _config.SeekBackword;
            textTFF.AssiciateKey = _config.SeekForward;
            textTPause.AssiciateKey = _config.Pause;
            textTSeekTo.AssiciateKey = _config.GotoCurrent;
            textTSeekPre.AssiciateKey = _config.GotoPrevious;
            textTAddTime.AssiciateKey = _config.AddTimePoint;
            textTAddCellTime.AssiciateKey = _config.AddCellTime;
            textTAddContTime.AssiciateKey = _config.AddContTimePoint;
            textTEnterEdit.AssiciateKey = _config.EnterEditMode;
            textTAddStartTime.AssiciateKey = _config.AddStartTime;
            textTAddEndTime.AssiciateKey = _config.AddEndTime;
            textTMTMinus.AssiciateKey = _config.MiniTrimMinus;
            textTMTPlus.AssiciateKey = _config.MiniTrimPlus;
            textTSaveAss.AssiciateKey = _config.SaveAss;

            StartTimeOffset = _config.StartOffset;
            EndTimeOffset = _config.EndOffset;
            SeekStep = _config.SeekStep;
            AutoOC = _config.AutoOverlapCorrection;

            MinitrimStep = _config.MinitrimStep;

            textKeyFF1.AssiciateKey = _config.PlayerFF;
            textKeyRW1.AssiciateKey = _config.PlayerRW;
            textKeyToggle1.AssiciateKey = _config.PlayerTogglePause;
            textKeySeek1.AssiciateKey = _config.PlayerJumpto;
            textKeyTimetag1.AssiciateKey = _config.InsertTag;

            textKeyFF2.AssiciateKey = _config.PlayerFF2;
            textKeyRW2.AssiciateKey = _config.PlayerRW2;
            textKeyToggle2.AssiciateKey = _config.PlayerTogglePause2;
            textKeySeek2.AssiciateKey = _config.PlayerJumpto2;
            textKeyTimetag2.AssiciateKey = _config.InsertTag2;

        }

        public SGSConfig _config;

        private double m_sto;
        public double StartTimeOffset
        {
            get { return m_sto; }
            set
            {
                if (value > 1) m_sto = 1;
                else if (value < -1) m_sto = -1;
                else m_sto = value;
            }
        }
        private double m_eto;
        public double EndTimeOffset
        {
            get { return m_eto; }
            set
            {
                if (value > 1) m_eto = 1;
                else if (value < -1) m_eto = -1;
                else m_eto = value;
            }
        }
        public double SeekStep;
        public bool AutoOC;

        public double MinitrimStep;

        private void KeyConfigForm_Load(object sender, EventArgs e)
        {
            numET.Value = (decimal)EndTimeOffset * 1000;
            numST.Value = (decimal)StartTimeOffset * 1000;
            numSS.Value = (decimal)SeekStep;
            numMTS.Value = (decimal)MinitrimStep * 1000;
            checkAOC.Checked = AutoOC;
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            EndTimeOffset = (double)(numET.Value) / 1000.0;
            StartTimeOffset = (double)(numST.Value) / 1000.0;
            _config.MinitrimStep = (double)(numMTS.Value) / 1000.0;
            SeekStep = (double)(numSS.Value);
            AutoOC = checkAOC.Checked;

            _config.SeekBackword = textTRW.AssiciateKey;
            _config.SeekForward = textTFF.AssiciateKey;
            _config.Pause = textTPause.AssiciateKey;
            _config.GotoCurrent = textTSeekTo.AssiciateKey;
            _config.GotoPrevious = textTSeekPre.AssiciateKey;
            _config.AddTimePoint = textTAddTime.AssiciateKey;
            _config.AddContTimePoint = textTAddContTime.AssiciateKey;
            _config.EnterEditMode = textTEnterEdit.AssiciateKey;
            _config.AddCellTime = textTAddCellTime.AssiciateKey;
            _config.AddStartTime = textTAddStartTime.AssiciateKey;
            _config.AddEndTime = textTAddEndTime.AssiciateKey;
            _config.MiniTrimPlus = textTMTPlus.AssiciateKey;
            _config.MiniTrimMinus = textTMTMinus.AssiciateKey;
            _config.SaveAss = textTSaveAss.AssiciateKey;


            _config.StartOffset = StartTimeOffset;
            _config.EndOffset = EndTimeOffset;
            _config.SeekStep = SeekStep;
            _config.AutoOverlapCorrection = AutoOC;

            _config.PlayerFF = textKeyFF1.AssiciateKey;
            _config.PlayerRW = textKeyRW1.AssiciateKey;
            _config.PlayerTogglePause = textKeyToggle1.AssiciateKey;
            _config.PlayerJumpto = textKeySeek1.AssiciateKey;
            _config.InsertTag = textKeyTimetag1.AssiciateKey;

            _config.PlayerFF2 = textKeyFF2.AssiciateKey;
            _config.PlayerRW2 = textKeyRW2.AssiciateKey;
            _config.PlayerTogglePause2 = textKeyToggle2.AssiciateKey;
            _config.PlayerJumpto2 = textKeySeek2.AssiciateKey;
            _config.InsertTag2 = textKeyTimetag2.AssiciateKey;

            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void KeyconfigText_KeyDown(object sender, KeyEventArgs e)
        {
            var senderTextbox = (KeyConfigTextBox)sender;
            senderTextbox.AssiciateKey = e.KeyCode;
            e.SuppressKeyPress = true;
        }
    }
    class KeyConfigTextBox : TextBox
    {
        private Keys _associateKey;
        public Keys AssiciateKey
        {
            get { return _associateKey; }
            set
            {
                _associateKey = value;
                Text = _associateKey.ToString();
            }
        }
    }
}
