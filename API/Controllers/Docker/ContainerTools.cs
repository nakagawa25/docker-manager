using Newtonsoft.Json.Linq;
using RestSharp;


namespace API.Controllers.Docker
{
    public class ContainerTools
    {
        public static JToken GetContainers()
        {
            var client = new RestClient(Models.Configuration.DockerURI + "containers/json");
            // client.Authenticator = new HttpBasicAuthenticator("username", "password");
            var request = new RestRequest(Method.GET);
            var response = client.Get(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                JToken jsonReceived = JToken.Parse(response.Content);
                return jsonReceived;
            }
            else
                return JToken.Parse("{\"message\":\"Não foi possível obter a lista de containers\"}");
        }

        public static JToken CreateContainer(string containerName, string imageName)
        {
            var client = new RestClient(Models.Configuration.DockerURI + "containers/create");
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("name", containerName, ParameterType.QueryString);
            request.AddJsonBody(new {image = imageName});
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                JToken jsonReceived = JToken.Parse(response.Content);
                return jsonReceived;
            }
            else
                return JToken.Parse("{\"message\":\"Não foi possível criar o container\"}");
        }
    }
}