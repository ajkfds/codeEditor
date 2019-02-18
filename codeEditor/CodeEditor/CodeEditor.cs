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

        // find & replace form
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
            entryParse();
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
            if (TextFile == textFile) return;
            if (textFile == null || textFile.CodeDocument == null)
            {
                codeTextbox.Document = null;
                codeTextbox.Visible = false;
                return;
            }
            if (TextFile == null || TextFile.GetType() != textFile.GetType())
            {
                codeTextbox.Style = textFile.DrawStyle;
            }
            
            codeTextbox.Visible = true;
            codeTextbox.Document = textFile.CodeDocument;
            TextFile = textFile;
            ScrollToCaret();

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
            if (parser != null)
            {
                backGroundParser.EntryParse(parser);
                Global.Controller.AppendLog("entry parse " + DateTime.Now.ToString());
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            DocumentParser parser = backGroundParser.GetResult();
            if (parser == null) return;
            if (TextFile.ID != parser.ID) return;
            if (CodeDocument.EditID != parser.EditId)
            {
                Global.Controller.AppendLog("parsed mismatch " + DateTime.Now.ToString());
                return;
            }

            Global.Controller.AppendLog("parsed "+DateTime.Now.ToString());
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

            Global.Controller.NavigatePanel.UpdateVisibleNode();
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

            Point screenPosition = PointToScreen(new Point(e.X, e.Y));
            popupForm.Visible = true;
            popupForm.Left = screenPosition.X+10;
            popupForm.Top = screenPosition.Y+10;
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


        // keys
        private void codeTextbox_BeforeKeyDown(object sender, KeyEventArgs e)
        {
            if (TextFile == null) return;

            if(e.KeyCode == Keys.Space)
            {
                if (e.Modifiers == Keys.Shift)
                {
                    e.Handled = true;
                    closeAutoComplete();
                    openToolSelectionForm();
                    return;
                }
                else if (e.Modifiers == Keys.Control)
                {
                    openAutoComplete();
                    e.Handled = true;
                    return;
                }
            }


            // autoComplete
            if (autoCompleteForm == null || !autoCompleteForm.Visible)
            {
                TextFile.BeforeKeyDown(e);
                return;
            }

            switch (e.KeyCode)
            {
                case Keys.Up:
                    autoCompleteForm.MoveUp();
                    e.Handled = true;
                    break;
                case Keys.Down:
                    autoCompleteForm.MoveDown();
                    e.Handled = true;
                    break;
                case Keys.Tab:
                case Keys.Return:
                    applyAutoCompleteSelection(e.KeyCode);
                    e.Handled = true;
                    break;
                case Keys.Space:
                    applyAutoCompleteSelection(e.KeyCode);
                    break;
                case Keys.Escape:
                case Keys.Delete:
                case Keys.Back:
                    autoCompleteForm.Visible = false;
                    break;
                default:
                    break;
            }
        }

        private void codeTextbox_AfterKeyDown(object sender, KeyEventArgs e)
        {
            if (TextFile == null) return;
            TextFile.AfterKeyDown(e);
            if(e.KeyData == Keys.Delete || e.KeyData == Keys.Back)
            {
                if (autoCompleteForm != null && autoCompleteForm.Visible)
                {
                    checkAutoComplete();
                }
            }
        }

        private void codeTextbox_BeforeKeyPressed(object sender, KeyPressEventArgs e)
        {
            if (TextFile == null) return;
            TextFile.BeforeKeyPressed(e);
        }

        private void codeTextbox_AfterKeyPressed(object sender, KeyPressEventArgs e)
        {
            if (TextFile == null || CodeDocument == null) return;

            char inChar = e.KeyChar;
            if ((inChar < 127 && inChar >= 0x20) || inChar == '\t' || inChar > 0xff)
            {
                checkAutoComplete();
            }

            TextFile.AfterKeyPressed(e);
        }

        // tool selection form /////////////////////////////////////////////////////////////////////////

        private ajkControls.SelectionForm toolSelectionForm = null;

        private void openToolSelectionForm()
        {
            if(toolSelectionForm == null)
            {
                toolSelectionForm = new ajkControls.SelectionForm();
                toolSelectionForm.Selected += ApplyTool;
            }
            if (toolSelectionForm.Visible) return;

            List<ToolItem> tools = TextFile.GetToolItems(CodeDocument.CaretIndex);
            List<ajkControls.SelectionItem> items = new List<ajkControls.SelectionItem>();
            foreach(ToolItem item in tools) { items.Add(item); }

            items.Add(new Snippets.ToLower());
            items.Add(new Snippets.ToUpper());

            toolSelectionForm.SetSelectionItems(items);
            toolSelectionForm.Font = codeTextbox.Font;

            Point screenPosition = PointToScreen(codeTextbox.GetCaretTopPoint());
            Global.Controller.ShowForm(toolSelectionForm, screenPosition);
        }

        private void ApplyTool(object sender,EventArgs e)
        {
            if (toolSelectionForm == null) return;
            if (toolSelectionForm.SelectedItem == null) return;
            if (CodeDocument == null) return;

            ((ToolItem)toolSelectionForm.SelectedItem).Apply(CodeDocument);
            codeTextbox.Refresh();
            entryParse();
        }

        private void closeToolSelectionForm()
        {
            if (toolSelectionForm != null && toolSelectionForm.Visible) toolSelectionForm.Visible = false;
            toolSelectionForm.Visible = false;
        }

        // auto complete form ////////////////////////////////////////////////////////////////////////////////////

        private AutoCompleteForm autoCompleteForm = null;

        private void openAutoComplete()
        {
            if (autoCompleteForm == null)
            {
                autoCompleteForm = new AutoCompleteForm();
                autoCompleteForm.Font = codeTextbox.Font;
            }
            if (autoCompleteForm.Visible) return;
            Point screenPosition = PointToScreen(codeTextbox.GetCaretBottomPoint());
            Global.Controller.ShowForm(autoCompleteForm, screenPosition);
        }

        private void closeAutoComplete()
        {
            if (autoCompleteForm == null) return;
            autoCompleteForm.Visible = false;
        }

        private void applyAutoCompleteSelection(Keys keyCode)
        {
            if (autoCompleteForm == null | !autoCompleteForm.Visible) return;
            AutocompleteItem item = autoCompleteForm.SelectItem();
            if (item == null) return;
            item.Apply(CodeDocument,keyCode);
        }

        /// <summary>
        /// update auto complete word text
        /// </summary>
        private void checkAutoComplete()
        {
            int prevIndex = CodeDocument.CaretIndex;
            if (CodeDocument.GetLineStartIndex(CodeDocument.GetLineAt(prevIndex)) != prevIndex && prevIndex != 0)
            {
                prevIndex--;
            }

            if (CodeDocument.SelectionStart == CodeDocument.SelectionLast)
            {
                int headIndex, length;
                CodeDocument.GetWord(prevIndex, out headIndex, out length);
                string word = CodeDocument.CreateString(headIndex, length);
                if (word == "")
                {
                    closeAutoComplete();
                }
                else
                {
                    openAutoComplete();
                    autoCompleteForm.SetAutocompleteItems(TextFile.GetAutoCompleteItems(CodeDocument.CaretIndex));
                    autoCompleteForm.UpdateVisibleItems(word);
                }
            }
        }

        private void codeTextbox_MouseLeave(object sender, EventArgs e)
        {
            closeAutoComplete();
            popupForm.Visible = false;
        }
    }
}
