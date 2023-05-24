using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeEditor.Tabs
{
    public class EditorPage : ajkControls.TabControl.TabPage
    {
        public EditorPage()
        {
            Controls.Add(this.codeEditor);
            Location = new System.Drawing.Point(4, 29);
            Name = "editorPage";
            Padding = new System.Windows.Forms.Padding(3);
            Size = new System.Drawing.Size(620, 439);
            TabIndex = 0;
            Text = "code";
            UseVisualStyleBackColor = true;
            IconImage = new ajkControls.Primitive.IconImage(Properties.Resources.text);
            
            // 
            // codeEditor
            // 
            codeEditor.BackColor = System.Drawing.Color.White;
            codeEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            codeEditor.Font = new System.Drawing.Font("MS UI Gothic", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            codeEditor.Location = new System.Drawing.Point(3, 3);
            codeEditor.Name = "codeEditor";
            codeEditor.Size = new System.Drawing.Size(614, 433);
            codeEditor.TabIndex = 0;

        }

        private CodeEditor.CodeEditor codeEditor = new CodeEditor.CodeEditor();
        public CodeEditor.CodeEditor CodeEditor
        {
            get
            {
                return codeEditor;
            }
        }

    }
}
