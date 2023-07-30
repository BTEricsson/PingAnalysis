using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.ServiceProcess;
using System.Timers;
using Business;

namespace AnalysisService
{
    public partial class Service : ServiceBase
    {
        PingNode pingNode = new PingNode();
        readonly Timer timer = new Timer();
        string DownIP = string.Empty;

        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            DownIP = string.Empty;
            pingNode.Load();

            WriteToFile($"{Environment.NewLine}Service is started at " + DateTime.Now);
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 60000;
            timer.Enabled = true;

            PingHost();
        }

        protected override void OnStop()
        {
            WriteToFile($"{Environment.NewLine}Service is stopped at " + DateTime.Now);
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            PingHost();
        }

        private void PingNetDown()
        {
            if (DownIP == string.Empty)
            {

                WriteToFile($"{Environment.NewLine}Host not responding");

                IList<string> IPAdress = GetNodesIP();

                foreach (string IP in IPAdress)
                {
                    if (DownIP != string.Empty)
                        continue;

                    var status = NodePing(IP);
                    if (status == IPStatus.Success)
                    {
                        WriteToFile($"{Environment.NewLine}Response UP @ IP: {IP} {DateTimeString()}");
                    }
                    else
                    {
                        DownIP = IP;
                        WriteToFile($"{Environment.NewLine}Response Down @ IP: {IP} {DateTimeString()}");
                    }
                }
            }
            else
            {
                var status = NodePing(DownIP);
                if (status == IPStatus.Success)
                {
                    DownIP = string.Empty;
                    PingHost();
                }
                else if (status != IPStatus.Success)
                {
                    if (!UpdateFile("Down"))
                        WriteToFile($"{Environment.NewLine}Response Down @ IP: {DownIP} {DateTimeString()}");
                }
            }
        }

        private Node GetHost()
        {
            return pingNode.Nodes.OrderByDescending(x => x.TTL).Where(y => y.Skip == false).FirstOrDefault();
        }

        private IList<string> GetNodesIP()
        {
            return pingNode.Nodes.OrderBy(T => T.TTL).Where(S => S.Skip == false).Select(IP => IP.IPAddress).ToList();
        }


        private void PingHost()
        {
            if (DownIP != string.Empty)
            {
                PingNetDown();
                return;
            }

            var node = GetHost();

            if (node == null)
            {
                WriteToFile($"{Environment.NewLine}No active Host to ping. " + DateTime.Now);
                return;
            }

            var status = NodePing(node.IPAddress);

            if (status == IPStatus.Success)
            {
                if (!UpdateFile("Ping"))
                    WriteToFile($"{Environment.NewLine}Host Ping @ {node.IPAddress}, Status: {status} {DateTimeString()}");
            }
            else
            {
                WriteToFile($"{Environment.NewLine}Host Ping @ {node.IPAddress}, Status: {status} {DateTimeString()}");
                PingNetDown();
            } 
                
        }


        private string DateTimeString()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }



        private IPStatus NodePing(string IPAddress)
        {
            const int timeout = 10000;
            const int maxTTL = 30;
            const int bufferSize = 32;

            byte[] buffer = new byte[bufferSize];
            new Random().NextBytes(buffer);

            using (var pinger = new Ping())
            {
                PingOptions options = new PingOptions(maxTTL, false);
                PingReply reply = pinger.Send(IPAddress, timeout, buffer, options);

                return reply.Status;
            }
        }

        public static string AssemblyLocation()
        {
            return Assembly.GetExecutingAssembly().Location;
        }


        private bool UpdateFile(string Type)
        {
            string filepath = pingNode.LogPath + "\\Logs\\PingLog_" + DateTime.Now.Date.ToString("yyyy-MM").Replace('/', '_') + ".txt";
            var allLines = File.ReadAllLines(filepath).ToList();
            var lastLine = allLines.Last();
            string updateLine = string.Empty;

            if (!lastLine.Contains(Type))
                return false;

            try
            {
                if (lastLine.Contains("Last"))
                {
                    updateLine = lastLine.Substring(0, lastLine.IndexOf("Last")) + "Last Update: " + DateTimeString();
                }
                else
                {
                    updateLine = lastLine + $" Last Update: {DateTimeString()}";
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
                WriteToFile(Environment.NewLine + "Ex: " + ex.Message);
                return false;
            }

            return true;
        }

        private void WriteToFile(string Message)
        {
            string path = pingNode.LogPath + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filepath = pingNode.LogPath + "\\Logs\\PingLog_" + DateTime.Now.Date.ToString("yyyy-MM").Replace('/', '_') + ".txt";

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
                    sw.Write(Message);
                }
            }
        }

    }
}
