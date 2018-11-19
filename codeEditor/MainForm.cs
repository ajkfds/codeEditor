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
        public MainForm()
        {
            InitializeComponent();
            Global.Controller = new ViewControl.Controller(this);

            // register filetype
            FileTypes.TextFile textFileType = new FileTypes.TextFile();
            Global.FileTypes.Add(textFileType.ID, textFileType);


            codeEditorPlugin.PulginManager pinManager = new codeEditorPlugin.PulginManager();
            List<codeEditorPlugin.IPlugin> plugins = pinManager.LoadPlugIns(@"dlls\");
            foreach (var plugin in plugins)
            {
                plugin.Initialize();
            }
            
/*
            ajkControls.Style.ColorPallet = new Color[16]
            {
                Color.DimGray,                   // default
                Color.LightGray,                 // inactivated
                Color.DarkGray,                  // 2
                Color.Crimson,                   // variable-heavy
                Color.MediumBlue,                // keyword
                Color.ForestGreen,               // comment
                Color.CadetBlue,                 // identifier
                Color.Orchid,                    // variable-fixed
                Color.SandyBrown,                // number
                Color.Salmon,                    // variable-light
                Color.Green,                     // highlighted comment
                Color.Black,                     // 11
                Color.Black,                     // 12
                Color.Black,                     // 13
                Color.Black,                     // 14
                Color.Black                      // 15
            };
            // #708090           SlateGray
*/
        }

        public void AddProject(Data.Project project)
        {
            navigatePanel.AddProject(project);
            Tools.ParseProjectForm pform = new Tools.ParseProjectForm(navigatePanel.GetPeojectNode(project.Name));
            pform.ShowDialog(this);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            codeEditor.Dispose();
        }


        // View controller interface //////////////////////////////////////////

        public void Controller_AddProject(string absolutePath)
        {
            Data.Project project = Data.Project.Create(absolutePath);
            AddProject(project);
        }

        // code editor

        public void Controller_RefreshCodeEditor()
        {
            if (InvokeRequired)
            {
                codeEditor.Invoke(new Action(codeEditor.Refresh));
            }
            else
            {
                codeEditor.Refresh();
            }
        }

        public void Controller_SetCodeEditorTextItem(Data.ITextFile textFile)
        {
            if(textFile == null)
            {
                codeEditor.SetTextFile(null);
                mainTab.TabPages[0].Text = "-";
            }
            else
            {
                codeEditor.SetTextFile(textFile);
                mainTab.TabPages[0].Text = textFile.Name;
            }
        }

        public void Controller_ScrollToCaret()
        {
            codeEditor.ScrollToCaret();
        }

        // navigate panel
        public void Controller_RefreshNavigatePanel()
        {
            navigatePanel.Refresh();
        }

        public void Controller_UpdateNavigateaPanel()
        {
            navigatePanel.UpdateWholeNode();
        }

        // message view
        public void Controller_UpdateMessageView(CodeEditor.ParsedDocument parsedDocument)
        {
            messageView.UpdateMessages(parsedDocument);
        }

        // menu //////////////////////////////////////////////////////////////

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            codeEditor.Save();
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
            codeEditor.OpenFind();
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
    }
}
