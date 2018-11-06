using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using codeEditor.NavigatePanel;

namespace codeEditor.Data
{
    public class TextFile : File, ITextFile
    {
        public new static TextFile Create(string relativePath, Project project)
        {
            string id = GetID(relativePath, project);
            if (project.IsRegistered(id))
            {
                TextFile item = project.GetRegisterdItem(id) as TextFile;
                project.RegisterProjectItem(item);
                return item;
            }

            TextFile fileItem = new TextFile();
            fileItem.Project = project;
            fileItem.ID = id;
            fileItem.RelativePath = relativePath;
            if (relativePath.Contains('\\'))
            {
                fileItem.Name = relativePath.Substring(relativePath.LastIndexOf('\\') + 1);
            }
            else
            {
                fileItem.Name = relativePath;
            }

            project.RegisterProjectItem(fileItem);
            return fileItem;
        }

        public CodeEditor.ParsedDocument ParsedDocument { get; set; }

        private CodeEditor.CodeDocument document = null;
        public CodeEditor.CodeDocument CodeDocument {
            get
            {
                if(document == null)
                {
                    try
                    {
                        using (System.IO.StreamReader sr = new System.IO.StreamReader( Project.GetAbsolutePath(RelativePath) ))
                        {
                            document = new CodeEditor.CodeDocument();
                            string text = sr.ReadToEnd();
                            document.Replace(0, 0, 0, text);
                            document.ParentID = ID;
                        }
                    }
                    catch
                    {
                        document = null;
                    }
                }
                return document;
            }
            protected set
            {
                document = value;
            }
        }

        public override NavigatePanelNode CreateNode()
        {
            return new NavigatePanel.TextFileNode(ID, Project);
        }

        public virtual CodeEditor.DocumentParser CreateDocumentParser(CodeEditor.CodeDocument document, string id, Project project)
        {
            return null;
        }
    }
}
