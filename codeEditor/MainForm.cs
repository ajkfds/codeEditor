using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace codeEditor
{
    /*
     *         +---- model <---+
     *         |               |
     * updates |               | manipulates
     *         v               |
     *        view         controller
     *         |               ^
     *    sees |               | uses
     *         |               | 
     *         +----> user ----+
     */

    /*
     *         +---- data <----+
     *         |               |
     * updates |               | manipulates
     *         v               |
     *        view         controller
     *     viewUpdater         
     *    (controlls
     *     NavigatePanel
     *     codeEditor)
     *        
     *         |               ^
     *    sees |               | uses
     *         |               | 
     *         +----> user ----+
     *         
     *         
     *         data --+-- project --+-- folder --+-- file
     *                |  (filelist) |            +-- folder
     *                |      |      +-- file
     *                |      +-- plugin project property
     *                |      +-- plugin project property
     *                |
     *                
     */

    public partial class MainForm : Form
    {
        private const string setupFileName = "codeEditor.json";

        public MainForm()
        {
            InitializeComponent();
            ajkControls.Global.Icon = Properties.Resources.ajEditor;
            this.Icon = ajkControls.Global.Icon;

            Global.Controller = new ViewControl.Controller(this);
            //            mainTab.SelectedBackgroundColor = Global.ColorMap.SelectedBackground;
            //            subTab.SelectedBackgroundColor = Global.ColorMap.SelectedBackground;
            mainTab.TabPages.Add(editorPage);


            commandShellToolStripMenuItem.Image = Global.IconImages.Terminal.GetImage(ajkControls.Global.MainMenuIconSize, ajkControls.IconImage.ColorStyle.White);
            saveToolStripMenuItem.Image = Global.IconImages.SaveFile.GetImage(ajkControls.Global.MainMenuIconSize, ajkControls.IconImage.ColorStyle.White);

            // register filetype
            FileTypes.TextFile textFileType = new FileTypes.TextFile();
            Global.FileTypes.Add(textFileType.ID, textFileType);

            codeEditorPlugin.PulginManager pinManager = new codeEditorPlugin.PulginManager();
            List<codeEditorPlugin.IPlugin> plugins = pinManager.LoadPlugIns(@"dlls\");
            foreach (var plugin in plugins)
            {
                plugin.Initialize();
            }

            if (System.IO.File.Exists(setupFileName))
            {
                Global.Setup.LoadSetup(setupFileName);
            }
        }
        private Tabs.EditorPage editorPage = new Tabs.EditorPage();

        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.Setup.SaveSetup(setupFileName);
        }

        private void addProject(Data.Project project)
        {
            navigatePanel.AddProject(project);
            Tools.ParseProjectForm pform = new Tools.ParseProjectForm(navigatePanel.GetPeojectNode(project.Name));
            pform.ShowDialog(this);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            editorPage.CodeEditor.Dispose();
        }


        // View controller interface //////////////////////////////////////////

        internal void Controller_AddProject(Data.Project project)
        {
            if (Global.Projects.ContainsKey(project.Name)) return;
            Global.Projects.Add(project.Name,project);
            addProject(project);
        }

        internal System.Windows.Forms.MenuStrip Controller_GetMenuStrip()
        {
            return menuStrip;
        }

        // tabs

        internal void Controller_AddTabPage(ajkControls.TabPage tabPage)
        {
            mainTab.TabPages.Add(tabPage);
        }

        internal void Controller_RemoveTabPage(ajkControls.TabPage tabPage)
        {
            mainTab.TabPages.Remove(tabPage);
        }

        // code editor

        internal void Controller_RefreshCodeEditor()
        {
            if (InvokeRequired)
            {
                editorPage.CodeEditor.Invoke(new Action(editorPage.CodeEditor.Refresh));
            }
            else
            {
                editorPage.CodeEditor.Refresh();
            }
        }

        internal void Controller_SetCodeEditorTextItem(Data.ITextFile textFile)
        {
            if(textFile == null)
            {
                editorPage.CodeEditor.SetTextFile(null);
                mainTab.TabPages[0].Text = "-";
            }
            else
            {
                editorPage.CodeEditor.SetTextFile(textFile);
                mainTab.TabPages[0].Text = textFile.Name;
                mainTab.SelectedTab = mainTab.TabPages[0];
            }
        }

        internal void Controller_ScrollToCaret()
        {
            editorPage.CodeEditor.ScrollToCaret();
        }

        // navigate panel
        internal void Controller_RefreshNavigatePanel()
        {
            navigatePanel.Refresh();
        }

        internal void Controller_UpdateNavigateaPanelWholeNode()
        {
            navigatePanel.UpdateWholeNode();
        }

        internal void Controller_UpdateNavigateaPanelWholeNode(NavigatePanel.NavigatePanelNode node)
        {
            navigatePanel.UpdateWholeNode(node);
        }

        internal void Controller_UpdateNavigateaPanelVisibleNode()
        {
            navigatePanel.UpdateWholeNode();
        }

        internal void Controller_UpdateNavigateaPanelVisibleNode(NavigatePanel.NavigatePanelNode node)
        {
            navigatePanel.UpdateWholeNode(node);
        }

        internal void Controller_UpdateNavigateaPanel(NavigatePanel.NavigatePanelNode node)
        {
            navigatePanel.UpdateWholeNode(node);
        }

        internal void Controller_GetNavigatePanelSelectedNode(out string project, out string id)
        {
            navigatePanel.GetSelectedNode(out project, out id);
        }

        internal System.Windows.Forms.ContextMenuStrip Controller_GetNavigateContextMenu()
        {
            return navigatePanel.GetContextMenuStrip();
        }

        // message view
        internal void Controller_UpdateMessageView(CodeEditor.ParsedDocument parsedDocument)
        {
            messageView.UpdateMessages(parsedDocument);
        }

        // log
        internal void Controller_LogAppend(string message)
        {
            logView.AppendLogLine(message);
        }
        internal void Controller_LogAppend(string message,System.Drawing.Color color)
        {
            logView.AppendLogLine(message, color);
        }

        // menu //////////////////////////////////////////////////////////////

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editorPage.CodeEditor.Save();
        }

        private void projectToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addNewProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tools.NewProjectFrom newProjectForm = new Tools.NewProjectFrom(this);
            newProjectForm.ShowDialog(this);
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editorPage.CodeEditor.OpenFind();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Form[] forms = this.OwnedForms;
            //for(int i= forms.Length-1; i >= 0; i--)
            //{
            //    forms[i].Close();
            //    forms[i].Dispose();
            //}
        }

        private void commandShellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ajkControls.ShellPanel panel = new ajkControls.ShellPanel(new ajkControls.CommandShell(new List<string> { "prompt $P$G$_" }));
            Controller.MainTabPage tab = new Controller.MainTabPage(panel, "command");
            tab.IconImage = Global.IconImages.Terminal;
            mainTab.TabPages.Add(tab);
            mainTab.SelectedTab = tab;
//            mainTab.Refresh();
        }
    }
}
