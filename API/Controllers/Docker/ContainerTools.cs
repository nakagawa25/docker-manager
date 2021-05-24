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

            if (response.StatusCode == System.Net.HttpStatusCode.OK){
                JToken jsonReceived = JToken.Parse(response.Content);
                return jsonReceived;
            }
            else
                return JToken.Parse("{\"message\":\"Não foi possível obter a lista de containers\"}");

        }
    }
}