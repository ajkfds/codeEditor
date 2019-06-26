using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeEditor.CodeEditor
{
    public class BackroungParser
    {
        public BackroungParser()
        {
        }

        public void Run()
        {
            thread = new System.Threading.Thread(new System.Threading.ThreadStart(run));
            thread.Start();
        }

        public void Terminate()
        {
            abortFlag = true;
//            thread.Abort();
        }
        private volatile bool abortFlag = false;
        System.Threading.Thread thread;

        public void EntryParse(DocumentParser documentParser)
        {
            lock (toBackgroundStock)
            {
                toBackgroundStock.Add(documentParser);
            }
        }

        private void run()
        {
            while (!abortFlag)
            {
                DocumentParser parser = null;
                lock (toBackgroundStock)
                {
                    if (toBackgroundStock.Count != 0)
                    {
                        parser = toBackgroundStock.Last();
                        toBackgroundStock.Clear();
                    }
                }
                if(parser != null)
                {
                    parser.Parse(DocumentParser.ParseMode.BackgroundParse);
                    lock (fromBackgroundStock)
                    {
                        fromBackgroundStock.Add(parser);
                    }
                }
                System.Threading.Thread.Sleep(1);
            }
        }

        public int RemainingStocks
        {
            get
            {
                lock (toBackgroundStock)
                {
                    return toBackgroundStock.Count;
                }
            }
        }

        public DocumentParser GetResult()
        {
            lock (fromBackgroundStock)
            {
                if (fromBackgroundStock.Count == 0) return null;
                DocumentParser parser = fromBackgroundStock.Last();
                fromBackgroundStock.Clear();
                return parser;
            }
        }

        private List<DocumentParser> toBackgroundStock = new List<DocumentParser>();
        private List<DocumentParser> fromBackgroundStock = new List<DocumentParser>();

    }
}
