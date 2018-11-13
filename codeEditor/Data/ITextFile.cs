using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeEditor.Data
{
    public interface ITextFile
    {
        CodeEditor.CodeDocument CodeDocument { get; }
        CodeEditor.DocumentParser CreateDocumentParser(CodeEditor.CodeDocument document, string id, Project project);
        CodeEditor.ParsedDocument ParsedDocument { get; set; }

        // projectItem
        string ID { get; }
        string RelativePath { get; }
        string Name { get; }
        Project Project { get; }

        void Update();
        void Dispose();

        void AfterKeyPressed(System.Windows.Forms.KeyPressEventArgs e);
        void AfterKeyDown(System.Windows.Forms.KeyEventArgs e);

        List<CodeEditor.PopupItem> GetPopupItems(int EditId, int index);
    }
}
