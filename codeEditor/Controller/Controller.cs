using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeEditor.ViewControl
{
    public class Controller
    {
        public Controller(MainForm mainForm)
        {
            this.mainForm = mainForm;
            CodeEditor = new _CodeEditorController(mainForm);
            NavigatePanel = new _NavigatePanelController(mainForm);
            Tabs = new _TabsController(mainForm);
            MessageView = new _MessageController(mainForm);
        }
        MainForm mainForm;

        public void AppendLog(string message)
        {
            mainForm.Controller_LogAppend(message);
        }
        public void AppendLog(string message,System.Drawing.Color color)
        {
            mainForm.Controller_LogAppend(message,color);
        }

        public void AddProject(Data.Project project)
        {
            mainForm.Controller_AddProject(project);
        }

        public System.Windows.Forms.MenuStrip GetMenuStrip()
        {
            return mainForm.Controller_GetMenuStrip();
        }

        public System.Windows.Forms.DialogResult ShowMessageBox(string text,string caption,System.Windows.Forms.MessageBoxButtons buttons,System.Windows.Forms.MessageBoxIcon icon)
        {
            return System.Windows.Forms.MessageBox.Show(text, caption, buttons, icon);
        }
        public System.Windows.Forms.DialogResult ShowMessageBox(string text, string caption, System.Windows.Forms.MessageBoxButtons buttons)
        {
            return System.Windows.Forms.MessageBox.Show(text, caption, buttons);
        }

        public void ShowForm(System.Windows.Forms.Form form)
        {
            form.Show(mainForm);
        }
        public void ShowDialogForm(System.Windows.Forms.Form form)
        {
            form.ShowDialog(mainForm);
        }
        public System.Windows.Forms.DialogResult ShowDialogForm(System.Windows.Forms.CommonDialog dialog)
        {
            return dialog.ShowDialog(mainForm);
        }
        public void ShowForm(System.Windows.Forms.Form form,System.Drawing.Point position)
        {
            form.Show(mainForm);
            form.Location = position;
        }

        public void DisposeOwndesForms()
        {
            System.Windows.Forms.Form[] forms = mainForm.OwnedForms;
            for(int i= forms.Count(); i >= 0; i--)
            {
                forms[i].Close();
            }
        }

        public _CodeEditorController CodeEditor;
        public _NavigatePanelController NavigatePanel;
        public _TabsController Tabs;
        public _MessageController MessageView;

        public class _CodeEditorController
        {
            public _CodeEditorController(MainForm mainForm)
            {
                this.mainForm = mainForm;
            }
            MainForm mainForm;

            public void SetTextFile(Data.ITextFile textFile)
            {
                mainForm.Controller_SetCodeEditorTextItem(textFile);
            }

            public Data.ITextFile GetTextFile()
            {
                return mainForm.Controller_GetCodeEditorTextItem();
            }

            public void Refresh()
            {
                mainForm.Controller_RefreshCodeEditor();
            }

            public void ScrollToCaret()
            {
                mainForm.Controller_ScrollToCaret();
            }
        }

        public class _NavigatePanelController
        {
            public _NavigatePanelController(MainForm mainForm)
            {
                this.mainForm = mainForm;
            }
            MainForm mainForm;

            public void Refresh()
            {
                mainForm.Controller_RefreshNavigatePanel();   
            }

            //public void UpdateWholeNode()
            //{
            //    mainForm.Controller_UpdateNavigateaPanelWholeNode();
            //}

            //public void UpdateWholeNode(NavigatePanel.NavigatePanelNode node)
            //{
            //    mainForm.Controller_UpdateNavigateaPanelWholeNode(node);
            //}

            public void UpdateVisibleNode()
            {
                mainForm.Controller_UpdateNavigateaPanelVisibleNode();
            }

            public void UpdateVisibleNode(NavigatePanel.NavigatePanelNode node)
            {
                mainForm.Controller_UpdateNavigateaPanelVisibleNode(node);
            }

            public void GetSelectedNode(out string project, out string id)
            {
                mainForm.Controller_GetNavigatePanelSelectedNode(out project, out id);
            }

            public System.Windows.Forms.ContextMenuStrip GetContextMenuStrip()
            {
                return mainForm.Controller_GetNavigateContextMenu();
            }
        }

        public class _TabsController
        {
            public _TabsController(MainForm mainForm)
            {
                this.mainForm = mainForm;
            }
            MainForm mainForm;

            public void AddPage(ajkControls.TabPage tabPage)
            {
                mainForm.Controller_AddTabPage(tabPage);
            }

            public void RemovePage(ajkControls.TabPage tabPage)
            {
                mainForm.Controller_RemoveTabPage(tabPage);
            }

            public void Refresh()
            {
            }
        }

        public class _MessageController
        {
            public _MessageController(MainForm mainForm)
            {
                this.mainForm = mainForm;
            }
            MainForm mainForm;

            public void Update(CodeEditor.ParsedDocument parsedDocument)
            {
                mainForm.Controller_UpdateMessageView(parsedDocument);
            }
        }
    }
}
