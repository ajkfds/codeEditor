using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeEditor.CodeEditor
{
    public class CodeDocument : ajkControls.CodeTextbox.Document
    {
        public CodeDocument(Data.TextFile textFile,bool textOnly) : base(textOnly)
        {

        }
        public CodeDocument(Data.TextFile textFile) 
        {
            textFileRef = new WeakReference<Data.TextFile>(textFile);
        }
        public CodeDocument(Data.TextFile textFile,string text) : base(text)
        {
            textFileRef = new WeakReference<Data.TextFile>(textFile);
        }

        public System.WeakReference<Data.TextFile> textFileRef;
        public Data.TextFile TextFile
        {
            get
            {
                Data.TextFile ret;
                if (textFileRef == null) return null;
                if (!textFileRef.TryGetTarget(out ret)) return null;
                return ret;
            }
        }

        private ulong version = 0;
        public override ulong Version
        {
            get
            {
                return version;
            }
            set
            {
                version = value;
                Data.TextFile file = TextFile;
            }
        }

        public override void Dispose()
        {
            if(Global.mainForm.editorPage.CodeEditor.codeTextbox.Document == this)
            {
                Global.mainForm.editorPage.CodeEditor.codeTextbox.Document = null;
            }
            base.Dispose();
        }

    }
}
