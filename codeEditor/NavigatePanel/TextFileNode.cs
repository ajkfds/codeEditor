﻿using System;
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
            if (TextFileNodeCreated != null) TextFileNodeCreated(this);
        }
        public static Action<TextFileNode> TextFileNodeCreated;

        public Data.ITextFile ITextFile
        {
            get { return Project.GetRegisterdItem(ID) as Data.ITextFile; }
        }

        public override string Text
        {
            get { return FileItem.Name; }
        }

        private static ajkControls.IconImage icon = new ajkControls.IconImage(Properties.Resources.text);
        public override void DrawNode(Graphics graphics, int x, int y, Font font, Color color, Color backgroundColor, Color selectedColor, int lineHeight, bool selected)
        {
            graphics.DrawImage(icon.GetImage(lineHeight, ajkControls.IconImage.ColorStyle.White), new Point(x, y));
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
