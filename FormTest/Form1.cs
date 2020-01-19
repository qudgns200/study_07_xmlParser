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
            // Load XML using PATH
            XmlDocument xml = new XmlDocument();
            xml.Load(path);

            // Devide Node as String
            XmlNodeList xlist = xml.SelectNodes("//MESSAGE");

            // Load as Attribute from Node
            foreach(XmlNode node in xlist)
            {
                XmlElement xelmnt = (XmlElement)node;
                string stream = "S" + xelmnt.GetAttribute("STREAM");
                string function = "F" + xelmnt.GetAttribute("FUNCTION");
                string desc = xelmnt.GetAttribute("DESC");
                //string content = node.OuterXml;
                string content = FormatXml(node);

                DataAddToList(stream, function, desc, content);
            }
        }

        // Added Values to ListView
        private void DataAddToList(string s, string f, string desc, string content)
        {
            listView1.BeginUpdate();

            ListViewItem lv = new ListViewItem(s + f);
            lv.SubItems.Add(desc);
            lv.SubItems.Add(content);
            listView1.Items.Add(lv);

            listView1.EndUpdate();
        }

        // Load xml event
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

        // To parse for XML format
        protected string FormatXml(XmlNode xmlNode)
        {
            StringBuilder bob = new StringBuilder();

            // We will use stringWriter to push the formated xml into our StringBuilder bob.
            using (System.IO.StringWriter stringWriter = new System.IO.StringWriter(bob))
            {
                // We will use the Formatting of our xmlTextWriter to provide our indentation.
                using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
                {
                    xmlTextWriter.Formatting = Formatting.Indented;
                    xmlNode.WriteTo(xmlTextWriter);
                }
            }

            return bob.ToString();
        }
    }
}
