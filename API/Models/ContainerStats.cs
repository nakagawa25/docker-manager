using System;
using System.Collections.Generic;

namespace API.Models
{
    public class ContainerStats
    {
        public ContainerStats()
        {
            Networks = new List<NetworkStats>();
        }
        public decimal CpuUsage { get; set; }
        public decimal MemoryUsage { get; set; }
        public List<NetworkStats> Networks { get; set; }
        public DateTime InsertionDateTime { get; set; }


        public void Insert()
        {
            // TODO: implementar
        }

        public static List<ContainerStats> GetAll()
        {
            // TODO: implementar
            return null;
        }
    }

    public class NetworkStats
    {
        public string NetworkInterface { get; set; }
        public decimal ReceiveBytes { get; set; }
        public decimal ReceivePackets { get; set; }
        public decimal ReceiveErros { get; set; }
        public decimal ReceiveDropped { get; set; }
        public decimal TransmitBytes { get; set; }
        public decimal TransmitPackets { get; set; }
        public decimal TransmitErros { get; set; }
        public decimal TransmitDropped { get; set; }
    }
}