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
            this.codeTextbox = new ajkControls.CodeTextbox();
            this.SuspendLayout();
            // 
            // codeTextbox
            // 
            this.codeTextbox.BackColor = Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.codeTextbox.Document = null;
            this.codeTextbox.Editable = false;
            this.codeTextbox.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.codeTextbox.Location = new Point(2, 0);
            this.codeTextbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.codeTextbox.Name = "codeTextbox";
            this.codeTextbox.ScrollBarVisible = false;
            this.codeTextbox.Size = new Size(656, 223);
            this.codeTextbox.TabIndex = 0;
            // 
            // PopupForm
            // 
            this.AutoScaleDimensions = new SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.ClientSize = new Size(657, 225);
            this.Controls.Add(this.codeTextbox);
            this.Font = new Font("Consolas", 8F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PopupForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "PopupForm";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private ajkControls.CodeTextbox codeTextbox;
    }
}