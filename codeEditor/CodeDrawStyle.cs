using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace codeEditor
{
    public class CodeDrawStyle : ajkControls.CodeDrawStyle
    {
        public CodeDrawStyle()
        {
            colors = new System.Drawing.Color[16]
                    {
                        System.Drawing.Color.FromArgb(212,212,212),     // default
                        System.Drawing.Color.LightGray, // 1
                        System.Drawing.Color.DarkGray, // 2
                        System.Drawing.Color.Red, // 3
                        System.Drawing.Color.Blue, // 4
                        System.Drawing.Color.Green, // 5
                        System.Drawing.Color.Turquoise, // 6
                        System.Drawing.Color.Purple, // 7
                        System.Drawing.Color.Orange, // 8
                        System.Drawing.Color.Pink, // 9
                        System.Drawing.Color.Black, // 10
                        System.Drawing.Color.Black, // 11
                        System.Drawing.Color.Black, // 12
                        System.Drawing.Color.Black, // 13
                        System.Drawing.Color.Black, // 14
                        System.Drawing.Color.Black  // 15
                    };
        }

        public override Color[] MarkColor
        {
            get
            {
                return new System.Drawing.Color[8]
                    {
                        System.Drawing.Color.FromArgb(128,System.Drawing.Color.Red),    // 0
                        System.Drawing.Color.Orange, // 1
                        System.Drawing.Color.Red, // 2
                        System.Drawing.Color.Red, // 3
                        System.Drawing.Color.Red, // 4
                        System.Drawing.Color.Red, // 5
                        System.Drawing.Color.Red, // 6
                        System.Drawing.Color.FromArgb(52,58,62)//  50,100,10,100)  // 7
                    };
            }
        }

        public override ajkControls.CodeTextbox.MarkStyleEnum[] MarkStyle
        {
            get
            {
                return new ajkControls.CodeTextbox.MarkStyleEnum[8]
                    {
                        ajkControls.CodeTextbox.MarkStyleEnum.wave,    // 0
                        ajkControls.CodeTextbox.MarkStyleEnum.underLine,
                        ajkControls.CodeTextbox.MarkStyleEnum.underLine,
                        ajkControls.CodeTextbox.MarkStyleEnum.underLine,
                        ajkControls.CodeTextbox.MarkStyleEnum.underLine,
                        ajkControls.CodeTextbox.MarkStyleEnum.underLine,
                        ajkControls.CodeTextbox.MarkStyleEnum.underLine,
                        ajkControls.CodeTextbox.MarkStyleEnum.fill              // 7 for selection highlight
                    };
            }
        }
    }
}
