using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sgsubdotnet
{
    public partial class KeyConfigForm : Form
    {
        public KeyConfigForm(Config.SGSConfig config)
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

        }

        public Config.SGSConfig m_Config;

        public Keys BWKey;
        public Keys FFKey;
        public Keys PauseKey;
        public Keys TimeKey;
        public Keys CTimeKey;
        public Keys STKey;
        public Keys ETKey;
        public Keys GCKey;
        public Keys GPKey;
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

        private void KeyConfigForm_Load(object sender, EventArgs e)
        {
            btnBW.Text = "后退" + BWKey.ToString();
            btnFF.Text = "前进" + FFKey.ToString();
            btnP.Text = "暂停" + PauseKey.ToString();
            btnT.Text = "插入时间点" + TimeKey.ToString();
            btnST.Text = "插入起始点" + STKey.ToString();
            btnET.Text = "插入终止点" + ETKey.ToString();
            btnGC.Text = "跳至当前行" + GCKey.ToString();
            btnGP.Text = "跳至上一行" + GPKey.ToString();
            btnCT.Text = "连续插入时间" + CTimeKey.ToString();

            numET.Value = (decimal)EndTimeOffset * 1000;
            numST.Value = (decimal)StartTimeOffset * 1000;
            numSS.Value = (decimal)SeekStep;
            checkAOC.Checked = AutoOC;
            
        }

        private void btnBW_KeyDown(object sender, KeyEventArgs e)
        {
            BWKey = e.KeyCode;
            btnBW.Text = "后退" + BWKey.ToString();
        }

        private void btnFF_KeyDown(object sender, KeyEventArgs e)
        {
            FFKey = e.KeyCode;
            btnFF.Text = "前进" + FFKey.ToString();
        }

        private void btnP_KeyDown(object sender, KeyEventArgs e)
        {
            PauseKey = e.KeyCode;
            btnP.Text = "暂停" + PauseKey.ToString();
        }

        private void btnT_KeyDown(object sender, KeyEventArgs e)
        {
            TimeKey = e.KeyCode;
            btnT.Text = "插入时间点" + TimeKey.ToString();
        }

        private void btnST_KeyDown(object sender, KeyEventArgs e)
        {
            STKey = e.KeyCode;
            btnST.Text = "插入起始点" + STKey.ToString();
        }

        private void btnET_KeyDown(object sender, KeyEventArgs e)
        {
            ETKey = e.KeyCode;
            btnET.Text = "插入终止点" + ETKey.ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            EndTimeOffset = (double)(numET.Value) / 1000.0;
            StartTimeOffset = (double)(numST.Value) / 1000.0;
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

            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnGotoCurrent_KeyDown(object sender, KeyEventArgs e)
        {
            GCKey = e.KeyCode;
            btnGC.Text = "跳至当前行" + GCKey.ToString();
        }

        private void btnGotoPrevious_KeyDown(object sender, KeyEventArgs e)
        {
            GPKey = e.KeyCode;
            btnGP.Text = "跳至上一行" + GPKey.ToString();
        }

        private void btnCT_KeyDown(object sender, KeyEventArgs e)
        {
            CTimeKey = e.KeyCode;
            btnCT.Text = "连续插入时间" + CTimeKey.ToString();
        }
    }
}
