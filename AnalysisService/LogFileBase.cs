using System;
using System.IO;
using System.Linq;

namespace AnalysisService
{
    public static class LogFileBase
    {
        public static bool WriteLastUpdateToFile(string LogPathAndFile, string Type)
        {
            var allLines = File.ReadAllLines(LogPathAndFile).ToList();
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
                File.WriteAllLines(LogPathAndFile, newLines);

                using (StreamWriter sw = File.AppendText(LogPathAndFile))
                {
                    sw.Write(updateLine);
                }
            }
            catch (Exception ex)
            {
                WriteToFile(LogPathAndFile, "Ex: " + ex.Message);
                return false;
            }

            return true;
        }

        public static void WriteToFile(string LogPathAndFile, string Message)
        {
            string path = Path.GetDirectoryName(LogPathAndFile);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (!File.Exists(LogPathAndFile))
            {
                using (StreamWriter sw = File.CreateText(LogPathAndFile))
                {
                    sw.Write(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(LogPathAndFile))
                {
                    sw.Write(Environment.NewLine + Message);
                }
            }
        }
    }
}