using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace codeEditor.CodeEditor
{
    public partial class AutoCompleteForm : Form
    {
        public AutoCompleteForm()
        {
            InitializeComponent();
        }

        public void SetAutocompleteItems(List<AutocompleteItem> items)
        {
            if(items == null)
            {
                this.items.Clear();
            }
            else
            {
                this.items = items;
            }
            UpdateVisibleItems("");
        }

        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }

        private List<AutocompleteItem> items = new List<AutocompleteItem>();
        private List<AutocompleteItem> visibleItems = new List<AutocompleteItem>();

        private AutocompleteItem selectedItem = null;

        private string inputText = "";
        public void UpdateVisibleItems(string inputText)
        {
            this.inputText = inputText;
            UpdateVisibleItems();
        }

        public void UpdateVisibleItems()
        {
            lock (visibleItems)
            {
                List<AutocompleteItem> partialMatch = new List<AutocompleteItem>();
                visibleItems.Clear();
                foreach (AutocompleteItem item in items)
                {
                    if (item.Text.StartsWith(inputText))
                    {
                        visibleItems.Add(item);
                        continue;
                    }
                    if (item.Text.Contains(inputText))
                    {
                        partialMatch.Add(item);
                        continue;
                    }
                }

                foreach (AutocompleteItem item in partialMatch)
                {
                    visibleItems.Add(item);
                }

                if (selectedItem != null && !visibleItems.Contains(selectedItem))
                {
                    selectedItem = null;
                }
                if (selectedItem == null && visibleItems.Count != 0)
                {
                    selectedItem = visibleItems[0];
                }
            }
            if (selectedItem == null)
            {
                Visible = false;
                return;
            }
            doubleBufferedDrawBox.Refresh();
        }

        public AutocompleteItem SelectItem()
        {
            if (!visibleItems.Contains(selectedItem))
            {
                return null;
            }
            Visible = false;
            return selectedItem;
        }

        public void MoveDown()
        {
            if (!visibleItems.Contains(selectedItem)) return;
            int index = visibleItems.IndexOf(selectedItem);
            index++;
            if (index < visibleItems.Count)
            {
                selectedItem = visibleItems[index];
            }
            UpdateVisibleItems();
        }

        public void MoveUp()
        {
            if (!visibleItems.Contains(selectedItem)) return;
            int index = visibleItems.IndexOf(selectedItem);
            index--;
            if (index >= 0)
            {
                selectedItem = visibleItems[index];
            }
            UpdateVisibleItems();
        }

        private int leftMargin = 4;
        private int rightMargin = 4;
        private int topMargin = 4;
        private int bottomMargin = 4;
        private int maxVisibleLines = 8;

        private SolidBrush selectionBrush = new SolidBrush(Color.FromArgb(100, 0, 50, 100));
        private void doubleBufferedDrawBox_DoubleBufferedPaint(PaintEventArgs e)
        {
            lock (visibleItems)
            {
                if (items.Count == 0) return;
                // update size
                int visibleLines = maxVisibleLines;
                if (visibleLines > visibleItems.Count)
                {
                    visibleLines = visibleItems.Count;
                }
                // update scrollbar
                vScrollBar.Minimum = 0;
                vScrollBar.Maximum = items.Count;
                vScrollBar.LargeChange = visibleLines;
                int selectedIndex = visibleItems.IndexOf(selectedItem);
                if (selectedIndex < vScrollBar.Value)
                {
                    vScrollBar.Value = selectedIndex;
                }else if (selectedIndex >= vScrollBar.Value + visibleLines)
                {
                    if((selectedIndex-visibleLines+1)<0)
                    {
                        vScrollBar.Value = 0;
                    }
                    else
                    {
                        vScrollBar.Value = selectedIndex - visibleLines + 1;
                    }
                }
                if(visibleItems.Count < vScrollBar.Value + visibleLines)
                {
                    if(visibleItems.Count - visibleLines < 0)
                    {
                        vScrollBar.Value = 0;
                    }
                    else
                    {
                        vScrollBar.Value = visibleItems.Count - visibleLines;
                    }
                }

                int y = topMargin;
                int height;
                bool setHeight = true;
                for(int i = vScrollBar.Value; i < vScrollBar.Value + visibleLines; i++)
                {
                    AutocompleteItem item = visibleItems[i];
                    item.Draw(e.Graphics, leftMargin, y, Font, BackColor, out height);
                    if (setHeight)
                    {
                        Height = height * visibleLines + topMargin + bottomMargin;
                        setHeight = false;
                    }
                    if (item == selectedItem)
                    {
                        e.Graphics.FillRectangle(selectionBrush, new Rectangle(leftMargin, y, Width - leftMargin - rightMargin, height));
                    }
                    y += height;
                }
            }
        }
    }
}
