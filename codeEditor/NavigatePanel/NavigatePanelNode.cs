using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeEditor.NavigatePanel
{
    public class NavigatePanelNode : ajkControls.TreeNode
    {
        protected NavigatePanelNode() { }
        public NavigatePanelNode(string id,Data.Project project)
        {
            this.Project = project;
            this.ID = id;
        }

        public Data.Project Project { get; protected set; }
        public string ID { get; protected set; }

        /// <summary>
        /// update this node and children
        /// </summary>
        public virtual void Update()
        {
        }

        /// <summary>
        /// update all nodes under this node
        /// </summary>
        //public virtual void HierarchicalUpdate()
        //{
        //    HierarchicalUpdate(0);
        //}

        //public virtual void HierarchicalUpdate(int depth)
        //{
        //    Update();
        //    if (depth > 100) return;
        //    foreach (NavigatePanelNode node in TreeNodes)
        //    {
        //        node.HierarchicalUpdate(depth+1);
        //    }
        //}

        public virtual void HierarchicalVisibleUpdate()
        {
            HierarchicalVisibleUpdate(0,Exanded);
        }

        public virtual void HierarchicalVisibleUpdate(int depth,bool expanded)
        {
            Update();
            if (depth > 100) return;
            if (!expanded) return;
            foreach (NavigatePanelNode node in TreeNodes)
            {
                node.HierarchicalVisibleUpdate(depth + 1,node.Exanded);
            }
        }

        public virtual void Selected()
        {
            
        }

        public virtual void Clicked()
        {

        }

        public virtual void DoubleClicked()
        {

        }

        public override void DrawNode(Graphics graphics, int x, int y, Font font, Color color, Color backgroundColor, Color selectedColor, int lineHeight, bool selected)
        {
            base.DrawNode(graphics, x, y, font, color, backgroundColor, selectedColor, lineHeight, selected);
        }

        public virtual void ShowProperyForm()
        {

        }
    }
}
