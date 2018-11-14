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
        }

        public void SetItems(List<PopupItem> items)
        {
            this.items = items;
        }

        List<PopupItem> items = new List<PopupItem>();
        public override System.Drawing.Font Font
        {
            set
            {
                base.Font = value;
            }
        }

        private void treeView_Load(object sender, EventArgs e)
        {

        }


        int lineHeight = 0;
        private void doubleBufferedDrawBox_DoubleBufferedPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            int width = 0;
            int y = 0;
            foreach(PopupItem item in items)
            {
                Size size;
                item.Draw(e.Graphics, 0, y, Font, BackColor,out size);
                y = y + size.Height;
                if (width < size.Width) width = size.Width;
            }
            Height = y;
            Width = width;
        }
    }
}
