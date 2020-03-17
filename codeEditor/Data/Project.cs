using System;
using System.Collections.Generic;
using System.Linq;

namespace codeEditor.Data
{
    public class Project : Folder
    {
        protected Project() { }

        public static Project Create(string rootPath)
        {
            Project project = new Project();
            project.RootPath = rootPath;
            if (project.RootPath.Contains('\\'))
            {
                project.Name = project.RootPath.Substring(project.RootPath.LastIndexOf('\\') + 1);
            }
            else
            {
                project.Name = project.RootPath;
            }

            initProject(project);
            return project;
        }
        public static Project Create(ajkControls.JsonReader jsonReader)
        {
            Project project = new Project();
            project.Name = jsonReader.Key;
            project.LoadSetup(jsonReader);

            initProject(project);
            return project;
        }

        private static void initProject(Project project)
        {
            project.RelativePath = "";
            project.Project = project;
            project.startFileSystemWatcher();
        }

        public override void Dispose()
        {
            stopFileSystemWatcher();
            base.Dispose();
        }

        protected System.IO.FileSystemWatcher fileSystemWatcher;
        protected void startFileSystemWatcher()
        {
            fileSystemWatcher = new System.IO.FileSystemWatcher();
            fileSystemWatcher.Path = RootPath;
            fileSystemWatcher.NotifyFilter =
                (System.IO.NotifyFilters.LastAccess
                | System.IO.NotifyFilters.LastWrite
                | System.IO.NotifyFilters.FileName
                | System.IO.NotifyFilters.DirectoryName);
            fileSystemWatcher.Filter = "";
            fileSystemWatcher.IncludeSubdirectories = true;

            fileSystemWatcher.Created += FileSystemWatcher_Created;
            fileSystemWatcher.Changed += FileSystemWatcher_Changed;
            fileSystemWatcher.Renamed += FileSystemWatcher_Renamed;
            fileSystemWatcher.Deleted += FileSystemWatcher_Deleted;

            fileSystemWatcher.EnableRaisingEvents = true;
        }

        private void stopFileSystemWatcher()
        {
            fileSystemWatcher.EnableRaisingEvents = false;
            fileSystemWatcher.Dispose();
            fileSystemWatcher = null;
        }

        public static Dictionary<string, Func<Project, ProjectProperty>> ProjectPropertyCreated = new Dictionary<string, Func<Project, ProjectProperty>>(); 

        private Dictionary<string, ProjectProperty> projectProperties = new Dictionary<string, ProjectProperty>();
        public ProjectProperty GetProjectProperty(string pluginID)
        {
            if (projectProperties.ContainsKey(pluginID))
            {
                return projectProperties[pluginID];
            }
            else
            {
                if (ProjectPropertyCreated.ContainsKey(pluginID))
                {
                    projectProperties.Add(pluginID,ProjectPropertyCreated[pluginID](this));
                    return projectProperties[pluginID];
                }
                else
                {
                    return null;
                }
            }
        }
//        public new string ID { get { return Name; } }
        public string RootPath { get; protected set; }
        public new string Name { get; protected set; }




        // get parse target
        private List<Item> parseItems = new List<Item>();
        public Item FetchReparseTarget()
        {
            lock (parseItems)
            {
                Item item = parseItems.FirstOrDefault();
                if (item == null) return null;
                while(
                    (item as TextFile) != null &&
                    (item as TextFile).ParsedDocument != null &&
                    (item as TextFile).IsCodeDocumentCashed &&
                    (item as TextFile).CodeDocument != null &&
                    (item as TextFile).ParsedDocument.EditID == (item as TextFile).CodeDocument.EditID
                    )
                {
                    item = parseItems.FirstOrDefault();
                    if (item == null) return null;
                    parseItems.Remove(item);
                }
                parseItems.Remove(item);
                return item;
            }
        }

        public void AddReparseTarget(Item item)
        {
            lock (parseItems)
            {
                if (!parseItems.Contains(item))
                {
                    parseItems.Add(item);
                }
            }
        }

        // path control
        public string GetAbsolutePath(string relativaPath)
        {
            string basePath = RootPath;
            if (!basePath.EndsWith(@"\")) basePath += @"\";
            string filePath = relativaPath;

            basePath = basePath.Replace("%", "%25");
            filePath = filePath.Replace("%", "%25");

            Uri u1 = new Uri(basePath);
            Uri u2 = new Uri(u1, filePath);
            string absolutePath = u2.LocalPath;
            absolutePath = absolutePath.Replace("%25", "%");

            return absolutePath;
        }

        public string GetRelativePath(string fullPath)
        {
            string basePath = RootPath;
            if (!basePath.EndsWith(@"\")) basePath += @"\";
            Uri u1 = new Uri(basePath);
            Uri u2 = new Uri(fullPath);
            Uri relativeUri = u1.MakeRelativeUri(u2);
            string relativePath = relativeUri.ToString();

            relativePath = relativePath.Replace('/', '\\');
            return relativePath;
        }

        // save

        public void SaveSetup(ajkControls.JsonWriter writer)
        {
            writer.writeKeyValue("RootPath", RootPath);

            using (var blockWriter = writer.GetObjectWriter("PluginProperties"))
            {
                foreach (var propertyKvp in projectProperties)
                {
                    using (var propertyWriter = blockWriter.GetObjectWriter(propertyKvp.Key))
                    {
                        propertyKvp.Value.SaveSetup(propertyWriter);
                    }
                }
            }
        }

        public void LoadSetup(ajkControls.JsonReader jsonReader)
        {
            using (var reader = jsonReader.GetNextObjectReader())
            {
                while (true)
                {
                    string key = reader.GetNextKey();
                    if (key == null) break;

                    switch (key)
                    {
                        case "RootPath":
                            RootPath = reader.GetNextStringValue();
                            break;
                        case "PluginProperties":
                            readProjectProperties(reader);
                            break;
                        default:
                            reader.SkipValue();
                            break;
                    }

                }
            }
        }

        private void readProjectProperties(ajkControls.JsonReader jsonReader)
        {
            using (var reader = jsonReader.GetNextObjectReader())
            {
                while (true)
                {
                    string key = reader.GetNextKey();
                    if (key == null) break;

                    Data.ProjectProperty property = GetProjectProperty(key);
                    if(property == null)
                    {
                        reader.SkipValue();
                        continue;
                    }
                    property.LoadSetup(reader);
                }
            }
        }

        // file system watcher
        private void FileSystemWatcher_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            Controller.AppendLog(e.Name + " changed");
            string relativePath = GetRelativePath(e.FullPath);
            Data.File file = GetItem(relativePath) as Data.File;
            if (file == null) return;
            Data.ITextFile textFile = file as Data.ITextFile;
            if (textFile == null) return;
            textFile.CloseRequested = true;
        }

        private void FileSystemWatcher_Renamed(object sender, System.IO.RenamedEventArgs e)
        {
            Controller.AppendLog(e.Name + " renamed");
            Item item = GetItem(GetRelativePath(e.FullPath));
            if (item.Parent != null) item.Parent.Update();
        }

        private void FileSystemWatcher_Created(object sender, System.IO.FileSystemEventArgs e)
        {
            Controller.AppendLog(e.Name + " created");
            Item item = GetItem(GetRelativePath(e.FullPath));
            if (item.Parent != null) item.Parent.Update();
        }

        private void FileSystemWatcher_Deleted(object sender, System.IO.FileSystemEventArgs e)
        {
            Controller.AppendLog(e.Name + " deleted");
            Item item  = GetItem(GetRelativePath(e.FullPath));
            if (item.Parent != null) item.Parent.Update();
        }
    }
}