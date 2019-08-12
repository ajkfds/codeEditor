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
            setText();

            if (FormCreated != null) FormCreated(this, project);
        }

        public static Action<ProjectPropertyForm, Data.Project> FormCreated;
        Data.Project project;

        private void setText()
        {
            projectRootPathTxt.Text = project.RootPath;
        }


        public void AppendTab(ProjectPropertyTab tab)
        {
            tabControl.TabPages.Add(tab);
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
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
    }
}
