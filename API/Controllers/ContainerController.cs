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
        public IActionResult Delete([FromBody] string containerId)
        {
            try
            {
                if (Docker.ContainerTools.DeleteContainer(containerId))
                    return Ok("{\"message\":\"Container excluído com sucesso\"}");
                else
                    return BadRequest("{\"message\":\"Não foi possível excluír o container\"}");
            }
            catch (Exception ex)
            {
                return BadRequest("{\"message\":\"Erro interno na API. Erro: " + ex.Message + "\"}");
            }
        }

        [HttpDelete]
        [Route("all")]
        public IActionResult DeleteAll()
        {
            try
            {
                Docker.ContainerTools.DeleteAllContainers();
                return Ok("{\"message\":\"Sucesso, todos os containers foram excluídos\"}");
            }
            catch (Utils.Exceptions.APIException apiEx)
            {
                return BadRequest("{\"message\":\"Erro da API. Erro: " + apiEx.Message + "\"}");
            }
            catch (Exception ex)
            {
                return BadRequest("{\"message\":\"Erro interno da API. Erro: " + ex.Message + "\"}");
            }
        }
    }
}