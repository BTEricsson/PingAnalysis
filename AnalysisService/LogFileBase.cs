using System;
using System.IO;
using System.Linq;

namespace AnalysisService
{
    public static class LogFileBase
    {

        public static bool WriteLastUpdateToFile(string LogPath, string Type)
        {
            string filepath = LogPath + "\\Logs\\PingLog_" + DateTime.Now.Date.ToString("yyyy-MM").Replace('/', '_') + ".txt";
            var allLines = File.ReadAllLines(filepath).ToList();
            var lastLine = allLines.Last();
            string updateLine = string.Empty;

            if (!lastLine.Contains(Type))
                return false;

            try
            {
                if (lastLine.Contains("Last"))
                {
                    updateLine = lastLine.Substring(0, lastLine.IndexOf("Last")) + "Last Update: " + DateTimeString.GetDateTimeString();
                }
                else
                {
                    updateLine = lastLine + $" Last Update: {DateTimeString.GetDateTimeString()}";
                }
                var newLines = allLines.GetRange(0, allLines.Count - 1);
                File.WriteAllLines(filepath, newLines);

                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.Write(updateLine);
                }
            }
            catch (Exception ex)
            {
                WriteToFile(LogPath, "Ex: " + ex.Message);
                return false;
            }

            return true;
        }

        public static void WriteToFile(string LogPath, string Message)
        {
            string path = LogPath + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filepath = LogPath + "\\Logs\\PingLog_" + DateTime.Now.Date.ToString("yyyy-MM").Replace('/', '_') + ".txt";

            if (!File.Exists(filepath))
            {
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.Write(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.Write(Environment.NewLine + Message);
                }
            }
        }
    }
}