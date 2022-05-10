using CA_MCare21_MasterAPI.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CA_MCare21_MasterAPI;

namespace CA_MCare21_MasterAPIIntegrationTest
{
    public class TestClientProvider:IDisposable
    {
        private TestServer server;

        public HttpClient Client { get; private set; }

        public TestClientProvider()
        {
            //Code Added for accessing appsetting.json file
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

            WebHostBuilder webHostBuilder = new WebHostBuilder();
            webHostBuilder.ConfigureServices(s => s.AddDbContext<MalaysiaDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("MalaysiaConnectionString"))));
            webHostBuilder.UseStartup<Startup>();
            //server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            server= new TestServer(webHostBuilder);
            Client = server.CreateClient();
        }

        public void Dispose()
        {
            server?.Dispose();
            Client?.Dispose();
        }
    }
}
