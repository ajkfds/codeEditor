﻿namespace codeEditor.CodeEditor
{
    partial class CodeEditor
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
            this.codeTextbox = new ajkControls.CodeTextbox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // codeTextbox
            // 
            this.codeTextbox.BackColor = System.Drawing.Color.White;
            this.codeTextbox.ContextMenuStrip = this.contextMenuStrip;
            this.codeTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeTextbox.Document = null;
            this.codeTextbox.Editable = true;
            this.codeTextbox.Font = new System.Drawing.Font("Consolas", 6F);
            this.codeTextbox.ForeColor = System.Drawing.Color.Black;
            this.codeTextbox.Location = new System.Drawing.Point(0, 0);
            this.codeTextbox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.codeTextbox.Name = "codeTextbox";
            this.codeTextbox.ScrollBarVisible = true;
            this.codeTextbox.Size = new System.Drawing.Size(635, 551);
            this.codeTextbox.TabIndex = 0;
            this.codeTextbox.CarletLineChanged += new System.EventHandler(this.codeTextbox_CarletLineChanged);
            this.codeTextbox.AfterKeyPressed += new System.Windows.Forms.KeyPressEventHandler(this.codeTextbox_AfterKeyPressed);
            this.codeTextbox.AfterKeyDown += new System.Windows.Forms.KeyEventHandler(this.codeTextbox_AfterKeyDown);
            this.codeTextbox.Load += new System.EventHandler(this.codeTextbox_Load);
            this.codeTextbox.DoubleClick += new System.EventHandler(this.codeTextbox_DoubleClick);
            this.codeTextbox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.codeTextbox_MouseDoubleClick);
            this.codeTextbox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.codeTextbox_MouseMove);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.propertiesToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(165, 34);
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 10;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(240, 30);
            this.propertiesToolStripMenuItem.Text = "Properties";
            // 
            // CodeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.codeTextbox);
            this.Font = new System.Drawing.Font("MS UI Gothic", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Name = "CodeEditor";
            this.Size = new System.Drawing.Size(635, 551);
            this.Load += new System.EventHandler(this.CodeEditor_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ajkControls.CodeTextbox codeTextbox;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
    }
}
