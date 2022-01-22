using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TIMnotepad
{
    public partial class web : Form
    {
        public bool lv = false;
        public web()
        {
            InitializeComponent();
            Uri url = new Uri("file:///" + TIMnotepad_api.site);
            webControl1.Source = url;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void web_SizeChanged(object sender, EventArgs e)
        {
            if (Height > 120)
            {
                textBox1.Text = Width.ToString() + ", " + (Height + 40).ToString();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    string[] size = textBox1.Text.Split(',');
                    if (Convert.ToInt32(size[1]) > 120 && Convert.ToInt32(size[0]) > 240)
                    {
                        Size size1 = new Size(Convert.ToInt32(size[0].Trim()), Convert.ToInt32(size[1].Trim()) + 40);
                        Size = size1;
                    }
                }
                catch (Exception)
                {
                    Size size1 = new Size(240, (320 + 40));
                    Size = size1;
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            webControl1.GoBack();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            webControl1.Refresh();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            webControl1.GoForward();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lv)
            {
                timer1.Enabled = false;
                TIMnotepad_api.liveViewer = String.Empty;
                MessageBox.Show("LiveViewer - выключен");
                lv = false;
            }
            else
            {
                timer1.Enabled = true;
                TIMnotepad_api.liveViewer = File.ReadAllText(TIMnotepad_api.site);
                MessageBox.Show("LiveViewer - включен");
                lv = true;
                timer1.Tick += timer1_Tick;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            webControl1.LoadHTML(TIMnotepad_api.liveViewer);
        }
    }
}
