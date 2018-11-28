namespace codeEditor.Shell
{
    partial class CmdShellPanel
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
            this.logView = new ajkControls.LogView();
            this.textBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // logView
            // 
            this.logView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logView.Location = new System.Drawing.Point(0, 0);
            this.logView.Name = "logView";
            this.logView.Size = new System.Drawing.Size(405, 386);
            this.logView.TabIndex = 0;
            // 
            // textBox
            // 
            this.textBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox.Location = new System.Drawing.Point(0, 386);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(405, 25);
            this.textBox.TabIndex = 1;
            // 
            // CmdShellPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.logView);
            this.Controls.Add(this.textBox);
            this.Name = "CmdShellPanel";
            this.Size = new System.Drawing.Size(405, 411);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ajkControls.LogView logView;
        private System.Windows.Forms.TextBox textBox;
    }
}
