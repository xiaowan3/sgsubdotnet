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
        private readonly Dictionary<string, Image> _layoutlist = new Dictionary<string, Image>();
        public ConfigForm(SGSConfig config)
        {
            _config = config;
            InitializeComponent();

            if (!Directory.Exists(SGSConfig.DefaultCfgPath)) throw new Exception("Directory not exist.");
            var files = Directory.GetFiles(SGSConfig.DefaultCfgPath, @"*.layout");
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
                Image defaultPreview = Image.FromFile(SGSConfig.DefaultCfgPath + "defaultpic.png");
                    
                try
                {
                    previewImage = Image.FromFile(SGSConfig.DefaultCfgPath + xmlnode.Attributes["PreviewFile"].Value);
                }
                catch
                {
                    previewImage = defaultPreview;
                }
                _layoutlist.Add(layoutname, previewImage);
                listLayout.Items.Add(layoutname);



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
            numAutosavePeriod.Value = _config.AutoSavePeriod / 60;
            numAutosaveLifeTime.Value = _config.AutoSaveLifeTime;
            numLineLength.Value = _config.LineLength;
            textWindowChar.Text = _config.HolePlaceholder.ToString();
            textCommentChar.Text = _config.CommentMark.ToString();
            textUncertainLeft.Text = _config.UncertainLeftMark.ToString();
            textUncertainRight.Text = _config.UncertainRightMark.ToString();
            comboPlayer.SelectedIndex = (int) _config.Player;
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
            _config.AutoSavePeriod = (int) numAutosavePeriod.Value*60;
            _config.AutoSaveLifeTime = (int) numAutosaveLifeTime.Value;
            _config.LineLength = (int) numLineLength.Value;
            _config.Player = (PlayerType) comboPlayer.SelectedIndex;
            if (textWindowChar.Text.Length > 0)
                _config.HolePlaceholder = textWindowChar.Text[0];
            if (textCommentChar.Text.Length > 0)
                _config.CommentMark = textCommentChar.Text[0];
            if (textUncertainLeft.Text.Length > 0)
                _config.UncertainLeftMark = textUncertainLeft.Text[0];
            if (textUncertainRight.Text.Length > 0)
                _config.UncertainRightMark = textUncertainRight.Text[0];

            _config.Save();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
