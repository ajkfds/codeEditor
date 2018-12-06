using System.Windows.Forms;

namespace codeEditor.NavigatePanel
{
    public partial class NavigatePanel : UserControl
    {
//        public event EventHandler<ajkControls.TreeNode> SelectedNodeChanged;

        public NavigatePanel()
        {
            InitializeComponent();
        }

        public void AddProject(Data.Project project)
        {
            ProjectNode pNode = new ProjectNode(project);
            treeView.TreeNodes.Add(pNode);
            pNode.Update();
            treeView.Refresh();
        }

        public ProjectNode GetPeojectNode(string projectName)
        {
            ProjectNode ret = null;
            foreach(ajkControls.TreeNode node in treeView.TreeNodes)
            {
                if(node is ProjectNode)
                {
                    ProjectNode pnode = node as ProjectNode;
                    if (pnode.Project.Name == projectName) ret = pnode;
                }
            }
            return ret;
        }

        private void treeView_SelectedNodeChanged(object sender, ajkControls.TreeNode e)
        {
            NavigatePanelNode node = e as NavigatePanelNode;
            if (node == null) return;

            node.Update();
            foreach(NavigatePanelNode subNode in node.TreeNodes)
            {
                subNode.Update();
            }

            node.Selected();
            //            if (SelectedNodeChanged != null) SelectedNodeChanged(this, e);
        }

        public void UpdateWholeNode()
        {
            foreach (NavigatePanelNode subNode in treeView.TreeNodes)
            {
                subNode.HierarchicalUpdate();
            }
        }

        private void treeView_NodeClicked(object sender, ajkControls.TreeView.NodeClickedEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            System.Drawing.Point screenPosition = PointToScreen(e.Location);
            contextMenuStrip.Show(screenPosition);
        }

        private void propertyToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            NavigatePanelNode node = treeView.SelectedNode as NavigatePanelNode;
            if (node == null) return;
            node.ShowProperyForm();
        }
    }
}
