using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace codeEditor.GitPanel
{
    public partial class GitPanel : UserControl,IDisposable
    {
        public GitPanel(Data.Project project)
        {
            this.project = project;
            InitializeComponent();
            shell.Start();

            shell.LineReceived += shell_Received;
        }

        public void Dispose()
        {
            shell.Dispose();
        }

        Data.Project project;
        private ajkControls.CommandShell shell = new ajkControls.CommandShell();

        private void LogButton_Click(object sender, EventArgs e)
        {

            string path = project.GetAbsolutePath("");
            shell.Execute("cd " + path);
            shell.Execute("");
            shell.StartLogging();
            shell.Execute(@"git log --pretty=""format:<%H> <%cd> <%an> %s %d "" --date=format:""%Y/%m/%d %H:%M:%S"" --all --graph");
            shell.Execute("echo <complete>");
            while (!shell.GetLastLine().EndsWith("echo <complete>"))
            {
                System.Threading.Thread.Sleep(10);
            }
            List<string> lines = shell.GetLogs();
            shell.EndLogging();

            tableView.TableItems.Clear();
            foreach (string line in lines)
            {
                GitCommit commit = GitCommit.Create(line);
                if (commit == null) continue;
                tableView.TableItems.Add(commit);
            }
            tableView.Refresh();

        }

//        private List<GitCommit> logs = new List<GitCommit>();


        private void shell_Received(string line)
        {
            logView.AppendLogLine(line);
        }


    }
}
