using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace codeEditor.CodeEditor
{
    public class PopupItem
    {
        public PopupItem() { }

        public PopupItem(string text, Color color)
        {
            this.text = text;
            this.color = color;
        }

        public PopupItem(string text, Color color, ajkControls.Icon icon, ajkControls.Icon.ColorStyle iconColorStyle)
        {
            this.text = text;
            this.color = color;
            this.icon = icon;
            this.iconColorStyle = iconColorStyle;
        }

        private string text;
        private ajkControls.Icon icon = null;
        private ajkControls.Icon.ColorStyle iconColorStyle;
        private Color color;

        public virtual void Draw(Graphics graphics, int x, int y, Font font, Color backgroundColor,out Size size)
        {
            Size tsize = System.Windows.Forms.TextRenderer.MeasureText(graphics, text, font);
            if (icon != null) graphics.DrawImage(icon.GetImage(tsize.Height, iconColorStyle), new Point(x, y));
            Color bgColor = backgroundColor;
            System.Windows.Forms.TextRenderer.DrawText(
                graphics,
                text,
                font,
                new Point(x + tsize.Height + (tsize.Height >> 2), y),
                color,
                bgColor,
                System.Windows.Forms.TextFormatFlags.NoPadding
                );
            size = new Size(tsize.Width + tsize.Height + (tsize.Height >> 2), tsize.Height);
        }
    }
}
