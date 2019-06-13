using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeEditor.Controller
{
    public class MainTabPage : ajkControls.TabPage
    {
        public MainTabPage(System.Windows.Forms.UserControl userControl,string name)
        {
            Text = name;
            CloseButtonEnable = true;

            panel = userControl;
            panel.Dock = System.Windows.Forms.DockStyle.Fill;
            Controls.Add(panel);
        }

        protected System.Windows.Forms.UserControl panel;

        public override void CloseButtonClicked()
        {
            codeEditor.Global.Controller.Tabs.RemovePage(this);
            panel.Dispose();
            Dispose();
        }


    }
}
