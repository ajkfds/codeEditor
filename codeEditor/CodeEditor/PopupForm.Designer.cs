using System.Drawing;

namespace codeEditor.CodeEditor
{
    partial class PopupForm
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
            this.doubleBufferedDrawBox = new ajkControls.DoubleBufferedDrawBox();
            this.SuspendLayout();
            // 
            // doubleBufferedDrawBox
            // 
            this.doubleBufferedDrawBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.doubleBufferedDrawBox.Location = new System.Drawing.Point(0, 0);
            this.doubleBufferedDrawBox.Name = "doubleBufferedDrawBox";
            this.doubleBufferedDrawBox.Size = new System.Drawing.Size(657, 225);
            this.doubleBufferedDrawBox.TabIndex = 0;
            this.doubleBufferedDrawBox.DoubleBufferedPaint += new ajkControls.DoubleBufferedDrawBox.DoubleBufferedPaintHandler(this.doubleBufferedDrawBox_DoubleBufferedPaint);
            // 
            // PopupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(657, 225);
            this.Controls.Add(this.doubleBufferedDrawBox);
            this.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PopupForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "PopupForm";
            this.ResumeLayout(false);

        }

        #endregion

        private ajkControls.DoubleBufferedDrawBox doubleBufferedDrawBox;
    }
}