﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace codeEditor.CodeEditor
{
    public partial class CodeEditor : UserControl
    {
        // 2 Parser Run Concurrently
        //  Edit Parser
        //  Background Parser

        // Entry Edit Parse
        public void RequestReparse()
        {
            entryParse();
        }

        private void entryParse()
        {
            if (Global.StopParse) return;
            if (TextFile == null) return;
            DocumentParser parser = TextFile.CreateDocumentParser(DocumentParser.ParseModeEnum.EditParse);
            if (parser != null)
            {
                Controller.AppendLog("entry edit parse ID :" + parser.TextFile.ID );
                backGroundParser.EntryParse(parser);
            }
        }

        // foreground parse
        private void timer_Tick(object sender, EventArgs e)
        {
            DocumentParser parser = backGroundParser.GetResult();
            if (parser == null) return;
            if (parser.ParsedDocument == null) return;
            //if (TextFile == null) return;
            //if (TextFile != parser.TextFile)
            //{   // return not current file
            //    return;
            //}
            
            Data.TextFile textFile = parser.TextFile;
            CodeDocument codeDocument = textFile.CodeDocument;

            if (textFile == null || textFile == null)
            {
                parser.Dispose();
                return;
            }

            Controller.AppendLog("complete edit parse ID :" + parser.TextFile.ID );
            if (codeDocument.Version != parser.ParsedDocument.Version)
            {
                Controller.AppendLog("edit parsed mismatch " + DateTime.Now.ToString()+"ver"+ codeDocument.Version +"<-"+ parser.ParsedDocument.Version );
                parser.Dispose();
                return;
            }

            //            CodeDocument.CopyFrom(parser.Document);
            codeDocument.CopyColorMarkFrom(parser.Document);

            if (parser.ParsedDocument != null)
            {
                parser.TextFile.AcceptParsedDocument(parser.ParsedDocument);
            }

            // update current view
            codeTextbox.Invoke(new Action(codeTextbox.Refresh));
            Controller.MessageView.Update(TextFile.ParsedDocument);
            codeTextbox.ReDrawHighlight();

            Controller.NavigatePanel.UpdateVisibleNode();
            Controller.NavigatePanel.Refresh();
        }

        private void acceptParseResult()
        {

        }


        // background parse
        private void subBgtimer_Tick(object sender, EventArgs e)
        {
            return;
            //DocumentParser parser = subBackGroundParser.GetResult();
            //if (parser == null || parser.ParsedDocument == null)
            //{ // entry parse
            //    if (subBackGroundParser.RemainingStocks != 0) return;
            //    NavigatePanel.NavigatePanelNode node;
            //    Controller.NavigatePanel.GetSelectedNode(out node);
            //    if (node == null || node.Item == null) return;
            //    Data.Project project = node.Item.Project;

            //    Data.Item item = project.FetchReparseTarget();
            //    if (item == null) return;

            //    DocumentParser newParser = item.CreateDocumentParser(DocumentParser.ParseModeEnum.BackgroundParse);
            //    if (newParser != null)
            //    {
            //        subBackGroundParser.EntryParse(newParser);
            //        Controller.AppendLog("entry parse " + item.ID + " " + DateTime.Now.ToString());
            //    }
            //}
            //else
            //{ // receive result
            //    if (TextFile != null && TextFile == parser.TextFile)
            //    {
            //        if (CodeDocument != null && CodeDocument.Version != parser.ParsedDocument.Version)
            //        {
            //            Controller.AppendLog("parsed mismatch sub " + parser.TextFile.Name + " " + DateTime.Now.ToString());
            //            parser.Dispose();
            //            //                        TextFile.ParseRequested = false;
            //            return;
            //        }
            //    }

            //    Controller.AppendLog("parsed sub  " + parser.TextFile.Name + " " + DateTime.Now.ToString());
            //    System.Diagnostics.Debug.Print("## parsed" + parser.TextFile.Name);
            //    Data.TextFile textFile = parser.TextFile;

            //    if (textFile == null) return;
            //    //if (textFile.ParsedDocument == null)
            //    //{
            //    //    textFile.Close();
            //    //    textFile.ParseRequested = false;
            //    //    return;
            //    //}

            //    textFile.AcceptParsedDocument(parser.ParsedDocument);
            //    if (TextFile != textFile) textFile.Close();
            //    if (textFile.NavigatePanelNode != null)
            //    {
            //        textFile.NavigatePanelNode.Update();
            //    }

            //    Controller.NavigatePanel.UpdateVisibleNode();
            //    Controller.NavigatePanel.Refresh();
            //    parser.Dispose();
            //}

        }

    }
}
