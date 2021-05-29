using System;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("v1/images")]
    public class ImageController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            try
            {
                var images = Docker.ImageTools.GetImages();
                return Ok(images);
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
        public IActionResult Create([FromBody] Models.ImageInput json)
        {
            try
            {
                if (Docker.ImageTools.CreateImage(json))
                    return Ok();
                else
                    return BadRequest("{\"message\":\"Não foi possível criar a imagem\"}");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao criar a imagem. Erro: " + ex.Message);
            }
        }

        [HttpDelete]
        [Route("")]
        public IActionResult Delete([FromBody] string imageId)
        {
            try
            {
                if (Docker.ImageTools.DeleteImage(imageId))
                    return Ok("{\"message\":\"Imagem excluído com sucesso\"}");
                else
                    return BadRequest("{\"message\":\"Não foi possível excluír a imagem\"}");
            }
            catch (Exception ex)
            {
                return BadRequest("{\"message\":\"Erro na API. Erro: " + ex.Message + "\"}");
            }
        }
    }
}