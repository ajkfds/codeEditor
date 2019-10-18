using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace codeEditor.Snippets
{
    public class InteractiveSnippet : CodeEditor.ToolItem
    {
        public InteractiveSnippet(string text) : base(text)
        {

        }

        public override void Apply(CodeEditor.CodeDocument codeDocument)
        {
            document = codeDocument;
            Controller.CodeEditor.startInteractiveSnippet(this);
            Controller.CodeEditor.Refresh();
        }
        CodeEditor.CodeDocument document;
         
        public virtual void Aborted()
        {
            document = null;
        }
        public virtual void Cancel()
        {
        }

        public virtual void BeforeKeyDown(object sender, KeyEventArgs e, codeEditor.CodeEditor.AutoCompleteForm  autoCompleteForm)
        {

        }
        public virtual void AfterAutoCompleteHandled(object sender, KeyEventArgs e, codeEditor.CodeEditor.AutoCompleteForm autoCompleteForm)
        {

        }
        public virtual void AfterKeyDown(object sender, KeyEventArgs e, codeEditor.CodeEditor.AutoCompleteForm autoCompleteForm)
        {

        }


    }
}
