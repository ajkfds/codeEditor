﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeEditor
{
    public static class Controller
    {
        public static void AppendLog(string message)
        {
            Global.mainForm.logView.AppendLogLine(message);
            System.Diagnostics.Debug.Print(message);
        }
        public static void AppendLog(string message,System.Drawing.Color color)
        {
            Global.mainForm.logView.AppendLogLine(message, color);
        }

        public static System.Drawing.Color GetBackColor()
        {
            return Global.mainForm.BackColor;
        }

        public static void AddProject(Data.Project project)
        {
            Global.mainForm.Controller_AddProject(project);
        }

        public static System.Windows.Forms.MenuStrip GetMenuStrip()
        {
            return Global.mainForm.Controller_GetMenuStrip();
        }

        public static System.Windows.Forms.DialogResult ShowMessageBox(string text,string caption,System.Windows.Forms.MessageBoxButtons buttons,System.Windows.Forms.MessageBoxIcon icon)
        {
            return System.Windows.Forms.MessageBox.Show(text, caption, buttons, icon);
        }
        public static System.Windows.Forms.DialogResult ShowMessageBox(string text, string caption, System.Windows.Forms.MessageBoxButtons buttons)
        {
            return System.Windows.Forms.MessageBox.Show(text, caption, buttons);
        }

        public static void ShowForm(System.Windows.Forms.Form form)
        {
            form.Show(Global.mainForm);
        }
        public static void ShowDialogForm(System.Windows.Forms.Form form)
        {
            form.ShowDialog(Global.mainForm);
        }
        public static System.Windows.Forms.DialogResult ShowDialogForm(System.Windows.Forms.CommonDialog dialog)
        {
            return dialog.ShowDialog(Global.mainForm);
        }
        public static void ShowForm(System.Windows.Forms.Form form,System.Drawing.Point position)
        {
            form.Show(Global.mainForm);
            form.Location = position;
        }

        public static void DisposeOwndesForms()
        {
            System.Windows.Forms.Form[] forms = Global.mainForm.OwnedForms;
            for(int i= forms.Count(); i >= 0; i--)
            {
                forms[i].Close();
            }
        }

        public static class CodeEditor
        {
            public static void SetTextFile(Data.TextFile textFile)
            {
                if (textFile == null)
                {
                    Global.mainForm.editorPage.CodeEditor.SetTextFile(null);
                    Global.mainForm.mainTab.TabPages[0].Text = "-";
                }
                else
                {
                    Global.mainForm.editorPage.CodeEditor.AbortInteractiveSnippet();
                    Global.mainForm.editorPage.CodeEditor.SetTextFile(textFile);
                    Global.mainForm.mainTab.TabPages[0].Text = textFile.Name;
                    Global.mainForm.mainTab.SelectedTab = Global.mainForm.mainTab.TabPages[0];
                }
            }

            public static void ForceOpenAutoComplete(List<codeEditor.CodeEditor.AutocompleteItem> autocompleteItems)
            {
                Global.mainForm.editorPage.CodeEditor.ForceOpenAutoComplete(autocompleteItems);
            }

            public static void RequestReparse()
            {
                Global.mainForm.editorPage.CodeEditor.RequestReparse();
            }
            public static Data.ITextFile GetTextFile()
            {
                return Global.mainForm.editorPage.CodeEditor.TextFile;
            }

            internal static void startInteractiveSnippet(Snippets.InteractiveSnippet interactiveSnippet)
            {
                Global.mainForm.editorPage.CodeEditor.StartInteractiveSnippet(interactiveSnippet);
            }

            public static void AbortInteractiveSnippet()
            {
                Global.mainForm.editorPage.CodeEditor.AbortInteractiveSnippet();
            }

            public static void AppendHighlight(int highlightStart, int highlightLast)
            {
                Global.mainForm.editorPage.CodeEditor.codeTextbox.AppendHighlight(highlightStart, highlightLast);
            }

            public static void GetHighlightPosition(int highlightIndex, out int highlightStart, out int highlightLast)
            {
                Global.mainForm.editorPage.CodeEditor.codeTextbox.GetHighlightPosition(highlightIndex, out highlightStart, out highlightLast);
            }


            public static void SelectHighlight(int highLightIndex)
            {
                Global.mainForm.editorPage.CodeEditor.codeTextbox.SelectHighlight(highLightIndex);
            }

            public static int GetHighlightIndex(int index)
            {
                return Global.mainForm.editorPage.CodeEditor.codeTextbox.GetHighlightIndex(index);
            }

            public static void ClearHighlight()
            {
                Global.mainForm.editorPage.CodeEditor.codeTextbox.ClearHighlight();
            }
            public static void Refresh()
            {
                Global.mainForm.Controller_RefreshCodeEditor();
            }

            public static void ScrollToCaret()
            {
                Global.mainForm.editorPage.CodeEditor.ScrollToCaret();
            }

            //public static void Select(long index,long length)
            //{
            //    Global.mainForm.editorPage.CodeEditor.In
            //}

        }

        public static class NavigatePanel
        {
            public static void Refresh()
            {
                Global.mainForm.navigatePanel.Refresh();
            }

            public static void UpdateVisibleNode()
            {
                Global.mainForm.navigatePanel.UpdateWholeVisibleNode();
            }

            public static void UpdateVisibleNode(codeEditor.NavigatePanel.NavigatePanelNode node)
            {
                Global.mainForm.navigatePanel.UpdateWholeVisibleNode(node);
            }

            public static void GetSelectedNode(out codeEditor.NavigatePanel.NavigatePanelNode node)
            {
                Global.mainForm.navigatePanel.GetSelectedNode(out node);
            }

            public static System.Windows.Forms.ContextMenuStrip GetContextMenuStrip()
            {
                return Global.mainForm.navigatePanel.GetContextMenuStrip();
            }
        }

        public static class Tabs
        {

            public static void AddPage(ajkControls.TabPage tabPage)
            {
                Global.mainForm.Controller_AddTabPage(tabPage);
            }

            public static void RemovePage(ajkControls.TabPage tabPage)
            {
                Global.mainForm.Controller_RemoveTabPage(tabPage);
            }

            public static void Refresh()
            {
            }
        }

        public static class MessageView
        {
            public static void Update(codeEditor.CodeEditor.ParsedDocument parsedDocument)
            {
                Global.mainForm.messageView.UpdateMessages(parsedDocument);
            }
        }
    }
}
