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
        // tool selection form /////////////////////////////////////////////////////////////////////////

        private ajkControls.SelectionForm toolSelectionForm = null;

        private void openToolSelectionForm()
        {
            if (toolSelectionForm == null)
            {
                toolSelectionForm = new ajkControls.SelectionForm();
                toolSelectionForm.InputAreaForecolor = Color.FromArgb(250, 250, 250);
                toolSelectionForm.InputAreaBackcolor = Color.FromArgb(90, 90, 90);
                toolSelectionForm.ForeColor = Color.FromArgb(240, 240, 240);
                toolSelectionForm.BackColor = Color.FromArgb(50, 50, 50);
                toolSelectionForm.Style = Global.DefaultDrawStyle;
                toolSelectionForm.SelectedColor = Color.FromArgb(128, (int)(52 * 3), (int)(58 * 3), (int)(64 * 3));
                toolSelectionForm.Selected += ApplyTool;
            }
            if (toolSelectionForm.Visible) return;

            List<ToolItem> tools = TextFile.GetToolItems(CodeDocument.CaretIndex);
            List<ajkControls.SelectionItem> items = new List<ajkControls.SelectionItem>();
            foreach (ToolItem item in tools) { items.Add(item); }

            items.Add(new Snippets.ToLower());
            items.Add(new Snippets.ToUpper());

            toolSelectionForm.SetSelectionItems(items);
            toolSelectionForm.Font = codeTextbox.Font;

            Point screenPosition = PointToScreen(codeTextbox.GetCaretTopPoint());
            Controller.ShowForm(toolSelectionForm, screenPosition);
        }

        private void ApplyTool(object sender, EventArgs e)
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
            item.Apply(CodeDocument, e);
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
                if (items == null || cantidateWord == null || cantidateWord =="")
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

    }
}
