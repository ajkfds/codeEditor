﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using codeEditor.NavigatePanel;


namespace codeEditor.Data
{
    public class File : Item
    {
        public static File Create(string relativePath, Project project, Item parent)
        {
            foreach (var fileType in Global.FileTypes.Values)
            {
                if (fileType.IsThisFileType(relativePath, project)) return fileType.CreateFile(relativePath, project);
            }

            File fileItem = new File();
            fileItem.Project = project;
            fileItem.RelativePath = relativePath;
            if (relativePath.Contains('\\'))
            {
                fileItem.Name = relativePath.Substring(relativePath.LastIndexOf('\\') + 1);
            }
            else
            {
                fileItem.Name = relativePath;
            }

            fileItem.Parent = parent;

            if (FileCreated != null) FileCreated(fileItem);
            return fileItem;
        }

        public string AbsolutePath
        {
            get
            {
                return Project.GetAbsolutePath(RelativePath);
            }
        }

        public bool IsSameAs(File file)
        {
            if (RelativePath != file.RelativePath) return false;
            if (Project != file.Project) return false;
            return true;
        }

        public static Action<File> FileCreated;

        //public static string GetID(string relativePath, Project project)
        //{
        //    return project.ID + ":File:" + relativePath;
        //}

        protected override NavigatePanelNode createNode()
        {
            return new FileNode(this);
        }

        public FileTypes.FileType FileType
        {
            get { return null; }
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
