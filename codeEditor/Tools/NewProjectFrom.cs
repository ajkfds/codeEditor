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
        }
        MainForm mainForm;

        private void okButton_Click(object sender, EventArgs e)
        {
            Data.Project project = Data.Project.Create(pathTextBox.Text);
            if(project == null)
            {
                warningLabel.Text = "path not found";
                return;
            }
            mainForm.AddProject(project);
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
