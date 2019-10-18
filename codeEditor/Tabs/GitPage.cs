using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeEditor.Tabs
{
    public class GitPage : Tabs.MainTabPage
    {
        public GitPage(Data.Project project) : base(new ajkControls.Git.GitPanel(project.RootPath),project.Name)
        {
            IconImage = codeEditor.Global.IconImages.Git;
        }

    }
}
