﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sgsubdotnet
{
    public partial class TimeOffsetDialog : Form
    {
        public TimeOffsetDialog()
        {
            InitializeComponent();
            TimeOffset = 0;
        }

        public double TimeOffset
        {
            get;
            private set;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            string str = maskedTextTimeOffset.Text;
            int sgn = str[0] == '-' ? -1 : 1;
            int h = Math.Abs(int.Parse(str.Substring(0, 2)));
            int m = int.Parse(str.Substring(3, 2));
            double s = double.Parse(str.Substring(6, 5));
            TimeOffset = sgn * (h * 3600 + m * 60 + s);
            DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            TimeOffset = 0;
            DialogResult = DialogResult.Cancel;
        }

       
    }
}