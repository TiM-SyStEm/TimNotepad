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
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.IO.Compression;

namespace TIMnotepad
{
    public partial class mode2 : Form
    {
        static string open_path = "";
        static string filename = "";
        static string[] links = { "mscorlib.dll", "System.Core.dll", "System.dll", "System.Windows.Forms.dll", "System.Drawing.dll" };
        static string path_to_dll = "";
        static string[] reservlist = {};
        public mode2()
        {
            InitializeComponent();
            #region Настройки
            panel2.Visible = false;
            fastColoredTextBox1.Text = File.ReadAllText(@"files/csharp.t$");
            richTextBox2.Text = File.ReadAllText(@"files/dictionaries/cs-reserv-list.dicr");
            #endregion
            #region Автодополнение
            reservlist = File.ReadAllText("files/dictionaries/cs-reserv-list.dicr").Split('\n');
            autocompleteMenu1.Items = reservlist;
            autocompleteMenu1.Colors = new AutocompleteMenuNS.Colors();
            #endregion
        }
        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "CSharp файлы исходного кода (*.cs)|*.cs";
            filename = saveFileDialog1.FileName;
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            File.WriteAllText(filename, fastColoredTextBox1.Text);

        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "CSharp файлы исходного кода (*.cs)|*.cs|Tim Notepad File (*.tnf)|*.tnf";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            filename = openFileDialog1.FileName;
            string text = File.ReadAllText(filename);
            fastColoredTextBox1.Text = text;
            TIMnotepad_api.path = filename;
            open_path = filename;
            string path = TIMnotepad_api.path;
            File.WriteAllText(path, fastColoredTextBox1.Text);
        }

