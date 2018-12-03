using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeEditorPlugin
{
    public class PluginSetup
    {
        public void SaveSetup (ajkControls.JsonWriter writer)
        {
            
        }

        public void ReadJson(ajkControls.JsonReader reader)
        {
            while (true)
            {
                string key = reader.GetNextKey();
                if (key == null) break;
                reader.SkipValue();
            }

        }
    }
}
