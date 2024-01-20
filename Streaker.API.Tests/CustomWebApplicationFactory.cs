using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Streaker.DAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace Streaker.API.Tests
{
    public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove existing DbContext registration
                var dbContextDescriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                if (dbContextDescriptor != null)
                    services.Remove(dbContextDescriptor);

                // Remove existing DbConnection registration
                var dbConnectionDescriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbConnection));

                if (dbConnectionDescriptor != null)
                    services.Remove(dbConnectionDescriptor);

                // Add in-memory database and configure DbContext
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase(new Guid().ToString());
                });

            });

            builder.UseEnvironment("Development");
        }
    }
}
