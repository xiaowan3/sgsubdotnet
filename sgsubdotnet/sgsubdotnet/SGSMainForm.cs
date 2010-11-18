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
    public partial class SGSMainForm : Form
    {
        public SGSMainForm()
        {
            InitializeComponent();
        }

        private Subtitle.AssSub sub = new Subtitle.AssSub();

        private void button1_Click(object sender, EventArgs e)
        {
            string formatline = "Format: Layer, Start, End, Style, Name, MarginL, MarginR, MarginV, Effect, Text";
            string linetoparse = "Dialogue: 0,0:01:11.05,0:01:15.23,*Default,NTP,0000,0000,0000,,好吃就行了管他的呢 再来一碗";
            Subtitle.AssLineParser parser = new Subtitle.AssLineParser(formatline);
      
            parser.ParseLine(linetoparse);

            DataGridViewColumn column;
            sub.LoadAss("E:\\test\\ass.ass");
            subtitleGrid.AutoGenerateColumns = false;
            subtitleGrid.AutoSize = false;

            subtitleGrid.DataSource = sub.SubItems;

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "StartTime";
            subtitleGrid.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "EndTime";
            subtitleGrid.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Text";
            subtitleGrid.Columns.Add(column);
        }
    }
}
