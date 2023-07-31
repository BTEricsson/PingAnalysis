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

            LogFileBase.WriteToFile(pingNode.LogPath, $"{Environment.NewLine}------------------------------------------------------");
            LogFileBase.WriteToFile(pingNode.LogPath, $"{Environment.NewLine}Service is started at " + DateTime.Now);
            
            SetTimer();
            PingHost();
        }

        protected override void OnStop()
        {
            LogFileBase.WriteToFile(pingNode.LogPath, $"{Environment.NewLine}Service is stopped at " + DateTime.Now);
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            PingHost();
        }

        private void SetTimer()
        {
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 60000 * pingNode.PingTimer;
            timer.Enabled = true;
        }


        private void PingNetDown()
        {
            if (DownIP == string.Empty)
            {

                LogFileBase.WriteToFile(pingNode.LogPath, $"{Environment.NewLine}Host not responding");

                IList<string> IPAdress = GetNodesIP();

                foreach (string IP in IPAdress)
                {
                    if (DownIP != string.Empty)
                        continue;

                    var status = GetIPStatus(IP);
                    if (status == IPStatus.Success)
                    {
                        LogFileBase.WriteToFile(pingNode.LogPath, $"{Environment.NewLine}Response UP @ IP: {IP} {DateTimeString.GetDateTimeString()}");
                    }
                    else
                    {
                        DownIP = IP;
                        LogFileBase.WriteToFile(pingNode.LogPath, $"{Environment.NewLine}Response Down @ IP: {IP} {DateTimeString.GetDateTimeString()}");
                    }
                }
            }
            else
            {
                var status = GetIPStatus(DownIP);
                if (status == IPStatus.Success)
                {
                    DownIP = string.Empty;
                    PingHost();
                }
                else if (status != IPStatus.Success)
                {
                    if (!LogFileBase.WriteLastUpdateToFile(pingNode.LogPath, "Down"))
                        LogFileBase.WriteToFile(pingNode.LogPath, $"{Environment.NewLine}Response Down @ IP: {DownIP} {DateTimeString.GetDateTimeString()}");
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

            var Host = GetHost();

            if (Host == null)
            {
                LogFileBase.WriteToFile(pingNode.LogPath, $"{Environment.NewLine}No active Host to ping. " + DateTime.Now);
                return;
            }

            var status = GetIPStatus(Host.IPAddress);

            if (status == IPStatus.Success)
            {
                if (!LogFileBase.WriteLastUpdateToFile(pingNode.LogPath, "Ping"))
                    LogFileBase.WriteToFile(pingNode.LogPath, $"{Environment.NewLine}Ping Host @ IP: {Host.IPAddress}, Status: {status} {DateTimeString.GetDateTimeString()}");
            }
            else
            {
                LogFileBase.WriteToFile(pingNode.LogPath, $"{Environment.NewLine}Ping Host @ IP {Host.IPAddress}, Status: {status} {DateTimeString.GetDateTimeString()}");
                PingNetDown();
            }

        }

        private IPStatus GetIPStatus(string IPAddress)
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

    }

}
