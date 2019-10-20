using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using codeEditor.NavigatePanel;

namespace codeEditor.Data
{
    public class Folder : Item
    {
        protected Folder() { }
        public static Folder Create(string relativePath,Project project)
        {
            string id = GetID(relativePath,project);
            if (project.IsRegistered(id))
            {
                Folder item = project.GetRegisterdItem(id) as Folder;
                project.RegisterProjectItem(item);
                return item;
            }

            Folder folder = new Folder();
            folder.Project = project;
            folder.RelativePath = relativePath;
            folder.ID = id;

            if (relativePath.Contains('\\'))
            {
                folder.Name = relativePath.Substring(relativePath.LastIndexOf('\\') + 1);
            }
            else
            {
                folder.Name = relativePath;
            }

            project.RegisterProjectItem(folder);
            return folder;
        }

        public static string GetID(string relativePath, Project project)
        {
            return project.ID + ":Folder:" + relativePath;
        }

        public override void DisposeItem()
        {
            base.DisposeItem();
        }

        public override void Update()
        {
            string absolutePath = Project.GetAbsolutePath(RelativePath);
            string[] absoluteFilePaths = System.IO.Directory.GetFiles(absolutePath);
            string[] absoluteFolderPaths = System.IO.Directory.GetDirectories(absolutePath);

            foreach (string absoluteFilePath in absoluteFilePaths)
            {
                string id = File.GetID(Project.GetRelativePath(absoluteFilePath), Project);
                if (!items.ContainsKey(id))
                {
                    File item = File.Create(Project.GetRelativePath(absoluteFilePath), Project);
                    items.Add(item.ID, item);
                }
            }

            foreach (string absoluteFolderPath in absoluteFolderPaths)
            {
                // skip invisiable folder
                string body = absoluteFolderPath;
                if (body.Contains('\\')) body = body.Substring(body.LastIndexOf('\\'));
                if (body.StartsWith("\\.")) continue;

                string id = Folder.GetID(Project.GetRelativePath(absoluteFolderPath), Project);
                if (!items.ContainsKey(id))
                {
                    Folder item = Folder.Create(Project.GetRelativePath(absoluteFolderPath), Project);
                    items.Add(item.ID, item);
                    item.Update();
                }
            }

            List<Item> removeItems = new List<Item>();
            foreach(Item item in items.Values)
            {
                string absoluteItemPath = Project.GetAbsolutePath(item.RelativePath);
                if(!absoluteFilePaths.Contains(absoluteItemPath) && !absoluteFolderPaths.Contains(absoluteItemPath))
                {
                    removeItems.Add(item);
                }
            }
            foreach(Item item in removeItems)
            {
                items.Remove(item.ID);
                item.DisposeItem();
            }
        }

        public override NavigatePanelNode CreateNode()
        {
            return new FolderNode(ID, Project);
        }
    }
}
