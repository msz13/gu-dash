namespace CompetencesService.Infrastructure.Persistance
{
    public class PostgresSettings
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string GetConnectionString()
        {
            //return "host = manny.db.elephantsql.com; database = ambexvwj; password = PYJrzb2mLVb5Qh3XXA2KPNwaXNBzhc4P; username = ambexvwj; port = 5432";
            return $"host= {Host};database= {Database};password= {Password};username= {Username};port= {Port}";
        }
    }
}
