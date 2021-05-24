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


        // TODO: É preciso enviar a IMAGEM a ser criada.
        // {
        //     "Image": "docker/getting-started"
        // }
        public static JToken CreateContainer(string containerName)
        {
            var client = new RestClient(Models.Configuration.DockerURI + "containers/create");
            var request = new RestRequest(Method.POST);
            request.AddQueryParameter("name", containerName);
            var response = client.Get(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                JToken jsonReceived = JToken.Parse(response.Content);
                return jsonReceived;
            }
            else
                return JToken.Parse("{\"message\":\"Não foi possível criar o container\"}");
        }
    }
}