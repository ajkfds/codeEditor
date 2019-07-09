using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace codeEditor.Tools
{
    public partial class ParseProjectForm : Form
    {
        public ParseProjectForm(NavigatePanel.ProjectNode projectNode)
        {
            InitializeComponent();
            Text = projectNode.Project.Name;
            this.projectNode = projectNode;
            this.Icon = ajkControls.Global.Icon;
            this.ShowInTaskbar = false;
        }

        private NavigatePanel.ProjectNode projectNode = null;
        private volatile bool close = false;


        private void ParseProjectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!close) e.Cancel = true;
            projectNode = null;
        }


        System.Threading.Thread thread = null;
        private void ParseProjectForm_Shown(object sender, EventArgs e)
        {
            if (thread != null) return;
            thread = new System.Threading.Thread(() => { worker(); });
            thread.Start();
        }

        private void worker()
        {
            projectNode.Project.Update();
            //int idCount = projectNode.Project.GetRegisteredIdList().Count;
            //while (idCount != projectNode.Project.GetRegisteredIdList().Count)
            //{
            //    List<string> ids = projectNode.Project.GetRegisteredIdList();
            //    idCount = ids.Count;
            //    for (int i = 0; i < ids.Count; i++)
            //    {
            //        Data.Item item = projectNode.Project.GetRegisterdItem(ids[i]);
            //        item.Update();
            //    }
            //}


            {
                // data update
                projectNode.HierarchicalVisibleUpdate();
                List<string> ids = projectNode.Project.GetRegisteredIdList();

                Invoke(new Action(() => { progressBar.Maximum = ids.Count; }));

                // parse items
                int i = 0;
                foreach (string id in ids)
                {
                    Data.Item item = projectNode.Project.GetRegisterdItem(id);
                    Invoke(new Action(() => { progressBar.Value = i; }));
                    i++;

                    if (!(item is Data.ITextFile)) continue;
                    Data.ITextFile textFile = item as Data.ITextFile;
                    Invoke(new Action(() => { label.Text = textFile.Name; }));
                    CodeEditor.DocumentParser parser = textFile.CreateDocumentParser(textFile.CodeDocument, textFile.ID, textFile.Project);
                    if (parser == null) continue;
                    parser.Parse(CodeEditor.DocumentParser.ParseMode.LoadParse);

                    textFile.CodeDocument.CopyColorsFrom(parser.Document);
                    textFile.CodeDocument.CopyMarksFrom(parser.Document);
                    //codeTextbox.Invoke(new Action(codeTextbox.Refresh));

                    if (textFile.ParsedDocument != null)
                    {
                        CodeEditor.ParsedDocument oldParsedDocument = textFile.ParsedDocument;
                        textFile.ParsedDocument = null;
                        oldParsedDocument.Dispose();
                    }

                    textFile.ParsedDocument = parser.ParsedDocument;
                    if (textFile.ParsedDocument != null) textFile.ParsedDocument.Accept();

                    textFile.ParseRequested = true;
                    textFile.Reload();
                }
            }

            close = true;
            Invoke(new Action(()=> { Close(); }));
        }

    }
}
