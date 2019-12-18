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
            if (ParsedDocument != null) ParsedDocument.Dispose();
            base.Dispose();
        }

        public bool IsCodeDocumentCashed
        {
            get { if (document == null) return false; else return true; }
        }

        public virtual CodeEditor.ParsedDocument ParsedDocument { get; set; }

        private volatile bool parseRequested = false;
        public virtual bool ParseRequested { get {return parseRequested; } set { parseRequested = value; } }

        private volatile bool reloadRequested = false;
        public virtual bool CloseRequested { get { return reloadRequested; } set { reloadRequested = value; } }

        public virtual void AcceptParsedDocument(ParsedDocument newParsedDocument)
        {
            ParsedDocument oldParsedDocument = ParsedDocument;
            ParsedDocument = null;
            if (oldParsedDocument != null) oldParsedDocument.Dispose();

            ParsedDocument = newParsedDocument;
            ParseRequested = false;
            Update();
        }
        public virtual void Close()
        {
            if (Dirty) return;
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

        private CodeEditor.CodeDocument document = null;
        public virtual CodeEditor.CodeDocument CodeDocument {
            get
            {
                if(document == null)
                {
                    try
                    {
                        using (System.IO.StreamReader sr = new System.IO.StreamReader( Project.GetAbsolutePath(RelativePath) ))
                        {
                            document = new CodeEditor.CodeDocument(this);
                            string text = sr.ReadToEnd();
                            document.Replace(0, 0, 0, text);
//                            document.ClearHistory();
                            document.Clean();
                        }
                    }
                    catch
                    {
                        document = null;
                    }
                }
                return document;
            }
            protected set
            {
                document = value;
            }
        }

        
        public virtual CodeDrawStyle DrawStyle
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

        public virtual List<PopupItem> GetPopupItems(int EditId, int index)
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
