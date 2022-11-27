using System;
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
        // find & replace form
        FindForm findForm;

        public void OpenReplace()
        {
            if (CodeDocument == null) return;

            if (findForm == null || !findForm.Visible) findForm = new FindForm(this);
            Point point = this.PointToScreen(new Point(Left, Top));
            findForm.Left = point.X;
            findForm.Top = point.Y;

            if (findForm.Visible) return;
            string initialText = "";
            if (CodeDocument.SelectionStart != CodeDocument.SelectionLast)
            {
                initialText = CodeDocument.CreateString(CodeDocument.SelectionStart, CodeDocument.SelectionLast - CodeDocument.SelectionStart);
            }
            findForm.OpenReplace(initialText, this.PointToScreen(new Point(Width - findForm.Width, 0)));
        }
        public void OpenFind()
        {
            if (CodeDocument == null) return;

            if (findForm == null || !findForm.Visible) findForm = new FindForm(this);
            Point point = this.PointToScreen(new Point(Left, Top));
            findForm.Left = point.X;
            findForm.Top = point.Y;

            if (findForm.Visible) return;
            string initialText = "";
            if (CodeDocument.SelectionStart != CodeDocument.SelectionLast)
            {
                initialText = CodeDocument.CreateString(CodeDocument.SelectionStart, CodeDocument.SelectionLast - CodeDocument.SelectionStart);
            }
            findForm.OpenFind(initialText, this.PointToScreen(new Point(Width - findForm.Width, 0)));
        }

        public void ReplaceNext(string findString, string replaceString)
        {
            string currentText = CodeDocument.CreateString(CodeDocument.CaretIndex, CodeDocument.SelectionLast - CodeDocument.SelectionStart);
            if (currentText == findString)
            {
                CodeDocument.Replace(CodeDocument.CaretIndex, CodeDocument.SelectionLast - CodeDocument.SelectionStart, 0, replaceString);
                return;
            }
            FindNext(findString);
            currentText = CodeDocument.CreateString(CodeDocument.CaretIndex, CodeDocument.SelectionLast - CodeDocument.SelectionStart);
            if (currentText != findString) return;
            CodeDocument.Replace(CodeDocument.CaretIndex, CodeDocument.SelectionLast - CodeDocument.SelectionStart, 0, replaceString);
        }

        public void ReplacePrevious(string findString, string replaceString)
        {
            string currentText = CodeDocument.CreateString(CodeDocument.CaretIndex, CodeDocument.SelectionLast - CodeDocument.SelectionStart);
            if (currentText == findString)
            {
                CodeDocument.Replace(CodeDocument.CaretIndex, CodeDocument.SelectionLast - CodeDocument.SelectionStart, 0, replaceString);
                return;
            }
            FindPrevious(findString);
            currentText = CodeDocument.CreateString(CodeDocument.CaretIndex, CodeDocument.SelectionLast - CodeDocument.SelectionStart);
            if (currentText != findString) return;
            CodeDocument.Replace(CodeDocument.CaretIndex, CodeDocument.SelectionLast - CodeDocument.SelectionStart, 0, replaceString);
        }
        public void FindNext(string findString)
        {
            string currentText = CodeDocument.CreateString(CodeDocument.SelectionStart, CodeDocument.SelectionLast - CodeDocument.SelectionStart);
            int findIndex;
            if (currentText == findString)
            {
                findIndex = CodeDocument.FindIndexOf(findString, CodeDocument.SelectionStart + 1);
            }
            else
            {
                findIndex = CodeDocument.FindIndexOf(findString, CodeDocument.CaretIndex);
            }

            if (findIndex == -1)
            {
                findIndex = CodeDocument.FindIndexOf(findString, 0);
                if (findIndex == -1) return;
            }

            CodeDocument.CaretIndex = findIndex;
            CodeDocument.SelectionStart = findIndex;
            CodeDocument.SelectionLast = findIndex + findString.Length;
            ScrollToCaret();
            Refresh();
        }

        public void FindPrevious(string findString)
        {
            string currentText = CodeDocument.CreateString(CodeDocument.SelectionStart, CodeDocument.SelectionLast - CodeDocument.SelectionStart);
            int findIndex;
            if (currentText == findString)
            {
                if (CodeDocument.SelectionStart == 0) return;
                findIndex = CodeDocument.FindPreviousIndexOf(findString, CodeDocument.SelectionStart - 1);
            }
            else
            {
                findIndex = CodeDocument.FindPreviousIndexOf(findString, CodeDocument.CaretIndex);
            }

            if (findIndex == -1)
            {
                if (CodeDocument.Length == 0) return;
                findIndex = CodeDocument.FindPreviousIndexOf(findString, CodeDocument.Length - 1);
                if (findIndex == -1) return;
            }

            CodeDocument.CaretIndex = findIndex;
            CodeDocument.SelectionStart = findIndex;
            CodeDocument.SelectionLast = findIndex + findString.Length;
            ScrollToCaret();
            Refresh();
        }

    }
}

