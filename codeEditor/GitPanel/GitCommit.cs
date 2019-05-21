using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeEditor.GitPanel
{
    public class GitCommit : ajkControls.TableView.TableItem
    {
        protected GitCommit() { }
        public static GitCommit Create(string line)
        {
            GitCommit commit = new GitCommit();
            if (!line.Contains("* <")) return null;

            string s = line;
            s = line.Substring(s.IndexOf("* <")+3);
            commit.CommitHash = s.Substring(0, 40);

            s = s.Substring(s.IndexOf(" <")+2);
            commit.CommitDate = DateTime.Parse(s.Substring(0,19));

            s = s.Substring(s.IndexOf(" <") + 2);
            commit.AuthorName = s.Substring(0,s.IndexOf(">"));

            s = s.Substring(s.IndexOf(">") + 2);
            commit.Subject = s;


            return commit;
        }

        public DateTime CommitDate;
        public string Subject;
        public string CommitHash;
        public string AuthorName;

        public override void Draw(Graphics g, Font font, List<Rectangle> rectangles)
        {
            SolidBrush brush = new SolidBrush(Color.DarkGray);
            g.DrawString(CommitHash.Substring(0,6), font, brush, rectangles[0]);
            g.DrawString(CommitDate.ToString(), font, brush, rectangles[1]);
            g.DrawString(AuthorName, font, brush,rectangles[2]);
            g.DrawString(Subject, font, brush, rectangles[3]);
        }       
    }
}
