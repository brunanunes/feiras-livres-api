using FeirasLivres.Core.Interfaces.Repositories;
using FeirasLivres.Infrastructure.Persistence;
using FeirasLivres.Infrastructure.Persistence.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FeirasLivres.Tests.FeirasLivres.Infrastruture.Repositories
{
    public class SubprefeituraRepositoryTest
    {
        public ISubprefeituraRepository CreateRepository()
        {
            //Create a fesh service provider, and therefore a fresh InMemory database instance
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<FeiraDBContext>()
                .UseInMemoryDatabase(databaseName: "FeiraDB")
                .UseInternalServiceProvider(serviceProvider);

            var dbContext = new FeiraDBContext(builder.Options);

            return new SubprefeituraRepository(dbContext);
        }

        [Fact(DisplayName = "GetByIdAsync: Should return an entity by it's id")]
        public async Task GetByIdAsync_should_return_an_entity_by_its_id()
        {
            //Arrange
            var entity = new Core.Entities.Subprefeitura() { CodigoSubprefeitura = 1, NomeSubprefeitura = "Subprefeitura" };
            var repository = CreateRepository();
            await repository.AddAsync(entity);

            //Act
            var result = await repository.GetByIdAsync(entity.CodigoSubprefeitura);

            //Assert
            result.CodigoSubprefeitura.Should().Be(entity.CodigoSubprefeitura);
        }

        [Fact(DisplayName = "GetAllAsync: Should return all elements")]
        public async Task GetAllAsync_should_return_all_elements()
        {
            //Arrange
            var entities = new List<Core.Entities.Subprefeitura>() {
                new Core.Entities.Subprefeitura(){ CodigoSubprefeitura = 1, NomeSubprefeitura = "Subprefeitura1" },
                new Core.Entities.Subprefeitura(){ CodigoSubprefeitura = 2, NomeSubprefeitura = "Subprefeitura2" }
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
