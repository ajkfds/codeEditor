using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace codeEditor.Shell
{
    public partial class CmdShellPanel : UserControl
    {
        public CmdShellPanel()
        {
            InitializeComponent();
            shell = new CmdInteractiveShell();
            shell.LineReceived += shell_LineReceived;

            shell.Start();
        }

        public new void Dispose()
        {
            shell.Dispose();
            base.Dispose();
        }

        CmdInteractiveShell shell = null;

        private void shell_LineReceived(string lineString)
        {
            logView.AppendLogLine(lineString);
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Return) return;
            string command = textBox.Text;
            textBox.Text = "";
            textBox.Refresh();

            shell.Execute(command);
            e.Handled = true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {

        }
    }
}
