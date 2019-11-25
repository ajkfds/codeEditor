using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using codeEditor.NavigatePanel;


namespace codeEditor.Data
{
    public class File : Item
    {
        public static File Create(string relativePath, Project project, Item parent)
        {
            foreach (var fileType in Global.FileTypes.Values)
            {
                if (fileType.IsThisFileType(relativePath, project)) return fileType.CreateFile(relativePath, project);
            }

            string id = File.GetID(relativePath, project);
            //if (project.IsRegistered(id))
            //{
            //    File item = project.GetRegisterdItem(id) as File;
            //    project.RegisterProjectItem(item);
            //    return item;
            //}

            File fileItem = new File();
            fileItem.Project = project;
            fileItem.RelativePath = relativePath;
            if (relativePath.Contains('\\'))
            {
                fileItem.Name = relativePath.Substring(relativePath.LastIndexOf('\\') + 1);
            }
            else
            {
                fileItem.Name = relativePath;
            }

            fileItem.Parent = parent;

            if (FileCreated != null) FileCreated(fileItem);
            return fileItem;
        }
        public static Action<File> FileCreated;

        public static string GetID(string relativePath, Project project)
        {
            return project.ID + ":File:" + relativePath;
        }

        public override  NavigatePanelNode CreateNode()
        {
            return new FileNode(this);
        }

        public FileTypes.FileType FileType
        {
            get { return null; }
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
