using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeEditor.CodeEditor
{
    public class CodeDocument : ajkControls.Document
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

        public virtual void GetWord(int index, out int headIndex, out int length)
        {
            headIndex = index;
            length = 0;
            char ch = GetCharAt(index);
            if (ch == ' ' || ch == '\r' || ch == '\n' || ch == '\t') return;

            while (headIndex > 0)
            {
                ch = GetCharAt(headIndex);
                if (ch == ' ' || ch == '\r' || ch == '\n' || ch == '\t')
                {
                    break;
                }
                headIndex--;
            }
            headIndex++;

            while (headIndex + length < Length)
            {
                ch = GetCharAt(headIndex + length);
                if (ch == ' ' || ch == '\r' || ch == '\n' || ch == '\t')
                {
                    break;
                }
                length++;
            }
        }

    }
}
