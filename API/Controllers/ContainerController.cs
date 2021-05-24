using System;
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

        [HttpPost]
        [Route("")]
        public string Create([FromBody] string containerId)
        {
            Console.WriteLine("Entrou: " + containerId);
            var response = Docker.ContainerTools.CreateContainer(containerId);
            Console.WriteLine("Passou");
            return response.ToString();
        }
    }
}