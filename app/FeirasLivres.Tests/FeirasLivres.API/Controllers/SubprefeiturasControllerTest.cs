using FeirasLivres.API.Controllers;
using FeirasLivres.Application.Services.Interfaces;
using FeirasLivres.ComponentTests.Utils;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace FeirasLivres.ComponentTests.FeirasLivres.API.Controllers
{
    public class SubprefeiturasDataResult
    {
        public Core.DTOs.SubprefeituraDTO Data { get; set; }
    }

    public class SubprefeiturasDataListResult
    {
        public List<Core.DTOs.SubprefeituraDTO> Data { get; set; }
    }

    public class SubprefeiturasControllerTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory webApplicationFactory;
        private readonly Core.DTOs.SubprefeituraDTO subprefeitura;
        private readonly SubprefeiturasDataResult expectedSubprefeituraDataResult;
        private readonly SubprefeiturasDataListResult expectedSubprefeituraDataListResult;
        private readonly JsonSerializerSettings serializerSettings;

        public SubprefeiturasControllerTest(CustomWebApplicationFactory webApplicationFactory)
        {
            this.webApplicationFactory = webApplicationFactory;

            serializerSettings = new JsonSerializerSettings();

            serializerSettings.ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
            serializerSettings.Converters.Add(new StringEnumConverter());
            serializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            serializerSettings.NullValueHandling = NullValueHandling.Ignore;

            subprefeitura = new Core.DTOs.SubprefeituraDTO() { CodigoSubprefeitura = 1, NomeSubprefeitura = "Subprefeitura" };

            expectedSubprefeituraDataResult = new SubprefeiturasDataResult() { Data = subprefeitura };
            expectedSubprefeituraDataListResult = new SubprefeiturasDataListResult() { Data = new List<Core.DTOs.SubprefeituraDTO>() { subprefeitura } };
        }

        [Fact(DisplayName = "Get: should return a Subprefeitura by its id")]
        public async Task Get_should_return_a_subprefeitura_by_its_id()
        {
            //Arrange
            var endpoint = new Uri($"/api/subprefeituras/{expectedSubprefeituraDataResult.Data.CodigoSubprefeitura}", UriKind.Relative);
            var mockService = new Mock<ISubprefeituraServices>();
            var mockLogger = new Mock<ILogger<SubprefeiturasController>>();

            mockService.Setup(s => s.FindSubprefeituraById(expectedSubprefeituraDataResult.Data.CodigoSubprefeitura)).Returns(Task.FromResult(subprefeitura));

            var client = webApplicationFactory.CreateHttpClient(services =>
            {
                services.AddTransient(x => mockService.Object);
            });

            //Act
            var response = await client.GetAsync(endpoint);
            var responseBody = JsonConvert.DeserializeObject<SubprefeiturasDataResult>(await response.Content.ReadAsStringAsync(), serializerSettings);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            responseBody.Should().BeEquivalentTo(expectedSubprefeituraDataResult);
        }

        [Fact(DisplayName = "Get: should return all Subprefeituras")]
        public async Task Get_should_return_all_subprefeituras()
        {
            //Arrange
            var endpoint = new Uri($"/api/subprefeituras", UriKind.Relative);
            var mockService = new Mock<ISubprefeituraServices>();
            var mockLogger = new Mock<ILogger<SubprefeiturasController>>();

            mockService.Setup(s => s.ListSubprefeituras()).Returns(Task.FromResult(new List<Core.DTOs.SubprefeituraDTO>() { subprefeitura }));

            var client = webApplicationFactory.CreateHttpClient(services =>
            {
                services.AddTransient(x => mockService.Object);
            });

            //Act
            var response = await client.GetAsync(endpoint);
            var responseBody = JsonConvert.DeserializeObject<SubprefeiturasDataListResult>(await response.Content.ReadAsStringAsync(), serializerSettings);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            responseBody.Should().BeEquivalentTo(expectedSubprefeituraDataListResult);
        }

        [Fact(DisplayName = "Get: should return BadRequest when invalid id")]
        public async Task Get_should_return_badrequest_when_invalid_id()
        {
            //Arrange
            var endpoint = new Uri($"/api/subprefeituras/A", UriKind.Relative);
            var mockService = new Mock<ISubprefeituraServices>();
            var mockLogger = new Mock<ILogger<DistritosController>>();

            mockService.Setup(s => s.FindSubprefeituraById(0)).Returns(Task.FromResult(subprefeitura));

            var client = webApplicationFactory.CreateHttpClient(services =>
            {
                services.AddTransient(x => mockService.Object);
            });

            //Act
            var response = await client.GetAsync(endpoint);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
