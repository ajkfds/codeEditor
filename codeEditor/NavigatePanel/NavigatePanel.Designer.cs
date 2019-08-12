using System.Drawing;

namespace codeEditor.NavigatePanel
{
    partial class NavigatePanel
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.gitLogTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.propertySeparator = new System.Windows.Forms.ToolStripSeparator();
            this.propertyTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.treeView = new ajkControls.TreeView();
            this.openWithExploererToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Font = new System.Drawing.Font("Meiryo UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addTsmi,
            this.deleteTsmi,
            this.gitLogTsmi,
            this.propertySeparator,
            this.propertyTsmi,
            this.openWithExploererToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(241, 163);
            // 
            // addTsmi
            // 
            this.addTsmi.Name = "addTsmi";
            this.addTsmi.Size = new System.Drawing.Size(240, 24);
            this.addTsmi.Text = "Add";
            // 
            // deleteTsmi
            // 
            this.deleteTsmi.Name = "deleteTsmi";
            this.deleteTsmi.Size = new System.Drawing.Size(240, 24);
            this.deleteTsmi.Text = "Delete";
            // 
            // gitLogTsmi
            // 
            this.gitLogTsmi.Name = "gitLogTsmi";
            this.gitLogTsmi.Size = new System.Drawing.Size(240, 24);
            this.gitLogTsmi.Text = "Git Log";
            this.gitLogTsmi.Click += new System.EventHandler(this.GitLogTsmi_Click);
            // 
            // propertySeparator
            // 
            this.propertySeparator.Name = "propertySeparator";
            this.propertySeparator.Size = new System.Drawing.Size(237, 6);
            // 
            // propertyTsmi
            // 
            this.propertyTsmi.Name = "propertyTsmi";
            this.propertyTsmi.Size = new System.Drawing.Size(240, 24);
            this.propertyTsmi.Text = "Property";
            this.propertyTsmi.Click += new System.EventHandler(this.propertyToolStripMenuItem_Click);
            // 
            // treeView
            // 
            this.treeView.BackColor = System.Drawing.Color.White;
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.treeView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.treeView.HScrollBarVisible = true;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.treeView.Name = "treeView";
            this.treeView.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(224)))), ((int)(((byte)(232)))));
            this.treeView.Size = new System.Drawing.Size(399, 618);
            this.treeView.TabIndex = 0;
            this.treeView.VScrollBarVisible = true;
            this.treeView.SelectedNodeChanged += new System.EventHandler<ajkControls.TreeNode>(this.treeView_SelectedNodeChanged);
            this.treeView.NodeClicked += new ajkControls.TreeView.NodeClickedEventHandler(this.treeView_NodeClicked);
            // 
            // openWithExploererToolStripMenuItem
            // 
            this.openWithExploererToolStripMenuItem.Name = "openWithExploererToolStripMenuItem";
            this.openWithExploererToolStripMenuItem.Size = new System.Drawing.Size(240, 24);
            this.openWithExploererToolStripMenuItem.Text = "Open With Exploerer";
            // 
            // NavigatePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeView);
            this.Name = "NavigatePanel";
            this.Size = new System.Drawing.Size(399, 618);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ajkControls.TreeView treeView;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem deleteTsmi;
        private System.Windows.Forms.ToolStripSeparator propertySeparator;
        private System.Windows.Forms.ToolStripMenuItem propertyTsmi;
        public System.Windows.Forms.ToolStripMenuItem gitLogTsmi;
        private System.Windows.Forms.ToolStripMenuItem addTsmi;
        private System.Windows.Forms.ToolStripMenuItem openWithExploererToolStripMenuItem;
    }
}
