﻿using System;
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

        public virtual void LoadSetup(ajkControls.JsonReader reader)
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
