using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace codeEditor.NavigatePanel
{
    public class TextFileNode : FileNode
    {
        public TextFileNode(string ID, Data.Project project) : base(ID, project)
        {

        }

        public Data.ITextFile ITextFile
        {
            get => Project.GetRegisterdItem(ID) as Data.ITextFile;
        }

        public override string Text
        {
            get => FileItem.Name;
        }

        private static ajkControls.Icon icon = new ajkControls.Icon(Properties.Resources.text);
        public override void DrawNode(Graphics graphics, int x, int y, Font font, Color color, Color backgroundColor, Color selectedColor, int lineHeight, bool selected)
        {
            graphics.DrawImage(icon.GetImage(lineHeight, ajkControls.Icon.ColorStyle.White), new Point(x, y));
            Color bgColor = backgroundColor;
            if (selected) bgColor = selectedColor;
            System.Windows.Forms.TextRenderer.DrawText(
                graphics,
                Text,
                font,
                new Point(x + lineHeight + (lineHeight>>2), y),
                color,
                bgColor,
                System.Windows.Forms.TextFormatFlags.NoPadding
                );
        }

        public override void Selected()
        {
            Global.Controller.CodeEditor.SetTextFile(ITextFile);
        }

    }
}
