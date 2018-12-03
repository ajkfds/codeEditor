using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace codeEditor.Setups
{
    public class Setup
    {

        public void SaveSetup(string path)
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(path))
            {
                using (ajkControls.JsonWriter writer = new ajkControls.JsonWriter(sw))
                {
                    writeJson(writer);
                }
            }
        }

        public void LoadSetup(string path)
        {
            using( System.IO.StreamReader sr = new System.IO.StreamReader(path))
            {
                using ( ajkControls.JsonReader reader = new ajkControls.JsonReader(sr))
                {
                    readJson(reader);
                }
            }
        }

        private void readJson(ajkControls.JsonReader reader)
        {
            while (true)
            {
                string key = reader.GetNextKey();
                if (key == null) break;

                switch (key)
                {
                    case "codeEditor":
                        using(var block = reader.GetNextObjectReader())
                        {
                            readCodeEdotorSetup(block);
                        }
                        break;
                    //case "PliginSetups":
                    //    break;
                    //case "Projects":
                    //    break;
                    default:
                        reader.SkipValue();
                        break;
                }
            }
        }

        private void readCodeEdotorSetup(ajkControls.JsonReader reader)
        {
            while (true)
            {
                string key = reader.GetNextKey();
                if (key == null) break;

                switch (key)
                {
                    case "ApplicationName":
                        string applicationName = reader.GetNextStringValue();
                        if (applicationName != "codeEditor") throw new Exception("illegal format");
                        break;
                    case "LastUpdate":
                        string lastUpdate = reader.GetNextStringValue();
                        break;
                    default:
                        reader.SkipValue();
                        break;
                }
            }
        }

        private void readPluginSetup(ajkControls.JsonReader reader)
        {
            while (true)
            {
                string key = reader.GetNextKey();
                if (key == null) break;

                if (Global.PluginSetups.ContainsKey(key))
                {
                    using (var block = reader.GetNextObjectReader())
                    {
                        Global.PluginSetups[key].ReadJson(block);
                        readCodeEdotorSetup(block);
                    }
                }
                else
                {
                    reader.SkipValue();
                }
            }
        }

        private void readProjects(ajkControls.JsonReader reader)
        {
            while (true)
            {
                string key = reader.GetNextKey();
                if (key == null) break;

                if (Global.Projects.ContainsKey(key))
                {
                    using (var block = reader.GetNextObjectReader())
                    {
                        Global.Projects[key].LoadSetup(block);
                        readCodeEdotorSetup(block);
                    }
                }
                else
                {
                    reader.SkipValue();
                }
            }
        }


        private void writeJson(ajkControls.JsonWriter writer)
        {
            using (var blockWriter = writer.GetObjectWriter("codeEditor"))
            {
                blockWriter.writeKeyValue("ApplicationName", "codeEditor");
                blockWriter.writeKeyValue("LastUpdate", DateTime.Now.ToString());
            }

            using (var blockWriter = writer.GetObjectWriter("PluginSetups"))
            {
                foreach(var pluginKvp in Global.PluginSetups)
                {
                    using(var pluginWriter = blockWriter.GetObjectWriter(pluginKvp.Key))
                    {
                        pluginKvp.Value.SaveSetup(pluginWriter);
                    }
                }
            }

            using(var blockWriter = writer.GetObjectWriter("Projects"))
            {
                foreach(var projectKvp in Global.Projects)
                {
                    using (var projectWriter = blockWriter.GetObjectWriter(projectKvp.Key))
                    {
                        projectKvp.Value.SaveSetup(projectWriter);
                    }
                }
            }

        }
    }
}
