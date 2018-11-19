using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace codeEditor.CodeEditor
{
    public class AutocompleteItem
    {
        public AutocompleteItem(string text, byte colorIndex,Color color)
        {
            this.text = text;
            this.colorIndex = colorIndex;
            this.Color = color;
        }
        public AutocompleteItem(string text, byte colorIndex,Color color,ajkControls.Icon icon,ajkControls.Icon.ColorStyle iconColorStyle)
        {
            this.text = text;
            this.colorIndex = colorIndex;
            this.Color = color;
            this.icon = icon;
            this.iconColorStyle = iconColorStyle;
        }

        private ajkControls.Icon icon = null;
        private ajkControls.Icon.ColorStyle iconColorStyle;
        
        private string text;
        public string Text { get { return text; } }

        public byte ColorIndex
        {
            get
            {
                return colorIndex;
            }
        }

        private byte colorIndex;
        private Color Color;

        public virtual void Draw(Graphics graphics, int x, int y, Font font, Color backgroundColor,out int height)
        {
            Size tsize = System.Windows.Forms.TextRenderer.MeasureText(graphics, text, font);
            if (icon != null) graphics.DrawImage(icon.GetImage(tsize.Height, iconColorStyle), new Point(x, y));
            Color bgColor = backgroundColor;
            System.Windows.Forms.TextRenderer.DrawText(
                graphics,
                text,
                font,
                new Point(x + tsize.Height + (tsize.Height >> 2), y),
                Color,
                bgColor,
                System.Windows.Forms.TextFormatFlags.NoPadding
                );
            height = tsize.Height;
        }

        public virtual void Apply(CodeDocument codeDocument,System.Windows.Forms.Keys keyCode)
        {
            int prevIndex = codeDocument.CaretIndex;
            if (codeDocument.GetLineStartIndex(codeDocument.GetLineAt(prevIndex)) != prevIndex && prevIndex != 0)
            {
                prevIndex--;
            }
            int headIndex, length;
            codeDocument.GetWord(prevIndex, out headIndex, out length);
            codeDocument.Replace(headIndex, length, ColorIndex, Text);
            codeDocument.CaretIndex = headIndex + Text.Length;
            codeDocument.SelectionStart = headIndex + Text.Length;
            codeDocument.SelectionLast = headIndex + Text.Length;
        }

    }
}
