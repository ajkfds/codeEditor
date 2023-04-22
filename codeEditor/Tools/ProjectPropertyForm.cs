using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace codeEditor.Tools
{
    public partial class ProjectPropertyForm : Form
    {
        public ProjectPropertyForm(Data.Project project)
        {
            InitializeComponent();
            this.project = project;
            this.Icon = ajkControls.Global.Icon;
            this.ShowInTaskbar = false;

            this.Text = project.Name + " Property";
            this.BackColor = Global.ColorMap.DarkBackground;
            this.ForeColor = Global.ColorMap.Foreground;

            setText();

            if (FormCreated != null) FormCreated(this, project);
        }

        private void ProjectPropertyForm_Shown(object sender, EventArgs e)
        {
            foreach(var tab in tabControl.TabPages)
            {
                TabPage page = tab as TabPage;
                page.BackColor = Global.ColorMap.DarkBackground;
                page.ForeColor = Global.ColorMap.Foreground;
            }
        }

        public static Action<ProjectPropertyForm, Data.Project> FormCreated;
        Data.Project project;

        private void setText()
        {
            projectRootPathTxt.Text = project.RootPath;
            StringBuilder sb = new StringBuilder();
            lock (project.ignoreList)
            {
                foreach (string ignore in project.ignoreList)
                {
                    sb.AppendLine(ignore);
                }
            }
            ignoreListTxt.Text = sb.ToString();
        }


        public void AppendTab(ProjectPropertyTab tab)
        {
            tabControl.TabPages.Add(tab);
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            // parse ignore lisrt
            {
                string text = ignoreListTxt.Text.Replace("\r\n", "\n");
                string[] lines = text.Split(new char[] { '\n' });

                project.ignoreList.Clear();
                foreach(string line in lines)
                {
                    if (!project.ignoreList.Contains(line)) project.ignoreList.Add(line);
                }
            }

            foreach(var tab in tabControl.TabPages)
            {
                if(tab is ProjectPropertyTab)
                {
                    (tab as ProjectPropertyTab).PropertyAccept();
                }
            }
            Close();
        }
        private void CancelBtn_Click(object sender, EventArgs e)
        {
            foreach (var tab in tabControl.TabPages)
            {
                if (tab is ProjectPropertyTab)
                {
                    (tab as ProjectPropertyTab).PropertyCancel();
                }
            }
        }

        private void mainPage_Click(object sender, EventArgs e)
        {

        }

    }
}
