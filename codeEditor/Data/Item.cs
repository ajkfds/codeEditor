using codeEditor.NavigatePanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeEditor.Data
{
    public class Item : IDisposable
    {
        protected Item() { }

        private System.WeakReference<Data.Item> parent;
        public Data.Item Parent
        {
            get
            {
                Data.Item ret;
                if (parent == null) return null;
                if (!parent.TryGetTarget(out ret)) return null;
                return ret;
            }
            set
            {
                parent = new WeakReference<Data.Item>(value);
            }
        }

        public virtual string ID
        {
            get
            {
                return RelativePath;
            }
        }

        public virtual bool Ignore
        {
            get; set;
        }
        public virtual string RelativePath { get; protected set; }

        public virtual string Name { get; protected set; }
        public virtual Project Project { get; protected set; }

        protected Dictionary<string, Item> items = new Dictionary<string, Item>();
        public virtual IReadOnlyDictionary<string, Item> Items
        {
            get { return items; }
        }

        public virtual Item GetItem(string relativePath)
        {
            // hier search to get a item
            if (Name == relativePath) return this;

            if (relativePath.Contains(@"\"))
            {
                string folderName = relativePath.Substring(0, relativePath.IndexOf(@"\"));
                if (items.ContainsKey(folderName))
                {
                    return items[folderName].GetItem(relativePath.Substring(folderName.Length));
                }
                else
                {
                    return null;
                }
            }
            else
            {
                if (items.ContainsKey(relativePath))
                {
                    return items[relativePath];
                }
                else
                {
                    return null;
                }
            }
        }

        public virtual  List<Item> FindItems(Func<Item,bool> match,Func<Item,bool> stop)
        {
            List<Item> result = new List<Item>();
            findItems(result, match, stop);
            return result;
        }

        protected void findItems(List<Item> result,Func<Item, bool> match, Func<Item, bool> stop)
        {
            foreach (Item item in items.Values)
            {
                if (match(item)) result.Add(item);
                if (!stop(item)) item.findItems(result, match, stop);
            }
        }


        public virtual void Dispose()
        {
        }

        public virtual void Update() { }


        protected WeakReference<NavigatePanel.NavigatePanelNode> nodeRef;
        public virtual NavigatePanel.NavigatePanelNode NavigatePanelNode
        {
            get
            {
                NavigatePanel.NavigatePanelNode node;
                if(nodeRef == null)
                {
                    node = createNode();
                    if (node == null) return null;
                    nodeRef = new WeakReference<NavigatePanelNode>(node);
                    return node;
                }

                if (nodeRef.TryGetTarget(out node)) return node;

                node = createNode();
                if (node == null) return null;
                nodeRef = new WeakReference<NavigatePanelNode>(node);
                return node;
            }
            protected set
            {
                nodeRef = new WeakReference<NavigatePanelNode>(value);
            }
        }
        protected virtual NavigatePanel.NavigatePanelNode createNode()
        {
            // should set nodeRef
            System.Diagnostics.Debugger.Break();
            return NavigatePanelNode;
        }

        public virtual NavigatePanelNode CreateLinkNode()
        {
            NavigatePanelNode node;
            node = createNode();
            if(node != null) node.Link = true;
            return node;
        }

        public virtual CodeEditor.DocumentParser CreateDocumentParser(CodeEditor.DocumentParser.ParseModeEnum parseMode)
        {
            return null;
        }
    }
}
