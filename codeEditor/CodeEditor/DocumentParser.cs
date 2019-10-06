using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeEditor.CodeEditor
{
    public class DocumentParser : IDisposable
    {
        public DocumentParser(CodeDocument document,string id,Data.Project project, ParseModeEnum parseMode)
        {
            this.EditId = document.EditID;
            this.Project = project;
            this.ID = id;
            this.document.CopyCharsFrom(document);
            this.document.CopyLineIndexFrom(document);
            this.ParseMode = parseMode;
        }

        public void Dispose()
        {
            document = null;
            Project = null;
        }

        public int EditId { get; protected set; }
        public Data.Project Project { get; protected set; }
        public string ID { get; protected set; }
        public ParseModeEnum ParseMode { get; protected set; }
        protected CodeDocument document = new CodeDocument();
        public CodeDocument Document
        {
            get
            {
                return document;
            }
        }

        public enum ParseModeEnum
        {
            LoadParse,
            BackgroundParse,
            ActivatedParse,
            EditParse,
            PostEditParse
        }

        public virtual void Parse()
        {
        }

        public virtual ParsedDocument ParsedDocument { get; protected set; }
    }
}
