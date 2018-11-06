using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace codeEditor.CodeEditor
{
    public partial class PopupForm : Form
    {
        public PopupForm()
        {
            InitializeComponent();
            ajkControls.Document document = new ajkControls.Document();
            codeTextbox.Document = document;
        }

        public ajkControls.Document Document
        {
            get
            {
                return codeTextbox.Document;
            }
        }
    }
}
