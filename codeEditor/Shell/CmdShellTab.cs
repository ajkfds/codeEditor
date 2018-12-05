using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeEditor.Shell
{
    public class CmdShellTab : ajkControls.TabPage
    {
        public CmdShellTab()
        {
            this.Text = "Command";

            panel = new ajkControls.ShellPanel(new ajkControls.CommandShell());
            this.Controls.Add(panel);
            panel.Dock = System.Windows.Forms.DockStyle.Fill;
        }
        private ajkControls.ShellPanel panel = null;



    }
}
