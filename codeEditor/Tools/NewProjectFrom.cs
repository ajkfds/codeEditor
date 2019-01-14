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
    public partial class NewProjectFrom : Form
    {
        public NewProjectFrom(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.Icon = ajkControls.Global.Icon;
            this.ShowInTaskbar = false;
        }
        MainForm mainForm;

        private void okButton_Click(object sender, EventArgs e)
        {
            if (!System.IO.Directory.Exists(pathTextBox.Text))
            {
                warningLabel.Text = "path not found";
                return;
            }
            Data.Project project = Data.Project.Create(pathTextBox.Text);
            Global.Controller.AddProject(project);
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
