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
    public class DistritoRepositoryTest
    {
        public IDistritoRepository CreateRepository()
        {
            //Create a fesh service provider, and therefore a fresh InMemory database instance
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<FeiraDBContext>()
                .UseInMemoryDatabase(databaseName: "FeiraDB")
                .UseInternalServiceProvider(serviceProvider);

            var dbContext = new FeiraDBContext(builder.Options);

            return new DistritoRepository(dbContext);
        }

        [Fact(DisplayName = "GetByIdAsync: Should return an entity by it's id")]
        public async Task GetByIdAsync_should_return_an_entity_by_its_id()
        {
            //Arrange
            var entity = new Core.Entities.Distrito() { CodigoDistrito = 1, NomeDistrito = "Distrito" };
            var repository = CreateRepository();
            await repository.AddAsync(entity);

            //Act
            var result = await repository.GetByIdAsync(entity.CodigoDistrito);

            //Assert
            result.CodigoDistrito.Should().Be(entity.CodigoDistrito);
        }

        [Fact(DisplayName = "GetAllAsync: Should return all elements")]
        public async Task GetAllAsync_should_return_all_elements()
        {
            //Arrange
            var entities = new List<Core.Entities.Distrito>() {
                new Core.Entities.Distrito(){ CodigoDistrito = 1, NomeDistrito = "Distrito1" },
                new Core.Entities.Distrito(){ CodigoDistrito = 2, NomeDistrito = "Distrito2" }
            };

            var repository = CreateRepository();

            foreach(var entity in entities)
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
