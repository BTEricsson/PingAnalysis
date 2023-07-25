﻿
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Business
{
    public class PingNode
    {
        readonly string _filePath = $@"{AppDomain.CurrentDomain.BaseDirectory}\PingConfig.Json";
        private static NodeData traceData = new NodeData();
        public NodeData GetNodeData { get { return traceData; } }
        public string Name { get { return traceData.Name; } set { traceData.Name = value; } }
        public string LogPath { get { return traceData.LogPath; } set { traceData.LogPath = value; } }
        public IList<Node> Nodes { get { return traceData.Nodes; } set { traceData.Nodes = value; } }

        public NodeData Load()
        {
            if (!File.Exists(_filePath))
                return traceData;

            using (StreamReader r = new StreamReader(_filePath))
            {
                try
                {
                    string json = r.ReadToEnd();
                    traceData = JsonSerializer.Deserialize<NodeData>(json);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Json Load error: {ex.Message}");
                }
            }
            return traceData;
        }

        public NodeData SaveOrUpdate()
        {
            if (traceData.Nodes == null)
                return null;
            try
            {
                var options = new JsonSerializerOptions() { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(traceData, options);
                using (StreamWriter outputFile = new StreamWriter(_filePath))
                {
                    outputFile.WriteLine(jsonString);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Json save error: {ex.Message}");
            }
            return traceData;
        }
    }
}