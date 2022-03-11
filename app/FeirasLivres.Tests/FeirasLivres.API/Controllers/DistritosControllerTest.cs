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
    public class DistritosDataResult
    {
        public Core.DTOs.DistritoDTO Data { get; set; }
    }

    public class DistritosDataListResult
    {
        public List<Core.DTOs.DistritoDTO> Data { get; set; }
    }

    public class DistritosControllerTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory webApplicationFactory;
        private readonly Core.DTOs.DistritoDTO distrito;
        private readonly DistritosDataResult expectedDistritoDataResult;
        private readonly DistritosDataListResult expectedDistritoDataListResult;
        private readonly JsonSerializerSettings serializerSettings;

        public DistritosControllerTest(CustomWebApplicationFactory webApplicationFactory)
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

            distrito = new Core.DTOs.DistritoDTO() { CodigoDistrito = 1, NomeDistrito = "Distrito" };

            expectedDistritoDataResult = new DistritosDataResult() { Data = distrito };
            expectedDistritoDataListResult = new DistritosDataListResult() { Data = new List<Core.DTOs.DistritoDTO>() { distrito } };
        }

        [Fact(DisplayName = "Get: should return a Distrito by its id")]
        public async Task Get_should_return_a_distrito_by_its_id()
        {
            //Arrange
            var endpoint = new Uri($"/api/distritos/{expectedDistritoDataResult.Data.CodigoDistrito}", UriKind.Relative);
            var mockService = new Mock<IDistritoServices>();
            var mockLogger = new Mock<ILogger<DistritosController>>();

            mockService.Setup(s => s.FindDistritoById(expectedDistritoDataResult.Data.CodigoDistrito)).Returns(Task.FromResult(distrito));

            var client = webApplicationFactory.CreateHttpClient(services =>
            {
                services.AddTransient(x => mockService.Object);
            });

            //Act
            var response = await client.GetAsync(endpoint);
            var responseBody = JsonConvert.DeserializeObject<DistritosDataResult>(await response.Content.ReadAsStringAsync(), serializerSettings);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            responseBody.Should().BeEquivalentTo(expectedDistritoDataResult);
        }

        [Fact(DisplayName = "Get: should return all Distritos")]
        public async Task Get_should_return_all_distritos()
        {
            //Arrange
            var endpoint = new Uri($"/api/distritos", UriKind.Relative);
            var mockService = new Mock<IDistritoServices>();
            var mockLogger = new Mock<ILogger<DistritosController>>();

            mockService.Setup(s => s.ListDistritos()).Returns(Task.FromResult(new List<Core.DTOs.DistritoDTO>() { distrito }));

            var client = webApplicationFactory.CreateHttpClient(services =>
            {
                services.AddTransient(x => mockService.Object);
            });

            //Act
            var response = await client.GetAsync(endpoint);
            var responseBody = JsonConvert.DeserializeObject<DistritosDataListResult>(await response.Content.ReadAsStringAsync(), serializerSettings);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            responseBody.Should().BeEquivalentTo(expectedDistritoDataListResult);
        }

        [Fact(DisplayName = "Get: should return BadRequest when invalid id")]
        public async Task Get_should_return_badrequest_when_invalid_id()
        {
            //Arrange
            var endpoint = new Uri($"/api/distritos/A", UriKind.Relative);
            var mockService = new Mock<IDistritoServices>();
            var mockLogger = new Mock<ILogger<DistritosController>>();

            mockService.Setup(s => s.FindDistritoById(0)).Returns(Task.FromResult(distrito));

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
