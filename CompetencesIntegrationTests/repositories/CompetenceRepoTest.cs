using CompetencesService.Application.ReadModel;
using FluentAssertions;
using GuDash.CompetencesService.Domain.Competences;
using GuDash.CompetencesService.Domain.Learner;
using GuDash.CompetencesService.Infrastructure.Persistance;
using GuDash.CompetencesService.Proto;
using Marten;
using Npgsql;
using System.Linq;
using System.Threading.Tasks;
using Xunit;



namespace CompetencesIntegrationTests.repositories
{
    
    public class CompetenceRepoTest
    {
        string connectionString = "host=manny.db.elephantsql.com;database=ambexvwj;password=PYJrzb2mLVb5Qh3XXA2KPNwaXNBzhc4P;username=ambexvwj;port=5432";

        IDocumentStore store;
         
        public CompetenceRepoTest()
        {

            this.store = CompetencesDocumentStore.CreateStore(connectionString);           
        }

        public async Task DropTables()
        {
            var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();

            var transaction = connection.BeginTransaction(); 

            using (var cmd = new NpgsqlCommand("DROP TABLE mt_events",connection, transaction))
            {
                await cmd.ExecuteNonQueryAsync();
            };

            using (var cmd = new NpgsqlCommand("DROP TABLE mt_streams", connection, transaction))
            {
                await cmd.ExecuteNonQueryAsync();
            };

            await transaction.CommitAsync();


        }

        [Fact]
        public async void AddCompetence()
        {
            //Given
            var dbContext = new MartenDataStore(this.store);
            var competenceRepo = dbContext.Competences;
            
            var competence = new Competence(
                competenceRepo.NextIdentity(),
                "Uprzejmość",
                new LearnerId(),
                ""                
                );

        
            //When
             using (dbContext)
             {
                competenceRepo.Add(competence);
                await dbContext.CommitChanges();
             }

            
            //Then
            var resultCompetence = await competenceRepo.CompetenceOfId(competence.Id as CompetenceId);

            using (var session = store.QuerySession())
            {

                var competenceView = await session.Query<CompetenceDTO>().Where(x=>x.Id==competence.Id.Id).SingleAsync();

                competenceView.Name.Should().Be(competence.GetSnapshot().Name);

            }
            resultCompetence.Id.Should().Be(competence.Id);
            resultCompetence.GetSnapshot().Name.Should().Be(competence.GetSnapshot().Name);

    
            await DropTables();
            
        }

        [Fact]
        public async void UpdateCompetence()
        {
            //Given
            var dbcontext = new MartenDataStore(store);
            var competenceRepo = dbcontext.Competences;

            var competence = new Competence(
                competenceRepo.NextIdentity(),
                "Uprzejmość",
                new LearnerId(),
                ""
                );

            competenceRepo.Add(competence);
            competence.ClearUncommittedEvents();

            //When
            competence.MarkAsRequired();
            competenceRepo.Update(competence);
            using (dbcontext)
            {
                await dbcontext.CommitChanges();
            };

            //Then
            var updatedCompetence = await competenceRepo.CompetenceOfId(competence.CompetenceId);

            updatedCompetence.IsRequired.Should().BeTrue();

            await DropTables();

        }
    }
}
