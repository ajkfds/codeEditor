using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using codeEditor.Data;

namespace codeEditor.NavigatePanel
{
    public class ProjectNode : FolderNode
    {
        public ProjectNode(Data.Project project) : base("", project)
        {
        }

        public override Folder Folder
        {
            get
            {
                return Project;
            }
        }

        public override string Text
        {
            get { return Project.Name; }
        }

        private static ajkControls.IconImage openFolder = new ajkControls.IconImage(Properties.Resources.openFolder);
        private static ajkControls.IconImage folder = new ajkControls.IconImage(Properties.Resources.folder);

        public override void Selected()
        {
            Global.Controller.NavigatePanel.GetContextMenuStrip().Items["gitLogTsmi"].Visible = true;
            base.Selected();
        }
        public override void DrawNode(Graphics graphics, int x, int y, Font font, Color color, Color backgroundColor, Color selectedColor, int lineHeight, bool selected)
        {
            if (Exanded)
            {
                graphics.DrawImage(openFolder.GetImage(lineHeight, ajkControls.IconImage.ColorStyle.Red), new Point(x, y));
            }
            else
            {
                graphics.DrawImage(folder.GetImage(lineHeight, ajkControls.IconImage.ColorStyle.Red), new Point(x, y));
            }
            Color bgColor = backgroundColor;
            if (selected) bgColor = selectedColor;
            System.Windows.Forms.TextRenderer.DrawText(
                graphics,
                Text,
                font,
                new Point(x + lineHeight + (lineHeight >> 2), y),
                color,
                bgColor,
                System.Windows.Forms.TextFormatFlags.NoPadding
                );
        }

    }
}
