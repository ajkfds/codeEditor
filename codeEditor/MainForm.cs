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
            Global.mainForm = this;

            InitializeComponent();
            ajkControls.Global.Icon = Properties.Resources.ajEditor;
            this.Icon = ajkControls.Global.Icon;

            editorPage.BackColor = this.BackColor;
            mainTab.TabPages.Add(editorPage);

            menuStrip.ImageScalingSize = new Size(menuStrip.Font.Height, menuStrip.Font.Height);
            menuStrip.BackColor = BackColor;
            mainTab.ImageList.Images.Add( new Bitmap(menuStrip.Font.Height, menuStrip.Font.Height));

            mainTab.FromBackgroundColor = BackColor;
            subTab.FromBackgroundColor = BackColor;

            commandShellToolStripMenuItem.Image = Global.IconImages.Terminal.GetImage(
                menuStrip.ImageScalingSize.Height,
                ajkControls.IconImage.ColorStyle.White
                );
            saveToolStripMenuItem.Image = Global.IconImages.SaveFile.GetImage(
                menuStrip.ImageScalingSize.Height,
                ajkControls.IconImage.ColorStyle.White
                );

            // register filetype
            FileTypes.TextFile textFileType = new FileTypes.TextFile();
            Global.FileTypes.Add(textFileType.ID, textFileType);

            codeEditorPlugin.PulginManager pinManager = new codeEditorPlugin.PulginManager();
            List<codeEditorPlugin.IPlugin> plugins = pinManager.LoadPlugIns(@"dlls\");
            while (true)
            {
                int registered = 0;
                foreach (var plugin in plugins)
                {
                    if (!Global.Plugins.ContainsKey(plugin.Id))
                    {
                        bool complete = plugin.Register();
                        if (complete)
                        {
                            registered++;
                            Global.Plugins.Add(plugin.Id,plugin);
                            Controller .AppendLog("Loading plugin ... "+plugin.Id);
                        }
                    }
                }
                if (registered == 0) break;
            }


            if (System.IO.File.Exists(setupFileName))
            {
                Global.Setup.LoadSetup(setupFileName);
            }

            List<string> initilalizedPulginName = new List<string>();
            while (true)
            {
                int initialized = 0;
                foreach(string pluginName in Global.Plugins.Keys)
                {
                    if (initilalizedPulginName.Contains(pluginName)) continue;
                    if (Global.Plugins[pluginName].Initialize())
                    {
                        initialized++;
                        Controller.AppendLog("Loading plugin ... " + pluginName);
                        initilalizedPulginName.Add(pluginName);
                    }
                }
                if (initialized == 0) break;
            }

        }


        internal Tabs.EditorPage editorPage = new Tabs.EditorPage();

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
        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editorPage.CodeEditor.OpenReplace();
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
            Tabs.MainTabPage tab = new Tabs.MainTabPage(panel, "command");
            tab.IconImage = Global.IconImages.Terminal;
            mainTab.TabPages.Add(tab);
            mainTab.SelectedTab = tab;
//            mainTab.Refresh();
        }

        private void ForceGCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GC.Collect(2);
        }

        private void CasheStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(var project in Global.Projects.Values)
            {
                System.Diagnostics.Debug.Print("## project : " + project.Name);
//                project.DumpItemsStatus();
            }
        }

    }
}
