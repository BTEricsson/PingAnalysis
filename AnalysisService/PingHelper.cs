using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using Business;

namespace AnalysisService
{
    public class PingHelper
    {
        private readonly Config config = new Config();

        public string DownIP = string.Empty;
        public int GetTimer { get { return config.PingTimer; } }
        public string GetLogPathAndFile { get { return config.LogPathAndFile; } }

        public void InitPingNode()
        {
            DownIP = string.Empty;
            config.Load();
        }

        public void PingNetDown()
        {
            if (DownIP == string.Empty)
            {

                LogFileBase.WriteToFile(config.LogPathAndFile, $"{Environment.NewLine}Host not responding");

                IList<string> IPAdress = GetNodesIP();

                foreach (string IP in IPAdress)
                {
                    if (DownIP != string.Empty)
                        continue;

                    var status = GetIPStatus(IP);
                    if (status == IPStatus.Success)
                    {
                        LogFileBase.WriteToFile(config.LogPathAndFile, $"Response UP @ IP: {IP} {DateTimeString.GetDateTimeString()}");
                    }
                    else
                    {
                        DownIP = IP;
                        LogFileBase.WriteToFile(config.LogPathAndFile, $"Response Down @ IP: {IP} {DateTimeString.GetDateTimeString()}");
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
                    if (!LogFileBase.WriteLastUpdateToFile(config.LogPathAndFile, "Down"))
                        LogFileBase.WriteToFile(config.LogPathAndFile, $"Response Down @ IP: {DownIP} {DateTimeString.GetDateTimeString()}");
                }
            }
        }

        private Node GetHost()
        {
            return config.Nodes.OrderByDescending(x => x.TTL).Where(y => y.Skip == false).FirstOrDefault();
        }

        private IList<string> GetNodesIP()
        {
            return config.Nodes.OrderBy(T => T.TTL).Where(S => S.Skip == false).Select(IP => IP.IPAddress).ToList();
        }


        public void PingHost()
        {
            if (DownIP != string.Empty)
            {
                PingNetDown();
                return;
            }

            var Host = GetHost();

            if (Host == null)
            {
                LogFileBase.WriteToFile(config.LogPathAndFile, $"No active Host to ping. " + DateTime.Now);
                return;
            }

            var status = GetIPStatus(Host.IPAddress);

            if (status == IPStatus.Success)
            {
                if (!LogFileBase.WriteLastUpdateToFile(config.LogPathAndFile, "Ping"))
                    LogFileBase.WriteToFile(config.LogPathAndFile, $"Ping Host Status: {status} @ IP: {Host.IPAddress} at {DateTimeString.GetDateTimeString()}");
            }
            else
            {
                LogFileBase.WriteToFile(config.LogPathAndFile, $"Ping Host Status: {status} @ IP: {Host.IPAddress} at {DateTimeString.GetDateTimeString()}");
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

    }
}
