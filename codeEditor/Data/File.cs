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
        public static File Create(string relativePath, Project project)
        {
            foreach (var fileType in Global.FileTypes.Values)
            {
                if (fileType.IsThisFileType(relativePath, project)) return fileType.CreateFile(relativePath, project);
            }

            string id = File.GetID(relativePath, project);
            if (project.IsRegistered(id))
            {
                File item = project.GetRegisterdItem(id) as File;
                project.RegisterProjectItem(item);
                return item;
            }

            File fileItem = new File();
            fileItem.Project = project;
            fileItem.RelativePath = relativePath;
            fileItem.ID = id;
            if (relativePath.Contains('\\'))
            {
                fileItem.Name = relativePath.Substring(relativePath.LastIndexOf('\\') + 1);
            }
            else
            {
                fileItem.Name = relativePath;
            }

            project.RegisterProjectItem(fileItem);
            return fileItem;
        }

        public static string GetID(string relativePath, Project project)
        {
            return project.ID + ":File:" + relativePath;
        }

        public override  NavigatePanelNode CreateNode()
        {
            return new FileNode(ID, Project);
        }

        public override void Dispose()
        {
            base.Dispose(); // remove from project
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
