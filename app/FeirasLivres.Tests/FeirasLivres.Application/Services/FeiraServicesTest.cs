using FeirasLivres.Application.Services;
using FeirasLivres.Application.Services.Interfaces;
using FeirasLivres.Core.Interfaces.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FeirasLivres.Tests.FeirasLivres.Application.Services
{
    public class FeiraServicesTest
    {
        private readonly Core.Entities.Feira feira;
        private readonly Core.DTOs.FeiraDTO feiraDTO;
        private readonly Core.DTOs.DistritoDTO distritoDTO;
        private readonly Core.DTOs.SubprefeituraDTO subprefeituraDTO;

        public FeiraServicesTest()
        {
            distritoDTO = new Core.DTOs.DistritoDTO() { CodigoDistrito = 1, NomeDistrito = "DISTRITO" };
            subprefeituraDTO = new Core.DTOs.SubprefeituraDTO() { CodigoSubprefeitura = 2, NomeSubprefeitura = "SUBPREFEITURA" };

            feira = new Core.Entities.Feira()
            {
                AreaPonderacao = "0000000000",
                Bairro = "BAIRRO",
                Id = 1,
                Latitude = -45468,
                Logradouro = "LOGRADOURO",
                Longitude = -324334,
                NomeFeira = "NOME",
                Numero = "1234",
                Referencia = "REFERENCIA",
                Regiao = "REGIAO",
                Registro = "4567-80",
                SetorCensitario = "999999999",
                Subregiao = "SUBREGIAO",
                Distrito = new Core.Entities.Distrito() { CodigoDistrito = distritoDTO.CodigoDistrito, NomeDistrito = distritoDTO.NomeDistrito },
                Subprefeitura = new Core.Entities.Subprefeitura() { CodigoSubprefeitura = subprefeituraDTO.CodigoSubprefeitura, NomeSubprefeitura = subprefeituraDTO.NomeSubprefeitura }
            };

            feiraDTO = new Core.DTOs.FeiraDTO()
            {
                AreaPonderacao = "0000000000",
                CodigoDistrito = distritoDTO.CodigoDistrito,
                CodigoSubprefeitura = subprefeituraDTO.CodigoSubprefeitura,
                Bairro = "BAIRRO",
                Latitude = -45468,
                Logradouro = "LOGRADOURO",
                Longitude = -324334,
                NomeFeira = "NOME",
                Numero = "1234",
                Referencia = "REFERENCIA",
                Regiao = "REGIAO",
                Registro = "4567-80",
                SetorCensitario = "999999999",
                Subregiao = "SUBREGIAO"
            };
        }

        [Fact(DisplayName = "CreateFeira: should create a new feira")]
        public async Task CreateFeira_should_create_a_new_feira()
        {
            //Arrange
            var mockRepository = new Mock<IFeiraRepository>();
            var mockDistritoService = new Mock<IDistritoServices>();
            var mockSubprefeituraService = new Mock<ISubprefeituraServices>();
            var mockService = new Mock<IDistritoServices>();
            var mockLogger = new Mock<ILogger<FeiraServices>>();

            mockRepository.Setup(s => s.AddAsync(It.IsAny<Core.Entities.Feira>())).Returns(Task.FromResult(feira));
            mockDistritoService.Setup(s => s.FindDistritoById(It.IsAny<int>())).Returns(Task.FromResult(distritoDTO));
            mockSubprefeituraService.Setup(s => s.FindSubprefeituraById(It.IsAny<int>())).Returns(Task.FromResult(subprefeituraDTO));

            var service = new FeiraServices(mockRepository.Object, mockLogger.Object, mockDistritoService.Object, mockSubprefeituraService.Object);

            //Act
            var result = await service.CreateFeira(feiraDTO);

            //Assert
            result.Should().BeEquivalentTo(feira);
        }

        [Fact(DisplayName = "UpdateFeira: should update feira by it's id")]
        public async Task UpdateFeira_should_update_feira_by_its_id()
        {
            //Arrange
            var mockRepository = new Mock<IFeiraRepository>();
            var mockDistritoService = new Mock<IDistritoServices>();
            var mockSubprefeituraService = new Mock<ISubprefeituraServices>();
            var mockService = new Mock<IDistritoServices>();
            var mockLogger = new Mock<ILogger<FeiraServices>>();

            var expectedResultUpdated = new Core.Entities.Feira();
            expectedResultUpdated = feira;
            expectedResultUpdated.Numero = "N/A";

            mockRepository.Setup(s => s.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(feira));
            mockRepository.Setup(s => s.UpdateAsync(It.IsAny<Core.Entities.Feira>())).Returns(Task.FromResult(expectedResultUpdated));
            mockDistritoService.Setup(s => s.FindDistritoById(It.IsAny<int>())).Returns(Task.FromResult(distritoDTO));
            mockSubprefeituraService.Setup(s => s.FindSubprefeituraById(It.IsAny<int>())).Returns(Task.FromResult(subprefeituraDTO));

            var service = new FeiraServices(mockRepository.Object, mockLogger.Object, mockDistritoService.Object, mockSubprefeituraService.Object);
                        
            var feiraDtoUpdated = new Core.DTOs.FeiraDTO();
            feiraDtoUpdated = feiraDTO;
            feiraDtoUpdated.Numero = "N/A";

            //Act
            var result = await service.UpdateFeira(feira.Id, feiraDtoUpdated);

            //Assert
            result.Should().BeEquivalentTo(expectedResultUpdated);
        }

        [Fact(DisplayName = "DeleteFeira: should update feira by it's id")]
        public async Task DeleteFeira_should_delete_feira_by_its_id()
        {
            //Arrange
            var mockRepository = new Mock<IFeiraRepository>();
            var mockDistritoService = new Mock<IDistritoServices>();
            var mockSubprefeituraService = new Mock<ISubprefeituraServices>();
            var mockService = new Mock<IDistritoServices>();
            var mockLogger = new Mock<ILogger<FeiraServices>>();

            mockRepository.Setup(s => s.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(feira));
            mockRepository.Setup(s => s.DeleteAsync(It.IsAny<Core.Entities.Feira>()));
            mockDistritoService.Setup(s => s.FindDistritoById(It.IsAny<int>())).Returns(Task.FromResult(distritoDTO));
            mockSubprefeituraService.Setup(s => s.FindSubprefeituraById(It.IsAny<int>())).Returns(Task.FromResult(subprefeituraDTO));

            var service = new FeiraServices(mockRepository.Object, mockLogger.Object, mockDistritoService.Object, mockSubprefeituraService.Object);
                        
            //Act
            Func<Task> func = async () => await  service.DeleteFeira(feira.Id);

            //Assert
            await func.Should().NotThrowAsync<Exception>();
            await func.Should().NotThrowAsync<Core.Exceptions.ApiException>();
        }

        [Fact(DisplayName = "FindFeiraByFilters: should return feiras by filters")]
        public async Task FindFeiraByFilters_should_return_feiras_by_filters()
        {
            //Arrange
            var mockRepository = new Mock<IFeiraRepository>();
            var mockDistritoService = new Mock<IDistritoServices>();
            var mockSubprefeituraService = new Mock<ISubprefeituraServices>();
            var mockService = new Mock<IDistritoServices>();
            var mockLogger = new Mock<ILogger<FeiraServices>>();

            mockRepository.Setup(s => s.ListFeirasAsync()).Returns(Task.FromResult(new List<Core.Entities.Feira>() { feira }));
            mockDistritoService.Setup(s => s.FindDistritoById(It.IsAny<int>())).Returns(Task.FromResult(distritoDTO));
            mockSubprefeituraService.Setup(s => s.FindSubprefeituraById(It.IsAny<int>())).Returns(Task.FromResult(subprefeituraDTO));

            var service = new FeiraServices(mockRepository.Object, mockLogger.Object, mockDistritoService.Object, mockSubprefeituraService.Object);

            //Act
            var result = await service.FindFeirasByFilters(feira.Distrito.NomeDistrito, feira.Regiao, feira.NomeFeira, feira.Bairro);

            //Assert
            result.Should().BeEquivalentTo(new List<Core.Entities.Feira> { feira });
        }
    }
}
