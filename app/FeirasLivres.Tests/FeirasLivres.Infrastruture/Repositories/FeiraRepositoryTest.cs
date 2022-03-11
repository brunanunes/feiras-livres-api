using FeirasLivres.Core.Interfaces.Repositories;
using FeirasLivres.Infrastructure.Persistence;
using FeirasLivres.Infrastructure.Persistence.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FeirasLivres.Tests.FeirasLivres.Infrastruture.Repositories
{
    public class FeiraRepositoryTest
    {
        public IFeiraRepository CreateRepository()
        {
            //Create a fesh service provider, and therefore a fresh InMemory database instance
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<FeiraDBContext>()
                .UseInMemoryDatabase(databaseName: "FeiraDB")
                .UseInternalServiceProvider(serviceProvider);

            var dbContext = new FeiraDBContext(builder.Options);

            return new FeiraRepository(dbContext);
        }

        [Fact(DisplayName = "AddAsync: Should create an entity")]
        public async Task AddAsync_should_create_an_entity()
        {
            //Arrange
            var entity = new Core.Entities.Feira()
            {
                AreaPonderacao = "0000000000",
                Bairro = "BAIRRO",
                CodigoDistrito = 1,
                CodigoSubprefeitura = 2,
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

            var repository = CreateRepository();

            //Act
            var result = await repository.AddAsync(entity);

            //Assert
            result.Should().BeEquivalentTo(entity);
        }

        [Fact(DisplayName = "UpdateAsync: Should update an entity by it's id")]
        public async Task UpdateAsync_should_update_an_entity_by_its_id()
        {
            //Arrange
            var entity = new Core.Entities.Feira()
            {
                AreaPonderacao = "0000000000",
                Bairro = "BAIRRO",
                CodigoDistrito = 1,
                CodigoSubprefeitura = 2,
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

            var repository = CreateRepository();
            var createdEntity = await repository.AddAsync(entity);

            createdEntity.Numero = "N/A";

            //Act
            var result = await repository.UpdateAsync(createdEntity);

            //Assert
            result.Numero.Should().Be(createdEntity.Numero);
        }

        [Fact(DisplayName = "DeteleAsync: Should delete an entity by it's id")]
        public async Task DeleteAsync_should_delete_an_entity_by_its_id()
        {
            //Arrange
            var entity = new Core.Entities.Feira()
            {
                AreaPonderacao = "0000000000",
                Bairro = "BAIRRO",
                CodigoDistrito = 1,
                CodigoSubprefeitura = 2,
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

            var repository = CreateRepository();
            var createdEntity = await repository.AddAsync(entity);

            //Act
            await repository.DeleteAsync(createdEntity);

            //Assert
            var result = await repository.GetByIdAsync(entity.Id);
            result.Should().BeNull();
        }

        [Fact(DisplayName = "GetByIdAsync: Should return an entity by it's id")]
        public async Task GetByIdAsync_should_return_an_entity_by_its_id()
        {
            //Arrange
            var entity = new Core.Entities.Feira()
            {
                AreaPonderacao = "0000000000",
                Bairro = "BAIRRO",
                CodigoDistrito = 1,
                CodigoSubprefeitura = 2,
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
                Subregiao = "SUBREGIAO"
            };

            var repository = CreateRepository();
            await repository.AddAsync(entity);

            //Act
            var result = await repository.GetByIdAsync(entity.Id);

            //Assert
            result.Id.Should().Be(entity.Id);
        }

        [Fact(DisplayName = "GetAllAsync: Should return all elements")]
        public async Task GetAllAsync_should_return_all_elements()
        {
            //Arrange
            var entities = new List<Core.Entities.Feira>() {
                new Core.Entities.Feira() {
                    AreaPonderacao = "0000000000",
                    Bairro = "BAIRRO1",
                    CodigoDistrito = 1,
                    CodigoSubprefeitura = 2,
                    Id = 1,
                    Latitude = -45468,
                    Logradouro = "LOGRADOURO1",
                    Longitude = -324334,
                    NomeFeira = "NOME1",
                    Numero = "12341",
                    Referencia = "REFERENCIA1",
                    Regiao = "REGIAO1",
                    Registro = "4567-801",
                    SetorCensitario = "999999999",
                    Subregiao = "SUBREGIAO1"
                },
                new Core.Entities.Feira() {
                    AreaPonderacao = "0000000000",
                    Bairro = "BAIRRO2",
                    CodigoDistrito = 1,
                    CodigoSubprefeitura = 2,
                    Id = 2,
                    Latitude = -45468,
                    Logradouro = "LOGRADOURO2",
                    Longitude = -324334,
                    NomeFeira = "NOME2",
                    Numero = "12341",
                    Referencia = "REFERENCIA2",
                    Regiao = "REGIAO2",
                    Registro = "4567-801",
                    SetorCensitario = "999999999",
                    Subregiao = "SUBREGIAO2"
                }
            };

            var repository = CreateRepository();

            foreach (var entity in entities)
            {
                await repository.AddAsync(entity);
            }

            //Act
            var result = await repository.GetAllAsync();

            //Assert
            result.Should().BeEquivalentTo(entities);
        }
    }
}
