using FeirasLivres.Application.Responses.Base;
using FeirasLivres.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeirasLivres.API.Controllers
{
    [ApiController]
    [Route("api/feiras")]
    [Produces("application/json")]
    public class FeirasController : Controller
    {
        private readonly ILogger<FeirasController> _logger;
        private readonly IFeiraServices _feiraServices;

        public FeirasController(ILogger<FeirasController> logger, IFeiraServices feiraServices)
        {
            _logger = logger;
            _feiraServices = feiraServices;
        }
                
        [HttpGet("feirasbyfilter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FindFeirasByFilterAsync(string distrito, string regiao, string nome_feira, string bairro)
        {
            _logger.LogDebug("Method FindFeirasByFilterAsync Called");

            if (string.IsNullOrEmpty(distrito) && string.IsNullOrEmpty(regiao) && string.IsNullOrEmpty(nome_feira) && string.IsNullOrEmpty(bairro))
            {
                _logger.LogError("Bad Request");
                return BadRequest(new BaseResponse<string>("You must use at least one filter"));
            }

            var feiras = await _feiraServices.FindFeirasByFilters(distrito, regiao, nome_feira, bairro);

            return Ok(new BaseResponse<List<Core.Entities.Feira>>(feiras) { Succeeded = true });
        }
                
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteFeiraAsync(int id)
        {
            _logger.LogDebug("Method DeleteFeiraAsync Called");

            if (id <= 0)
            {
                _logger.LogError("Bad Request");
                return BadRequest(new BaseResponse<string>("Invalid Id"));
            }

            await _feiraServices.DeleteFeira(id);
            return Ok(new BaseResponse<string>("Feira deleted successfully") { Succeeded = true });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateFeiraAsync([FromBody] Core.DTOs.FeiraDTO feiraRequest)
        {
            _logger.LogDebug("Method CreateFeiraAsync Called");

            if (!ModelState.IsValid)
            {
                _logger.LogError("Bad Request");
                return BadRequest(new BaseResponse<object>(ModelState));
            }

            var feira = await _feiraServices.CreateFeira(feiraRequest);

            return Ok(new BaseResponse<Core.Entities.Feira>(feira) { Succeeded = true });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateFeiraAsync(int id, [FromBody] Core.DTOs.FeiraDTO feiraRequest)
        {
            _logger.LogDebug("Method UpdateFeiraAsync Called");

            if (!ModelState.IsValid)
            {
                _logger.LogError("Bad Request");
                return BadRequest(new BaseResponse<object>(ModelState));
            }

            var feira = await _feiraServices.UpdateFeira(id, feiraRequest);

            return Ok(new BaseResponse<Core.Entities.Feira>(feira) { Succeeded = true });
        }
    }
}
