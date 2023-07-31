using System.Drawing;
using System.ServiceProcess;

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
                    break;
                case "Running":
                    return Color.Green;
                    break;
                case "Stopped":
                    return Color.Red;
                    break;
                default:
                    return Color.Black;
                    break;
            }
        }
    }
}
