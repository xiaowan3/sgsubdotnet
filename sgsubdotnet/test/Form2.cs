using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;
using SGSDatatype;
namespace test
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            _config = SGSConfig.FromFile(@"E:\test\newsgscfg.xml");

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AutoSize = false;
            var column1 = new DataGridViewTextBoxColumn
            {
                HeaderText = @"Time",
                DataPropertyName = "SaveTime",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            dataGridView1.Columns.Add(column1);
            var column2 = new DataGridViewTextBoxColumn
            {
                HeaderText = @"Filename",
                DataPropertyName = "Filename",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            dataGridView1.Columns.Add(column2);
            dataGridView1.DataSource = _autosave.AutoSaveFileBindingSource;

        }

        private SGSConfig _config;
        private SubStationAlpha _sub;
        private string _filename;
        

        private void button1_Click(object sender, EventArgs e)
        {

            SubStationAlpha ssa = SubStationAlpha.Load(@"E:\test\Haruhi_14.ass");
            var writer = new FileStream("E:\\test\\ttttt.xml", FileMode.Create);
            ssa.WriteXml(writer);
            writer.Close();
            ssa.Save(@"E:\test\testout.txt",Encoding.Unicode);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

            }
        }
        SGSAutoSave _autosave = new SGSAutoSave(@"E:\test\testsave");
        private void button3_Click(object sender, EventArgs e)
        {
            _autosave.SaveHistory(_sub, _filename);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            _autosave.Load();

        }

        private void button5_Click(object sender, EventArgs e)
        {

                //style.SetProperty("fontsize", 16);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var guid  = Guid.NewGuid();
            var ba = guid.ToByteArray();
            textBox1.Text = guid.ToString();
        }
    }
}
