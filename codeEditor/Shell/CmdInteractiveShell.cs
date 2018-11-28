using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeEditor.Shell
{
    public class CmdInteractiveShell : InteractiveShell
    {
        System.Diagnostics.Process process = null;

        public delegate void ReceivedHandler(string lineString);
        public event ReceivedHandler LineReceived;

        public void Start()
        {
            process = new System.Diagnostics.Process();

            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = false;
            process.StartInfo.CreateNoWindow = true;
            process.OutputDataReceived += outputDataReceived;
            process.ErrorDataReceived += errorDataReceived;
            process.StartInfo.RedirectStandardInput = false;

            process.StartInfo.FileName = System.Environment.GetEnvironmentVariable("ComSpec"); // cmd.exe
            process.StartInfo.Arguments = "";// @"/c dir c:\ /w"; // /c to close after execute

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            process.WaitForExit();
            process.Close();
        }

        public void Execute(string command)
        {
            System.IO.StreamWriter sw = process.StandardInput;
            if (sw.BaseStream.CanWrite)
            {
                sw.WriteLine(command);
            }
            sw.Close();
        }

        public void WaitPrompt()
        {

        }


        List<string> logs = new List<string>();
        private const int maxLogs = 100;

        public string GetLastLine()
        {
            lock (logs)
            {
                if(logs.Count == 0)
                {
                    return "";
                }
                else
                {
                    return logs.Last();
                }
            }
        }

        private void outputDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            lock (logs)
            {
                logs.Add(e.Data);
                if (logs.Count > maxLogs) logs.RemoveAt(0);
                if (LineReceived != null) LineReceived(e.Data);
            }
        }


        private void errorDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            lock (logs)
            {
                logs.Add(e.Data);
                if (logs.Count > maxLogs) logs.RemoveAt(0);
                if (LineReceived != null) LineReceived(e.Data);
            }
        }
    }


}
