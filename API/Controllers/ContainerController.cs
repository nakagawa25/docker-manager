using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("v1/containers")]
    public class ContainerController
    {
        [HttpGet]
        [Route("")]
        public string Get()
        {
            return Docker.ContainerTools.GetContainers().ToString();
        }
    }
}