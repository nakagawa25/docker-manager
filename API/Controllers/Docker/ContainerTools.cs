using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace API.Controllers.Docker
{
    public class ContainerTools
    {
        public static List<Models.Container> GetContainers(bool returnAllContainers = true)
        {
            var client = new RestClient(Models.Configuration.DockerURI + "containers/json");
            var request = new RestRequest(Method.GET);
            request.AddParameter("all", returnAllContainers, ParameterType.QueryString);
            var response = client.Get(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                List<Models.Container> containers = new List<Models.Container>();
                foreach (var container in JToken.Parse(response.Content))
                    containers.Add(CreateContainerObject(container));

                return containers;
            }
            else
                throw new Utils.Exceptions.APIException("Erro ao obter a lista de containers do Docker.");
        }

        public static bool CreateContainer(string containerName, string imageName)
        {
            var client = new RestClient(Models.Configuration.DockerURI + "containers/create");
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("name", containerName, ParameterType.QueryString);
            request.AddJsonBody(new { image = imageName });
            IRestResponse response = client.Execute(request);

            return response.IsSuccessful;
        }

        public static bool DeleteContainer(string containerId)
        {
            var client = new RestClient(Models.Configuration.DockerURI + "containers/" + containerId);
            var request = new RestRequest(Method.DELETE);
            request.AddParameter("force", true, ParameterType.QueryString);
            IRestResponse response = client.Execute(request);

            return response.IsSuccessful;
        }

        public static void DeleteAllContainers()
        {
            DeleteStoppedContainers();
            var containers = GetContainers();
            foreach (var container in containers)
                DeleteContainer(container.Id);
        }

        private static void DeleteStoppedContainers()
        {
            var client = new RestClient(Models.Configuration.DockerURI + "containers/prune");
            var request = new RestRequest(Method.POST);
            IRestResponse response = client.Execute(request);
            if (!response.IsSuccessful)
                throw new Utils.Exceptions.APIException("Erro ao deletar os containers sem uso. Erro da API: " + response.ErrorMessage);
        }

        public static void SaveRunningContainersStats(){
            try
            {
                foreach (var container in GetContainers(false))
                    GetContainerStats(container.Id).Insert();              
            }
            catch (Exception ex)
            {
                // TODO: Add Log.
                throw;
            }
        }

        public static Models.ContainerStats GetContainerStats(string containerId)
        {
            var client = new RestClient(Models.Configuration.DockerURI + $@"containers/{containerId}/stats");
            var request = new RestRequest(Method.GET);
            request.AddParameter("stream", false, ParameterType.QueryString);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
                return CreateContainerStatsObject(JToken.Parse(response.Content));
            else
                throw new Utils.Exceptions.APIException("Não foi possível obter os stats do container: " + containerId);
        }

        public static Models.ContainerStats CreateContainerStatsObject(JToken json)
        {
            Models.ContainerStats containerStats = new Models.ContainerStats();
            var cpuDelta = (decimal)json["cpu_stats"]["cpu_usage"]["total_usage"] - (decimal)json["precpu_stats"]["cpu_usage"]["total_usage"];
            var systemCpuDelta = (decimal)json["cpu_stats"]["system_cpu_usage"] - (decimal)json["precpu_stats"]["system_cpu_usage"];
            var numberCpus = (decimal)json["cpu_stats"]["online_cpus"];
            containerStats.CpuUsage = Math.Round((cpuDelta / systemCpuDelta) * numberCpus * 100, 2);
            var usedMemory = (decimal)json["memory_stats"]["usage"] - (decimal)json["memory_stats"]["stats"]["cache"];
            var availableMemory = (decimal)json["memory_stats"]["limit"];
            containerStats.MemoryUsage = Math.Round((usedMemory / availableMemory) * 100, 2);
            foreach (JToken network in json["networks"])
            {
                Models.NetworkStats networkStats = new Models.NetworkStats();
                networkStats.NetworkInterface = (network as JProperty).Name;
                networkStats.ReceiveBytes = (decimal)network.First()["rx_bytes"];
                networkStats.ReceivePackets = (decimal)network.First()["rx_packets"];
                networkStats.ReceiveErros = (decimal)network.First()["rx_errors"];
                networkStats.ReceiveDropped = (decimal)network.First()["rx_dropped"];
                networkStats.TransmitBytes = (decimal)network.First()["tx_bytes"];
                networkStats.TransmitPackets = (decimal)network.First()["tx_packets"];
                networkStats.TransmitErros = (decimal)network.First()["tx_errors"];
                networkStats.TransmitDropped = (decimal)network.First()["tx_dropped"];
                containerStats.Networks.Add(networkStats);
            }
            containerStats.InsertionDateTime = System.DateTime.Now;

            return containerStats;
        }

        public static Models.Container CreateContainerObject(JToken json)
        {
            Models.Container container = new Models.Container();
            container.Id = json["Id"].ToString();
            container.Name = json["Names"][0].ToString();
            container.imageName = json["Image"].ToString();
            container.Status = json["State"].ToString();
            if (json["Ports"] != null && json["Ports"].Any())
            {
                foreach (var port in json["Ports"])
                    container.Ports.Add((int)port["PrivatePort"]);
            }

            return container;
        }
    }
}