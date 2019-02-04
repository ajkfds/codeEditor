using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeEditor
{
    public static class Global
    {
        public static ViewControl.Controller Controller;
        public static Dictionary<string, FileTypes.FileType> FileTypes = new Dictionary<string, FileTypes.FileType>();
        public static Dictionary<string, Data.Project> Projects = new Dictionary<string, Data.Project>();

        public static Setups.Setup Setup = new Setups.Setup();
        public static Dictionary<string, codeEditorPlugin.PluginSetup> PluginSetups = new Dictionary<string, codeEditorPlugin.PluginSetup>();


        public static ajkControls.CodeDrawStyle DefaultDrawStyle = new ajkControls.CodeDrawStyle();


        public static class IconImages
        {
            public static ajkControls.IconImage Terminal = new ajkControls.IconImage(Properties.Resources.terminal);
            public static ajkControls.IconImage Text = new ajkControls.IconImage(Properties.Resources.text);
        }

        public static class ColorMap
        {
            public static System.Drawing.Color DarkBackground = System.Drawing.Color.FromArgb(0x20, 0x38, 0x64);
            public static System.Drawing.Color LightBackground = System.Drawing.Color.FromArgb(0x32, 0x59, 0xa0);
            public static System.Drawing.Color SelectedBackground = System.Drawing.Color.FromArgb(32, 56, 100); //;System.Drawing.Color.FromArgb(0xa9, 0xba, 0xda);
        }

    }
}
