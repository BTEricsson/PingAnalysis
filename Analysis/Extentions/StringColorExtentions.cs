using System.Drawing;

namespace Analysis.Extentions
{
    public static class StringColorExtentions
    {
        public static Color GetStatusColor(this string Status)
        {

            switch (Status)
            {
                case "not installed":
                    return Color.OrangeRed;
                case "Running":
                    return Color.Green;
                case "Stopped":
                    return Color.Red;
                default:
                    return Color.Black;
            }
        }

        public static Color GetTextLineStatusColor(this string TextLine)
        {
            if (TextLine.Contains("Success"))
                return Color.Green;

            if (TextLine.Contains("TimedOut"))
                return Color.DarkOrange;

            if (TextLine.Contains("UP"))
                return Color.Green;

            if (TextLine.Contains("Down"))
                return Color.Red;

            if (TextLine.Contains("DestinationHostUnreachable"))
                return Color.Red;

            if (TextLine.Contains("Host not responding"))
                return Color.Red;

            return Color.Black;
        }
    }
}
