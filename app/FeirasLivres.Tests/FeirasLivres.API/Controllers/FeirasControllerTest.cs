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
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FeirasLivres.ComponentTests.FeirasLivres.API.Controllers
{
    public class FeirasDataResult
    {
        public Core.Entities.Feira Data { get; set; }
    }

    public class FeirasDataListResult
    {
        public List<Core.Entities.Feira> Data { get; set; }
    }

    public class FeirasControllerTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory webApplicationFactory;
        private readonly Core.Entities.Feira feira;
        private readonly FeirasDataResult expectedFeirasDataResult;
        private readonly FeirasDataListResult expectedFeirasDataListResult;
        private readonly JsonSerializerSettings serializerSettings;
        private readonly Core.DTOs.FeiraDTO feiraDTO;

        public FeirasControllerTest(CustomWebApplicationFactory webApplicationFactory)
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
                Distrito = new Core.Entities.Distrito() { CodigoDistrito = 1, NomeDistrito = "DISTRITO" },
                Subprefeitura = new Core.Entities.Subprefeitura() { CodigoSubprefeitura = 2, NomeSubprefeitura = "SUBPREFEITURA" }
            };

            feiraDTO = new Core.DTOs.FeiraDTO()
            {
                AreaPonderacao = "0000000000",
                CodigoDistrito = 1,
                CodigoSubprefeitura = 2,
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

            expectedFeirasDataResult = new FeirasDataResult() { Data = feira };
            expectedFeirasDataListResult = new FeirasDataListResult() { Data = new List<Core.Entities.Feira>() { feira } };
        }

        [Fact(DisplayName = "Get: should return Feiras by filters")]
        public async Task Get_should_return_feiras_by_filters()
        {
            //Arrange
            var endpoint = new Uri($"/api/feiras/feirasbyfilter?distrito=DISTRITO&regiao=REGIAO&nome_feira=NOME&bairro=BAIRRO", UriKind.Relative);
            var mockService = new Mock<IFeiraServices>();
            var mockLogger = new Mock<ILogger<FeirasController>>();

            mockService.Setup(s => s.FindFeirasByFilters("DISTRITO", "REGIAO", "NOME", "BAIRRO")).Returns(Task.FromResult(new List<Core.Entities.Feira>() { feira }));

            var client = webApplicationFactory.CreateHttpClient(services =>
            {
                services.AddTransient(x => mockService.Object);
            });

            //Act
            var response = await client.GetAsync(endpoint);
            var responseBody = JsonConvert.DeserializeObject<FeirasDataListResult>(await response.Content.ReadAsStringAsync(), serializerSettings);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            responseBody.Should().BeEquivalentTo(expectedFeirasDataListResult);
        }

        [Fact(DisplayName = "Get: should return BadRequest when no filters")]
        public async Task Get_should_return_badrequest_when_no_filters()
        {
            //Arrange
            var endpoint = new Uri($"/api/feiras/feirasbyfilter?distrito=&regiao=&nome_feira=&bairro=", UriKind.Relative);
            var mockService = new Mock<IFeiraServices>();
            var mockLogger = new Mock<ILogger<FeirasController>>();

            mockService.Setup(s => s.FindFeirasByFilters("DISTRITO", "REGIAO", "NOME", "BAIRRO")).Returns(Task.FromResult(new List<Core.Entities.Feira>() { feira }));

            var client = webApplicationFactory.CreateHttpClient(services =>
            {
                services.AddTransient(x => mockService.Object);
            });

            //Act
            var response = await client.GetAsync(endpoint);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact(DisplayName = "Delete: should return BadRequest when invalid id")]
        public async Task Delete_should_return_badrequest_when_invalid_id()
        {
            //Arrange
            var endpoint = new Uri($"/api/feiras/A", UriKind.Relative);
            var mockService = new Mock<IFeiraServices>();
            var mockLogger = new Mock<ILogger<FeirasController>>();

            mockService.Setup(s => s.DeleteFeira(0));

            var client = webApplicationFactory.CreateHttpClient(services =>
            {
                services.AddTransient(x => mockService.Object);
            });

            //Act
            var response = await client.DeleteAsync(endpoint);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact(DisplayName = "Delete: should delete feira by its id")]
        public async Task Delete_should_delete_feira_by_its_id()
        {
            //Arrange
            var endpoint = new Uri($"/api/feiras/1", UriKind.Relative);
            var mockService = new Mock<IFeiraServices>();
            var mockLogger = new Mock<ILogger<FeirasController>>();

            mockService.Setup(s => s.DeleteFeira(1));

            var client = webApplicationFactory.CreateHttpClient(services =>
            {
                services.AddTransient(x => mockService.Object);
            });

            //Act
            var response = await client.DeleteAsync(endpoint);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact(DisplayName = "Post: should return BadRequest when invalid model")]
        public async Task Post_should_return_badrequest_when_invalid_model()
        {
            //Arrange
            var endpoint = new Uri($"/api/feiras", UriKind.Relative);
            var mockService = new Mock<IFeiraServices>();
            var mockLogger = new Mock<ILogger<FeirasController>>();

            mockService.Setup(s => s.CreateFeira(It.IsAny<Core.DTOs.FeiraDTO>())).Returns(Task.FromResult(feira));

            var client = webApplicationFactory.CreateHttpClient(services =>
            {
                services.AddTransient(x => mockService.Object);
            });

            var request = new Core.DTOs.FeiraDTO { };
            var jsonRequest = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            //Act            
            var response = await client.PostAsync(endpoint, content);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact(DisplayName = "Post: should insert a new feira")]
        public async Task Post_should_insert_a_new_feira()
        {
            //Arrange
            var endpoint = new Uri($"/api/feiras", UriKind.Relative);
            var mockService = new Mock<IFeiraServices>();
            var mockLogger = new Mock<ILogger<FeirasController>>();

            mockService.Setup(s => s.CreateFeira(It.IsAny<Core.DTOs.FeiraDTO>())).Returns(Task.FromResult(feira));

            var client = webApplicationFactory.CreateHttpClient(services =>
            {
                services.AddTransient(x => mockService.Object);
            });
                        
            var jsonRequest = JsonConvert.SerializeObject(feiraDTO, serializerSettings);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            //Act
            var response = await client.PostAsync(endpoint, content);
            var responseBody = JsonConvert.DeserializeObject<FeirasDataResult>(await response.Content.ReadAsStringAsync(), serializerSettings);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            responseBody.Should().BeEquivalentTo(expectedFeirasDataResult);

        }

        [Fact(DisplayName = "Put: should return BadRequest when invalid model")]
        public async Task Put_should_return_badrequest_when_invalid_model()
        {
            //Arrange
            var endpoint = new Uri($"/api/feiras/1", UriKind.Relative);
            var mockService = new Mock<IFeiraServices>();
            var mockLogger = new Mock<ILogger<FeirasController>>();

            mockService.Setup(s => s.UpdateFeira(1, feiraDTO)).Returns(Task.FromResult(feira));

            var client = webApplicationFactory.CreateHttpClient(services =>
            {
                services.AddTransient(x => mockService.Object);
            });

            var request = new Core.DTOs.FeiraDTO { };
            var jsonRequest = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            //Act
            var response = await client.PutAsync(endpoint, content);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact(DisplayName = "Put: should return BadRequest when invalid id")]
        public async Task Put_should_return_badrequest_when_invalid_id()
        {
            //Arrange
            var endpoint = new Uri($"/api/feiras/A", UriKind.Relative);
            var mockService = new Mock<IFeiraServices>();
            var mockLogger = new Mock<ILogger<FeirasController>>();

            mockService.Setup(s => s.UpdateFeira(0, feiraDTO)).Returns(Task.FromResult(feira));

            var client = webApplicationFactory.CreateHttpClient(services =>
            {
                services.AddTransient(x => mockService.Object);
            });

            var request = new Core.DTOs.FeiraDTO { };
            var jsonRequest = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            //Act
            var response = await client.PutAsync(endpoint, content);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact(DisplayName = "Put: should update feira by its id")]
        public async Task Put_should_update_feira_by_its_id()
        {
            //Arrange
            var endpoint = new Uri($"/api/feiras/1", UriKind.Relative);
            var mockService = new Mock<IFeiraServices>();
            var mockLogger = new Mock<ILogger<FeirasController>>();

            var expectedResultUpdated = new Core.Entities.Feira();
            expectedResultUpdated = feira;
            expectedResultUpdated.Numero = "N/A";

            mockService.Setup(s => s.UpdateFeira(It.IsAny<int>(), It.IsAny<Core.DTOs.FeiraDTO>())).Returns(Task.FromResult(expectedResultUpdated));

            var client = webApplicationFactory.CreateHttpClient(services =>
            {
                services.AddTransient(x => mockService.Object);
            });

            var feiraDtoUpdated = new Core.DTOs.FeiraDTO();
            feiraDtoUpdated = feiraDTO;
            feiraDtoUpdated.Numero = "N/A";

            var jsonRequest = JsonConvert.SerializeObject(feiraDtoUpdated, serializerSettings);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            //Act
            var response = await client.PutAsync(endpoint, content);
            var responseBody = JsonConvert.DeserializeObject<FeirasDataResult>(await response.Content.ReadAsStringAsync(), serializerSettings);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            responseBody.Data.Should().BeEquivalentTo(expectedResultUpdated);
        }
    }
}
