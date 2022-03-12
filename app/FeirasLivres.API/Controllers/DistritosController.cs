using AutoMapper;
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
    [Route("api/distritos")]
    [Produces("application/json")]
    public class DistritosController : Controller
    {
        private readonly ILogger<DistritosController> _logger;
        private readonly IDistritoServices _distritoServices;

        public DistritosController(ILogger<DistritosController> logger, IDistritoServices distritoServices)
        {
            _logger = logger;
            _distritoServices = distritoServices;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FindDistritoByIdAsync(int id)
        {
            _logger.LogDebug("Method FindDistritoByIdAsync Called");

            if (id <= 0)
            {
                _logger.LogError("Bad Request");
                return BadRequest(new BaseResponse<string>("Invalid Id"));
            }

            var distrito = await _distritoServices.FindDistritoById(id);

            return Ok(new BaseResponse<Core.DTOs.DistritoDTO>(distrito) { Succeeded = true });            
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ListDistritosAsync()
        {
            _logger.LogDebug("Method ListDistritosAsync Called");

            var distritos = await _distritoServices.ListDistritos();
            return Ok(new BaseResponse<List<Core.DTOs.DistritoDTO>>(distritos) { Succeeded = true });
        }
    }
}
