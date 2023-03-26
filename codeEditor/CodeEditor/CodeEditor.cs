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


        public void MoveToNextHighlight(out bool moved)
        {
            codeTextbox.MoveToNextHighlight(out moved);
        }
        public void Save()
        {
            if (TextFile == null) return;
            entryParse();
            TextFile.Save();
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
            get
            {
                if(TextFile == null) return null;
                return TextFile.CodeDocument;
            }
        }

        List<Data.TextFile> closeCantidateTextFiles = new List<Data.TextFile>();
        const int FilesCasheNumbers = 20;

        public void SetTextFile(Data.TextFile textFile)
        {
            if (TextFile == textFile) return;
            if(TextFile != null)
            {
                if (closeCantidateTextFiles.Contains(textFile))
                {
                    closeCantidateTextFiles.Remove(textFile);
                }
                closeCantidateTextFiles.Add(textFile);
                if (closeCantidateTextFiles.Count > FilesCasheNumbers)
                {
                    closeCantidateTextFiles[0].Close();
                    closeCantidateTextFiles.RemoveAt(0);
                }
//                TextFile.Close(); // release document
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

        private ulong previousVersion= uint.MaxValue;
        private void codeTextbox_CarletLineChanged(object sender, EventArgs e)
        {
            bool first1, first2;
            System.Diagnostics.Debug.Print("document id"
                + " " + Global.IdGenerator.GetId(codeTextbox.Document,out first1).ToString()
                + " " + Global.IdGenerator.GetId(TextFile.CodeDocument, out first2).ToString()
                );

            if (codeTextbox.Document == null) return;
            if(previousVersion != codeTextbox.Document.Version)
            {
                entryParse();
            }
            previousVersion = codeTextbox.Document.Version;
        }


        private void checkID(string messgae)
        {
            // debug fuction
            //bool first1, first2;
            //if (codeTextbox.Document != null)
            //{
            //    System.Diagnostics.Debug.Print(messgae+ " id"
            //        + " " + Global.IdGenerator.GetId(codeTextbox.Document, out first1).ToString()
            //        + " " + Global.IdGenerator.GetId(TextFile.CodeDocument, out first2).ToString()
            //        );
            //}
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
                CodeDocument.Version != TextFile.ParsedDocument.Version)
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
            items = TextFile.GetPopupItems(CodeDocument.Version, index);
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


        // keys handling ////////////////////////////////////////

        // call order :  keydown -> keypress -> keyup
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

            switch (e.KeyCode)
            {
                case Keys.Delete:
                case Keys.Back:
                    openAutoComplete();
                    checkAutoComplete();
                    break;
                default:
                    break;
            }

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


        private void codeTextbox_MouseLeave(object sender, EventArgs e)
        {
//            closeAutoComplete();
//            popupForm.Visible = false;
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
