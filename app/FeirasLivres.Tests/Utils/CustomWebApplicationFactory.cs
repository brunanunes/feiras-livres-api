using FeirasLivres.API;
using FeirasLivres.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net.Http;

namespace FeirasLivres.ComponentTests.Utils
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        public DbContext dbContext { get; private set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("local");

            //Remove the app's DBContext registration
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DbContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                //Add a database context using an in-memoru database for testing
                services.AddDbContext<DbContext>(options => 
                {
                    options.UseInMemoryDatabase("InMemoryAppDb");

                    //Create a new service provider for the DbContext
                    var internalServiceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
                    options.UseInternalServiceProvider(internalServiceProvider);
                });

                services.AddMvc().AddApplicationPart(typeof(Startup).Assembly);

                //Build the service provider
                var serviceProvider = services.BuildServiceProvider();

                //Create a scope to obtain the reference to the DbContext
                using (var scope = serviceProvider.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    dbContext = scopedServices.GetRequiredService<FeiraDBContext>();
                    dbContext.Database.EnsureCreated();
               }
            });
        }

        public HttpClient CreateHttpClient(Action<IServiceCollection> servicesConfiguration)
        {
            var client = this.WithWebHostBuilder(builder => builder.ConfigureTestServices(servicesConfiguration)).CreateClient();

            client.DefaultRequestHeaders.Clear();
            return client;
        }
    }
}
