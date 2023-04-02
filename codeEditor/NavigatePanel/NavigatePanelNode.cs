using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeEditor.NavigatePanel
{
    public class NavigatePanelNode : ajkControls.TreeNode,IDisposable
    {
        protected NavigatePanelNode()
        {
        }
        public NavigatePanelNode(Data.Item item)
        {
            this.itemRef = new WeakReference<Data.Item>(item);
            this.Name = item.Name;
            if (NavigatePanelNodeCreated != null) NavigatePanelNodeCreated(this);
        }

        public string Name { get; protected set; }

        private bool link = false;
        
        public bool Link
        {
            get
            {
                return link;
            }
            set
            {
                link = value;
            }
        }

        private System.WeakReference<Data.Item> itemRef;
        public Data.Item Item
        {
            get
            {
                Data.Item ret;
                if (!itemRef.TryGetTarget(out ret)) return null;
                return ret;
            }
        }

        public static Action<NavigatePanelNode> NavigatePanelNodeCreated;

        /// <summary>
        /// update this node and children
        /// </summary>
        public virtual void Update()
        {
        }

        public virtual void Dispose()
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
            if(Link) graphics.DrawImage(Global.IconImages.Link.GetImage(lineHeight, ajkControls.IconImage.ColorStyle.Blue), new Point(x, y));
            base.DrawNode(graphics, x, y, font, color, backgroundColor, selectedColor, lineHeight, selected);
        }

        public virtual void ShowProperyForm()
        {

        }


    }
}
