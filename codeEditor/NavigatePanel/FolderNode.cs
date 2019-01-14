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

        public FolderNode(string ID,Data.Project project) : base(ID,project)
        {
        
        }
        
        public virtual Data.Folder Folder
        {
            get { return Project.GetRegisterdItem(ID) as Data.Folder; }
        }

        public override string Text
        {
            get { return Folder.Name; }
        }


        public override void Update()
        {
            Folder.Update();

            List<string> currentDataIds = new List<string>();
            foreach (string key in Folder.Items.Keys)
            {
                currentDataIds.Add(key);
            }

            List<NavigatePanelNode> removeNodes = new List<NavigatePanelNode>();
            foreach (NavigatePanelNode node in TreeNodes)
            {
                if (currentDataIds.Contains(node.ID))
                {
                    currentDataIds.Remove(node.ID);
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

            foreach (string id in currentDataIds)
            {
                Data.Item item = Project.GetRegisterdItem(id);
                if (item == null) continue;
                TreeNodes.Add(item.CreateNode());
            }
        }

        private static ajkControls.IconImage openFolder = new ajkControls.IconImage(Properties.Resources.openFolder);
        private static ajkControls.IconImage folder = new ajkControls.IconImage(Properties.Resources.folder);

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
