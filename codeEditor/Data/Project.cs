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
//            fileSystemWatcher.SynchronizingObject = this;

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


        private Dictionary<string, ProjectProperty> projectProperties = new Dictionary<string, ProjectProperty>();
        public Dictionary<string, ProjectProperty> ProjectProperties
        {
            get { return projectProperties; }
        }

        public new string ID { get { return Name; } }
        public string RootPath { get; protected set; }
        public new string Name { get; protected set; }

        private Dictionary<string, Item> wholeItems = new Dictionary<string, Item>();
        private Dictionary<string, int> wholeItemReferenceCounts = new Dictionary<string, int>();
        private List<string> wholeKeys = new List<string>();

        public List<string> GetRegisteredIdList()
        {
            return wholeItems.Keys.ToList();
        }

        public void RegisterProjectItem(Item projectItem)
        {
            if (wholeItems.ContainsKey(projectItem.ID))
            {
                wholeItemReferenceCounts[projectItem.ID]++;
            }
            else
            {
                wholeItemReferenceCounts.Add(projectItem.ID, 1);
                wholeItems.Add(projectItem.ID,projectItem);
                wholeKeys.Add(projectItem.ID);
            }
        }

        public void RemoveRegisteredItem(Item projectItem)
        {
            if (wholeItems.ContainsKey(projectItem.ID))
            {
                wholeItemReferenceCounts[projectItem.ID]--;
                if(wholeItemReferenceCounts[projectItem.ID] == 0)
                {
                    wholeItemReferenceCounts.Remove(projectItem.ID);
                    wholeItems.Remove(projectItem.ID);
                    wholeKeys.Remove(projectItem.ID);
                }
            }
            else
            {
                System.Diagnostics.Debugger.Break();
            }
        }

        public bool IsRegistered(string id)
        {
            if (wholeItems.ContainsKey(id))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Item GetRegisterdItem(string id)
        {
            if(id == null)
            {
                System.Diagnostics.Debugger.Break();
            }
            if (wholeItems.ContainsKey(id))
            {
                return wholeItems[id];
            }
            else
            {
//                System.Diagnostics.Debugger.Break();
                return null;
            }
        }

        // get parse target

        private int parseIndex = 0;
        private int parseSearchLimit = 100;
        public ITextFile GetReparseTarget()
        {

            int i = 0;
            while (i < parseSearchLimit)
            {
                if (parseIndex >= wholeItems.Count)
                {
                    parseIndex = 0;
                    return null;
                }
                string key = wholeKeys[parseIndex];
                Item item = wholeItems[key];
                if(item is ITextFile)
                {
                    ITextFile textFile = item as ITextFile;
                    if (textFile.ParseRequested) return textFile;
                    if (textFile.ReloadRequested) return textFile;
                }
                parseIndex++;
                i++;
            }
            return null;
        }

        // path control
        public string GetAbsolutePath(string relativaPath)
        {
            string basePath = RootPath;
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
            Uri u1 = new Uri(RootPath);
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
                foreach (var propertyKvp in ProjectProperties)
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

                    if (projectProperties.ContainsKey(key))
                    {
                        projectProperties[key].LoadSetup(reader);
                    }
                    else
                    {
                        reader.SkipValue();
                    }
                }
            }
        }

        // file system watcher
        private void FileSystemWatcher_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            Global.Controller.AppendLog(e.Name + " changed");
            string relativePath = GetRelativePath(e.FullPath);
            string id = Data.File.GetID(relativePath, this);
            Data.File file = GetRegisterdItem(id) as Data.File;
            if (file == null) return;
            Data.ITextFile textFile = file as Data.ITextFile;
            if (textFile == null) return;
            textFile.ReloadRequested = true;
        }

        private void FileSystemWatcher_Renamed(object sender, System.IO.RenamedEventArgs e)
        {
            Global.Controller.AppendLog(e.Name + " renamed");

        }

        private void FileSystemWatcher_Created(object sender, System.IO.FileSystemEventArgs e)
        {
            Global.Controller.AppendLog(e.Name + " created");

        }

        private void FileSystemWatcher_Deleted(object sender, System.IO.FileSystemEventArgs e)
        {
            Global.Controller.AppendLog(e.Name + " deleted");

        }
    }
}