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
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.propertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeView = new ajkControls.TreeView();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.toolStripSeparator1,
            this.propertyToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(241, 103);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(240, 30);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(237, 6);
            // 
            // propertyToolStripMenuItem
            // 
            this.propertyToolStripMenuItem.Name = "propertyToolStripMenuItem";
            this.propertyToolStripMenuItem.Size = new System.Drawing.Size(240, 30);
            this.propertyToolStripMenuItem.Text = "Property";
            this.propertyToolStripMenuItem.Click += new System.EventHandler(this.propertyToolStripMenuItem_Click);
            // 
            // treeView
            // 
            this.treeView.BackColor = System.Drawing.Color.White;
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Font = new System.Drawing.Font("Meiryo UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.treeView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.treeView.HScrollBarVisible = true;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(399, 618);
            this.treeView.TabIndex = 0;
            this.treeView.VScrollBarVisible = true;
            this.treeView.SelectedNodeChanged += new System.EventHandler<ajkControls.TreeNode>(this.treeView_SelectedNodeChanged);
            this.treeView.NodeClicked += new ajkControls.TreeView.NodeClickedEventHandler(this.treeView_NodeClicked);
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
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem propertyToolStripMenuItem;
    }
}
