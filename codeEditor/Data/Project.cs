﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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
        protected System.Windows.Forms.Timer fsTimer = new System.Windows.Forms.Timer();
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

            fsTimer.Interval = 10;
            fsTimer.Tick += fsTimer_Tick;
            fsTimer.Start();
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
                    projectProperties.Add(pluginID, ProjectPropertyCreated[pluginID](this));
                    return projectProperties[pluginID];
                }
                else
                {
                    return null;
                }
            }
        }
        public string RootPath { get; protected set; }
        public new string Name { get; protected set; }




        // get parse target
        //        private List<Item> parseItems = new List<Item>();

        private Dictionary<string, Item> parseItems = new Dictionary<string, Item>();
        public Item FetchReparseTarget()
        {
            lock (parseItems)
            {
                Item item = parseItems.Values.FirstOrDefault();
                if (item == null) return null;
                while (
                    (item as TextFile) != null &&
                    (item as TextFile).ParsedDocument != null &&
                    (item as TextFile).IsCodeDocumentCashed &&
                    (item as TextFile).CodeDocument != null &&
                    (item as TextFile).ParsedDocument.EditID == (item as TextFile).CodeDocument.EditID
                    )
                {
                    item = parseItems.Values.FirstOrDefault();
                    if (item == null) return null;
                    parseItems.Remove(item.ID);
                }
                parseItems.Remove(item.ID);
                return item;
            }
        }

        public void AddReparseTarget(Item item)
        {
            lock (parseItems)
            {
                if (!parseItems.ContainsKey(item.ID))
                {
                    System.Diagnostics.Debug.Print("entry add parse:" + item.ID);
                    parseItems.Add(item.ID, item);
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
                    if (property == null)
                    {
                        reader.SkipValue();
                        continue;
                    }
                    property.LoadSetup(reader);
                }
            }
        }

        // file system watcher

        Dictionary<string, FileSystemEventArgs> fileSystemEvents = new Dictionary<string, FileSystemEventArgs>();

        private void addFileSystemEvent(System.IO.FileSystemEventArgs e)
        {
            lock (fileSystemEvents)
            {
                while (fileSystemEvents.ContainsKey(e.FullPath))
                {
                    System.IO.FileSystemEventArgs prevE = fileSystemEvents[e.FullPath];
                    switch (prevE.ChangeType)
                    {
                        case WatcherChangeTypes.Changed:
                            fileSystemEvents.Remove(prevE.FullPath);
                            break;
                        case WatcherChangeTypes.Created:
                            fileSystemEvents.Remove(prevE.FullPath);
                            break;
                        case WatcherChangeTypes.Deleted:
                            fileSystemEvents.Remove(prevE.FullPath);
                            break;
                        case WatcherChangeTypes.Renamed:
                            fileSystemEvents.Remove(prevE.FullPath);
                            break;
                    }
                }
                fileSystemEvents.Add(e.FullPath, e);
//                fsTimer.Enabled = true;
            }
        }

        private void FileSystemWatcher_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            addFileSystemEvent(e);
        }

        private void FileSystemWatcher_Renamed(object sender, System.IO.RenamedEventArgs e)
        {
            addFileSystemEvent(e);
        }

        private void FileSystemWatcher_Created(object sender, System.IO.FileSystemEventArgs e)
        {
            addFileSystemEvent(e);
        }

        private void FileSystemWatcher_Deleted(object sender, System.IO.FileSystemEventArgs e)
        {
            addFileSystemEvent(e);
        }

        private void fsTimer_Tick(object sender, System.EventArgs e)
        {
            lock (fileSystemEvents)
            {
                while (fileSystemEvents.Count != 0)
                {
                    System.IO.FileSystemEventArgs fs = fileSystemEvents.Values.FirstOrDefault();
                    fileSystemEvents.Remove(fs.FullPath);
                    {
                        Controller.AppendLog(fs.Name + " changed");
                        string relativePath = GetRelativePath(fs.FullPath);
                        Data.File file = GetItem(relativePath) as Data.File;
                        if (file == null) return;
                        Data.ITextFile textFile = file as Data.ITextFile;
                        if (textFile == null) return;
                        if (textFile.Dirty)
                        {
                            Controller.AppendLog(fs.FullPath + " conflict!");
                        }
                        else
                        {
                            DateTime lastWriteTime = System.IO.File.GetLastWriteTime(fs.FullPath);
                            if(textFile.LoadedFileLastWriteTime != lastWriteTime)
                            {
                                textFile.LoadFormFile();
                                textFile.Update();
                            }
                        }
                    }

                }
//                fsTimer.Enabled = false;
            }
        }

    }
}