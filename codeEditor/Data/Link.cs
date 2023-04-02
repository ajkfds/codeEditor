using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using codeEditor.NavigatePanel;


namespace codeEditor.Data
{
    public class Link : Item
    {
        public static Link Create(string relativePath, Project project, Item parent)
        {
            if (!relativePath.EndsWith(".lnk")) System.Diagnostics.Debugger.Break();

            IWshRuntimeLibrary.IWshShortcut shortcut = Global.WshShell.CreateShortcut(project.GetAbsolutePath(relativePath));
            string linkTargetPath = shortcut.TargetPath;

            if (System.IO.File.Exists(linkTargetPath)) return createFileLink(relativePath, linkTargetPath, project);
            if (System.IO.Directory.Exists(linkTargetPath)) return createFolderLink(relativePath, linkTargetPath, project);

            return null;
        }

        private static Link createFileLink(string relativePath,string linkTargetPath,Project project)
        {
            Link link = new Link();
            link.LinkRelativePath = relativePath;
            link.LinkItem = project.GetItem(project.GetRelativePath(linkTargetPath));

            if (relativePath.Contains('\\'))
            {
                link.Name = relativePath.Substring(relativePath.LastIndexOf('\\') + 1);
            }
            else
            {
                link.Name = relativePath;
            }

            if (link.LinkItem == null) return null;

            return link;
        }

        private static Link createFolderLink(string relativePath, string linkTargetPath, Project project)
        {
            Link link = new Link();
            link.LinkRelativePath = relativePath;
            link.LinkItem = project.GetItem(project.GetRelativePath(linkTargetPath));

            if (relativePath.Contains('\\'))
            {
                link.Name = relativePath.Substring(relativePath.LastIndexOf('\\') + 1);
            }
            else
            {
                link.Name = relativePath;
            }

            if (link.LinkItem == null) return null;

            return link;
        }


        protected Item LinkItem;

        protected string linkRelativePath;
        public string LinkRelativePath
        {
            get
            {
                return linkRelativePath;
            }
            protected set
            {
                linkRelativePath = value;
            }
        }
        public override string RelativePath {
            get
            {
                return LinkItem.RelativePath;
            }
        }

        public override Project Project { get
            {
                return LinkItem.Project;
            }
        }

        public override Item.ItemList Items
        {
            get { return LinkItem.Items; }
        }

        public override Item GetItem(string relativePath)
        {
            return LinkItem.GetItem(relativePath);
        }

        public override List<Item> FindItems(Func<Item, bool> match, Func<Item, bool> stop)
        {
            return LinkItem.FindItems(match, stop);
        }



        public override void Dispose()
        {
        }

        public override void Update() 
        {
            LinkItem.Update();
        }


        protected override NavigatePanel.NavigatePanelNode createNode()
        {
            NavigatePanelNode node = LinkItem.CreateLinkNode();
            return node;
        }

        public override CodeEditor.DocumentParser CreateDocumentParser(CodeEditor.DocumentParser.ParseModeEnum parseMode)
        {
            return LinkItem.CreateDocumentParser(parseMode);
        }
    }
}
