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
        public static Folder Create(string relativePath, Project project, Item parent)
        {
            string id = GetID(relativePath,project);
            //if (project.IsRegistered(id))
            //{
            //    Folder item = project.GetRegisterdItem(id) as Folder;
            //    project.RegisterProjectItem(item);
            //    return item;
            //}

            Folder folder = new Folder();
            folder.Project = project;
            folder.RelativePath = relativePath;

            if (relativePath.Contains('\\'))
            {
                folder.Name = relativePath.Substring(relativePath.LastIndexOf('\\') + 1);
            }
            else
            {
                folder.Name = relativePath;
            }

            folder.Parent = parent;
            //project.RegisterProjectItem(folder);
            
            return folder;
        }

        public static string GetID(string relativePath, Project project)
        {
            return project.ID + ":Folder:" + relativePath;
        }


        public override void Update()
        {
            string absolutePath = Project.GetAbsolutePath(RelativePath);
            string[] absoluteFilePaths = System.IO.Directory.GetFiles(absolutePath);
            string[] absoluteFolderPaths = System.IO.Directory.GetDirectories(absolutePath);

            foreach (string absoluteFilePath in absoluteFilePaths)
            {
                string relativePath = Project.GetRelativePath(absoluteFilePath);
                string name;
                if (relativePath.Contains('\\'))
                {
                    name = relativePath.Substring(relativePath.LastIndexOf('\\') + 1);
                }
                else
                {
                    name = relativePath;
                }

                if (!items.ContainsKey(name))
                {
                    File item = File.Create(Project.GetRelativePath(absoluteFilePath), Project,this);
                    items.Add(item.Name,item);
                }
            }

            foreach (string absoluteFolderPath in absoluteFolderPaths)
            {
                // skip invisiable folder
                string body = absoluteFolderPath;
                if (body.Contains('\\')) body = body.Substring(body.LastIndexOf('\\')+1);
                if (body.StartsWith(".")) continue;

                if (!items.ContainsKey(body))
                {
                    Folder item = Folder.Create(Project.GetRelativePath(absoluteFolderPath), Project,this);
                    items.Add(item.Name, item);
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
                items.Remove(item.Name);
                item.Dispose();
            }
        }

        public override NavigatePanelNode CreateNode()
        {
            return new FolderNode(this);
        }
    }
}
