using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace codeEditor.CodeEditor
{
    public partial class CodeEditor : UserControl
    {
        public CodeEditor()
        {
            InitializeComponent();
            this.Disposed += (sender, args) => {
                backGroundParser.Terminate();
            };
            //            this.codeTextbox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.dbDrawBox_MouseWheel);
        }

        FindForm findForm;
        public void OpenFind()
        {
            if (CodeDocument == null) return;

            if(findForm == null || !findForm.Visible) findForm = new FindForm(this);
            Point point = this.PointToScreen(new Point(Left, Top));
            findForm.Left = point.X;
            findForm.Top = point.Y;

            if (findForm.Visible) return;
            string initialText = "";
            if(CodeDocument.SelectionStart != CodeDocument.SelectionLast)
            {
                initialText = CodeDocument.CreateString(CodeDocument.SelectionStart, CodeDocument.SelectionLast - CodeDocument.SelectionStart);
            }
            findForm.OpenFind(initialText, this.PointToScreen(new Point(Width-findForm.Width, 0)));
        }

        public void FindNext(string findString)
        {
            string currentText = CodeDocument.CreateString(CodeDocument.SelectionStart, CodeDocument.SelectionLast - CodeDocument.SelectionStart);
            int findIndex;
            if (currentText == findString)
            {
                findIndex = CodeDocument.FindIndexOf(findString, CodeDocument.SelectionStart + 1);
            }
            else
            {
                findIndex = CodeDocument.FindIndexOf(findString, CodeDocument.CaretIndex);
            }

            if (findIndex == -1)
            {
                findIndex = CodeDocument.FindIndexOf(findString, 0);
                if (findIndex == -1) return;
            }

            CodeDocument.CaretIndex = findIndex;
            CodeDocument.SelectionStart = findIndex;
            CodeDocument.SelectionLast = findIndex + findString.Length;
            ScrollToCaret();
            Refresh();
        }

        public void FindPrevious(string findString)
        {
            string currentText = CodeDocument.CreateString(CodeDocument.SelectionStart, CodeDocument.SelectionLast - CodeDocument.SelectionStart);
            int findIndex;
            if (currentText == findString)
            {
                if (CodeDocument.SelectionStart == 0) return;
                findIndex = CodeDocument.FindPreviousIndexOf(findString, CodeDocument.SelectionStart - 1);
            }
            else
            {
                findIndex = CodeDocument.FindPreviousIndexOf(findString, CodeDocument.CaretIndex);
            }

            if (findIndex == -1)
            {
                if (CodeDocument.Length == 0) return;
                findIndex = CodeDocument.FindPreviousIndexOf(findString, CodeDocument.Length - 1);
                if (findIndex == -1) return;
            }

            CodeDocument.CaretIndex = findIndex;
            CodeDocument.SelectionStart = findIndex;
            CodeDocument.SelectionLast = findIndex + findString.Length;
            ScrollToCaret();
            Refresh();
        }

        public void Save()
        {
            if (TextFile == null) return;
            using(System.IO.StreamWriter sw = new System.IO.StreamWriter(TextFile.Project.GetAbsolutePath(TextFile.RelativePath)))
            {
                sw.Write(TextFile.CodeDocument.CreateString());
            }
        }

        public void ScrollToCaret()
        {
            codeTextbox.ScrollToCaret();
        }

        private BackroungParser backGroundParser = new BackroungParser();
        private void CodeEditor_Load(object sender, EventArgs e)
        {
            backGroundParser.Run();
        }

        public Data.ITextFile TextFile { get; protected set; }

        private CodeDocument CodeDocument
        {
            get { return codeTextbox.Document as CodeDocument; }
            set
            {
                codeTextbox.Document = value;
            }
        }

        public void SetTextFile(Data.ITextFile textFile)
        {
            if(textFile == null || textFile.CodeDocument == null)
            {
                codeTextbox.Document = null;
                codeTextbox.Visible = false;
                return;
            }
            codeTextbox.Visible = true;
            codeTextbox.Document = textFile.CodeDocument;
            TextFile = textFile;

            entryParse();
        }


        private int previousEditIdD= -1;
        private void codeTextbox_CarletLineChanged(object sender, EventArgs e)
        {
            if (codeTextbox.Document == null) return;
            if(previousEditIdD != codeTextbox.Document.EditID)
            {
                entryParse();
            }
            previousEditIdD = codeTextbox.Document.EditID;
        }

        private void entryParse()
        {
            if (TextFile == null) return;
            DocumentParser parser = TextFile.CreateDocumentParser(CodeDocument, TextFile.ID, TextFile.Project);
            if (parser != null) backGroundParser.EntryParse(parser);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            DocumentParser parser = backGroundParser.GetResult();
            if (parser == null) return;
            if (TextFile.ID != parser.ID) return;
            if (CodeDocument.EditID != parser.EditId) return;

            CodeDocument.CopyColorsFrom(parser.Document);
            CodeDocument.CopyMarksFrom(parser.Document);
            codeTextbox.Invoke(new Action(codeTextbox.Refresh));

            if(TextFile.ParsedDocument != null)
            {
                ParsedDocument oldParsedDocument = TextFile.ParsedDocument;
                TextFile.ParsedDocument = null;
                oldParsedDocument.Dispose();
                TextFile.ParsedDocument = parser.ParsedDocument;
                TextFile.ParsedDocument.Accept();
                TextFile.Update();

                Global.Controller.MessageView.Update(TextFile.ParsedDocument);
            }

            Global.Controller.NavigatePanel.Update();
            Global.Controller.NavigatePanel.Refresh();
        }

        private PopupForm popupForm = new PopupForm();
        private int popupInex = -1;
        private void codeTextbox_MouseMove(object sender, MouseEventArgs e)
        {
            if (
                CodeDocument == null ||
                TextFile == null ||
                TextFile.ParsedDocument == null ||
                TextFile.ID != TextFile.ParsedDocument.ItemID ||
                CodeDocument.EditID != TextFile.ParsedDocument.EditID)
            {
                popupForm.Visible = false;
                return;
            }
            int index = codeTextbox.GetIndexAt(e.X, e.Y);
            int headIndex, length;
            CodeDocument.GetWord(index, out headIndex, out length);

            if (popupInex == headIndex) return;
            popupInex = headIndex;

            List<PopupItem> items;
            items = TextFile.GetPopupItems(CodeDocument.EditID, index);
            if(items == null || items.Count == 0)
            {
                popupForm.Visible = false;
                return;
            }
            popupForm.SetItems(items);
            popupForm.Font = codeTextbox.Font;
            /*
            if(codeTextbox.Document.GetMarkAt(index) == 0)
            {
                popupForm.Visible = false;
                return;
            }
            */
            /*
            popupForm.Document.Replace(0, popupForm.Document.Length, 0, "");
            foreach(var message in TextFile.ParsedDocument.Messages)
            {
                if(index >= message.Index && index < message.Index + message.Length)
                {
                    popupForm.Document.Replace(popupForm.Document.Length, 0, 0, message.Text + "\r\n");
                }
            }
            */
            Point screenPosition = PointToScreen(new Point(e.X, e.Y));
            popupForm.Left = screenPosition.X+10;
            popupForm.Top = screenPosition.Y+10;
            popupForm.Visible = true;
        }

        private void codeTextbox_Load(object sender, EventArgs e)
        {

        }

        private void codeTextbox_DoubleClick(object sender, EventArgs e)
        {
 


        }

        private void codeTextbox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (
                CodeDocument != null
                )
            {
                int index = codeTextbox.GetIndexAt(e.X, e.Y);
                int headIndex, length;
                CodeDocument.GetWord(index, out headIndex, out length);

                codeTextbox.SetSelection(headIndex, length);
            }
        }

        private void codeTextbox_AfterKeyPressed(object sender, KeyPressEventArgs e)
        {
            if (CodeDocument == null) return;
//            CodeDocument.AfterKeyPressed(e);
        }

        private void codeTextbox_AfterKeyDown(object sender, KeyEventArgs e)
        {
            if (CodeDocument == null) return;
//            CodeDocument.AfterKeyDown(e);
        }
    }
}
