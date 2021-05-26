using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("v1/images")]
    public class ImageController
    {
        [HttpGet]
        [Route("")]
        public string Get()
        {
            return Docker.ImageTools.GetImages().ToString();
        }

        [HttpPost]
        [Route("")]
        public string Create([FromBody] string imageName)
        {
            return Docker.ImageTools.CreateImage(imageName).ToString();
        }

        [HttpDelete]
        [Route("")]
        public string Delete([FromBody] string imageId)
        {
            return Docker.ImageTools.DeleteImage(imageId).ToString();
        }
    }
}