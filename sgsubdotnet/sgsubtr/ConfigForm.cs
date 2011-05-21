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
using SGSDatatype;
// ReSharper disable PossibleLossOfFraction
namespace sgsubtr
{
    public partial class ConfigForm : Form
    {
        private readonly SGSConfig _config;
        private readonly string _configDir;
        private readonly Dictionary<string, Image> _layoutlist = new Dictionary<string, Image>();
        public ConfigForm(SGSConfig config, string configDir)
        {
            _config = config;
            InitializeComponent();
            _configDir = configDir;
            if (!Directory.Exists(configDir)) throw new Exception("Directory not exist.");
            var files = Directory.GetFiles(configDir, @"*.layout");
            if (files.Count() == 0) throw new Exception(("No layout found"));
            foreach (var file in files)
            {
                int s1 = file.LastIndexOf('\\');
                int s2 = file.LastIndexOf('.');
                var layoutname = file.Substring(s1 + 1, s2 - s1 - 1);
                var reader = new XmlTextReader(file);
                var xmldoc = new XmlDocument();
                xmldoc.Load((reader));
                XmlNode xmlnode =
                    xmldoc.ChildNodes.Cast<XmlNode>().Where(node => node.Name == "SGSUBLayout").FirstOrDefault();
                if (xmlnode == null) continue;
                if (xmlnode.Attributes == null) continue;
                Image previewImage;
                Image defaultPreview = Image.FromFile(_configDir + "defaultpic.png");
                    
                try
                {
                    previewImage = Image.FromFile(_configDir + xmlnode.Attributes["PreviewFile"].Value);
                }
                catch
                {
                    previewImage = defaultPreview;
                }
                _layoutlist.Add(layoutname, previewImage);
                listLayout.Items.Add(layoutname);
                numAutosavePeriod.Value = _config.AutoSavePeriod/60;
                numAutosaveLifeTime.Value = _config.AutoSaveLifeTime;

            }
            if (_config.LayoutName != null && _layoutlist.ContainsKey(_config.LayoutName))
            {
                listLayout.SelectedItem =
                    listLayout.Items.Cast<string>().Where(name => name == _config.LayoutName).FirstOrDefault();
            }
            else
            {
                listLayout.SelectedIndex = 0;
            }
        }

        private void listLayout_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listLayout.SelectedItem != null)
            {
                pictureLayout.Image = _layoutlist[listLayout.SelectedItem.ToString()];
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            _config.LayoutName = listLayout.SelectedItem.ToString();
            _config.AutoSavePeriod = (int)numAutosavePeriod.Value * 60;
            _config.AutoSaveLifeTime = (int) numAutosaveLifeTime.Value;
            _config.Save();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
