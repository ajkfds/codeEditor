using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace codeEditor.NavigatePanel
{
    public class FileNode : NavigatePanelNode
    {
        public FileNode(string ID, Data.Project project) : base(ID, project)
        {
            if (FileNodeCreated != null) FileNodeCreated(this);
        }
        public static Action<FileNode> FileNodeCreated;

        public virtual Data.File FileItem
        {
            get { return Project.GetRegisterdItem(ID) as Data.File; }
        }

        public override string Text
        {
            get { return FileItem.Name; }
        }

        public override void Selected()
        {
            Data.ITextFile textFile = FileItem as Data.ITextFile;
//            ViewControl.ViewController.

        }
    }
}
