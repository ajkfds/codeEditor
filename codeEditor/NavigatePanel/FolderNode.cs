using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace codeEditor.NavigatePanel
{
    public class FolderNode : NavigatePanelNode
    {
        protected FolderNode() { }

        public FolderNode(Data.Folder folder) : base(folder)
        {
            if (FolderNodeCreated != null) FolderNodeCreated(this);
        }
        public static Action<FolderNode> FolderNodeCreated;

        private System.WeakReference<Data.Folder> folderRef;
        public virtual Data.Folder Folder
        {
            get
            {
                return Item as Data.Folder;
            }
        }

        public override string Text
        {
            get { return Folder.Name; }
        }

        public override void Selected()
        {
            codeEditor.Controller.NavigatePanel.GetContextMenuStrip().Items["openWithExploererTsmi"].Visible = true;
            codeEditor.Controller.NavigatePanel.GetContextMenuStrip().Items["ignoreTsmi"].Visible = true;
        }

        public override void Update()
        {
            Folder.Update();

            List<Data.Item> currentItems = new List<Data.Item>();
            foreach (Data.Item item in Folder.Items.Values)
            {
                currentItems.Add(item);
            }

            List<NavigatePanelNode> removeNodes = new List<NavigatePanelNode>();
            foreach (NavigatePanelNode node in TreeNodes)
            {
                if (currentItems.Contains(node.Item))
                {
                    currentItems.Remove(node.Item);
                }
                else
                {
                    removeNodes.Add(node);
                }
            }

            foreach (NavigatePanelNode nodes in removeNodes)
            {
                TreeNodes.Remove(nodes);
            }

            foreach (Data.Item item in currentItems)
            {
                if (item == null) continue;
                TreeNodes.Add(item.CreateNode());
            }
        }

        private static ajkControls.IconImage openFolder = new ajkControls.IconImage(Properties.Resources.openFolder);
        private static ajkControls.IconImage folder = new ajkControls.IconImage(Properties.Resources.folder);
        private static ajkControls.IconImage ignoreIcon = new ajkControls.IconImage(Properties.Resources.ignore);

        public override void DrawNode(Graphics graphics, int x, int y, Font font, Color color, Color backgroundColor, Color selectedColor, int lineHeight, bool selected)
        {
            if (Exanded)
            {
                graphics.DrawImage(openFolder.GetImage(lineHeight, ajkControls.IconImage.ColorStyle.Blue), new Point(x, y));
            }
            else
            {
                graphics.DrawImage(folder.GetImage(lineHeight, ajkControls.IconImage.ColorStyle.Blue), new Point(x, y));
            }

            if (Item.Ignore)
            {
                graphics.DrawImage(ignoreIcon.GetImage(lineHeight, ajkControls.IconImage.ColorStyle.Gray), new Point(x, y));
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
