﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeEditor.CodeEditor
{
    public class ParsedDocument : IDisposable
    {
        public ParsedDocument(Data.Project project,string itemID,int editID)
        {
            this.Project = project;
            this.ItemID = itemID;
            this.EditID = editID;
        }

        public Data.Project Project { get; protected set; }
        public string ItemID { get; protected set; }
        public int EditID { get; protected set; }


        public virtual void Accept()
        {
        }

        public virtual void Dispose()
        {
        }


        public List<Message> Messages = new List<Message>();

        public class Message
        {
            public string ItemID { get; protected set; }

            public int Index { get; protected set; }
            public int Length { get; protected set; }
            public string Text { get; protected set; }
            public Data.Project Project { get; protected set; }
            public virtual MessageView.MessageNode CreateMessageNode()
            {
                return null;
            }
        }
    }
}
