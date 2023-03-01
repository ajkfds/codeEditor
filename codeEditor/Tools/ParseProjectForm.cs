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
            this.BackColor = Controller.GetBackColor();
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
                List<Data.Item> items = projectNode.Project.FindItems(
                    (x) => (x is Data.TextFile ),
                    (x) => (false)
                    );

                Invoke(new Action(() => { progressBar.Maximum = items.Count; }));

                // parse items
                int i = 0;
                int gc = 0;
                foreach (Data.Item item in items)
                {
                    Invoke(new Action(() => { progressBar.Value = i; }));
                    i++;

                    if (!(item is Data.TextFile)) continue;
                    Data.TextFile textFile = item as Data.TextFile;
                    Invoke(new Action(() => { label.Text = textFile.Name; }));
                    CodeEditor.DocumentParser parser = textFile.CreateDocumentParser(CodeEditor.DocumentParser.ParseModeEnum.LoadParse);
                    if (parser == null) continue;
                    parser.Parse();

                    textFile.CodeDocument.CopyFrom(parser.Document);

                    if (textFile.ParsedDocument != null)
                    {
                        CodeEditor.ParsedDocument oldParsedDocument = textFile.ParsedDocument;
                        textFile.ParsedDocument = null;
                        oldParsedDocument.Dispose();
                    }

                    textFile.AcceptParsedDocument(parser.ParsedDocument);
                    
//                    textFile.ParseRequested = true;
                    textFile.Close();

                    gc++;
                    if (gc > 100)
                    {
                        System.GC.Collect();
                        gc = 0;
                        System.Diagnostics.Debug.Print("process memory " + (Environment.WorkingSet / 1024 / 1024).ToString() + "Mbyte");
                    }
                }
            }

            close = true;
            Invoke(new Action(()=> { Close(); }));
        }

    }
}
