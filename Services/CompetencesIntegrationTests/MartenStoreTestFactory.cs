using CompetencesService.Infrastructure.Persistance;
using GuDash.CompetencesService.Infrastructure.Persistance;
using Marten;
using Microsoft.Extensions.Configuration;
using System;

namespace CompetencesIntegrationTests
{
    public class MartenStoreTestFactory : IDisposable
    {

        public IDocumentStore Store { get; private set; }


        public MartenStoreTestFactory()
        {
            Store = CompetencesDocumentStore.CreateStore(GetConnectionString());
        }

        public void Dispose()
        {
            Store.Dispose();
        }

        string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("DbSettings.json");
            var configuration = builder.Build();

            return configuration.GetSection("Postgres").Get<PostgresSettings>().GetConnectionString();

        }
    }
}
