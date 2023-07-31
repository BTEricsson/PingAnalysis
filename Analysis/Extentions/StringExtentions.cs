using System.Drawing;

namespace Analysis.Extentions
{
    public static class StringExtentions
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
    }
}
