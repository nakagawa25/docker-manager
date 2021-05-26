using Newtonsoft.Json.Linq;
using RestSharp;

namespace API.Controllers.Docker
{
    public class ImageTools
    {
        public static JToken GetImages()
        {
            var client = new RestClient(Models.Configuration.DockerURI + "images/json");
            var request = new RestRequest(Method.GET);
            var response = client.Get(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                JToken jsonReceived = JToken.Parse(response.Content);
                return jsonReceived;
            }
            else
                return JToken.Parse("{\"message\":\"Não foi possível obter a lista de imagens\"}");
        }

        public static JToken CreateImage(string imageName)
        {
            var client = new RestClient(Models.Configuration.DockerURI + "images/create");
            var request = new RestRequest(Method.POST);
            request.AddParameter("fromImage", imageName, ParameterType.QueryString);
            request.AddParameter("text/plain", "",  ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
                return JToken.Parse("{\"message\":\"Imagem criada com sucesso\"}");
            else
                return JToken.Parse("{\"message\":\"Não foi possível criar a imagem\"}");
        }

        public static JToken DeleteImage(string imageId)
        {
            var client = new RestClient(Models.Configuration.DockerURI + "images/" + imageId);
            var request = new RestRequest(Method.DELETE);
            request.AddParameter("force", true, ParameterType.QueryString);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
                return JToken.Parse("{\"message\":\"Imagem excluído com sucesso\"}");
            else
                return JToken.Parse("{\"message\":\"Não foi possível excluír a imagem\"}");
        }

    }
}