using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeEditor
{
    public static class Global
    {
        public static Dictionary<string, FileTypes.FileType> FileTypes = new Dictionary<string, FileTypes.FileType>();
        public static Dictionary<string, Data.Project> Projects = new Dictionary<string, Data.Project>();
        public static Dictionary<string, codeEditorPlugin.IPlugin> Plugins = new Dictionary<string, codeEditorPlugin.IPlugin>();

        public static Setups.Setup Setup = new Setups.Setup();
        public static Dictionary<string, codeEditorPlugin.PluginSetup> PluginSetups = new Dictionary<string, codeEditorPlugin.PluginSetup>();
        internal static MainForm mainForm;

        public static ajkControls.CodeDrawStyle DefaultDrawStyle = new ajkControls.CodeDrawStyle();

        public static class IconImages
        {
            public static ajkControls.IconImage Terminal = new ajkControls.IconImage(Properties.Resources.terminal);
            public static ajkControls.IconImage Text = new ajkControls.IconImage(Properties.Resources.text);
            public static ajkControls.IconImage SaveFile = new ajkControls.IconImage(Properties.Resources.saveFile);
            public static ajkControls.IconImage Wave0 = new ajkControls.IconImage(Properties.Resources.wave0);
            public static ajkControls.IconImage Wave1 = new ajkControls.IconImage(Properties.Resources.wave1);
            public static ajkControls.IconImage Wave2 = new ajkControls.IconImage(Properties.Resources.wave2);
            public static ajkControls.IconImage Wave3 = new ajkControls.IconImage(Properties.Resources.wave3);
            public static ajkControls.IconImage Wave4 = new ajkControls.IconImage(Properties.Resources.wave4);
            public static ajkControls.IconImage Wave5 = new ajkControls.IconImage(Properties.Resources.wave5);
            public static ajkControls.IconImage Play = new ajkControls.IconImage(Properties.Resources.play);
            public static ajkControls.IconImage Pause = new ajkControls.IconImage(Properties.Resources.pause);
            public static ajkControls.IconImage Git = new ajkControls.IconImage(Properties.Resources.tree);
            public static ajkControls.IconImage NewBadge = new ajkControls.IconImage(Properties.Resources.newBadge);
        }

        public static class ColorMap
        {
            public static System.Drawing.Color DarkBackground = System.Drawing.Color.FromArgb(0x20, 0x38, 0x64);
            public static System.Drawing.Color LightBackground = System.Drawing.Color.FromArgb(0x32, 0x59, 0xa0);
            public static System.Drawing.Color SelectedBackground = System.Drawing.Color.FromArgb(32, 56, 100); //;System.Drawing.Color.FromArgb(0xa9, 0xba, 0xda);
        }

    }
}
