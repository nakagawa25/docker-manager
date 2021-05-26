using System;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

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
        public string Create([FromBody] Models.Container body)
        {
            Console.WriteLine("Entrou: " + body.containerName + " " + body.imageName);

            var response = Docker.ContainerTools.CreateContainer(body.containerName, body.imageName);
            
            return response.ToString();
        }
    }
}