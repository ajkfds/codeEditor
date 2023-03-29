using System.Windows.Forms;

namespace codeEditor.NavigatePanel
{
    public partial class NavigatePanel : UserControl
    {
//        public event EventHandler<ajkControls.TreeNode> SelectedNodeChanged;

        public NavigatePanel()
        {
            InitializeComponent();
            
            contextMenuStrip.ImageScalingSize = new System.Drawing.Size(contextMenuStrip.Font.Height, contextMenuStrip.Font.Height);
            gitLogTsmi.Image = Global.IconImages.Git.GetImage(
               contextMenuStrip.ImageScalingSize.Height,
               ajkControls.IconImage.ColorStyle.White);
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
        
        public System.Windows.Forms.ContextMenuStrip GetContextMenuStrip()
        {
            return contextMenuStrip;
        }

        public void GetSelectedNode(out NavigatePanelNode node)
        {
            node = treeView.SelectedNode as NavigatePanelNode;
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

            foreach(ToolStripItem tsi in contextMenuStrip.Items)
            {
                if (tsi == propertyTsmi) continue;
                if (tsi == propertySeparator) continue;
                if (tsi == deleteTsmi) continue;
                if (tsi == addTsmi) continue;
                if (tsi == parseHierarchyTsmi) continue;
                tsi.Visible = false;
            }
            node.Selected();
            //            if (SelectedNodeChanged != null) SelectedNodeChanged(this, e);
        }

        public void UpdateWholeVisibleNode()
        {
            foreach (NavigatePanelNode subNode in treeView.TreeNodes)
            {
                subNode.HierarchicalVisibleUpdate();
            }
        }

        public void UpdateWholeVisibleNode(NavigatePanelNode node)
        {
            node.HierarchicalVisibleUpdate();
        }

        //public void UpdateWholeNode()
        //{
        //    foreach (NavigatePanelNode subNode in treeView.TreeNodes)
        //    {
        //        subNode.HierarchicalUpdate();
        //    }
        //}

        //public void UpdateWholeNode(NavigatePanelNode node)
        //{
        //    foreach (NavigatePanelNode subNode in node.TreeNodes)
        //    {
        //        subNode.HierarchicalUpdate();
        //    }
        //}

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

        private void GitLogTsmi_Click(object sender, System.EventArgs e)
        {
            ProjectNode node = treeView.SelectedNode as ProjectNode;
            if (node == null) return;

            Controller.Tabs.AddPage(new Tabs.GitPage(node.Project));
        }
        private void ignoreTsmi_Click(object sender, System.EventArgs e)
        {
            NavigatePanelNode node = treeView.SelectedNode as NavigatePanelNode;
            if (node == null) return;
            node.Item.Ignore = !node.Item.Ignore;
            node.Update();
            treeView.Invalidate();
        }

        private void openWithExploererTsmi_Click(object sender, System.EventArgs e)
        {
            NavigatePanelNode node = treeView.SelectedNode as NavigatePanelNode;
            if (node == null) return;

            if(node is FolderNode)
            {
                Data.Folder folder = (node as FolderNode).Folder;
                if (folder == null || folder.Project == null) return;
                System.Diagnostics.Process.Start("EXPLORER.EXE", folder.Project.GetAbsolutePath(folder.RelativePath));

            }else if(node is FileNode)
            {
                Data.File file = (node as FileNode).FileItem;
                if (file == null || file.Project == null) return;
                System.Diagnostics.Process.Start("EXPLORER.EXE", "/select,\""+file.Project.GetAbsolutePath(file.RelativePath)+"\"");
            }
        }

        private void parseHierarchyTsmi_Click(object sender, System.EventArgs e)
        {
            codeEditor.NavigatePanel.NavigatePanelNode node;
            codeEditor.Controller.NavigatePanel.GetSelectedNode(out node);
            if (node == null) return;

            Tools.ParseHierarchyForm form = new Tools.ParseHierarchyForm(node);
            Controller.ShowForm(form);

        }
    }
}
