using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TIMnotepad
{
    public partial class PluginMeneger : Form
    {
        private List<string> all_plugins = new List<string>();
        public PluginMeneger()
        {
            InitializeComponent();
            // richTextBox1.Text = File.ReadAllText(@"files\plugins\all-plugins.t$");

            IEnumerable<string> files = Directory.EnumerateFiles(@"files\plugins", "*.dll", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                string[] sfile = file.Split('\\');
                sfile = sfile[sfile.Length - 1].Split('.');
                string gfile = sfile[0];

                richTextBox1.Text += gfile;
                all_plugins.Add(gfile);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "TIMnotepad plugin (*.timp) | *.timp";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string fname = openFileDialog1.FileName;
            string[] objs = fname.Split('.');
            objs = objs[0].Split('\\');
            string dllname = objs[objs.Length - 1] + ".dll";

            File.WriteAllBytes(@"files\plugins\" + dllname, File.ReadAllBytes(fname));
            File.Delete(fname);
            richTextBox1.Text += objs[objs.Length - 1];
        }
    }
}
