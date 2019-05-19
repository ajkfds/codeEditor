using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeEditor.Tabs
{
    public class GitPage : ajkControls.TabPage
    {
        public GitPage(Data.Project project)
        {
            gitPanel = new GitPanel.GitPanel(project);
            Controls.Add(this.gitPanel);
            Location = new System.Drawing.Point(4, 29);
            Name = "gitPage";
            Padding = new System.Windows.Forms.Padding(3);
            Size = new System.Drawing.Size(620, 439);
            TabIndex = 0;
            Text = "git";
            UseVisualStyleBackColor = true;
            IconImage = new ajkControls.IconImage(Properties.Resources.text);

            // 
            // gitPanel
            // 
            gitPanel.BackColor = System.Drawing.Color.White;
            gitPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            gitPanel.Font = new System.Drawing.Font("MS UI Gothic", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            gitPanel.Location = new System.Drawing.Point(3, 3);
            gitPanel.Name = "gitPanel";
            gitPanel.Size = new System.Drawing.Size(614, 433);
            gitPanel.TabIndex = 0;

        }

        private GitPanel.GitPanel gitPanel;
    }
}
