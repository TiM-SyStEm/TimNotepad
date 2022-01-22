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
using System.Threading;
using Yandex.Metrica;

namespace TIMnotepad
{
    public partial class Form1 : Form
    {
        static string open_path = "";
        static string filename = "";
        public Form1()
        {
            InitializeComponent();
            #region Яндекс.Метрика
            YandexMetricaFolder.SetCurrent(@"files/metrica");
            YandexMetrica.Activate("eec0be42-e202-4f51-ba44-a1ec4c76275f");
            #endregion
            #region Настройка фильтров файлов
            openFileDialog1.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            saveFileDialog1.Filter = "Text File(*.txt)|*.txt";
            #endregion
            #region Настройка текствого поля
            richTextBox1.Multiline = true;
            richTextBox1.AcceptsTab = true;
            richTextBox1.SelectionTabs = new int[] { 12 };
            autocompleteMenu1.Items = File.ReadAllText(@"files/dictionaries/RUS-ENG-MINI.dicr").Split('\n');
            #endregion
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog1.FileName;
            File.WriteAllText(filename, richTextBox1.Text);
            string path = File.ReadAllText("files/path.t$");
        }

        private void отрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename2 = openFileDialog1.FileName;
            string text = File.ReadAllText(filename2);
            richTextBox1.Text = text;
            open_path = filename2;
            string path = File.ReadAllText("files/path.t$");
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string path = File.ReadAllText("files/path.t$");
                if (open_path != "")
                {
                    File.WriteAllText(open_path, richTextBox1.Text);
                }
                else
                {
                    MessageBox.Show(
                        "Error!",
                        "Файл не сохранён",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
                }
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "Файл не сохранён",
                    "Ошибка!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.TextLength > 0)
            {
                richTextBox1.Copy();
            }
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.TextLength > 0)
            {
                richTextBox1.Paste();
            }
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.TextLength > 0)
            {
                richTextBox1.Cut();
            }
        }
        private void настройкиШрифтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            richTextBox1.Font = fontDialog1.Font;
        }

        private void настройкиФонаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            richTextBox1.BackColor = colorDialog1.Color;
        }

        private void выделитьВсёToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.TextLength > 0)
            {
                richTextBox1.SelectAll();
            }
        }

        private void копироватьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.TextLength > 0)
            {
                richTextBox1.Copy();
            }
        }

        private void вставитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.TextLength > 0)
            {
                richTextBox1.Paste();
            }
        }

        private void вырезатьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.TextLength > 0)
            {
                richTextBox1.Cut();
            }
        }

        private void выделитьВсёToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.TextLength > 0)
            {
                richTextBox1.SelectAll();
            }
        }

        private void режимРазработчикаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Visible = false;
            mode1 mode1 = new mode1();
            mode1.Show();
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            //Количество символов
            string textr = richTextBox1.Text;
            int len = textr.Length;
            label2.Text = len.ToString();
            //Количество строк
            string[] k = richTextBox1.Text.Split('\n');
            int n = k.Length;
            label4.Text = n.ToString();
        }

        private void режимРедактированияTnfФайловToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                richTextBox1.ContextMenuStrip = contextMenuStrip1;
            }
        }

        private void режимРазработчикаНаCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Visible = false;
            mode2 m2 = new mode2();
            m2.Show();
        }

        private void toolStripComboBox1_TextChanged(object sender, EventArgs e)
        {
            if (toolStripComboBox1.Text == "Светлая")
            {
                Color myColor = ColorTranslator.FromHtml("#FFFFFF");
                richTextBox1.BackColor = myColor;
                Color myColor2 = ColorTranslator.FromHtml("#000000");
                richTextBox1.ForeColor = myColor2;
                menuStrip1.BackColor = ColorTranslator.FromHtml("#F8F8F8");
                menuStrip1.ForeColor = myColor2;
                panel1.BackColor = ColorTranslator.FromHtml("#F0F0F0");
                label1.ForeColor = myColor2;
                label2.ForeColor = myColor2;
                label3.ForeColor = myColor2;
                label4.ForeColor = myColor2;
                //----
                panel3.BackColor = ColorTranslator.FromHtml("#F0F0F0");
                richTextBox2.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                richTextBox2.ForeColor = myColor2;
                button3.ForeColor = myColor2;
                button3.FlatAppearance.BorderColor = myColor2;
                button4.ForeColor = myColor2;
                button4.FlatAppearance.BorderColor = myColor2;
                textBox1.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                textBox1.ForeColor = myColor2;
            }
            else if (toolStripComboBox1.Text == "Тёмная")
            {
                Color myColor = ColorTranslator.FromHtml("#1E1E1E");
                richTextBox1.BackColor = myColor;
                Color myColor2 = ColorTranslator.FromHtml("#B4B4B4");
                Color myColor3 = ColorTranslator.FromHtml("#2F2F32");
                richTextBox1.ForeColor = myColor2;
                menuStrip1.BackColor = ColorTranslator.FromHtml("#2D2D30");
                menuStrip1.ForeColor = myColor2;
                panel1.BackColor = ColorTranslator.FromHtml("#2D2D30");
                label1.ForeColor = myColor2;
                label2.ForeColor = myColor2;
                label3.ForeColor = myColor2;
                label4.ForeColor = myColor2;
                //----
                panel3.BackColor = ColorTranslator.FromHtml("#151515");
                richTextBox2.BackColor = ColorTranslator.FromHtml("#1E1E1E");
                richTextBox2.ForeColor = myColor2;
                button3.ForeColor = myColor2;
                button3.FlatAppearance.BorderColor = myColor2;
                button4.ForeColor = myColor2;
                button4.FlatAppearance.BorderColor = myColor2;
                textBox1.BackColor = ColorTranslator.FromHtml("#383838");
                textBox1.ForeColor = myColor2;
            }
            else if (toolStripComboBox1.Text == "Облачная") 
            {
                Color myColor = ColorTranslator.FromHtml("#BFCDDB");
                richTextBox1.BackColor = myColor;
                Color myColor2 = ColorTranslator.FromHtml("#FFFFFF");
                Color myColor3 = ColorTranslator.FromHtml("#2F2F32");
                richTextBox1.ForeColor = myColor2;
                menuStrip1.BackColor = ColorTranslator.FromHtml("#3862BC");
                menuStrip1.ForeColor = ColorTranslator.FromHtml("#103B97");
                panel1.BackColor = ColorTranslator.FromHtml("#3862BC");
                label1.ForeColor = myColor2;
                label2.ForeColor = myColor2;
                label3.ForeColor = myColor2;
                label4.ForeColor = myColor2;
                //----
                panel3.BackColor = ColorTranslator.FromHtml("#3862BC");
                richTextBox2.BackColor = ColorTranslator.FromHtml("#3862BC");
                richTextBox2.ForeColor = myColor2;
                button3.ForeColor = myColor2;
                button3.FlatAppearance.BorderColor = myColor2;
                button4.ForeColor = myColor2;
                button4.FlatAppearance.BorderColor = myColor2;
                textBox1.BackColor = ColorTranslator.FromHtml("#3862BC");
                textBox1.ForeColor = myColor2;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string newsnippet = textBox1.Text;
            textBox1.Text = "";
            richTextBox2.Text = richTextBox2.Text + "\n" + newsnippet;
            File.WriteAllText(@"files/dictionaries/RUS-ENG-MINI.dicr", richTextBox2.Text);
            autocompleteMenu1.Items = File.ReadAllText("files/dictionaries/RUS-ENG-MINI.dicr").Split('\n');
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            File.WriteAllText(@"files/dictionaries/RUS-ENG-MINI.dicr", richTextBox2.Text);
        }

        private void открытьРедакторСловаряToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
            richTextBox2.Text = File.ReadAllText(@"files/dictionaries/RUS-ENG-MINI.dicr");
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            panel3.Location = new Point((Width / 2) - (537 / 2), (Height / 2) - (424 / 2));
        }

        private void открытьТерминалToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode.Equals(Keys.S))
            {
                saveFileDialog1.Filter = "CSharp файлы исходного кода (*.cs)|*.cs";
                filename = saveFileDialog1.FileName;
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                File.WriteAllText(filename, richTextBox1.Text);
                File.WriteAllText("files/site.t$", filename);
            }
            else if (e.Control && e.KeyCode.Equals(Keys.S))
            {
                string path = File.ReadAllText("files/path.t$");
                if (open_path != "")
                {
                    File.WriteAllText(open_path, richTextBox1.Text);
                }
                else
                {
                    MessageBox.Show(
                        "Ошибка!",
                        "Файл не сохранён",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
                }
            }
        }
    }
}
