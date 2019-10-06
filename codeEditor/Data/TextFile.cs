﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ajkControls;
using codeEditor.CodeEditor;
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


        public bool IsCodeDocumentCashed
        {
            get { if (document == null) return false; else return true; }
        }

        public CodeEditor.ParsedDocument ParsedDocument { get; set; }

        private volatile bool parseRequested = false;
        public bool ParseRequested { get {return parseRequested; } set { parseRequested = value; } }

        private volatile bool reloadRequested = false;
        public bool ReloadRequested { get { return reloadRequested; } set { reloadRequested = value; } }

        public void Reload()
        {
            CodeDocument = null;
        }

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
                            document.ClearHistory();
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

        
        public CodeDrawStyle DrawStyle
        {
            get
            {
                return Global.DefaultDrawStyle;
            }
        }

        public override NavigatePanelNode CreateNode()
        {
            return new NavigatePanel.TextFileNode(ID, Project);
        }

        public virtual CodeEditor.DocumentParser CreateDocumentParser(CodeEditor.CodeDocument document, string id, Project project,CodeEditor.DocumentParser.ParseModeEnum parseMode)
        {
            return null;
        }


        public List<PopupItem> GetPopupItems(int EditId, int index)
        {
            return null;
        }

        public List<AutocompleteItem> GetAutoCompleteItems(int index,out string cantidateWord)
        {
            cantidateWord = null;
            return null;
        }
        public List<codeEditor.CodeEditor.ToolItem> GetToolItems(int index)
        {
            return null;
        }


        public virtual void BeforeKeyDown(KeyEventArgs e)
        {

        }

        public virtual void AfterKeyDown(System.Windows.Forms.KeyEventArgs e)
        {

        }

        public virtual void BeforeKeyPressed(KeyPressEventArgs e)
        {

        }

        public virtual void AfterKeyPressed(System.Windows.Forms.KeyPressEventArgs e)
        {
        }

    }
}
