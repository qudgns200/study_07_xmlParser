using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace FormTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // listView Init
            listView1.View = View.Details;           //컬럼형식으로 변경
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("Name", 80);        //컬럼추가
            listView1.Columns.Add("Desc", 250);
            listView1.Columns.Add("Content", 0);
        }

        private void LoadXml(string path)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(path);

            XmlNodeList xlist = xml.SelectNodes("//MESSAGE");

            foreach(XmlNode node in xlist)
            {
                XmlElement xelmnt = (XmlElement)node;
                string stream = "S" + xelmnt.GetAttribute("STREAM");
                string function = "F" + xelmnt.GetAttribute("FUNCTION");
                string desc = xelmnt.GetAttribute("DESC");
                string content = node.OuterXml.ToString();

                DataAddToList(stream, function, desc, content);
            }
        }

        private void DataAddToList(string s, string f, string desc, string content)
        {
            listView1.BeginUpdate();

            ListViewItem lv = new ListViewItem(s + f);
            lv.SubItems.Add(desc);
            lv.SubItems.Add(content);
            listView1.Items.Add(lv);

            listView1.EndUpdate();
        }

        private void openOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String file_path = null;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Application.StartupPath;
            DialogResult dr = ofd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                file_path = ofd.FileName;
                LoadXml(file_path);
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView lv = (ListView)sender;
            content.Text = lv.FocusedItem.SubItems[2].Text;
        }
    }
}
