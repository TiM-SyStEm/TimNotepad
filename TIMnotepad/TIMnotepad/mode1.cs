using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;

namespace TIMnotepad
{
    public partial class mode1 : Form
    {
        static string open_path = "";
        static string hexc = "#FFFFFF";
        static string rgbc = "255,255,255";
        static string htmlc = "white";
        static string filename = "";
        static string mainnodename = "Проект";
        static string openprojecyfile = "main.html";
        public mode1()
        {
            InitializeComponent();
            openFileDialog1.Filter = "Все-файлы (*.*)|*.*";
            saveFileDialog1.Filter = "Веб-старница (*.html)|*.html";
            String fullAppName = Application.ExecutablePath;
            String fullAppPath = Path.GetDirectoryName(fullAppName);
            String fullFileName = Path.Combine(fullAppPath, "files/button.wav");
            sp = new SoundPlayer(fullFileName);
            fastColoredTextBox1.Text = File.ReadAllText(@"files/html.t$");
            //autocompleteMenu1.Items = File.ReadAllText(@"files/dictionaries/html-reserv-list.dicr").Split('\n');
        }
        private SoundPlayer sp;
        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            filename = saveFileDialog1.FileName;
            File.WriteAllText(filename, fastColoredTextBox1.Text);
            TIMnotepad_api.site = filename;
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            filename = openFileDialog1.FileName;
            string text = File.ReadAllText(filename);
            fastColoredTextBox1.Text = text;
            TIMnotepad_api.path = filename;
            open_path = filename;
            string path = TIMnotepad_api.path;
            File.WriteAllText(path, fastColoredTextBox1.Text);
            TIMnotepad_api.site = filename;
        }

