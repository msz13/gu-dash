using CompetencesService.Infrastructure.Persistance;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Threading.Tasks;

namespace CompetencesFunctionalTests
{
    public class DbTestHelper
    {
        NpgsqlConnection connection;

        public DbTestHelper()
        {
            this.connection = new NpgsqlConnection(GetConnectionString());
        }

        public async void StartConnecton()
        {
            await this.connection.OpenAsync();
        }
        public async Task ClearTables()
        {
            using var cmd = new NpgsqlCommand("TRUNCATE mt_events, mt_streams, mt_doc_competencedto;", connection);
            await cmd.ExecuteNonQueryAsync();

        }


        string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("DbSettings.json");
            var configuration = builder.Build();

            return configuration.GetSection("Postgres").Get<PostgresSettings>().GetConnectionString();

        }

        public async void Dispose()
        {
            await this.connection.DisposeAsync();
        }
    }
}
