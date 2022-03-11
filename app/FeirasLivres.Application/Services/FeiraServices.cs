using FeirasLivres.Application.Services.Interfaces;
using FeirasLivres.Core.Exceptions;
using FeirasLivres.Core.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FeirasLivres.Application.Services
{
    public class FeiraServices : IFeiraServices
    {
        private readonly IFeiraRepository _feiraRepository;
        private readonly ILogger<FeiraServices> _logger;
        private readonly IDistritoServices _distritoServices;
        private readonly ISubprefeituraServices _subprefeituraServices;

        public FeiraServices(IFeiraRepository feirasRepository, ILogger<FeiraServices> logger, IDistritoServices distritoServices, ISubprefeituraServices subprefeituraServices)
        {
            _feiraRepository = feirasRepository;
            _logger = logger;
            _distritoServices = distritoServices;
            _subprefeituraServices = subprefeituraServices;
        }

        public async Task<Core.Entities.Feira> CreateFeira(Core.DTOs.FeiraDTO feiraRequest)
        {
            try
            {
                _logger.LogInformation("Searching foreign keys");
                var distrito = await _distritoServices.FindDistritoById(feiraRequest.CodigoDistrito);
                var subprefeitura = await _subprefeituraServices.FindSubprefeituraById(feiraRequest.CodigoSubprefeitura);

                var feira = new Core.Entities.Feira()
                {
                    AreaPonderacao = feiraRequest.AreaPonderacao,
                    Bairro = feiraRequest.Bairro,
                    CodigoDistrito = feiraRequest.CodigoDistrito,
                    CodigoSubprefeitura = feiraRequest.CodigoSubprefeitura,
                    Latitude = feiraRequest.Latitude,
                    Logradouro = feiraRequest.Logradouro,
                    Longitude = feiraRequest.Longitude,
                    NomeFeira = feiraRequest.NomeFeira,
                    Numero = feiraRequest.Numero,
                    Referencia = feiraRequest.Referencia,
                    Regiao = feiraRequest.Regiao,
                    Registro = feiraRequest.Registro,
                    SetorCensitario = feiraRequest.SetorCensitario,
                    Subregiao = feiraRequest.Subregiao
                };

                _logger.LogInformation("Creating new Feira");
                var feiraResponse = await _feiraRepository.AddAsync(feira);

                if (feiraResponse == null)
                {
                    _logger.LogError("Internal Server Error");
                    throw new Exception("Internal Server Error");
                }

                _logger.LogInformation("New Feira created successfully");
                return feiraResponse;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error. Exception: {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteFeira(int id)
        {
            try
            {
                _logger.LogInformation("Searching Feira by Id");
                var feira = await _feiraRepository.GetByIdAsync(id);

                if (feira == null)
                {
                    _logger.LogError("Not Found");
                    throw new ApiException("Id not found") { StatusCode = (int)HttpStatusCode.NotFound };
                }

                _logger.LogInformation("Deleting Feira");
                await _feiraRepository.DeleteAsync(feira);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error. Exception: {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Core.Entities.Feira>> FindFeirasByFilters(string distrito = "", string regiao = "", string nome = "", string bairro = "")
        {
            try
            {
                _logger.LogInformation("Searching records according to filters");

                var feiras = await _feiraRepository.ListFeirasAsync();

                if (!string.IsNullOrEmpty(distrito))
                    feiras = feiras.Where(x => x.Distrito.NomeDistrito.ToUpper() == distrito.ToUpper()).ToList();
                if (!string.IsNullOrEmpty(regiao))
                    feiras = feiras.Where(x => x.Regiao.ToUpper() == regiao.ToUpper()).ToList();
                if (!string.IsNullOrEmpty(nome))
                    feiras = feiras.Where(x => x.NomeFeira.ToUpper() == nome.ToUpper()).ToList();
                if (!string.IsNullOrEmpty(bairro))
                    feiras = feiras.Where(x => !string.IsNullOrEmpty(x.Bairro) && x.Bairro.ToUpper() == bairro.ToUpper()).ToList();

                if (feiras.Count == 0)
                {
                    _logger.LogError("Not Found");
                    throw new ApiException("No record found for the this filters") { StatusCode = (int)HttpStatusCode.NotFound };
                }

                return feiras.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error. Exception: {ex.Message}");
                throw new Exception(ex.Message);
            }            
        }

        public async Task<Core.Entities.Feira> UpdateFeira(int id, Core.DTOs.FeiraDTO feiraRequest)
        {
            try
            {
                _logger.LogInformation("Searching for existing Feira");

                var feira = await _feiraRepository.GetByIdAsync(id);
                if (feira == null)
                {
                    _logger.LogInformation("Not Found");
                    throw new ApiException("Feira found") { StatusCode = (int)HttpStatusCode.NotFound };
                }

                _logger.LogInformation("Searching foreign keys");
                var distrito = await _distritoServices.FindDistritoById(feiraRequest.CodigoDistrito);
                var subprefeitura = await _subprefeituraServices.FindSubprefeituraById(feiraRequest.CodigoSubprefeitura);

                feira.AreaPonderacao = feiraRequest.AreaPonderacao;
                feira.Bairro = feiraRequest.Bairro;
                feira.CodigoDistrito = feiraRequest.CodigoDistrito;
                feira.CodigoSubprefeitura = feiraRequest.CodigoSubprefeitura;
                feira.Latitude = feiraRequest.Latitude;
                feira.Logradouro = feiraRequest.Logradouro;
                feira.Longitude = feiraRequest.Longitude;
                feira.NomeFeira = feiraRequest.NomeFeira;
                feira.Numero = feiraRequest.Numero;
                feira.Referencia = feiraRequest.Referencia;
                feira.Regiao = feiraRequest.Regiao;
                feira.Registro = feiraRequest.Registro;
                feira.SetorCensitario = feiraRequest.SetorCensitario;
                feira.Subregiao = feiraRequest.Subregiao;

                _logger.LogInformation("Updanting Feira");
                var feiraResponse = await _feiraRepository.UpdateAsync(feira);

                if (feiraResponse == null)
                {
                    _logger.LogError("Internal Server Error");
                    throw new Exception("Internal Server Error");
                }

                _logger.LogInformation("Feira updated successfully");
                return feiraResponse;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error. Exception: {ex.Message}");
                throw new Exception(ex.Message);
            }
        }
    }
}
