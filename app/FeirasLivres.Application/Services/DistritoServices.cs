using AutoMapper;
using FeirasLivres.Application.Services.Interfaces;
using FeirasLivres.Core.Exceptions;
using FeirasLivres.Core.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FeirasLivres.Application.Services
{
    public class DistritoServices : IDistritoServices
    {
        private readonly IDistritoRepository _distritoRepository;
        private readonly ILogger<DistritoServices> _logger;
        private readonly IMapper _mapper;

        public DistritoServices(IDistritoRepository distritoRepository, ILogger<DistritoServices> logger, IMapper mapper)
        {
            _distritoRepository = distritoRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Core.DTOs.DistritoDTO> FindDistritoById(int id)
        {
            _logger.LogInformation("Searching Distrito by Id");

            var distrito = await _distritoRepository.GetByIdAsync(id);

            if (distrito == null)
            {
                _logger.LogError("Not Found");
                throw new ApiException("Distrito not found") { StatusCode = (int)HttpStatusCode.NotFound };
            }

            return _mapper.Map<Core.DTOs.DistritoDTO>(distrito);
        }

        public async Task<List<Core.DTOs.DistritoDTO>> ListDistritos()
        {
            _logger.LogInformation("Searching all Distritos");

            var distritos = await _distritoRepository.GetAllAsync();
            
            if (distritos.Count == 0)
            {
                _logger.LogError("Not Found");
                throw new ApiException("No Distritos found") { StatusCode = (int)HttpStatusCode.NotFound };
            }

            return _mapper.Map<List<Core.DTOs.DistritoDTO>>(distritos).ToList();
        }
    }
}
