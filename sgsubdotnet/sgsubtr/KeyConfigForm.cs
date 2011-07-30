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
            m_Config = config;

            BWKey = m_Config.SeekBackword;
            FFKey = m_Config.SeekForward;
            PauseKey = m_Config.Pause;
            TimeKey = m_Config.AddTimePoint;
            STKey = m_Config.AddStartTime;
            ETKey = m_Config.AddEndTime;
            StartTimeOffset = m_Config.StartOffset;
            EndTimeOffset = m_Config.EndOffset;
            SeekStep = m_Config.SeekStep;
            AutoOC = m_Config.AutoOverlapCorrection;
            GCKey = m_Config.GotoCurrent;
            GPKey = m_Config.GotoPrevious;
            CTimeKey = m_Config.AddContTimePoint;
            EEMKey = m_Config.EnterEditMode;
            CellTimeKey = m_Config.AddCellTime;
            SaveAssKey = m_Config.SaveAss;
            MinitrimMKey = m_Config.MiniTrimMinus;
            MinitrimPKey = m_Config.MiniTrimPlus;
            MinitrimStep = m_Config.MinitrimStep;

            PlayerFF = m_Config.PlayerFF;
            PlayerRW = m_Config.PlayerRW;
            PlayerToggle = m_Config.PlayerTogglePause;
            PlayerSeek = m_Config.PlayerJumpto;
            InsertTag = m_Config.InsertTag;

        }

        public SGSConfig m_Config;

        public Keys BWKey;
        public Keys FFKey;
        public Keys PauseKey;
        public Keys TimeKey;
        public Keys CTimeKey;
        public Keys STKey;
        public Keys ETKey;
        public Keys GCKey;
        public Keys GPKey;
        public Keys EEMKey;
        public Keys CellTimeKey;
        public Keys SaveAssKey;
        public Keys MinitrimPKey;
        public Keys MinitrimMKey;

        public Keys PlayerFF;
        public Keys PlayerRW;
        public Keys PlayerToggle;
        public Keys PlayerSeek;
        public Keys InsertTag;

        private double m_sto;
        public double StartTimeOffset
        {
            get { return m_sto; }
            set {
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
            btnBW.Text = BWKey.ToString();
            btnFF.Text = FFKey.ToString();
            btnP.Text = PauseKey.ToString();
            btnT.Text =  TimeKey.ToString();
            btnST.Text =  STKey.ToString();
            btnET.Text =  ETKey.ToString();
            btnGC.Text = GCKey.ToString();
            btnGP.Text =  GPKey.ToString();
            btnCT.Text =  CTimeKey.ToString();
            btnEEM.Text =  EEMKey.ToString();
            btnCellT.Text = CellTimeKey.ToString();
            btnSaveAss.Text = SaveAssKey.ToString();
            btnMinitrimM.Text = MinitrimMKey.ToString();
            btnMinitrimP.Text = MinitrimPKey.ToString();
            numET.Value = (decimal)EndTimeOffset * 1000;
            numST.Value = (decimal)StartTimeOffset * 1000;
            numSS.Value = (decimal)SeekStep;
            numMTS.Value = (decimal) MinitrimStep*1000;
            checkAOC.Checked = AutoOC;

            textKeyFF1.Text = PlayerFF.ToString();
            textKeyRW1.Text = PlayerRW.ToString();
            textKeyToggle1.Text = PlayerToggle.ToString();
            textKeySeek1.Text = PlayerSeek.ToString();
            textKeyTimetag1.Text = InsertTag.ToString();

        }

        private void btnBW_KeyDown(object sender, KeyEventArgs e)
        {
            BWKey = e.KeyCode;
            btnBW.Text = BWKey.ToString();
        }

        private void btnFF_KeyDown(object sender, KeyEventArgs e)
        {
            FFKey = e.KeyCode;
            btnFF.Text = FFKey.ToString();
        }

        private void btnP_KeyDown(object sender, KeyEventArgs e)
        {
            PauseKey = e.KeyCode;
            btnP.Text = PauseKey.ToString();
        }

        private void btnT_KeyDown(object sender, KeyEventArgs e)
        {
            TimeKey = e.KeyCode;
            btnT.Text =  TimeKey.ToString();
        }

        private void btnST_KeyDown(object sender, KeyEventArgs e)
        {
            STKey = e.KeyCode;
            btnST.Text = STKey.ToString();
        }

        private void btnET_KeyDown(object sender, KeyEventArgs e)
        {
            ETKey = e.KeyCode;
            btnET.Text =  ETKey.ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            EndTimeOffset = (double)(numET.Value) / 1000.0;
            StartTimeOffset = (double)(numST.Value) / 1000.0;
            m_Config.MinitrimStep = (double) (numMTS.Value)/1000.0;
            SeekStep = (double)(numSS.Value);
            AutoOC = checkAOC.Checked;

            m_Config.SeekBackword = BWKey;
            m_Config.SeekForward = FFKey;
            m_Config.Pause = PauseKey;
            m_Config.AddTimePoint = TimeKey;
            m_Config.AddStartTime = STKey;
            m_Config.AddEndTime = ETKey;
            m_Config.StartOffset = StartTimeOffset;
            m_Config.EndOffset = EndTimeOffset;
            m_Config.SeekStep = SeekStep;
            m_Config.AutoOverlapCorrection = AutoOC;
            m_Config.GotoCurrent = GCKey;
            m_Config.GotoPrevious = GPKey;
            m_Config.AddContTimePoint = CTimeKey;
            m_Config.EnterEditMode = EEMKey;
            m_Config.AddCellTime = CellTimeKey;
            m_Config.SaveAss = SaveAssKey;
            m_Config.MiniTrimPlus = MinitrimPKey;
            m_Config.MiniTrimMinus = MinitrimMKey;

            m_Config.PlayerFF = PlayerFF;
            m_Config.PlayerRW = PlayerRW;
            m_Config.PlayerTogglePause = PlayerToggle;
            m_Config.PlayerJumpto = PlayerSeek;
            m_Config.InsertTag = InsertTag;
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnGotoCurrent_KeyDown(object sender, KeyEventArgs e)
        {
            GCKey = e.KeyCode;
            btnGC.Text = GCKey.ToString();
        }

        private void btnGotoPrevious_KeyDown(object sender, KeyEventArgs e)
        {
            GPKey = e.KeyCode;
            btnGP.Text =  GPKey.ToString();
        }

        private void btnCT_KeyDown(object sender, KeyEventArgs e)
        {
            CTimeKey = e.KeyCode;
            btnCT.Text =  CTimeKey.ToString();
        }

        private void btnEEM_KeyDown(object sender, KeyEventArgs e)
        {
            EEMKey = e.KeyCode;
            btnEEM.Text = EEMKey.ToString();
        }

        private void btnCellT_KeyDown(object sender, KeyEventArgs e)
        {
            CellTimeKey = e.KeyCode;
            btnCellT.Text = CellTimeKey.ToString();
        }

        private void btnSaveAss_KeyDown(object sender, KeyEventArgs e)
        {
            SaveAssKey = e.KeyCode;
            btnSaveAss.Text = SaveAssKey.ToString();
        }

        private void btnMinitrimM_KeyDown(object sender, KeyEventArgs e)
        {
            MinitrimMKey = e.KeyCode;
            btnMinitrimM.Text = MinitrimMKey.ToString();
        }

        private void btnMinitrimP_KeyDown(object sender, KeyEventArgs e)
        {
            MinitrimPKey = e.KeyCode;
            btnMinitrimP.Text = MinitrimPKey.ToString();
        }


        private void textKeyFF1_KeyDown(object sender, KeyEventArgs e)
        {
            PlayerFF = e.KeyCode;
            textKeyFF1.Text = PlayerFF.ToString();
            e.SuppressKeyPress = true;
        }

        private void textKeyRW1_KeyDown(object sender, KeyEventArgs e)
        {
            PlayerRW = e.KeyCode;
            textKeyRW1.Text = PlayerRW.ToString();
            e.SuppressKeyPress = true;
        }

        private void textKeyToggle1_KeyDown(object sender, KeyEventArgs e)
        {
            PlayerToggle = e.KeyCode;
            textKeyToggle1.Text = PlayerToggle.ToString();
            e.SuppressKeyPress = true;
        }

        private void textKeySeek1_KeyDown(object sender, KeyEventArgs e)
        {
            PlayerSeek = e.KeyCode;
            textKeySeek1.Text = PlayerSeek.ToString();
            e.SuppressKeyPress = true;
        }

        private void textKeyTimetag1_KeyDown(object sender, KeyEventArgs e)
        {
            InsertTag = e.KeyCode;
            textKeyTimetag1.Text = InsertTag.ToString();
            e.SuppressKeyPress = true;
        }


    }
}
