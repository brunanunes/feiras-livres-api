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
    public class SubprefeituraServices : ISubprefeituraServices
    {
        private readonly ISubprefeituraRepository _subprefeituraRepository;
        private readonly ILogger<SubprefeituraServices> _logger;
        private readonly IMapper _mapper;

        public SubprefeituraServices(ISubprefeituraRepository subprefeituraRepository, ILogger<SubprefeituraServices> logger, IMapper mapper)
        {
            _subprefeituraRepository = subprefeituraRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Core.DTOs.SubprefeituraDTO> FindSubprefeituraById(int id)
        {
            _logger.LogInformation("Searching Subprefeitura by Id");

            var subprefeitura = await _subprefeituraRepository.GetByIdAsync(id);

            if (subprefeitura == null)
            {
                _logger.LogError("Not Found");
                throw new ApiException("Subprefeitura not found") { StatusCode = (int)HttpStatusCode.NotFound };
            }

            return _mapper.Map<Core.DTOs.SubprefeituraDTO>(subprefeitura);
        }

        public async Task<List<Core.DTOs.SubprefeituraDTO>> ListSubprefeituras()
        {
            _logger.LogInformation("Searching all Subprefeitura");

            var subprefeituras = await _subprefeituraRepository.GetAllAsync();

            if (subprefeituras.Count == 0)
            {
                _logger.LogError("Not Found");
                throw new ApiException("No Subprefeitura found") { StatusCode = (int)HttpStatusCode.NotFound };
            }

            return _mapper.Map<List<Core.DTOs.SubprefeituraDTO>>(subprefeituras).ToList();
        }
    }
}
