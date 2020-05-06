using Npgsql;


namespace CompetencesIntegrationTests
{
    public class DbTestHelper
    {
        NpgsqlConnection connection;

        public DbTestHelper(string connectionString)
        {
            this.connection = new NpgsqlConnection(connectionString);
        }

        public async void StartConnecton()
        {
            await this.connection.OpenAsync();
        }
        public async void ClearTables()
        {
            using var cmd = new NpgsqlCommand("TRUNCATE mt_events, mt_streams, mt_doc_competencedto;", connection);
            await cmd.ExecuteNonQueryAsync();
        }

        public async void Dispose()
        {
            await this.connection.DisposeAsync();
        }
    }
}
