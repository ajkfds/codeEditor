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
            ajkControls.CodeTextbox.CodeDrawStyle codeDrawStyle1 = new ajkControls.CodeTextbox.CodeDrawStyle();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.collapseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expandAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertyTsmiSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.propertiesTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.subBgtimer = new System.Windows.Forms.Timer(this.components);
            this.codeTextbox = new ajkControls.CodeTextbox.CodeTextbox();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.collapseAllToolStripMenuItem,
            this.expandAllToolStripMenuItem,
            this.propertyTsmiSeparator,
            this.propertiesTsmi});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(241, 139);
            // 
            // collapseAllToolStripMenuItem
            // 
            this.collapseAllToolStripMenuItem.Name = "collapseAllToolStripMenuItem";
            this.collapseAllToolStripMenuItem.Size = new System.Drawing.Size(240, 32);
            this.collapseAllToolStripMenuItem.Text = "Collapse All";
            // 
            // expandAllToolStripMenuItem
            // 
            this.expandAllToolStripMenuItem.Name = "expandAllToolStripMenuItem";
            this.expandAllToolStripMenuItem.Size = new System.Drawing.Size(240, 32);
            this.expandAllToolStripMenuItem.Text = "Expand All";
            // 
            // propertyTsmiSeparator
            // 
            this.propertyTsmiSeparator.Name = "propertyTsmiSeparator";
            this.propertyTsmiSeparator.Size = new System.Drawing.Size(237, 6);
            // 
            // propertiesTsmi
            // 
            this.propertiesTsmi.Name = "propertiesTsmi";
            this.propertiesTsmi.Size = new System.Drawing.Size(240, 32);
            this.propertiesTsmi.Text = "Properties";
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 10;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // subBgtimer
            // 
            this.subBgtimer.Enabled = true;
            this.subBgtimer.Interval = 1;
            this.subBgtimer.Tick += new System.EventHandler(this.subBgtimer_Tick);
            // 
            // codeTextbox
            // 
            this.codeTextbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.codeTextbox.BlockUnderlineColor = System.Drawing.Color.LightGray;
            this.codeTextbox.CarletColor = System.Drawing.Color.LightGray;
            this.codeTextbox.CarletUnderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.codeTextbox.ContextMenuStrip = this.contextMenuStrip;
            this.codeTextbox.CrColor = System.Drawing.Color.LightGray;
            this.codeTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeTextbox.Document = null;
            this.codeTextbox.Editable = true;
            this.codeTextbox.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeTextbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(212)))));
            this.codeTextbox.LeftColumnColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.codeTextbox.LfColor = System.Drawing.Color.LightGray;
            this.codeTextbox.LineNumberColor = System.Drawing.Color.Gray;
            this.codeTextbox.Location = new System.Drawing.Point(0, 0);
            this.codeTextbox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.codeTextbox.MultiLine = true;
            this.codeTextbox.Name = "codeTextbox";
            this.codeTextbox.ScrollBarVisible = true;
            this.codeTextbox.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(79)))), ((int)(((byte)(120)))));
            this.codeTextbox.Size = new System.Drawing.Size(635, 551);
            this.codeTextbox.Style = codeDrawStyle1;
            this.codeTextbox.TabColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.codeTextbox.TabIndex = 0;
            this.codeTextbox.CarletLineChanged += new System.EventHandler(this.codeTextbox_CarletLineChanged);
            this.codeTextbox.AfterKeyPressed += new System.Windows.Forms.KeyPressEventHandler(this.codeTextbox_AfterKeyPressed);
            this.codeTextbox.AfterKeyDown += new System.Windows.Forms.KeyEventHandler(this.codeTextbox_AfterKeyDown);
            this.codeTextbox.BeforeKeyPressed += new System.Windows.Forms.KeyPressEventHandler(this.codeTextbox_BeforeKeyPressed);
            this.codeTextbox.BeforeKeyDown += new System.Windows.Forms.KeyEventHandler(this.codeTextbox_BeforeKeyDown);
            this.codeTextbox.SelectionChanged += new System.Action(this.CodeTextbox_SelectionChanged);
            this.codeTextbox.Load += new System.EventHandler(this.codeTextbox_Load);
            this.codeTextbox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.codeTextbox_MouseDoubleClick);
            this.codeTextbox.MouseLeave += new System.EventHandler(this.codeTextbox_MouseLeave);
            this.codeTextbox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.codeTextbox_MouseMove);
            // 
            // CodeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.codeTextbox);
            this.Font = new System.Drawing.Font("MS UI Gothic", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Name = "CodeEditor";
            this.Size = new System.Drawing.Size(635, 551);
            this.Load += new System.EventHandler(this.CodeEditor_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal ajkControls.CodeTextbox.CodeTextbox codeTextbox;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem propertiesTsmi;
        private System.Windows.Forms.Timer subBgtimer;
        private System.Windows.Forms.ToolStripMenuItem collapseAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expandAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator propertyTsmiSeparator;
    }
}
