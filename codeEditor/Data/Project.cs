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
            project.RelativePath = "";
            project.Project = project;
            return project;
        }

        public static Project Create(ajkControls.JsonReader jsonReader)
        {
            Project project = new Project();
            project.Name = jsonReader.Key;
            project.LoadSetup(jsonReader);
            project.RelativePath = "";
            project.Project = project;
            return project;
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
                System.Diagnostics.Debugger.Break();
                return null;
            }
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

    }
}