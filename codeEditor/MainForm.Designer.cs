﻿using System.Drawing;

namespace codeEditor
{
    partial class MainForm
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

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commandShellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forceGCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.mainTab = new ajkControls.TabControl();
            this.subTab = new ajkControls.TabControl();
            this.navigatorPage = new System.Windows.Forms.TabPage();
            this.navigatePanel = new codeEditor.NavigatePanel.NavigatePanel();
            this.logView = new ajkControls.LogView();
            this.messageView = new codeEditor.MessageView.MessageView();
            this.menuStrip.SuspendLayout();
            this.subTab.SuspendLayout();
            this.navigatorPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Font = new System.Drawing.Font("Meiryo UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.projectToolStripMenuItem,
            this.toolToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(5, 3, 0, 3);
            this.menuStrip.Size = new System.Drawing.Size(1110, 34);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.saveAllToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(47, 28);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(187, 30);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAllToolStripMenuItem
            // 
            this.saveAllToolStripMenuItem.Name = "saveAllToolStripMenuItem";
            this.saveAllToolStripMenuItem.Size = new System.Drawing.Size(187, 30);
            this.saveAllToolStripMenuItem.Text = "SaveAll";
            this.saveAllToolStripMenuItem.Click += new System.EventHandler(this.saveAllToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findToolStripMenuItem,
            this.replaceToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(51, 28);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            this.findToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.findToolStripMenuItem.Size = new System.Drawing.Size(212, 30);
            this.findToolStripMenuItem.Text = "Find";
            this.findToolStripMenuItem.Click += new System.EventHandler(this.findToolStripMenuItem_Click);
            // 
            // replaceToolStripMenuItem
            // 
            this.replaceToolStripMenuItem.Name = "replaceToolStripMenuItem";
            this.replaceToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.replaceToolStripMenuItem.Size = new System.Drawing.Size(212, 30);
            this.replaceToolStripMenuItem.Text = "Replace";
            // 
            // projectToolStripMenuItem
            // 
            this.projectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewProjectToolStripMenuItem});
            this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            this.projectToolStripMenuItem.Size = new System.Drawing.Size(76, 28);
            this.projectToolStripMenuItem.Text = "Project";
            this.projectToolStripMenuItem.Click += new System.EventHandler(this.projectToolStripMenuItem_Click);
            // 
            // addNewProjectToolStripMenuItem
            // 
            this.addNewProjectToolStripMenuItem.Name = "addNewProjectToolStripMenuItem";
            this.addNewProjectToolStripMenuItem.Size = new System.Drawing.Size(218, 30);
            this.addNewProjectToolStripMenuItem.Text = "Add new project";
            this.addNewProjectToolStripMenuItem.Click += new System.EventHandler(this.addNewProjectToolStripMenuItem_Click);
            // 
            // toolToolStripMenuItem
            // 
            this.toolToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.commandShellToolStripMenuItem,
            this.debugToolStripMenuItem});
            this.toolToolStripMenuItem.Name = "toolToolStripMenuItem";
            this.toolToolStripMenuItem.Size = new System.Drawing.Size(61, 28);
            this.toolToolStripMenuItem.Text = "Tools";
            // 
            // commandShellToolStripMenuItem
            // 
            this.commandShellToolStripMenuItem.Name = "commandShellToolStripMenuItem";
            this.commandShellToolStripMenuItem.Size = new System.Drawing.Size(207, 30);
            this.commandShellToolStripMenuItem.Text = "CommandShell";
            this.commandShellToolStripMenuItem.Click += new System.EventHandler(this.commandShellToolStripMenuItem_Click);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.forceGCToolStripMenuItem});
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(207, 30);
            this.debugToolStripMenuItem.Text = "Debug";
            // 
            // forceGCToolStripMenuItem
            // 
            this.forceGCToolStripMenuItem.Name = "forceGCToolStripMenuItem";
            this.forceGCToolStripMenuItem.Size = new System.Drawing.Size(161, 30);
            this.forceGCToolStripMenuItem.Text = "Force GC";
            this.forceGCToolStripMenuItem.Click += new System.EventHandler(this.ForceGCToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(56, 28);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 502);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1110, 3);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // splitter2
            // 
            this.splitter2.Location = new System.Drawing.Point(272, 34);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 468);
            this.splitter2.TabIndex = 6;
            this.splitter2.TabStop = false;
            // 
            // splitter3
            // 
            this.splitter3.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter3.Location = new System.Drawing.Point(903, 34);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(3, 468);
            this.splitter3.TabIndex = 12;
            this.splitter3.TabStop = false;
            // 
            // mainTab
            // 
            this.mainTab.BackgroundColor = System.Drawing.Color.White;
            this.mainTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTab.Font = new System.Drawing.Font("Meiryo UI", 8F);
            this.mainTab.ItemSize = new System.Drawing.Size(80, 33);
            this.mainTab.LineColor = System.Drawing.Color.Black;
            this.mainTab.Location = new System.Drawing.Point(275, 34);
            this.mainTab.Multiline = true;
            this.mainTab.Name = "mainTab";
            this.mainTab.Padding = new System.Drawing.Point(15, 3);
            this.mainTab.SelectedBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(56)))), ((int)(((byte)(100)))));
            this.mainTab.SelectedForeColor = System.Drawing.Color.White;
            this.mainTab.SelectedIndex = 0;
            this.mainTab.Size = new System.Drawing.Size(628, 468);
            this.mainTab.TabIndex = 10;
            this.mainTab.UnselectedBackgroundColor = System.Drawing.Color.LightGray;
            // 
            // subTab
            // 
            this.subTab.BackgroundColor = System.Drawing.Color.White;
            this.subTab.Controls.Add(this.navigatorPage);
            this.subTab.Dock = System.Windows.Forms.DockStyle.Left;
            this.subTab.Font = new System.Drawing.Font("Meiryo UI", 8F);
            this.subTab.ItemSize = new System.Drawing.Size(80, 33);
            this.subTab.LineColor = System.Drawing.Color.Black;
            this.subTab.Location = new System.Drawing.Point(0, 34);
            this.subTab.Multiline = true;
            this.subTab.Name = "subTab";
            this.subTab.Padding = new System.Drawing.Point(15, 3);
            this.subTab.SelectedBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(56)))), ((int)(((byte)(100)))));
            this.subTab.SelectedForeColor = System.Drawing.Color.White;
            this.subTab.SelectedIndex = 0;
            this.subTab.Size = new System.Drawing.Size(272, 468);
            this.subTab.TabIndex = 9;
            this.subTab.UnselectedBackgroundColor = System.Drawing.Color.Gray;
            // 
            // navigatorPage
            // 
            this.navigatorPage.Controls.Add(this.navigatePanel);
            this.navigatorPage.Location = new System.Drawing.Point(4, 37);
            this.navigatorPage.Name = "navigatorPage";
            this.navigatorPage.Padding = new System.Windows.Forms.Padding(3);
            this.navigatorPage.Size = new System.Drawing.Size(264, 427);
            this.navigatorPage.TabIndex = 0;
            this.navigatorPage.Text = "navigator";
            this.navigatorPage.UseVisualStyleBackColor = true;
            // 
            // navigatePanel
            // 
            this.navigatePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navigatePanel.Location = new System.Drawing.Point(3, 3);
            this.navigatePanel.Name = "navigatePanel";
            this.navigatePanel.Size = new System.Drawing.Size(258, 421);
            this.navigatePanel.TabIndex = 0;
            // 
            // logView
            // 
            this.logView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.logView.ForeColor = System.Drawing.Color.Gray;
            this.logView.Location = new System.Drawing.Point(0, 505);
            this.logView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.logView.MaxLogs = 200;
            this.logView.Name = "logView";
            this.logView.Size = new System.Drawing.Size(1110, 122);
            this.logView.TabIndex = 0;
            // 
            // messageView
            // 
            this.messageView.Dock = System.Windows.Forms.DockStyle.Right;
            this.messageView.Font = new System.Drawing.Font("Meiryo UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.messageView.Location = new System.Drawing.Point(906, 34);
            this.messageView.Name = "messageView";
            this.messageView.Size = new System.Drawing.Size(204, 468);
            this.messageView.TabIndex = 11;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1110, 627);
            this.Controls.Add(this.mainTab);
            this.Controls.Add(this.splitter3);
            this.Controls.Add(this.messageView);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.subTab);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.logView);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Consolas", 8F);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "codeEditor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.subTab.ResumeLayout(false);
            this.navigatorPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ajkControls.LogView logView;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Splitter splitter2;
        private NavigatePanel.NavigatePanel navigatePanel;
        private ajkControls.TabControl subTab;
        private System.Windows.Forms.TabPage navigatorPage;
        private ajkControls.TabControl mainTab;
        private MessageView.MessageView messageView;
        private System.Windows.Forms.Splitter splitter3;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem replaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem commandShellToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem forceGCToolStripMenuItem;
    }
}

