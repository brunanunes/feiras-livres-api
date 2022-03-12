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
    [Route("api/subprefeituras")]
    [Produces("application/json")]
    public class SubprefeiturasController : Controller
    {
        private readonly ILogger<SubprefeiturasController> _logger;
        private readonly ISubprefeituraServices _subprefeituraServices;

        public SubprefeiturasController(ILogger<SubprefeiturasController> logger, ISubprefeituraServices subprefeituraServices)
        {
            _logger = logger;
            _subprefeituraServices = subprefeituraServices;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FindSubprefeituraByIdAsync(int id)
        {
            _logger.LogDebug("Method FindSubprefeituraByIdAsync Called");

            if (id <= 0)
            {
                _logger.LogError("Bad Request");
                return BadRequest(new BaseResponse<string>("Invalid Id"));
            }

            var subprefeitura = await _subprefeituraServices.FindSubprefeituraById(id);

            return Ok(new BaseResponse<Core.DTOs.SubprefeituraDTO>(subprefeitura) { Succeeded = true });
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ListSubprefeiturasAsync()
        {
            _logger.LogDebug("Method ListSubprefeiturasAsync Called");

            var subprefeituras = await _subprefeituraServices.ListSubprefeituras();
            return Ok(new BaseResponse<List<Core.DTOs.SubprefeituraDTO>>(subprefeituras) { Succeeded = true });
        }
    }
}
