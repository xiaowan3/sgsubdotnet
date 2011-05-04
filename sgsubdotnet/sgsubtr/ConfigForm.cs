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

namespace sgsubtr
{
    public partial class ConfigForm : Form
    {
        private Config.SGSConfig _config;
        private string _configDir;
        private Dictionary<string, Image> _layoutlist = new Dictionary<string, Image>();
        public ConfigForm(Config.SGSConfig config, string configDir)
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
                try
                {
                    previewImage = Image.FromFile(_configDir + xmlnode.Attributes["PreviewFile"].Value);
                }
                catch
                {
                    continue;
                }
                _layoutlist.Add(layoutname, previewImage);
                listLayout.Items.Add(layoutname);
            }
        }

        private void listLayout_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listLayout.SelectedItem != null)
            {
                pictureLayout.Image = _layoutlist[listLayout.SelectedItem.ToString()];
            }
        }
    }
}
