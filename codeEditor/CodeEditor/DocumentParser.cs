using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeEditor.CodeEditor
{
    public class DocumentParser
    {
        public DocumentParser(CodeDocument document,string id,Data.Project project)
        {
            this.EditId = document.EditID;
            this.Project = project;
            this.ID = id;
            this.document.CopyCharsFrom(document);
            this.document.CopyLineIndexFrom(document);
        }

        public int EditId { get; protected set; }
        public Data.Project Project { get; protected set; }
        public string ID { get; protected set; }

        protected CodeDocument document = new CodeDocument();
        public CodeDocument Document
        {
            get
            {
                return document;
            }
        }

        public virtual void Parse()
        {
        }

        public virtual ParsedDocument ParsedDocument { get; protected set; }
    }
}
