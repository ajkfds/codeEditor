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


        public static class Icons
        {
            public static ajkControls.Icon Terminal = new ajkControls.Icon(Properties.Resources.terminal);
            public static ajkControls.Icon Text = new ajkControls.Icon(Properties.Resources.text);
        }
    }
}
