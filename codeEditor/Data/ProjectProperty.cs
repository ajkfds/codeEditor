using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeEditor.Data
{
    public class ProjectProperty
    {
        public virtual void SaveSetup(ajkControls.JsonWriter writer)
        {

        }

        public virtual void LoadSetup(ajkControls.JsonReader jsonReader)
        {
            using (var reader = jsonReader.GetNextObjectReader())
            {
                while (true)
                {
                    string key = reader.GetNextKey();
                    if (key == null) break;

                    reader.SkipValue();
                }
            }
        }

        private void readMacros(ajkControls.JsonReader reader)
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
                    }
                }
                else
                {
                    reader.SkipValue();
                }
            }
        }

    }
}