        private void настройкиШрифтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            fastColoredTextBox1.Font = fontDialog1.Font;
        }

        private void настройкиФонаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            fastColoredTextBox1.BackColor = colorDialog1.Color;
        }

        private void запускСайтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            web web1 = new web();
            web1.Show();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = TIMnotepad_api.path;
            if (open_path != "")
            {
                File.WriteAllText(open_path, fastColoredTextBox1.Text);
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void fastColoredTextBox1_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            //Количество символов
            string textr = fastColoredTextBox1.Text;
            int len = textr.Length;
            label2.Text = len.ToString();
            //Количество строк
            string[] k = fastColoredTextBox1.Text.Split('\n');
            int n = k.Length;
            label4.Text = n.ToString();
            TIMnotepad_api.liveViewer = fastColoredTextBox1.Text;
        }
        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (toolStripTextBox1.Text.Length == 7)
            {
                try
                {
                    string hexColor = toolStripTextBox1.Text;
                    Color myColor = ColorTranslator.FromHtml(hexColor);
                    panel2.BackColor = myColor;
                    hexc = hexColor;
                }
                catch (Exception) 
                {
                    toolStripTextBox1.Text = "Ошибка";
                }
            }
        }

        private void toolStripTextBox2_TextChanged(object sender, EventArgs e)
        {
            string[] rgb_test = toolStripTextBox2.Text.Split(',');
            if (rgb_test.Length == 3 && rgb_test[2] != "")
            {
                try
                {
                    string[] rgb = toolStripTextBox2.Text.Split(',');
                    panel3.BackColor = Color.FromArgb(Convert.ToInt32(rgb[0]), Convert.ToInt32(rgb[1]), Convert.ToInt32(rgb[2]));
                    rgbc = toolStripTextBox2.Text;
                }
                catch (Exception)
                {
                    toolStripTextBox2.Text = "Ошибка";
                }
            }
        }
        private void panel2_Click(object sender, EventArgs e)
        {
            sp.Play();
            Clipboard.Clear();
            Clipboard.SetText(hexc);
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            sp.Play();
            Clipboard.Clear();
            Clipboard.SetText(rgbc);
        }

        private void panel2_DoubleClick(object sender, EventArgs e)
        {
            sp.Play();
            Clipboard.Clear();
            Clipboard.SetText(hexc.Replace("#","").Trim());
        }

        private void panel3_DoubleClick(object sender, EventArgs e)
        {
            sp.Play();
            Clipboard.Clear();
            Clipboard.SetText("rgb(" + rgbc + ")");
        }
        private void toolStripTextBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                panel4.BackColor = Color.FromName(toolStripTextBox3.Text);
                htmlc = toolStripTextBox3.Text;
            }
            catch (Exception)
            {
                
            }
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            sp.Play();
            Clipboard.Clear();
            Clipboard.SetText(htmlc);
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "HTML")
            {
                if (fastColoredTextBox1.Text == "")
                {
                    saveFileDialog1.Filter = "Веб-старница (*.html)|*.html";
                    fastColoredTextBox1.Language = FastColoredTextBoxNS.Language.HTML;
                    fastColoredTextBox1.Text = File.ReadAllText(@"files/html.t$");
                }
                else
                {
                    saveFileDialog1.Filter = "Веб-старница (*.html)|*.html";
                    fastColoredTextBox1.Language = FastColoredTextBoxNS.Language.HTML;
                }
            }
            else if (comboBox1.Text == "JavaScript")
            {
                saveFileDialog1.Filter = "JavaScript file(*.js)|*.js";
                fastColoredTextBox1.Language = FastColoredTextBoxNS.Language.JS;
            }
            else if (comboBox1.Text == "CSS")
            {
                saveFileDialog1.Filter = "CSS file(*.css)|*.css";
            }
            else if (comboBox1.Text == "JSON")
            {
                saveFileDialog1.Filter = "JSON file(*.json)|*.json";
                fastColoredTextBox1.Language = FastColoredTextBoxNS.Language.JSON;
            }
            else if (comboBox1.Text == "PHP")
            {
                saveFileDialog1.Filter = "PHP file(*.php)|*.php";
                fastColoredTextBox1.Language = FastColoredTextBoxNS.Language.PHP;
            }
        }
        private void выделитьВсёToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fastColoredTextBox1.TextLength > 0)
            {
                fastColoredTextBox1.SelectAll();
            }
        }

        private void копироватьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (fastColoredTextBox1.TextLength > 0)
            {
                fastColoredTextBox1.Copy();
            }
        }

        private void вставитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (fastColoredTextBox1.TextLength > 0)
            {
                fastColoredTextBox1.Paste();
            }
        }

        private void вырезатьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (fastColoredTextBox1.TextLength > 0)
            {
                fastColoredTextBox1.Cut();
            }
        }

        private void toolStripComboBox1_TextChanged(object sender, EventArgs e)
        {
            if (toolStripComboBox1.Text == "Светлая")
            {
                Color myColor = ColorTranslator.FromHtml("#FFFFFF");
                fastColoredTextBox1.BackColor = myColor;
                Color myColor2 = ColorTranslator.FromHtml("#000000");
                Color myColor3 = ColorTranslator.FromHtml("#F0F0F0");
                fastColoredTextBox1.ForeColor = myColor2;
                fastColoredTextBox1.ServiceLinesColor = ColorTranslator.FromHtml("#C0C0C0");
                fastColoredTextBox1.LineNumberColor = ColorTranslator.FromHtml("#0078D7");
                fastColoredTextBox1.IndentBackColor = ColorTranslator.FromHtml("#F5F5F5");
                menuStrip1.BackColor = ColorTranslator.FromHtml("#F8F8F8");
                menuStrip1.ForeColor = myColor2;
                panel1.BackColor = ColorTranslator.FromHtml("#F0F0F0");
                label1.ForeColor = myColor2;
                label2.ForeColor = myColor2;
                label3.ForeColor = myColor2;
                label4.ForeColor = myColor2;
                panel2.BackColor = myColor3;
                panel3.BackColor = myColor3;
                panel4.BackColor = myColor3;
            }
            else if (toolStripComboBox1.Text == "Тёмная")
            {
                Color myColor = ColorTranslator.FromHtml("#1E1E1E");
                fastColoredTextBox1.BackColor = myColor;
                Color myColor2 = ColorTranslator.FromHtml("#B4B4B4");
                Color myColor3 = ColorTranslator.FromHtml("#2F2F32");
                fastColoredTextBox1.ForeColor = myColor2;
                fastColoredTextBox1.ServiceLinesColor = ColorTranslator.FromHtml("#383838");
                fastColoredTextBox1.LineNumberColor = ColorTranslator.FromHtml("#0078D7");
                fastColoredTextBox1.IndentBackColor = ColorTranslator.FromHtml("#383838");
                menuStrip1.BackColor = ColorTranslator.FromHtml("#2D2D30");
                menuStrip1.ForeColor = myColor2;
                panel1.BackColor = ColorTranslator.FromHtml("#2D2D30");
                label1.ForeColor = myColor2;
                label2.ForeColor = myColor2;
                label3.ForeColor = myColor2;
                label4.ForeColor = myColor2;
                panel2.BackColor = myColor3;
                panel3.BackColor = myColor3;
                panel4.BackColor = myColor3;
            }
            else if (toolStripComboBox1.Text == "Облачная")
            {
                Color myColor = ColorTranslator.FromHtml("#BFCDDB");
                fastColoredTextBox1.BackColor = myColor;
                Color myColor2 = ColorTranslator.FromHtml("#FFFFFF");
                Color myColor3 = ColorTranslator.FromHtml("#466BBC");
                fastColoredTextBox1.ForeColor = ColorTranslator.FromHtml("#4375E1");
                fastColoredTextBox1.ServiceLinesColor = ColorTranslator.FromHtml("#2F529D");
                fastColoredTextBox1.LineNumberColor = ColorTranslator.FromHtml("#FFFFFF");
                fastColoredTextBox1.IndentBackColor = ColorTranslator.FromHtml("#3862BC");
                menuStrip1.BackColor = ColorTranslator.FromHtml("#3862BC");
                menuStrip1.ForeColor = ColorTranslator.FromHtml("#103B97");
                panel1.BackColor = ColorTranslator.FromHtml("#3862BC");
                label1.ForeColor = myColor2;
                label2.ForeColor = myColor2;
                label3.ForeColor = myColor2;
                label4.ForeColor = myColor2;
                panel2.BackColor = myColor3;
                panel3.BackColor = myColor3;
                panel4.BackColor = myColor3;
            }
        }

        private void mode1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string[] dfiles = Directory.GetFiles(@"files\current-project");
            foreach (string f in dfiles)
            {
                File.Delete(f);
            }
            //Application.Exit();
        }

        private void fastColoredTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode.Equals(Keys.S))
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                filename = saveFileDialog1.FileName;
                File.WriteAllText(filename, fastColoredTextBox1.Text);
                File.WriteAllText("files/site.t$", filename);
                TIMnotepad_api.site = filename;
            }
            else if (e.Control && e.KeyCode.Equals(Keys.S))
            {
                string path = TIMnotepad_api.path;
                if (open_path != "")
                {
                    File.WriteAllText(open_path, fastColoredTextBox1.Text);
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
                TIMnotepad_api.site = filename;
            }
        }

        private void открытьПроектToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] dfiles = Directory.GetFiles(@"files\current-project");
            foreach (string f in dfiles)
            {
                File.Delete(f);
            }
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add(new TreeNode("Проект"));
            treeView1.Nodes[0].Nodes.Add(new TreeNode("main.html"));

            openFileDialog2.Filter = "Tim Notepad Project(*.tnp)|*.tnp";
            if (openFileDialog2.ShowDialog() == DialogResult.Cancel)
                return;
            filename = openFileDialog2.FileName;
            Directory.CreateDirectory(@"files\open-project");
            string[] file = filename.Split('\\');
            file = file[file.Length - 1].Split('.');
            File.Copy(filename, Directory.GetCurrentDirectory() + @"\files\open-project\" + file[0] + ".zip");

            ZipFile.ExtractToDirectory(@"files\open-project\" + file[0] + ".zip", "files/open-project");
            File.Delete("files/open-project/" + file[0] + ".zip");

            if (File.Exists("files/open-project/main.html"))
            {
                try
                {
                    string[] files = Directory.GetFiles(@"files/open-project");
                    foreach (string f in files)
                    {
                        string[] fparts = f.Split('\\');
                        if (f.Contains("appversion") || f.Contains("main.html") || f.Contains("colors")){}
                        else
                        {
                            treeView1.Nodes[0].Nodes.Add(new TreeNode(fparts[fparts.Length - 1]));
                        }
                    }
                    if (File.ReadAllText("files/open-project/appversion") == TIMnotepad_api.GetVersion())  //=> version
                    {
                        fastColoredTextBox1.Text = File.ReadAllText("files/open-project/main.html");
                        string[] colors = File.ReadAllLines("files/open-project/colors");
                        toolStripTextBox1.Text = colors[0];
                        toolStripTextBox2.Text = colors[1];
                        toolStripTextBox3.Text = colors[2];
                    }
                    else
                    {
                        if (MessageBox.Show("Версия приложения не соответствует, прописанной версии в проекте! Проект может быть не совместим!") == DialogResult.OK)
                        {
                            fastColoredTextBox1.Text = File.ReadAllText("files/open-project/main.html");
                        }
                    }
                }
                catch (InvalidDataException)
                {
                    MessageBox.Show(
                        "Файл проекта повреждён, или им не является!",
                        "Ошибка!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                }
                try
                {
                    string[] files = Directory.GetFiles(@"files\open-project");
                    foreach (string f in files)
                    {
                        File.Delete(f);
                    }
                    Directory.Delete(@"files\open-project");
                }
                catch (Exception) { }
            }
            else
            {
                MessageBox.Show(
                    "Файл проекта создан в другом режиме!",
                    "Ошибка!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
            }
            TIMnotepad_api.site = @"files\current-project\main.html";
        }

        private void сохранитьПроектToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog2.Filter = "Tim Notepad Project(*.tnp)|*.tnp";
            if (saveFileDialog2.ShowDialog() == DialogResult.Cancel)
                return;
            filename = saveFileDialog2.FileName;
            Directory.CreateDirectory("files/save-project");
            File.WriteAllText(@"files\save-project\main.html", File.ReadAllText(@"files\current-project\main.html"));
            File.WriteAllText(@"files\save-project\appversion", "27121300");

            List<string> colors = new List<string>();
            if (toolStripTextBox1.Text == "")
            {
                colors.Add("#FFFFFF");
            }
            if (toolStripTextBox2.Text == "")
            {
                colors.Add("255,255,255");
            }
            if (toolStripTextBox3.Text == "")
            {
                colors.Add("white");
            }
            File.WriteAllLines(@"files\save-project\colors", colors.ToArray());

            string[] files = Directory.GetFiles(@"files\current-project");
            foreach (string f in files)
            {
                string[] fparts = f.Split('\\');
                if (f.Contains("main.html") == false)
                {
                    File.Move(f, @"files\save-project\" + fparts[fparts.Length - 1]);
                }
            }
            TIMnotepad_api.site = @"files\current-project\main.html";
            MessageBox.Show("Проект сохранён!");
            ZipFile.CreateFromDirectory(Directory.GetCurrentDirectory() + @"\files\save-project", filename + ".zip");
            string[] dfiles2 = Directory.GetFiles(@"files\save-project");
            foreach (string f in dfiles2)
            {
                File.Delete(f);
            }
            Directory.Delete(@"files\save-project");
            try
            {
                File.Move(filename + ".zip", filename);
            }
            catch (Exception)
            {
                File.Move(filename + ".zip", "files/project.tnp");
                File.Delete(filename);
                File.Move("files/project.tnp", filename);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            treeView1.Nodes[0].Nodes.Add(new TreeNode(textBox1.Text));
            File.Create("files/current-project/" + textBox1.Text);
            textBox1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Text != mainnodename)
            {
                if(treeView1.SelectedNode.Text != "main.html")
                {
                    File.Delete("files/current-project/" + treeView1.SelectedNode.Text);
                    treeView1.Nodes[0].Nodes.RemoveAt(treeView1.SelectedNode.Index);
                }
                else
                {
                    MessageBox.Show("Точку вхождения проекта удалить нельзя!");
                }
            }
            else
            {
                MessageBox.Show("Корневой католог проекта удалить нельзя!");
            }
        }

        private void файлыПроектаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.WriteAllText("files/current-project/main.html", fastColoredTextBox1.Text);
            FileMeneger.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FileMeneger.Visible = false;
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllText(@"files\current-project\" + openprojecyfile, fastColoredTextBox1.Text);
                fastColoredTextBox1.Text = File.ReadAllText("files/current-project/" + treeView1.SelectedNode.Text);
            }
            catch (IOException)
            {
                MessageBox.Show("Файл создаётся, подождите...");
                File.WriteAllText(@"files\current-project\" + openprojecyfile, fastColoredTextBox1.Text);
            }
            openprojecyfile = treeView1.SelectedNode.Text;
        }

        private void zIPФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog3.ShowDialog() == DialogResult.Cancel)
                return;

            ZipFile.CreateFromDirectory(Directory.GetCurrentDirectory() + @"\files\current-project", saveFileDialog3.FileName);
        }

        private void сохранитьТекущийФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.WriteAllText(@"files\current-project\" + openprojecyfile, fastColoredTextBox1.Text);
        }

        private void всеФайлыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string folder = folderBrowserDialog1.SelectedPath;

            string[] files = Directory.GetFiles(@"files\current-project");
            foreach (string f in files)
            {
                string[] f2 = f.Split('\\');
                File.WriteAllText(folder + "\\" + f2[f2.Length - 1], File.ReadAllText(f));
            }
        }

        private void fastColoredTextBox1_Load(object sender, EventArgs e)
        {

        }
    }
}
