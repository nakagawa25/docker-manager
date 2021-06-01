using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace API.Controllers.Docker
{
    public class ImageTools
    {
        public static List<Models.Image> GetImages()
        {
            var client = new RestClient(Models.Configuration.DockerURI + "images/json");
            var request = new RestRequest(Method.GET);
            var response = client.Get(request);

            if (response.IsSuccessful)
            {
                List<Models.Image> images = new List<Models.Image>();
                foreach (var image in JToken.Parse(response.Content))
                    images.Add(CreateImageObject(image));

                return images;
            }
            else
                throw new Utils.Exceptions.APIException("Erro ao obter a lista de imagens do Docker.");
        }

        public static bool CreateImage(Models.ImageInput imageInput)
        {
            var client = new RestClient(Models.Configuration.DockerURI + "images/create");
            var request = new RestRequest(Method.POST);
            request.AddParameter("fromImage", imageInput.ImageName, ParameterType.QueryString);
            request.AddParameter("tag", imageInput.Tag, ParameterType.QueryString);
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            return response.IsSuccessful;
        }

        public static bool DeleteImage(string imageId)
        {
            var client = new RestClient(Models.Configuration.DockerURI + "images/" + imageId);
            var request = new RestRequest(Method.DELETE);
            request.AddParameter("force", true, ParameterType.QueryString);
            IRestResponse response = client.Execute(request);

            return response.IsSuccessful;
        }

        public static void DeleteAllImages()
        {
            DeleteUnusedImages();
            var images = GetImages();
            foreach (var image in images)
                DeleteImage(image.Id);
        }

        private static void DeleteUnusedImages()
        {
            var client = new RestClient(Models.Configuration.DockerURI + "images/prune");
            var request = new RestRequest(Method.POST);
            IRestResponse response = client.Execute(request);
            if (!response.IsSuccessful)
                throw new Utils.Exceptions.APIException("Erro ao deletar as imagens sem uso. Erro da API: " + response.ErrorMessage);
        }

        public static Models.Image CreateImageObject(JToken json)
        {
            Models.Image image = new Models.Image();
            image.Id = json["Id"].ToString();
            image.RepoDigest = json["RepoDigests"][0].ToString();
            image.RepoTags = json["RepoTags"][0].ToString();
            image.Size = (int)json["Size"];

            return image;
        }
    }
}