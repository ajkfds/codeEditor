namespace codeEditor.GitPanel
{
    partial class GitPanel
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
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
            this.panel = new System.Windows.Forms.Panel();
            this.splitter = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.logButton = new System.Windows.Forms.Button();
            this.tableView = new ajkControls.TableView.TableView();
            this.logView = new ajkControls.LogView();
            this.pullButton = new System.Windows.Forms.Button();
            this.panel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.logView);
            this.panel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel.Location = new System.Drawing.Point(0, 592);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(734, 115);
            this.panel.TabIndex = 1;
            // 
            // splitter
            // 
            this.splitter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter.Location = new System.Drawing.Point(0, 589);
            this.splitter.Name = "splitter";
            this.splitter.Size = new System.Drawing.Size(734, 3);
            this.splitter.TabIndex = 2;
            this.splitter.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pullButton);
            this.panel1.Controls.Add(this.logButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(734, 69);
            this.panel1.TabIndex = 3;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel1_Paint);
            // 
            // logButton
            // 
            this.logButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.logButton.Location = new System.Drawing.Point(31, 13);
            this.logButton.Name = "logButton";
            this.logButton.Size = new System.Drawing.Size(86, 41);
            this.logButton.TabIndex = 0;
            this.logButton.Text = "log";
            this.logButton.UseVisualStyleBackColor = true;
            this.logButton.Click += new System.EventHandler(this.LogButton_Click);
            // 
            // tableView
            // 
            this.tableView.Columns = 5;
            this.tableView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableView.HeaderHeight = 0;
            this.tableView.Location = new System.Drawing.Point(0, 69);
            this.tableView.Name = "tableView";
            this.tableView.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(91)))), ((int)(((byte)(125)))), ((int)(((byte)(159)))));
            this.tableView.Size = new System.Drawing.Size(734, 520);
            this.tableView.TabIndex = 0;
            // 
            // logView
            // 
            this.logView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logView.Font = new System.Drawing.Font("Meiryo UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.logView.ForeColor = System.Drawing.Color.Gray;
            this.logView.Location = new System.Drawing.Point(0, 0);
            this.logView.MaxLogs = 200;
            this.logView.Name = "logView";
            this.logView.Size = new System.Drawing.Size(734, 115);
            this.logView.TabIndex = 0;
            // 
            // pullButton
            // 
            this.pullButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pullButton.Location = new System.Drawing.Point(137, 13);
            this.pullButton.Name = "pullButton";
            this.pullButton.Size = new System.Drawing.Size(123, 41);
            this.pullButton.TabIndex = 1;
            this.pullButton.Text = "pull remote";
            this.pullButton.UseVisualStyleBackColor = true;
            this.pullButton.Click += new System.EventHandler(this.PullButton_Click);
            // 
            // GitPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableView);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter);
            this.Controls.Add(this.panel);
            this.Font = new System.Drawing.Font("Meiryo UI", 8F);
            this.Name = "GitPanel";
            this.Size = new System.Drawing.Size(734, 707);
            this.panel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ajkControls.TableView.TableView tableView;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Splitter splitter;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button logButton;
        private ajkControls.LogView logView;
        private System.Windows.Forms.Button pullButton;
    }
}
