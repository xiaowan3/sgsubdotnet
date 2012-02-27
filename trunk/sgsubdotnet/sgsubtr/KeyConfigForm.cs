using System;
using System.Windows.Forms;
using SGS.Datatype;

namespace sgsubtr
{
    public partial class KeyConfigForm : Form
    {
        public KeyConfigForm(SGSConfig config)
        {
            InitializeComponent();
            _config = config;

            textTRW.Key = _config.SeekBackword;
            textTFF.Key = _config.SeekForward;
            textTPause.Key = _config.Pause;
            textTSeekTo.Key = _config.GotoCurrent;
            textTSeekPre.Key = _config.GotoPrevious;
            textTAddTime.Key = _config.AddTimePoint;
            textTAddCellTime.Key = _config.AddCellTime;
            textTAddContTime.Key = _config.AddContTimePoint;
            textTEnterEdit.Key = _config.EnterEditMode;
            textTAddStartTime.Key = _config.AddStartTime;
            textTAddEndTime.Key = _config.AddEndTime;
            textTMTMinus.Key = _config.MiniTrimMinus;
            textTMTPlus.Key = _config.MiniTrimPlus;
            textTSaveAss.Key = _config.SaveAss;

            StartTimeOffset = _config.StartOffset;
            EndTimeOffset = _config.EndOffset;
            SeekStep = _config.SeekStep;
            AutoOC = _config.AutoOverlapCorrection;

            MinitrimStep = _config.MinitrimStep;

            textKeyFF1.Key = _config.PlayerFF;
            textKeyRW1.Key = _config.PlayerRW;
            textKeyToggle1.Key = _config.PlayerTogglePause;
            textKeySeek1.Key = _config.PlayerJumpto;
            textKeyTimetag1.Key = _config.InsertTag;

            textKeyFF2.Key = _config.PlayerFF2;
            textKeyRW2.Key = _config.PlayerRW2;
            textKeyToggle2.Key = _config.PlayerTogglePause2;
            textKeySeek2.Key = _config.PlayerJumpto2;
            textKeyTimetag2.Key = _config.InsertTag2;

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

            _config.SeekBackword = textTRW.Key;
            _config.SeekForward = textTFF.Key;
            _config.Pause = textTPause.Key;
            _config.GotoCurrent = textTSeekTo.Key;
            _config.GotoPrevious = textTSeekPre.Key;
            _config.AddTimePoint = textTAddTime.Key;
            _config.AddContTimePoint = textTAddContTime.Key;
            _config.EnterEditMode = textTEnterEdit.Key;
            _config.AddCellTime = textTAddCellTime.Key;
            _config.AddStartTime = textTAddStartTime.Key;
            _config.AddEndTime = textTAddEndTime.Key;
            _config.MiniTrimPlus = textTMTPlus.Key;
            _config.MiniTrimMinus = textTMTMinus.Key;
            _config.SaveAss = textTSaveAss.Key;


            _config.StartOffset = StartTimeOffset;
            _config.EndOffset = EndTimeOffset;
            _config.SeekStep = SeekStep;
            _config.AutoOverlapCorrection = AutoOC;

            _config.PlayerFF = textKeyFF1.Key;
            _config.PlayerRW = textKeyRW1.Key;
            _config.PlayerTogglePause = textKeyToggle1.Key;
            _config.PlayerJumpto = textKeySeek1.Key;
            _config.InsertTag = textKeyTimetag1.Key;

            _config.PlayerFF2 = textKeyFF2.Key;
            _config.PlayerRW2 = textKeyRW2.Key;
            _config.PlayerTogglePause2 = textKeyToggle2.Key;
            _config.PlayerJumpto2 = textKeySeek2.Key;
            _config.InsertTag2 = textKeyTimetag2.Key;

            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
    class KeyConfigTextBox : TextBox
    {
        private Keys _key;
        public Keys Key
        {
            get { return _key; }
            set
            {
                _key = value;
                Text = _key.ToString();
            }
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            _key = e.KeyCode;
            Text = e.KeyCode.ToString();
            e.SuppressKeyPress = true;
            base.OnKeyDown(e);
        }
    }
}
