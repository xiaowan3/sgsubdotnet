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
        public KeyConfigForm()
        {
            InitializeComponent();
        }

        public Keys BWKey;
        public Keys FFKey;
        public Keys PauseKey;
        public Keys TimeKey;
        public Keys STKey;
        public Keys ETKey;
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

        private void KeyConfigForm_Load(object sender, EventArgs e)
        {
            btnBW.Text = "后退" + BWKey.ToString();
            btnFF.Text = "前进" + FFKey.ToString();
            btnP.Text = "暂停" + PauseKey.ToString();
            btnT.Text = "插入时间点" + TimeKey.ToString();
            btnST.Text = "插入起始点" + STKey.ToString();
            btnET.Text = "插入终止点" + ETKey.ToString();

            numET.Value = (decimal)EndTimeOffset * 1000;
            numST.Value = (decimal)StartTimeOffset * 1000;
            numSS.Value = (decimal)SeekStep;
            
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
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
