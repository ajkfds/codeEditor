using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeEditor.CodeEditor
{
    public class CodeDocument : ajkControls.CodeTextbox.Document
    {
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

    }
}
