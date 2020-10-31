using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeEditor.Tabs
{
    public class GitPage : Tabs.MainTabPage
    {
        public GitPage(Data.Project project) : base(new ajkControls.Git.GitPanel(project.RootPath),project.Name)
        {
            IconImage = codeEditor.Global.IconImages.Git;

            gitPanel.BackColor = System.Drawing.Color.FromArgb(50, 50, 50);
            gitPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            gitPanel.Font = new System.Drawing.Font("MS UI Gothic", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            gitPanel.Location = new System.Drawing.Point(3, 3);
            gitPanel.Name = "codeEditor";
            gitPanel.Size = new System.Drawing.Size(614, 433);
            gitPanel.TabIndex = 0;
        }

        public ajkControls.Git.GitPanel gitPanel
        {
            get
            {
                return (ajkControls.Git.GitPanel)panel;
            }
        }

    }
}
