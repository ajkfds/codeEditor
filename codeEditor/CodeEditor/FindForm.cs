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
        }
        CodeEditor codeEditor;
        bool replace = false;
        
        public void OpenFind(string initialText,Point position)
        {
            System.Diagnostics.Debug.Print(position.X.ToString() + "," + position.Y.ToString());
            replace = false;
            findTextBox.Text = initialText;
            replaceToTextBox.Enabled = false;
            doButton.Text = "Find";
            this.Left = position.X;
            this.Top = position.Y;
            Global.Controller.ShowForm(this,position);
            findTextBox.Focus();
            findTextBox.SelectAll();
        }

        public void OpenReplace(string initialText)
        {
            replace = true;
            findTextBox.Text = initialText;
            replaceToTextBox.Enabled = true;
            doButton.Text = "Replace";
            Show();
            findTextBox.Focus();
            findTextBox.SelectAll();
        }

        private void findTextBox_KeyDown(object sender, KeyEventArgs e)
        {

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
    }
}
