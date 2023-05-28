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
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            projectNode.Project.Update();

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
                int workerThreads = 4;
                List<List<Data.TextFile>> filesThreads = new List<List<Data.TextFile>>();
                
                for(int t = 0; t < workerThreads; t++)
                {
                    filesThreads.Add(new List<Data.TextFile>());
                }

                int p = 0;
                foreach (Data.Item item in items)
                {
                    if (!(item is Data.TextFile)) continue;
                    filesThreads[p].Add(item as Data.TextFile);
                    p++;
                    if (p >= workerThreads) p =0;
                }

                List<TextParseTask> tasks = new List<TextParseTask>();
                for(int t = 0; t < workerThreads; t++)
                {
                    tasks.Add(new TextParseTask());
                    tasks[t].Run(
                        filesThreads[t],
                        (
                            (f) =>
                            {
                                Invoke(
                                    new Action(() => { 
                                        progressBar.Value = i;
                                        label.Text = f.Name;
                                        i++;
                                    })
                                    );
                            }
                        )
                    );
                }

                while (true)
                {
                    int completes = 0;
                    foreach(TextParseTask task in tasks)
                    {
                        if (task.Complete) completes++;
                    }
                    if (completes == workerThreads) break;
                    System.Diagnostics.Debug.Print("comp"+completes.ToString());
                }


                //int gc = 0;
                //foreach (Data.Item item in items)
                //{
                //    Invoke(new Action(() => { progressBar.Value = i; }));
                //    i++;

                //    if (!(item is Data.TextFile)) continue;
                //    Data.TextFile textFile = item as Data.TextFile;
                //    Invoke(new Action(() => { label.Text = textFile.Name; }));
                //    CodeEditor.DocumentParser parser = textFile.CreateDocumentParser(CodeEditor.DocumentParser.ParseModeEnum.LoadParse);
                //    if (parser == null) continue;
                //    parser.Parse();

                //    textFile.CodeDocument.CopyFrom(parser.Document);

                //    if (textFile.ParsedDocument != null)
                //    {
                //        CodeEditor.ParsedDocument oldParsedDocument = textFile.ParsedDocument;
                //        textFile.ParsedDocument = null;
                //        oldParsedDocument.Dispose();
                //    }

                //    textFile.AcceptParsedDocument(parser.ParsedDocument);
                //    textFile.Close();

                //    gc++;
                //    if (gc > 100)
                //    {
                //        System.GC.Collect();
                //        gc = 0;
                //        System.Diagnostics.Debug.Print("process memory " + (Environment.WorkingSet / 1024 / 1024).ToString() + "Mbyte");
                //    }
                //}
            }
            
            System.Diagnostics.Debug.Print(projectNode.Project.Name + ":" + sw.ElapsedMilliseconds.ToString() + "ms");
            close = true;
            Invoke(new Action(()=> { Close(); }));
        }

        


        private void timer_Tick(object sender, EventArgs e)
        {
            progressBar.Update();
            Update();
        }
    }
}
