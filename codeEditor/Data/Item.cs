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
        protected Item(string id,string relativePath,string name,Project project) { }
        public virtual string ID { get; protected set; }
        public virtual string RelativePath { get; protected set; }
        public virtual string Name { get; protected set; }
        public virtual Project Project { get; protected set; }

        protected Dictionary<string, Item> items = new Dictionary<string, Item>();
        public IReadOnlyDictionary<string, Item> Items
        {
            get { return items; }
        }

        /// <summary>
        /// remove links to support item dispose with gc.
        /// </summary>
        public virtual void DisposeItem()
        {
            lock (items)
            {
                foreach(Item item in Items.Values)
                {
                    item.DisposeItem();
                }
            }

            Project.RemoveRegisteredItem(this);
        }

        public void Dispose()
        {
            DisposeItem();
        }

        public virtual void Update() { }

        public virtual NavigatePanel.NavigatePanelNode CreateNode()
        {
            System.Diagnostics.Debugger.Break();
            return null;
        }

    }
}
