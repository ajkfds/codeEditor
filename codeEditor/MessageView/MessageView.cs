﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace codeEditor.MessageView
{
    public partial class MessageView : UserControl
    {
        public MessageView()
        {
            InitializeComponent();
        }

        public void UpdateMessages(CodeEditor.ParsedDocument parsedDocument)
        {
            treeView.TreeNodes.Clear();
            foreach(CodeEditor.ParsedDocument.Message message in parsedDocument.Messages)
            {
                treeView.TreeNodes.Add(message.CreateMessageNode());
            }
            treeView.Refresh();
        }

        private void treeView_SelectedNodeChanged(object sender, ajkControls.TreeNode e)
        {
        }

        private void treeView_NodeClicked(object sender, ajkControls.TreeNode e)
        {
            MessageNode node = e as MessageNode;
            node.Selected();
        }
    }
}
