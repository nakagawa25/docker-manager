using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("v1/containers")]
    public class ContainerController // : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public string Get()
        {
            return Docker.ContainerTools.GetContainers().ToString();
        }

        [HttpPost]
        [Route("")]
        public string Create([FromBody] Models.Container body)
        {
            var response = Docker.ContainerTools.CreateContainer(body.containerName, body.imageName);
            return response.ToString();
        }

        [HttpDelete]
        [Route("")]
        public string Delete([FromBody] string containerId)
        {
            var response = Docker.ContainerTools.DeleteContainer(containerId);
            return response.ToString();
        }
    }
}