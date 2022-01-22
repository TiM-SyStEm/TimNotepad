using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace TIMnotepad
{
    public partial class start_screen : Form
    {
        public start_screen()
        {
            InitializeComponent();
        }

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Версия программы: mh-" + TIMnotepad_api.GetVersion());
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            /*if (DateTime.Now.Month <= 12 && DateTime.Now.Year == 2020)
            {
                MessageBox.Show("С Новым 2021 годом!");
            }*/
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            mode2 m2 = new mode2();
            m2.Show();
        }

        private void label7_Click(object sender, EventArgs e)
        {

            mode1 m1 = new mode1();
            m1.Show();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Process.Start(@"Redutext.exe");
        }
    }
}