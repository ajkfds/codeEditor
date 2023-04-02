using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace codeEditor.Tools
{
    public partial class ParseHierarchyForm : Form
    {
        public ParseHierarchyForm(NavigatePanel.NavigatePanelNode rootNode)
        {
            InitializeComponent();
            //            Text = projectNode.Project.Name;
            this.rootNode = rootNode;
            this.Icon = ajkControls.Global.Icon;
            this.ShowInTaskbar = false;
            this.BackColor = codeEditor.Controller.GetBackColor();
        }

        private NavigatePanel.NavigatePanelNode rootNode = null;
        private volatile bool close = false;


        private void ParseHierarchyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!close) e.Cancel = true;
            rootNode = null;
        }

        System.Threading.Thread thread = null;
        private void ParseHierarchyForm_Shown(object sender, EventArgs e)
        {
            if (thread != null) return;
            thread = new System.Threading.Thread(() => { worker(); });
            thread.Start();
            timer.Enabled = true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Update();
        }

        private void worker()
        {
            parseHier(rootNode.Item);
            rootNode.Update();

            close = true;
            Invoke(new Action(() => { Close(); }));
        }

        private void parseHier(Data.Item item)
        {
            if (item == null) return;
            Data.ITextFile textFile = item as Data.TextFile;
            if (textFile == null) return;

            textFile.ParseHierarchy( (tFile) => {
                Invoke(new Action(() => { label.Text = tFile.ID; }));
            });

            //if (textFile.ParsedDocument != null)
            //{
            //    textFile.Update();
            //}
            //else
            //{
            //    CodeEditor.DocumentParser parser = item.CreateDocumentParser(CodeEditor.DocumentParser.ParseModeEnum.BackgroundParse);
            //    if (parser != null)
            //    {
            //        parser.Parse();
            //        if (parser.ParsedDocument == null) return;
            //        textFile.AcceptParsedDocument(parser.ParsedDocument);
            //        textFile.Update();
            //    }
            //}
            //parsedIds.Add(textFile.ID);
            //if (textFile.NavigatePanelNode != null) textFile.NavigatePanelNode.Update();

            //List<Data.Item> items = new List<Data.Item>();
            //foreach(Data.Item subItem in textFile.Items.Values)
            //{
            //    items.Add(subItem);
            //}

            //foreach(Data.Item subitem in items)
            //{
            //    parseHier(subitem);
            //}
        }
         
    }
}
