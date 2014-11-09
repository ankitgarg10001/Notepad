using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Notepad_Ankit
{
    public partial class Notepad_Ankit : Form
    {
        public Notepad_Ankit()
        {
            InitializeComponent();
            
        }
        bool saved = true;
        string s = null;

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            saved = false;
            toolStripStatusLabel1.Text = "Lines : " + richTextBox1.Lines.Length + "   Columns : " + richTextBox1.Text.Length;
        }


        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!saved)
            {
                DialogResult oo = MessageBox.Show("Do you want to save changes to File "+s+" ?", "Notepad_Ankit", MessageBoxButtons.YesNo);
                if (oo == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(sender, e);
                    richTextBox1.Clear();
                    s = null;


                }
                else if (oo == DialogResult.No)
                {
                    richTextBox1.Clear();

                }
                //How to apply Cancel here
            }
            else
            {
                richTextBox1.Clear();
            }
        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog x = new SaveFileDialog();
            if (s == null)
            {
                x.DefaultExt = ".txt";
                x.FileName = "Untitled";
                if (x.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(x.FileName, richTextBox1.Text);
                    s = x.FileName;
                    saved = true;
                }
            }
            else
            {
                File.WriteAllText(s, richTextBox1.Text);
                saved = true;
            }

            
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            s = null;
            saveToolStripMenuItem_Click(sender, e);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            if (o.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Text = File.ReadAllText(o.FileName);
                s = o.FileName;
                saved = true;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!saved)
            {
                DialogResult oo = MessageBox.Show("Do you want to save changes to File " + s + " ?", "Notepad_Ankit", MessageBoxButtons.YesNo);
                if (oo == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(sender, e);


                }
            }
            Application.Exit();
        }

        Stack <string> k=new Stack<string>();
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)//have to work on it so that it doesent take whole document
        {
            if (richTextBox1.CanUndo == true)
            {
                k.Push(richTextBox1.Text);
                richTextBox1.Undo();
            }
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(richTextBox1.CanRedo==true)
            {
                if (k.Count > 0)
                {
                    richTextBox1.Text = k.Pop();
                }

            }

        }
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //temp=richTextBox1.SelectedText;
            //richTextBox1.SelectedText = null;
            string s = null;
            Clipboard.SetDataObject(richTextBox1.SelectedText, true);
           // richTextBox1.SelectedText.Remove(0,richTextBox1.SelectedText.Length-1);

            SendKeys.Send("{DELETE}");

        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(richTextBox1.SelectedText,true);

        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
            richTextBox1.AppendText(Clipboard.GetDataObject().GetData(DataFormats.Text).ToString()) ;

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{DELETE}");
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void timeDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.AppendText(DateTime.Now.ToString());
        }

        private void worldWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            worldWrapToolStripMenuItem.Checked = !worldWrapToolStripMenuItem.Checked;
            richTextBox1.WordWrap=!richTextBox1.WordWrap;

        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog f = new FontDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Font = f.Font;
            }
        }

        private void colourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog c = new ColorDialog();
            if (c.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.ForeColor = c.Color;
            }
        }

        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            st.Visible = !st.Visible;
            statusBarToolStripMenuItem.Checked = st.Visible;
            richTextBox1.Dock = DockStyle.Fill;
        }

        private void Notepad_Ankit_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Lines : " + richTextBox1.Lines.Length + "   Columns : " + richTextBox1.Text.Length;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Notepad By Ankit.. :D\n version 1.0.0", "ABOUT", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            find f = new find();
            f.Show();
        }

        
    }
}

