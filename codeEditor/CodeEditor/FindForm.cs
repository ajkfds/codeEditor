using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace codeEditor.CodeEditor
{
    public partial class FindForm : Form
    {
        public FindForm(CodeEditor codeEditor)
        {
            InitializeComponent();
            this.codeEditor = codeEditor;
            this.ShowInTaskbar = false;
            this.BackColor = Controller.GetBackColor();
        }
        CodeEditor codeEditor;
        bool replace = false;
        
        public void OpenFind(string initialText,Point position)
        {
            replace = false;
            findTextBox.Text = initialText;
            findTextBox.Text = initialText;
            replaceToTextBox.Enabled = false;
            doButton.Text = "Find";
            this.Left = position.X;
            this.Top = position.Y;
            Controller.ShowForm(this,position);
            findTextBox.Focus();
            findTextBox.SelectAll();
        }

        public void OpenReplace(string initialText, Point position)
        {
            replace = true;
            findTextBox.Text = initialText;
            replaceToTextBox.Text = initialText;
            replaceToTextBox.Enabled = true;
            doButton.Text = "Replace";
            this.Left = position.X;
            this.Top = position.Y;
            Controller.ShowForm(this, position);
            replaceToTextBox.Focus();
            replaceToTextBox.SelectAll();
        }

        private void findTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                Close();
                return;
            }
            if (e.KeyCode != Keys.Return) return;
//            if (e.KeyData != Keys.Return) return;
            if(e.Shift == true)
            {
                doFindOrReplace(true);
            }
            else
            {
                doFindOrReplace(false);
            }
        }

        private void replaceToTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
                return;
            }
            if (e.KeyCode != Keys.Return) return;
            if (e.Shift == true)
            {
                doFindOrReplace(true);
            }
            else
            {
                doFindOrReplace(false);
            }
        }
        private void FindForm_FormClosing(object sender, FormClosingEventArgs e)
        {
//            this.Hide();
//            e.Cancel = true;
        }

        private void findButton_Click(object sender, EventArgs e)
        {
            doFindOrReplace(false);
        }


        private void doFindOrReplace(bool searchPrevious)
        {
            if (replace)
            {
                if (searchPrevious)
                {
                    codeEditor.ReplacePrevious(findTextBox.Text,replaceToTextBox.Text);
                }
                else
                {
                    codeEditor.ReplaceNext(findTextBox.Text, replaceToTextBox.Text);
                }
            }
            else
            {
                if (searchPrevious)
                {
                    codeEditor.FindPrevious(findTextBox.Text);
                }
                else
                {
                    codeEditor.FindNext(findTextBox.Text);
                }
            }
        }

        private void findTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void replaceToTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

    }
}
