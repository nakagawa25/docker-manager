using System;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("v1/containers")]
    public class ContainerController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            try
            {
                var containers = Docker.ContainerTools.GetContainers();
                return Ok(containers);
            }
            catch (Utils.Exceptions.APIException apiEx)
            {
                // TODO: Adicionar algum tratamento melhor?
                return BadRequest(apiEx.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Houve um erro na API. Erro: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("")]
        public IActionResult Create([FromBody] Models.ContainerInput body)
        {
            try
            {
                if (Docker.ContainerTools.CreateContainer(body.containerName, body.imageName))
                    return Ok();
                else
                    return BadRequest("{\"message\":\"Não foi possível criar o container\"}");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao criar o container. Erro: " + ex.Message);
            }
        }

        [HttpDelete]
        [Route("")]
        public string Delete([FromBody] string containerId)
        {
            var response = Docker.ContainerTools.DeleteContainer(containerId);
            return response.ToString();
        }

        [HttpDelete]
        [Route("/all")]
        public IActionResult DeleteAll()
        {
            // TODO: Adicionar o DeleteAll.
            return null;
        }
    }
}