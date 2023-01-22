using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ajkControls;
using codeEditor.CodeEditor;
using codeEditor.NavigatePanel;

namespace codeEditor.Data
{
    public class TextFile : File, ITextFile
    {
        public static TextFile Create(string relativePath, Project project)
        {
            TextFile fileItem = new TextFile();
            fileItem.Project = project;
            fileItem.RelativePath = relativePath;
            if (relativePath.Contains('\\'))
            {
                fileItem.Name = relativePath.Substring(relativePath.LastIndexOf('\\') + 1);
            }
            else
            {
                fileItem.Name = relativePath;
            }

            return fileItem;
        }

        public override void Dispose()
        {
            document.Dispose();
            if (ParsedDocument != null) ParsedDocument.Dispose();
            base.Dispose();
        }

        public bool IsCodeDocumentCashed
        {
            get { if (document == null) return false; else return true; }
        }

        public virtual CodeEditor.ParsedDocument ParsedDocument { get; set; }

        //private volatile bool parseRequested = false;
        //public virtual bool ParseRequested { get {return parseRequested; } set { parseRequested = value; } }

        //private volatile bool reloadRequested = false;
        //public virtual bool CloseRequested { get { return reloadRequested; } set { reloadRequested = value; } }

        public virtual void AcceptParsedDocument(ParsedDocument newParsedDocument)
        {
            ParsedDocument oldParsedDocument = ParsedDocument;
            ParsedDocument = null;
            if (oldParsedDocument != null) oldParsedDocument.Dispose();

            ParsedDocument = newParsedDocument;
            Update();
        }
        public virtual void Close()
        {
            if (Dirty) return;
            CodeDocument.Dispose();
            CodeDocument = null;
        }

        public virtual bool Dirty
        {
            get
            {
                if (CodeDocument == null) return false;
                if (CodeDocument.IsDirty) return true;
                return false;
            }
        }

        public virtual void LoadFormFile()
        {
            loadDoumentFromFile();
        }

        protected CodeEditor.CodeDocument document = null;
        
        public virtual CodeEditor.CodeDocument CodeDocument {
            get
            {
                if(document == null)
                {
                    loadDoumentFromFile();
                }
                else
                {
                    loadedFileLastWriteTime = null;
                }
                return document;
            }
            protected set
            {
                document = value;
            }
        }

        public void Save()
        {
            if (CodeDocument == null) return;

            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(AbsolutePath))
            {
                sw.Write(CodeDocument.CreateString());
            }
            loadedFileLastWriteTime = System.IO.File.GetLastWriteTime(AbsolutePath);
        }

        public DateTime? LoadedFileLastWriteTime
        {
            get
            {
                return loadedFileLastWriteTime;
            }
        }

        protected DateTime? loadedFileLastWriteTime;
        private void loadDoumentFromFile()
        {
            try
            {
                if(document == null) document = new CodeEditor.CodeDocument(this);
                using (System.IO.StreamReader sr = new System.IO.StreamReader(AbsolutePath))
                {
                    loadedFileLastWriteTime = System.IO.File.GetLastWriteTime(AbsolutePath);

                    string text = sr.ReadToEnd();
                    document.Replace(0, document.Length, 0, text);
                    document.ClearHistory();
                    document.Clean();
                }
            }
            catch
            {
                document = null;
            }
        }


        public virtual ajkControls.CodeTextbox.CodeDrawStyle DrawStyle
        {
            get
            {
                return Global.DefaultDrawStyle;
            }
        }



        public override NavigatePanelNode CreateNode()
        {
            NavigatePanel.TextFileNode node = new TextFileNode(this);
            nodeRef = new WeakReference<NavigatePanelNode>(node);
            return node;
        }


        public override CodeEditor.DocumentParser CreateDocumentParser(CodeEditor.DocumentParser.ParseModeEnum parseMode)
        {
            return null;
        }

        public virtual List<PopupItem> GetPopupItems(ulong Version, int index)
        {
            return null;
        }

        public virtual List<AutocompleteItem> GetAutoCompleteItems(int index,out string cantidateWord)
        {
            cantidateWord = null;
            return null;
        }
        public virtual List<codeEditor.CodeEditor.ToolItem> GetToolItems(int index)
        {
            return null;
        }


        public virtual void BeforeKeyDown(KeyEventArgs e)
        {

        }

        public virtual void AfterKeyDown(System.Windows.Forms.KeyEventArgs e)
        {

        }

        public virtual void BeforeKeyPressed(KeyPressEventArgs e)
        {

        }

        public virtual void AfterKeyPressed(System.Windows.Forms.KeyPressEventArgs e)
        {
        }


    }
}
