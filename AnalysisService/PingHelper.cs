using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace AnalysisService
{
    public class PingHelper
    {
        private PingConfig NodeBase = new PingConfig();
        public string DownIP = string.Empty;

        public int GetTimer { get { return NodeBase.PingTimer; } }
        public string GetLogPath { get { return NodeBase.LogPath; } }

        public void InitPingNode()
        {
            DownIP = string.Empty;
            NodeBase.Load();
        }

        public void PingNetDown()
        {
            if (DownIP == string.Empty)
            {

                LogFileBase.WriteToFile(NodeBase.LogPath, $"Host not responding");

                IList<string> IPAdress = GetNodesIP();

                foreach (string IP in IPAdress)
                {
                    if (DownIP != string.Empty)
                        continue;

                    var status = GetIPStatus(IP);
                    if (status == IPStatus.Success)
                    {
                        LogFileBase.WriteToFile(NodeBase.LogPath, $"Response UP @ IP: {IP} {DateTimeString.GetDateTimeString()}");
                    }
                    else
                    {
                        DownIP = IP;
                        LogFileBase.WriteToFile(NodeBase.LogPath, $"Response Down @ IP: {IP} {DateTimeString.GetDateTimeString()}");
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
                    if (!LogFileBase.WriteLastUpdateToFile(NodeBase.LogPath, "Down"))
                        LogFileBase.WriteToFile(NodeBase.LogPath, $"Response Down @ IP: {DownIP} {DateTimeString.GetDateTimeString()}");
                }
            }
        }

        private Node GetHost()
        {
            return NodeBase.Nodes.OrderByDescending(x => x.TTL).Where(y => y.Skip == false).FirstOrDefault();
        }

        private IList<string> GetNodesIP()
        {
            return NodeBase.Nodes.OrderBy(T => T.TTL).Where(S => S.Skip == false).Select(IP => IP.IPAddress).ToList();
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
                LogFileBase.WriteToFile(NodeBase.LogPath, $"No active Host to ping. " + DateTime.Now);
                return;
            }

            var status = GetIPStatus(Host.IPAddress);

            if (status == IPStatus.Success)
            {
                if (!LogFileBase.WriteLastUpdateToFile(NodeBase.LogPath, "Ping"))
                    LogFileBase.WriteToFile(NodeBase.LogPath, $"Ping Host @ IP: {Host.IPAddress}, Status: {status} {DateTimeString.GetDateTimeString()}");
            }
            else
            {
                LogFileBase.WriteToFile(NodeBase.LogPath, $"Ping Host @ IP {Host.IPAddress}, Status: {status} {DateTimeString.GetDateTimeString()}");
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
