using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeEditor.CodeEditor
{
    public class DocumentParser : IDisposable
    {
        protected DocumentParser(){}
        public DocumentParser(Data.TextFile textFile, ParseModeEnum parseMode)
        {
            this.Version = textFile.CodeDocument.Version;
            this.document = new CodeDocument(textFile);
            this.document.CopyTextOnlyFrom(textFile.CodeDocument);
/*            lock (this.document)
            {
                this.document.CopyCharsFrom(textFile.CodeDocument);
                this.document.CopyLineIndexFrom(textFile.CodeDocument);
            }
*/            this.ParseMode = parseMode;
            this.TextFile = textFile;
            this.ID =  textFile.RelativePath + "," + "version=" + Version.ToString();
        }

        public void Dispose()
        {
            document = null;
        }
        public string ID { get; protected set; }

        public ulong Version { get; protected set; }
        public Data.TextFile TextFile { get; protected set; }
        public ParseModeEnum ParseMode { get; protected set; }
        protected CodeDocument document;
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
