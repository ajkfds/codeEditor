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

/*        public static string GetID(string relativePath,Project project)
        {
            System.Diagnostics.Debugger.Break();
            return null;
        }
*/
        public virtual string ID { get; protected set; }
        public virtual string RelativePath { get; protected set; }
        public virtual string Name { get; protected set; }
        public virtual Project Project { get; protected set; }

        protected Dictionary<string, Item> items = new Dictionary<string, Item>();
        public IReadOnlyDictionary<string, Item> Items
        {
            get => items;
        }

        public virtual void Dispose()
        {
            Project.RemoveRegisteredItem(this);
        }

        public virtual void Update() { }

        public virtual NavigatePanel.NavigatePanelNode CreateNode()
        {
            System.Diagnostics.Debugger.Break();
            return null;
        }
    }
}
