using AutoMapper;
using FeirasLivres.Application.Services;
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
    public class SubprefeituraServicesTest
    {
        private readonly Core.Entities.Subprefeitura subprefeitura;
        private readonly IMapper mapper;

        public SubprefeituraServicesTest()
        {
            subprefeitura = new Core.Entities.Subprefeitura() { CodigoSubprefeitura = 1, NomeSubprefeitura = "Subprefeitura" };
            mapper = new MapperConfiguration(c => c.AddProfile<Infrastructure.Persistence.Mappers.AutoMapperProfile>()).CreateMapper();

        }

        [Fact(DisplayName = "FindSubprefeituraById: should return a subprefeitura by its id")]
        public async Task FindSubprefeituraById_should_return_a_subprefeitura_by_its_id()
        {
            //Arrange
            var mockRepository = new Mock<ISubprefeituraRepository>();
            var mockLogger = new Mock<ILogger<SubprefeituraServices>>();

            var expectedResult = new Core.DTOs.SubprefeituraDTO()
            {
                CodigoSubprefeitura = subprefeitura.CodigoSubprefeitura,
                NomeSubprefeitura = subprefeitura.NomeSubprefeitura
            };

            mockRepository.Setup(s => s.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(subprefeitura));

            var service = new SubprefeituraServices(mockRepository.Object, mockLogger.Object, mapper);

            //Act
            var result = await service.FindSubprefeituraById(1);

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact(DisplayName = "ListSubprefeituras: should return all subprefeituras")]
        public async Task ListSubprefeituras_should_return_all_subprefeituras()
        {
            //Arrange
            var mockRepository = new Mock<ISubprefeituraRepository>();
            var mockLogger = new Mock<ILogger<SubprefeituraServices>>();

            var expectedResult = new List<Core.DTOs.SubprefeituraDTO>() { new Core.DTOs.SubprefeituraDTO()
            {
                CodigoSubprefeitura = subprefeitura.CodigoSubprefeitura,
                NomeSubprefeitura = subprefeitura.NomeSubprefeitura
            }};

            mockRepository.Setup(s => s.GetAllAsync()).Returns(Task.FromResult(new List<Core.Entities.Subprefeitura>() { subprefeitura }));

            var service = new SubprefeituraServices(mockRepository.Object, mockLogger.Object, mapper);

            //Act
            var result = await service.ListSubprefeituras();

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact(DisplayName = "ListSubprefeituras: should return NotFound when no subprefeituras found")]
        public async Task FindSubprefeituraById_should_return_notfound_when_no_subprefeituras_found()
        {
            //Arrange
            var mockRepository = new Mock<ISubprefeituraRepository>();
            var mockLogger = new Mock<ILogger<SubprefeituraServices>>();

            var expectedResult = new Core.DTOs.SubprefeituraDTO()
            {
                CodigoSubprefeitura = subprefeitura.CodigoSubprefeitura,
                NomeSubprefeitura = subprefeitura.NomeSubprefeitura
            };

            mockRepository.Setup(s => s.GetAllAsync()).Returns(Task.FromResult(new List<Core.Entities.Subprefeitura>()));

            var service = new SubprefeituraServices(mockRepository.Object, mockLogger.Object, mapper);

            //Act and Assert   
            await FluentActions.Invoking(() => service.ListSubprefeituras()).Should().ThrowAsync<Core.Exceptions.ApiException>();
        }
    }
}
