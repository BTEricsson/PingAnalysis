using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Business;

namespace Analysis
{
    internal static class TraceHelpers
    {
        public static IList<Node> GetTraceRoute(string hostname)
        {
            const int timeout = 10000;
            const int maxTTL = 30;
            const int bufferSize = 32;

            byte[] buffer = new byte[bufferSize];
            new Random().NextBytes(buffer);

            IList<Node> Nodes = new List<Node>(); 

            using (var pinger = new Ping())
            {
                for (int ttl = 1; ttl <= maxTTL; ttl++)
                {
                    PingOptions options = new PingOptions(ttl, true);
                    PingReply reply = pinger.Send(hostname, timeout, buffer, options);

                    if (reply.Status == IPStatus.Success || reply.Status == IPStatus.TtlExpired)
                    {
                        string hostName = GetHostName(reply, timeout);
                        Nodes.Add(new Node
                        {
                            Skip = false,
                            TTL = ttl,
                            IPAddress = reply.Address.ToString(),
                            DNSHostName = hostName,
                            Alias = string.Empty,
                            Local = false
                        });
                    }

                    if (reply.Status != IPStatus.TtlExpired && reply.Status != IPStatus.TimedOut)
                        break;
                }
                return Nodes;
            }
        }

        private static string GetHostName(PingReply pingReply, int timeout)
        {
            if (pingReply == null)
                return string.Empty;
            
            Task<string> task = Task<string>.Factory.StartNew(() =>
            {
                try
                {
                    return Dns.GetHostEntry(pingReply.Address).HostName.ToString();
                }
                catch
                {
                    return string.Empty;
                } 
            });

            bool success = task.Wait(timeout);

            if (!success)
                return string.Empty;
            
            return task.Result;
        }
    }
}