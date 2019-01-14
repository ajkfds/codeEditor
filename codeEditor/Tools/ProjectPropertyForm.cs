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
            this.Icon = ajkControls.Global.Icon;
            this.ShowInTaskbar = false;
        }
    }
}
