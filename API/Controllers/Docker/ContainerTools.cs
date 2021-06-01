using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace API.Controllers.Docker
{
    public class ContainerTools
    {
        public static List<Models.Container> GetContainers()
        {
            var client = new RestClient(Models.Configuration.DockerURI + "containers/json");
            var request = new RestRequest(Method.GET);
            request.AddParameter("all", true, ParameterType.QueryString);
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