        private void настройкиШрифтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            fastColoredTextBox1.Font = fontDialog1.Font;
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
                    "Файл не сохранён",
                    "Ошибка!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
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
        }

        private void запускПриложенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            for (int i = 0; i < links.Length; i++) 
            {
                if (links[i].Contains("\\"))
                {
                    File.Copy(links[i], Directory.GetCurrentDirectory());
                    gen_links = gen_links + "\n";
                }
            }*/
            if (File.ReadAllText(@"files/run-app-file.cs") != fastColoredTextBox1.Text)
            {
                File.WriteAllText(@"run-app-file.cs", fastColoredTextBox1.Text);
            }
            string sourceName = @"run-app-file.cs";
            FileInfo sourceFile = new FileInfo(sourceName);
            CodeDomProvider provider = null;
            bool compileOk = false;

            // Select the code provider based on the input file extension.
            provider = CodeDomProvider.CreateProvider("CSharp");

            if (provider != null)
            {

                // Format the executable file name.
                // Build the output assembly path using the current directory
                // and <source>_cs.exe or <source>_vb.exe.

                String exeName = String.Format(@"{0}\{1}.exe",
                    System.Environment.CurrentDirectory,
                    sourceFile.Name.Replace(".", "_"));

                CompilerParameters cp = new CompilerParameters(links, fastColoredTextBox1.Text, true);

                // Generate an executable instead of
                // a class library.
                cp.GenerateExecutable = true;

                // Specify the assembly file name to generate.
                cp.OutputAssembly = exeName;

                // Save the assembly as a physical file.
                cp.GenerateInMemory = false;

                // Set whether to treat all warnings as errors.
                cp.TreatWarningsAsErrors = false;

                // Invoke compilation of the source file.
                CompilerResults cr = provider.CompileAssemblyFromFile(cp,
                    sourceName);

                if (cr.Errors.Count > 0)
                {
                    foreach (CompilerError ce in cr.Errors)
                    {
                        MessageBox.Show(ce.ToString());
                    }
                }

                // Return the results of the compilation.
                if (cr.Errors.Count > 0)
                {
                    MessageBox.Show("Приложение не запущено!");
                }
                else
                {
                    MessageBox.Show("Приложение запущено");
                    Process.Start(@"run-app-file_cs.exe");
                }
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

        private void выделитьВсёToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (fastColoredTextBox1.TextLength > 0)
            {
                fastColoredTextBox1.SelectAll();
            }
        }
        private void toolStripComboBox1_TextChanged(object sender, EventArgs e)
        {
            if (toolStripComboBox1.Text == "Светлая")
            {
                Color myColor = ColorTranslator.FromHtml("#FFFFFF");
                fastColoredTextBox1.BackColor = myColor;
                Color myColor2 = ColorTranslator.FromHtml("#000000");
                fastColoredTextBox1.ForeColor = myColor2;
                fastColoredTextBox1.ServiceLinesColor = ColorTranslator.FromHtml("#C0C0C0");
                fastColoredTextBox1.IndentBackColor = ColorTranslator.FromHtml("#F5F5F5");
                menuStrip1.BackColor = ColorTranslator.FromHtml("#F8F8F8");
                menuStrip1.ForeColor = myColor2;
                panel1.BackColor = ColorTranslator.FromHtml("#F0F0F0");
                label1.ForeColor = myColor2;
                label2.ForeColor = myColor2;
                label3.ForeColor = myColor2;
                label4.ForeColor = myColor2;
                //------
                panel2.BackColor = ColorTranslator.FromHtml("#F0F0F0");
                richTextBox1.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                richTextBox1.ForeColor = myColor2;
                button1.ForeColor = myColor2;
                button1.FlatAppearance.BorderColor = myColor2;
                button2.ForeColor = myColor2;
                button2.FlatAppearance.BorderColor = myColor2;
                //------
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
                fastColoredTextBox1.BackColor = myColor;
                Color myColor2 = ColorTranslator.FromHtml("#B4B4B4");
                fastColoredTextBox1.ForeColor = myColor2;
                fastColoredTextBox1.ServiceLinesColor = ColorTranslator.FromHtml("#383838");
                fastColoredTextBox1.IndentBackColor = ColorTranslator.FromHtml("#383838");
                menuStrip1.BackColor = ColorTranslator.FromHtml("#2D2D30");
                menuStrip1.ForeColor = myColor2;
                panel1.BackColor = ColorTranslator.FromHtml("#2D2D30");
                label1.ForeColor = myColor2;
                label2.ForeColor = myColor2;
                label3.ForeColor = myColor2;
                label4.ForeColor = myColor2;
                //----
                panel2.BackColor = ColorTranslator.FromHtml("#151515");
                richTextBox1.BackColor = ColorTranslator.FromHtml("#1E1E1E");
                richTextBox1.ForeColor = myColor2;
                button1.ForeColor = myColor2;
                button1.FlatAppearance.BorderColor = myColor2;
                button2.ForeColor = myColor2;
                button2.FlatAppearance.BorderColor = myColor2;
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
                //----
                panel2.BackColor = ColorTranslator.FromHtml("#3862BC");
                richTextBox1.BackColor = ColorTranslator.FromHtml("#3862BC");
                richTextBox1.ForeColor = myColor2;
                button1.ForeColor = myColor2;
                button1.FlatAppearance.BorderColor = myColor2;
                button2.ForeColor = myColor2;
                button2.FlatAppearance.BorderColor = myColor2;
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }
        private void fastColoredTextBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                fastColoredTextBox1.ContextMenuStrip = contextMenuStrip1;
            }
        }

        private void скомпилироватьПроектToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Windows приложение (*.exe)|*.exe";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            if (File.ReadAllText(@"files/run-app-file.cs") != fastColoredTextBox1.Text)
            {
                File.WriteAllText(@"run-app-file.cs", fastColoredTextBox1.Text);
            }
          
            filename = saveFileDialog1.FileName;
            string sourceName = @"run-app-file.cs";
            FileInfo sourceFile = new FileInfo(sourceName);
            CodeDomProvider provider = null;
            bool compileOk = false;

            // Select the code provider based on the input file extension.
            provider = CodeDomProvider.CreateProvider("CSharp");

            if (provider != null)
            {

                // Format the executable file name.
                // Build the output assembly path using the current directory
                // and <source>_cs.exe or <source>_vb.exe.

                string exeName = filename;

                CompilerParameters cp = new CompilerParameters(links, fastColoredTextBox1.Text, true);

                // Generate an executable instead of
                // a class library.
                cp.GenerateExecutable = true;

                // Specify the assembly file name to generate.
                cp.OutputAssembly = exeName;

                // Save the assembly as a physical file.
                cp.GenerateInMemory = false;

                // Set whether to treat all warnings as errors.
                cp.TreatWarningsAsErrors = false;

                // Invoke compilation of the source file.
                CompilerResults cr = provider.CompileAssemblyFromFile(cp,
                    sourceName);

                if (cr.Errors.Count > 0)
                {
                    foreach (CompilerError ce in cr.Errors)
                    {
                        MessageBox.Show(ce.ToString());
                    }
                }

                // Return the results of the compilation.
                if (cr.Errors.Count > 0)
                {
                    MessageBox.Show("Приложение не скомпилировано!");
                }
                else
                {
                    MessageBox.Show("Приложение скомпилировано");
                }
            }
        }

        private void добавитьСсылкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel2.Visible = true;
            for (int i = 0; i < links.Length; i++)
            {
                if (richTextBox1.Text != "")
                {
                    richTextBox1.Text = richTextBox1.Text + "\n" + links[i];
                }
                else
                {
                    richTextBox1.Text = links[i].Trim();
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Dll библиотеки (*.dll)|*.dll";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            path_to_dll = openFileDialog1.FileName;
            richTextBox1.Text = richTextBox1.Text + "\n" + "\"" + path_to_dll + "\"";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            links = richTextBox1.Text.Replace("\"","").Trim().Split('\n');
            panel2.Visible = false;
        }

        private void mode2_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Application.Exit();
        }

        private void fastColoredTextBox1_TextChanged_1(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            //Количество символов
            string textr = fastColoredTextBox1.Text;
            int len = textr.Length;
            label2.Text = len.ToString();
            //Количество строк
            string[] k = fastColoredTextBox1.Text.Split('\n');
            int n = k.Length;
            label4.Text = n.ToString();
        }

        private void mode2_SizeChanged(object sender, EventArgs e)
        {
        }

        private void mode2_Resize(object sender, EventArgs e)
        {
            panel3.Location = new Point((Width / 2) - (537 / 2), (Height / 2) - (424 / 2));
            panel2.Location = new Point((Width / 2) - (533 / 2), (Height / 2) - (483 / 2));
        }

        private void открытьРедакторСловаряToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            File.WriteAllText(@"files/dictionaries/cs-reserv-list.dicr", richTextBox2.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string newsnippet = textBox1.Text;
            textBox1.Text = "";
            richTextBox2.Text = richTextBox2.Text + "\n" + newsnippet;
            File.WriteAllText(@"files/dictionaries/cs-reserv-list.dicr", richTextBox2.Text);
            reservlist = File.ReadAllText("files/dictionaries/cs-reserv-list.dicr").Split('\n');
            autocompleteMenu1.Items = reservlist;
        }

        private void fastColoredTextBox1_Load(object sender, EventArgs e)
        {

        }

        private void fastColoredTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                if (File.ReadAllText(@"files/run-app-file.cs") != fastColoredTextBox1.Text)
                {
                    File.WriteAllText(@"run-app-file.cs", fastColoredTextBox1.Text);
                }
                string sourceName = @"run-app-file.cs";
                FileInfo sourceFile = new FileInfo(sourceName);
                CodeDomProvider provider = null;
                bool compileOk = false;

                // Select the code provider based on the input file extension.
                provider = CodeDomProvider.CreateProvider("CSharp");

                if (provider != null)
                {

                    // Format the executable file name.
                    // Build the output assembly path using the current directory
                    // and <source>_cs.exe or <source>_vb.exe.

                    String exeName = String.Format(@"{0}\{1}.exe",
                        System.Environment.CurrentDirectory,
                        sourceFile.Name.Replace(".", "_"));

                    CompilerParameters cp = new CompilerParameters(links, fastColoredTextBox1.Text, true);

                    // Generate an executable instead of
                    // a class library.
                    cp.GenerateExecutable = true;

                    // Specify the assembly file name to generate.
                    cp.OutputAssembly = exeName;

                    // Save the assembly as a physical file.
                    cp.GenerateInMemory = false;

                    // Set whether to treat all warnings as errors.
                    cp.TreatWarningsAsErrors = false;

                    // Invoke compilation of the source file.
                    CompilerResults cr = provider.CompileAssemblyFromFile(cp,
                        sourceName);

                    if (cr.Errors.Count > 0)
                    {
                        foreach (CompilerError ce in cr.Errors)
                        {
                            MessageBox.Show(ce.ToString());
                        }
                    }

                    // Return the results of the compilation.
                    if (cr.Errors.Count > 0)
                    {
                        MessageBox.Show("Приложение не запущено!");
                    }
                    else
                    {
                        MessageBox.Show("Приложение запущено");
                        Process.Start(@"run-app-file_cs.exe");
                    }
                }
            }
            else if (e.KeyCode == Keys.F6)
            {
                saveFileDialog1.Filter = "Windows приложение (*.exe)|*.exe";
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                if (File.ReadAllText(@"files/run-app-file.cs") != fastColoredTextBox1.Text)
                {
                    File.WriteAllText(@"run-app-file.cs", fastColoredTextBox1.Text);
                }

                filename = saveFileDialog1.FileName;
                string sourceName = @"run-app-file.cs";
                FileInfo sourceFile = new FileInfo(sourceName);
                CodeDomProvider provider = null;
                bool compileOk = false;

                // Select the code provider based on the input file extension.
                provider = CodeDomProvider.CreateProvider("CSharp");

                if (provider != null)
                {

                    // Format the executable file name.
                    // Build the output assembly path using the current directory
                    // and <source>_cs.exe or <source>_vb.exe.

                    string exeName = filename;

                    CompilerParameters cp = new CompilerParameters(links, fastColoredTextBox1.Text, true);

                    // Generate an executable instead of
                    // a class library.
                    cp.GenerateExecutable = true;

                    // Specify the assembly file name to generate.
                    cp.OutputAssembly = exeName;

                    // Save the assembly as a physical file.
                    cp.GenerateInMemory = false;

                    // Set whether to treat all warnings as errors.
                    cp.TreatWarningsAsErrors = false;

                    // Invoke compilation of the source file.
                    CompilerResults cr = provider.CompileAssemblyFromFile(cp,
                        sourceName);

                    if (cr.Errors.Count > 0)
                    {
                        foreach (CompilerError ce in cr.Errors)
                        {
                            MessageBox.Show(ce.ToString());
                        }
                    }

                    // Return the results of the compilation.
                    if (cr.Errors.Count > 0)
                    {
                        MessageBox.Show("Приложение не скомпилировано!");
                    }
                    else
                    {
                        MessageBox.Show("Приложение скомпилировано");
                    }
                }
            }
            else if (e.KeyCode == Keys.F7)
            {
                panel3.Visible = false;
                panel2.Visible = true;
                for (int i = 0; i < links.Length; i++)
                {
                    if (richTextBox1.Text != "")
                    {
                        richTextBox1.Text = richTextBox1.Text + "\n" + links[i];
                    }
                    else
                    {
                        richTextBox1.Text = links[i].Trim();
                    }
                }
            }
            else if (e.KeyCode == Keys.F8)
            {
                panel2.Visible = false;
                panel3.Visible = true;
            }
            else if (e.Control && e.Shift && e.KeyCode.Equals(Keys.S))
            {
                saveFileDialog1.Filter = "CSharp файлы исходного кода (*.cs)|*.cs";
                filename = saveFileDialog1.FileName;
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                File.WriteAllText(filename, fastColoredTextBox1.Text);
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
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = richTextBox1.Text + "\n" + textBox2.Text;
        }

        private void сохранитьПроектToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog2.Filter = "Tim Notepad Project(*.tnp)|*.tnp";
            if (saveFileDialog2.ShowDialog() == DialogResult.Cancel)
                return;
            filename = saveFileDialog2.FileName;
            Directory.CreateDirectory("files/save-project");
            File.WriteAllLines(@"files\save-project\links", links);
            File.WriteAllText(@"files\save-project\main.cs", fastColoredTextBox1.Text);
            File.WriteAllText(@"files\save-project\appversion", "27121300");
            ZipFile.CreateFromDirectory(Directory.GetCurrentDirectory() + @"\files\save-project", filename + ".zip");
            File.Delete(@"files\save-project\links");
            File.Delete(@"files\save-project\main.cs");
            File.Delete(@"files\save-project\appversion");
            Directory.Delete(@"files\save-project");
            File.Move(filename + ".zip", filename);
            MessageBox.Show("Проект сохранён!");
        }

        private void открытьПроектToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists("files/open-project/main.cs"))
            {
                try
                {
                    openFileDialog3.Filter = "Tim Notepad Project(*.tnp)|*.tnp";
                    if (openFileDialog3.ShowDialog() == DialogResult.Cancel)
                        return;
                    filename = openFileDialog3.FileName;
                    Directory.CreateDirectory(@"files\open-project");
                    string[] file = filename.Split('\\');
                    file = file[file.Length - 1].Split('.');
                    File.Copy(filename, Directory.GetCurrentDirectory() + @"\files\open-project\" + file[0] + ".zip");

                    ZipFile.ExtractToDirectory(@"files\open-project\" + file[0] + ".zip", "files/open-project");
                    File.Delete("files/open-project/" + file[0] + ".zip");

                    if (File.ReadAllText("files/open-project/appversion") == TIMnotepad_api.GetVersion())  //=> version
                    {
                        fastColoredTextBox1.Text = File.ReadAllText("files/open-project/main.cs");
                        links = File.ReadAllLines(@"files\open-project\links");
                    }
                    else
                    {
                        if (MessageBox.Show("Версия приложения не соответствует, прописанной версии в проекте! Проект может быть не совместим!") == DialogResult.OK)
                        {
                            fastColoredTextBox1.Text = File.ReadAllText("files/open-project/main.cs");
                            links = File.ReadAllLines(@"files\open-project\links");
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
                    "Файл проекта, не имеет точки вхождения или создан в другом режиме!",
                    "Ошибка!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
            }
        }
    }
}
