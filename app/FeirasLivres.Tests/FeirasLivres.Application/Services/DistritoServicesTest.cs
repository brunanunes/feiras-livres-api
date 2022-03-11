using AutoMapper;
using FeirasLivres.Application.Services;
using FeirasLivres.Core.Interfaces.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FeirasLivres.Tests.FeirasLivres.Application.Services
{
    public class DistritoServicesTest
    {
        private readonly Core.Entities.Distrito distrito;
        private readonly IMapper mapper;

        public DistritoServicesTest()
        {
            distrito = new Core.Entities.Distrito() { CodigoDistrito = 1, NomeDistrito = "Distrito" };
            mapper = new MapperConfiguration(c => c.AddProfile<Infrastructure.Persistence.Mappers.AutoMapperProfile>()).CreateMapper();

        }

        [Fact(DisplayName = "FindDistritoById: should return a distrito by its id")]
        public async Task FindDistritoById_should_return_a_distrito_by_its_id()
        {
            //Arrange
            var mockRepository = new Mock<IDistritoRepository>();
            var mockLogger = new Mock<ILogger<DistritoServices>>();

            var expectedResult = new Core.DTOs.DistritoDTO()
            {
                CodigoDistrito = distrito.CodigoDistrito,
                NomeDistrito = distrito.NomeDistrito
            };

            mockRepository.Setup(s => s.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(distrito));

            var service = new DistritoServices(mockRepository.Object, mockLogger.Object, mapper);

            //Act
            var result = await service.FindDistritoById(1);

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }
        
        [Fact(DisplayName = "ListDistritos: should return all distritos")]
        public async Task ListDistritos_should_return_all_distritos()
        {
            //Arrange
            var mockRepository = new Mock<IDistritoRepository>();
            var mockLogger = new Mock<ILogger<DistritoServices>>();

            var expectedResult = new List<Core.DTOs.DistritoDTO>() { new Core.DTOs.DistritoDTO()
            {
                CodigoDistrito = distrito.CodigoDistrito,
                NomeDistrito = distrito.NomeDistrito
            }};

            mockRepository.Setup(s => s.GetAllAsync()).Returns(Task.FromResult(new List<Core.Entities.Distrito>() { distrito }));

            var service = new DistritoServices(mockRepository.Object, mockLogger.Object, mapper);

            //Act
            var result = await service.ListDistritos();

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact(DisplayName = "FindDistritoById: should return NotFound when id doesn't exists")]
        public async Task FindDistritoById_should_return_notfound_when_id_doesnt_exists()
        {
            //Arrange
            var mockRepository = new Mock<IDistritoRepository>();
            var mockLogger = new Mock<ILogger<DistritoServices>>();

            var expectedResult = new Core.DTOs.DistritoDTO()
            {
                CodigoDistrito = distrito.CodigoDistrito,
                NomeDistrito = distrito.NomeDistrito
            };

            mockRepository.Setup(s => s.GetByIdAsync(distrito.CodigoDistrito)).Returns(Task.FromResult(distrito));

            var service = new DistritoServices(mockRepository.Object, mockLogger.Object, mapper);

            //Act and Assert   
            await FluentActions.Invoking(() => service.FindDistritoById(0)).Should().ThrowAsync<Core.Exceptions.ApiException>();
        }

        [Fact(DisplayName = "ListDistritos: should return NotFound when no distritos found")]
        public async Task ListDistritos_should_return_notfound_when_no_distritos_found()
        {
            //Arrange
            var mockRepository = new Mock<IDistritoRepository>();
            var mockLogger = new Mock<ILogger<DistritoServices>>();

            var expectedResult = new Core.DTOs.DistritoDTO()
            {
                CodigoDistrito = distrito.CodigoDistrito,
                NomeDistrito = distrito.NomeDistrito
            };

            mockRepository.Setup(s => s.GetAllAsync()).Returns(Task.FromResult(new List<Core.Entities.Distrito>()));

            var service = new DistritoServices(mockRepository.Object, mockLogger.Object, mapper);

            //Act and Assert   
            await FluentActions.Invoking(() => service.ListDistritos()).Should().ThrowAsync<Core.Exceptions.ApiException>();
        }
    }
}
