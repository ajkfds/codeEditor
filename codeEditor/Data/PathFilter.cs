using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeEditor.Data
{
    public class PathFilter
    {

        public List<PathFilterItem> Items = new List<PathFilterItem>();


        public void SaveSetup(ajkControls.JsonWriter writer)
        {
//            writer.writeKeyValue("RootPath", RootPath);

            using (var blockWriter = writer.GetObjectWriter("Items"))
            {
                foreach (var item in Items)
                {
                    using (var propertyWriter = blockWriter.GetObjectWriter("PathFilterItem"))
                    {
                        item.SaveSetup(propertyWriter);
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
                        case "PathFilterItem":
                            break;
                        default:
                            reader.SkipValue();
                            break;
                    }

                }
            }
        }



        public class PathFilterItem
        {
            public enum PathFilterType
            {
                Ignore,
                Add
            }

            public PathFilterType Type = PathFilterType.Ignore;
            public String PathFilterString = "";


            public void Run()
            {

            }

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

        }


    }
}
