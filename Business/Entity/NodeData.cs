using System.Collections.Generic;

namespace Business
{
    public class Node
    {
        public bool Skip { get; set; }
        public int TTL { get; set; }
        public string IPAddress { get; set; }
        public string DNSHostName { get; set; }
        public string Alias { get; set; }
        public bool Local { get; set; }
    }

    public class NodeData
    {
        public string Name { get; set; }
        public int PingTimer { get; set; }
        public string LogPath { get; set; }
        public IList<Node> Nodes { get; set; }
    }
}
