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
                int workerThreads = 6;

                System.Collections.Concurrent.BlockingCollection<Data.TextFile> fileQueue = new System.Collections.Concurrent.BlockingCollection<Data.TextFile>();

                List<TextParseTask> tasks = new List<TextParseTask>();
                for(int t = 0; t < workerThreads; t++)
                {
                    tasks.Add(new TextParseTask());
                    tasks[t].Run(
                        fileQueue,
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

                foreach (Data.Item item in items)
                {
                    if (!(item is Data.TextFile)) continue;
                    fileQueue.Add(item as Data.TextFile);
                }
                fileQueue.CompleteAdding();

                while (!fileQueue.IsCompleted)
                {
                    System.Threading.Thread.Sleep(1);
                }

                while (true)
                {
                    int completeTasks = 0;
                    foreach(TextParseTask task in tasks)
                    {
                        if (task.Complete) completeTasks++;
                    }
                    if (completeTasks == tasks.Count) break;
                }


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
