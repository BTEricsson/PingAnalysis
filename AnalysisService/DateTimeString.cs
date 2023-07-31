using System;

namespace AnalysisService
{
    public static class DateTimeString
    {
        public static string GetDateTimeString()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
