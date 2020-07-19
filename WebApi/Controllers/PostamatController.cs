using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostamatController : ControllerBase
    {
        private readonly PostamatService _service;

        public PostamatController(PostamatService service)
        {
            _service = service;
        }
        /// <summary>
        /// Получить информацию о постамате
        /// </summary>
        /// <param name="postamatId">Номер постамата</param>
        /// <returns></returns>
        [HttpGet("{postamatId}")]
        public IActionResult GetPostamatData(string postamatId)
        {
            var postamat = _service.GetPostamat(postamatId);
            return Ok(postamat);
        }
    }
}