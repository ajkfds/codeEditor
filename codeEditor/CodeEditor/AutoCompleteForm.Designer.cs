namespace codeEditor.CodeEditor
{
    partial class AutoCompleteForm
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
            this.doubleBufferedDrawBox = new ajkControls.Primitive.DoubleBufferedDrawBox();
            this.vScrollBar = new System.Windows.Forms.VScrollBar();
            this.SuspendLayout();
            // 
            // doubleBufferedDrawBox
            // 
            this.doubleBufferedDrawBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.doubleBufferedDrawBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.doubleBufferedDrawBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.doubleBufferedDrawBox.Location = new System.Drawing.Point(0, 0);
            this.doubleBufferedDrawBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.doubleBufferedDrawBox.Name = "doubleBufferedDrawBox";
            this.doubleBufferedDrawBox.Size = new System.Drawing.Size(389, 488);
            this.doubleBufferedDrawBox.TabIndex = 0;
            this.doubleBufferedDrawBox.DoubleBufferedPaint += new ajkControls.Primitive.DoubleBufferedDrawBox.DoubleBufferedPaintHandler(this.doubleBufferedDrawBox_DoubleBufferedPaint);
            // 
            // vScrollBar
            // 
            this.vScrollBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar.Location = new System.Drawing.Point(389, 0);
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(23, 488);
            this.vScrollBar.TabIndex = 1;
            // 
            // AutoCompleteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(412, 488);
            this.Controls.Add(this.doubleBufferedDrawBox);
            this.Controls.Add(this.vScrollBar);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "AutoCompleteForm";
            this.Text = "AutoCompleteForm";
            this.VisibleChanged += new System.EventHandler(this.AutoCompleteForm_VisibleChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private ajkControls.Primitive.DoubleBufferedDrawBox doubleBufferedDrawBox;
        private System.Windows.Forms.VScrollBar vScrollBar;
    }
}