namespace codeEditor.Tools
{
    partial class ProjectPropertyForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectPropertyForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.okBtn = new System.Windows.Forms.Button();
            this.tabControl = new ajkControls.TabControl();
            this.mainPage = new System.Windows.Forms.TabPage();
            this.ignoreListTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.projectRootPathTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.mainPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cancelBtn);
            this.panel1.Controls.Add(this.okBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 448);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(4);
            this.panel1.Size = new System.Drawing.Size(800, 52);
            this.panel1.TabIndex = 1;
            // 
            // cancelBtn
            // 
            this.cancelBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.cancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelBtn.Location = new System.Drawing.Point(588, 4);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(104, 44);
            this.cancelBtn.TabIndex = 1;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // okBtn
            // 
            this.okBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.okBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.okBtn.Location = new System.Drawing.Point(692, 4);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(104, 44);
            this.okBtn.TabIndex = 0;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // tabControl
            // 
            this.tabControl.BackgroundColor = System.Drawing.Color.White;
            this.tabControl.Controls.Add(this.mainPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.FromBackgroundColor = System.Drawing.SystemColors.Control;
            this.tabControl.ItemSize = new System.Drawing.Size(0, 33);
            this.tabControl.LineColor = System.Drawing.Color.Black;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.Padding = new System.Drawing.Point(15, 3);
            this.tabControl.SelectedBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(56)))), ((int)(((byte)(100)))));
            this.tabControl.SelectedForeColor = System.Drawing.Color.White;
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(800, 448);
            this.tabControl.TabIndex = 0;
            this.tabControl.UnselectedBackgroundColor = System.Drawing.Color.LightGray;
            // 
            // mainPage
            // 
            this.mainPage.BackColor = System.Drawing.Color.White;
            this.mainPage.Controls.Add(this.ignoreListTxt);
            this.mainPage.Controls.Add(this.label2);
            this.mainPage.Controls.Add(this.projectRootPathTxt);
            this.mainPage.Controls.Add(this.label1);
            this.mainPage.Location = new System.Drawing.Point(4, 37);
            this.mainPage.Name = "mainPage";
            this.mainPage.Padding = new System.Windows.Forms.Padding(3);
            this.mainPage.Size = new System.Drawing.Size(792, 407);
            this.mainPage.TabIndex = 0;
            this.mainPage.Text = "project";
            this.mainPage.Click += new System.EventHandler(this.mainPage_Click);
            // 
            // ignoreListTxt
            // 
            this.ignoreListTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ignoreListTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ignoreListTxt.Location = new System.Drawing.Point(3, 83);
            this.ignoreListTxt.Multiline = true;
            this.ignoreListTxt.Name = "ignoreListTxt";
            this.ignoreListTxt.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ignoreListTxt.Size = new System.Drawing.Size(786, 321);
            this.ignoreListTxt.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(3, 57);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(3);
            this.label2.Size = new System.Drawing.Size(99, 26);
            this.label2.TabIndex = 2;
            this.label2.Text = "Ignore List";
            // 
            // projectRootPathTxt
            // 
            this.projectRootPathTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.projectRootPathTxt.Dock = System.Windows.Forms.DockStyle.Top;
            this.projectRootPathTxt.Location = new System.Drawing.Point(3, 29);
            this.projectRootPathTxt.Name = "projectRootPathTxt";
            this.projectRootPathTxt.ReadOnly = true;
            this.projectRootPathTxt.Size = new System.Drawing.Size(786, 28);
            this.projectRootPathTxt.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(3);
            this.label1.Size = new System.Drawing.Size(92, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Root Path";
            // 
            // ProjectPropertyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Meiryo UI", 8F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProjectPropertyForm";
            this.Text = "ProjectPropertyForm";
            this.Shown += new System.EventHandler(this.ProjectPropertyForm_Shown);
            this.panel1.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.mainPage.ResumeLayout(false);
            this.mainPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ajkControls.TabControl tabControl;
        private System.Windows.Forms.TabPage mainPage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.TextBox projectRootPathTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ignoreListTxt;
    }
}