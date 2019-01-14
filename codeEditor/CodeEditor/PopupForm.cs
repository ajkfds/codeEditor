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
            this.ShowInTaskbar = false;
        }

        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }

        public void SetItems(List<PopupItem> items)
        {
            this.items = items;
        }

        private List<PopupItem> items = new List<PopupItem>();
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

        private int topMargin = 4;
        private int bottomMargin = 4;
        private int leftMargin = 4;
        private int rightMargin = 4;
        private int itemGap = 2;

        private void doubleBufferedDrawBox_DoubleBufferedPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            int width = 0;
            int height = 0;
            List<int> y = new List<int>();
            foreach (PopupItem item in items)
            {
                y.Add(height);
                Size size = item.GetSize(e.Graphics, Font);
                if (size.Width > width) width = size.Width;
                height += size.Height;
                height += itemGap;
            }
            Width = width+leftMargin+rightMargin;
            Height = height+topMargin+bottomMargin;

            int i = 0;
            foreach(PopupItem item in items)
            {
                item.Draw(e.Graphics, leftMargin, y[i]+topMargin, Font, BackColor);
            }
            e.Graphics.DrawRectangle(new Pen(Color.Gray), 0, 0, Width-1, Height-1);
        }
    }
}
