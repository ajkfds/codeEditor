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

        public void AddProject(string absolutePath)
        {
            mainForm.Controller_AddProject(absolutePath);
        }

        public System.Windows.Forms.MenuStrip GetMenuStrip()
        {
            return mainForm.Controller_GetMenuStrip();
        }


        public void ShowForm(System.Windows.Forms.Form form)
        {
            form.Show(mainForm);
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

            public void Update()
            {
                mainForm.Controller_UpdateNavigateaPanel();
            }
        }

        public class _TabsController
        {
            public _TabsController(MainForm mainForm)
            {
                this.mainForm = mainForm;
            }
            MainForm mainForm;

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
