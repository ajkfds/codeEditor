﻿using System;
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
                subBackGroundParser.Terminate();
            };
            //            this.codeTextbox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.dbDrawBox_MouseWheel);
        }

        Snippets.InteractiveSnippet snippet = null;
        
        public void StartInteractiveSnippet(Snippets.InteractiveSnippet interactiveSnippet)
        {
            AbortInteractiveSnippet();
            snippet = interactiveSnippet;
        }

        public void AbortInteractiveSnippet()
        {
            if (snippet == null) return;
            snippet.Aborted();
            snippet = null;
        }

        public void AppendHighlight(int highlightStart, int highlightLast)
        {
            codeTextbox.AppendHighlight(highlightStart,highlightLast);
        }

        public void ClearHighlight()
        {
            codeTextbox.ClearHighlight();
        }

        // find & replace form
        FindForm findForm;

        public void OpenReplace()
        {
            if (CodeDocument == null) return;

            if (findForm == null || !findForm.Visible) findForm = new FindForm(this);
            Point point = this.PointToScreen(new Point(Left, Top));
            findForm.Left = point.X;
            findForm.Top = point.Y;

            if (findForm.Visible) return;
            string initialText = "";
            if (CodeDocument.SelectionStart != CodeDocument.SelectionLast)
            {
                initialText = CodeDocument.CreateString(CodeDocument.SelectionStart, CodeDocument.SelectionLast - CodeDocument.SelectionStart);
            }
            findForm.OpenReplace(initialText, this.PointToScreen(new Point(Width - findForm.Width, 0)));
        }
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

        public void ReplaceNext(string findString,string replaceString)
        {
            string currentText = CodeDocument.CreateString(CodeDocument.CaretIndex, CodeDocument.SelectionLast - CodeDocument.SelectionStart);
            if (currentText == findString)
            {
                CodeDocument.Replace(CodeDocument.CaretIndex, CodeDocument.SelectionLast - CodeDocument.SelectionStart, 0, replaceString);
                return;
            }
            FindNext(findString);
            currentText = CodeDocument.CreateString(CodeDocument.CaretIndex, CodeDocument.SelectionLast - CodeDocument.SelectionStart);
            if (currentText != findString) return;
            CodeDocument.Replace(CodeDocument.CaretIndex, CodeDocument.SelectionLast - CodeDocument.SelectionStart, 0, replaceString);
        }

        public void ReplacePrevious(string findString, string replaceString)
        {
            string currentText = CodeDocument.CreateString(CodeDocument.CaretIndex, CodeDocument.SelectionLast - CodeDocument.SelectionStart);
            if (currentText == findString)
            {
                CodeDocument.Replace(CodeDocument.CaretIndex, CodeDocument.SelectionLast - CodeDocument.SelectionStart, 0, replaceString);
                return;
            }
            FindPrevious(findString);
            currentText = CodeDocument.CreateString(CodeDocument.CaretIndex, CodeDocument.SelectionLast - CodeDocument.SelectionStart);
            if (currentText != findString) return;
            CodeDocument.Replace(CodeDocument.CaretIndex, CodeDocument.SelectionLast - CodeDocument.SelectionStart, 0, replaceString);
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

        public void MoveToNextHighlight(out bool moved)
        {
            codeTextbox.MoveToNextHighlight(out moved);
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
        private BackroungParser subBackGroundParser = new BackroungParser();
        private void CodeEditor_Load(object sender, EventArgs e)
        {
            backGroundParser.Run();
            subBackGroundParser.Run();
        }

        public Data.TextFile TextFile { get; protected set; }

        private CodeDocument CodeDocument
        {
            get { return codeTextbox.Document as CodeDocument; }
            set
            {
                codeTextbox.Document = value;
            }
        }

        public void SetTextFile(Data.TextFile textFile)
        {
            if (TextFile == textFile) return;
            if(TextFile != null)
            {
                TextFile.Close(); // release document
            }

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

        public void RequestReparse()
        {
            entryParse();
        }

        private void entryParse()
        {
            if (TextFile == null) return;
            DocumentParser parser = TextFile.CreateDocumentParser(DocumentParser.ParseModeEnum.EditParse);
            if (parser != null)
            {
                Controller.AppendLog("parserID " + CodeDocument.EditID.ToString());
                backGroundParser.EntryParse(parser);
                Controller.AppendLog("entry parse " + DateTime.Now.ToString());
            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            DocumentParser parser = backGroundParser.GetResult();
            if (parser == null) return;
            if (TextFile == null) return;
            if (TextFile != parser.TextFile) return;
            Controller.AppendLog("ID " + CodeDocument.EditID.ToString()+" parserID "+parser.EditId.ToString());

            if (CodeDocument.EditID != parser.EditId)
            {
                Controller.AppendLog("parsed mismatch " + DateTime.Now.ToString());
                return;
            }

            Controller.AppendLog("parsed "+DateTime.Now.ToString());
            CodeDocument.CopyColorsFrom(parser.Document);
            CodeDocument.CopyMarksFrom(parser.Document);
            CodeDocument.CopyBlocksFrom(parser.Document);
            codeTextbox.Invoke(new Action(codeTextbox.Refresh));

            if(parser.ParsedDocument != null)
            {
                TextFile.AcceptParsedDocument(parser.ParsedDocument);
            }

            Controller.MessageView.Update(TextFile.ParsedDocument);
            codeTextbox.ReDrawHighlight();

            Controller.NavigatePanel.UpdateVisibleNode();
            Controller.NavigatePanel.Refresh();
        }

        private void subBgtimer_Tick(object sender, EventArgs e)
        {
            DocumentParser parser = subBackGroundParser.GetResult();
            if (parser == null) { // entry parse
                if (subBackGroundParser.RemainingStocks != 0) return;
                NavigatePanel.NavigatePanelNode node;
                Controller.NavigatePanel.GetSelectedNode(out node);
                if (node == null) return;
                Data.Project project = node.Item.Project;
                Data.Item item = project.FetchReparseTarget();
                if (item == null) return;

                DocumentParser newParser = item.CreateDocumentParser(DocumentParser.ParseModeEnum.BackgroundParse);
                if (newParser != null)
                {
                    subBackGroundParser.EntryParse(newParser);
                    Controller.AppendLog("entry parse " + item.Name + " " + DateTime.Now.ToString());
                }
            }
            else
            { // receive result
                if(TextFile != null && TextFile == parser.TextFile)
                {
                    if (CodeDocument != null && CodeDocument.EditID != parser.EditId)
                    {
                        Controller.AppendLog("parsed mismatch sub " + parser.TextFile.Name + " " + DateTime.Now.ToString());
                        TextFile.ParseRequested = false;
                        return;
                    }
                }

                Controller.AppendLog("parsed sub  " + parser.TextFile.Name + " " + DateTime.Now.ToString());
                if(parser.TextFile.Name == "TOP_0")
                {
                    string a = "";
                }
                Data.TextFile textFile = parser.TextFile;

                if (textFile == null) return;
                //if (textFile.ParsedDocument == null)
                //{
                //    textFile.Close();
                //    textFile.ParseRequested = false;
                //    return;
                //}

                textFile.AcceptParsedDocument(parser.ParsedDocument);
                textFile.Close();
                if (textFile.NavigatePanelNode != null) 
                {
                    textFile.NavigatePanelNode.Update();
                }

                Controller.NavigatePanel.UpdateVisibleNode();
                Controller.NavigatePanel.Refresh();
                parser.Dispose();
            }

        }


        private PopupForm popupForm = new PopupForm();
        private int popupInex = -1;
        private void codeTextbox_MouseMove(object sender, MouseEventArgs e)
        {
            if (
                CodeDocument == null ||
                TextFile == null ||
                TextFile.ParsedDocument == null ||
                TextFile != TextFile.ParsedDocument.Item ||
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

            if (snippet != null) snippet.BeforeKeyDown(sender, e, autoCompleteForm);

            if( e.KeyCode == Keys.Space && e.Modifiers == Keys.Shift )
            { // open auto select menu
                e.Handled = true;
                closeAutoComplete();
                openToolSelectionForm();
                return;
            }

            if (e.KeyCode == Keys.Space && e.Modifiers == Keys.Control)
            { // force open auto complete
                openAutoComplete();
                e.Handled = true;
                return;
            }

            if (autoCompleteForm == null || autoCompleteForm.Visible == false)
            {
                TextFile.BeforeKeyDown(e);
                return;
            }

            // autoComplete form handle
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
                    applyAutoCompleteSelection(e);
                    e.Handled = true;
                    break;
                case Keys.OemPeriod:
                case Keys.Space:
                    applyAutoCompleteSelection(e);
                    break;
                case Keys.Escape:
                case Keys.Delete:
                case Keys.Back:
                    autoCompleteForm.Visible = false;
                    break;
                default:
                    break;
            }

            if (snippet != null) snippet.AfterAutoCompleteHandled(sender, e, autoCompleteForm);
        }

        private void codeTextbox_AfterKeyDown(object sender, KeyEventArgs e)
        {
            if (TextFile == null) return;

            bool autoCompleteFormActive = (autoCompleteForm != null && autoCompleteForm.Visible);
            if (snippet != null) snippet.AfterKeyDown(sender, e, autoCompleteForm);

            TextFile.AfterKeyDown(e);
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
                toolSelectionForm.ForeColor = System.Drawing.Color.Black;
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
            Controller.ShowForm(toolSelectionForm, screenPosition);
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
            Controller.ShowForm(autoCompleteForm, screenPosition);
        }

        private void closeAutoComplete()
        {
            if (autoCompleteForm == null) return;
            autoCompleteForm.Visible = false;
        }

        private void applyAutoCompleteSelection(KeyEventArgs e)
        {
            if (autoCompleteForm == null | !autoCompleteForm.Visible) return;
            AutocompleteItem item = autoCompleteForm.SelectItem();
            if (item == null) return;
            item.Apply(CodeDocument,e);
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
                string cantidateWord;
                List<AutocompleteItem> items = TextFile.GetAutoCompleteItems(CodeDocument.CaretIndex, out cantidateWord);
                if(items == null || cantidateWord == null)
                {
                    closeAutoComplete();
                }
                else
                {
                    openAutoComplete();
                    autoCompleteForm.SetAutocompleteItems(items);
                    autoCompleteForm.UpdateVisibleItems(cantidateWord);
                }
            }
        }

        private void codeTextbox_MouseLeave(object sender, EventArgs e)
        {
            closeAutoComplete();
            popupForm.Visible = false;
        }

        private void CodeTextbox_SelectionChanged()
        {
            if (snippet != null) return;

            if (codeTextbox.Document.SelectionStart == codeTextbox.Document.SelectionLast || 
                codeTextbox.Document.SelectionStart + 3 > codeTextbox.Document.SelectionLast)
            {
                codeTextbox.ClearHighlight();
            }
            else
            {
                string target = codeTextbox.Document.CreateString(codeTextbox.Document.SelectionStart, codeTextbox.Document.SelectionLast - codeTextbox.Document.SelectionStart);
                if (target.Contains('\n'))
                {
                    codeTextbox.ClearHighlight();
                }
                else
                {
                    codeTextbox.ClearHighlight();
                    codeTextbox.DoAtVisibleLines((line) =>
                    {
                        int i = codeTextbox.Document.CreateLineString(line).IndexOf(target);
                        if (i >= 0)
                        {
                            codeTextbox.AppendHighlight(codeTextbox.Document.GetLineStartIndex(line) + i, codeTextbox.Document.GetLineStartIndex(line) + i + target.Length-1);
                        }

                    });
                }
            }
        }
    }
}
