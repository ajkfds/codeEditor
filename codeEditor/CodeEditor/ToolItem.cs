using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace codeEditor.CodeEditor
{
    public class ToolItem : ajkControls.SelectionItem
    {
        public ToolItem(string text) : base(text,Color.Gray)
        {
        }

        public virtual void Apply(CodeDocument codeDocument)
        {

        }
    }
}
