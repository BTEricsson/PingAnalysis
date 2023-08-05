using System;
using System.Windows.Forms;
using Analysis.Extentions;

namespace Analysis
{
    public static class  RichTextBoxExtentions
    {
        public static void AddTexLineWithStatusColor(this RichTextBox Rtb, string Text)
        {
            Text += Environment.NewLine;

            int StartPos = Rtb.TextLength;
            int Length = Text.Length;

            Rtb.SelectionColor = Text.GetTextLineStatusColor();
            Rtb.SelectionStart = StartPos;
            Rtb.SelectionLength = Length;

            Rtb.AppendText(Text);
        }
    }
}
