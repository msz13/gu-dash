using System;
using Xunit;
using GuDash.Competences.API.Ports_Adapters.MongoPersistance;
using GuDash.Competences.API.ReadModel;
using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using System.Threading.Tasks;
using MongoDB.Bson;
using Competences.API;
using GuDash.Competences.API.Application;

namespace CompetencesIntegrationTests
{
    public class CompetenceReadModelTest   {

      

        public CompetenceViewModelService getService()
        {
            var configuration = new MongoDbConfiguration
            {
                Url = "mongodb://test_user:test_2019@ds046037.mlab.com:46037/type_test/?retryWrites=false",
                DatabaseName = "type_test"
            };

            var context = new MongoDbContext(configuration.Url, configuration.DatabaseName);

            return new CompetenceViewModelService(context);

        }

        [Fact]
        public async Task create_Read_Model()
        {

            var service = getService();

            var id = Guid.NewGuid().ToString();

            var expectedCompetence = new CompetenceView(id, "13", "Uprzejmoœæ", "Wa¿na", false);

       
            service.Create(expectedCompetence);

            var competence = await service.GetById(id);

            Assert.Equal(expectedCompetence.Description, competence.Description);
            Assert.Equal(expectedCompetence.NumberOfActiveGoals, competence.NumberOfActiveGoals);
            Assert.Equal(expectedCompetence.NumberOfDoneGoals, competence.NumberOfDoneGoals);
            Assert.Equal(expectedCompetence.NumberOfHoldedGoals, competence.NumberOfHoldedGoals);

        }

   
        public async Task return_by_id()
        {
            var service = getService();
            var id = "1";
            var competence = await service.GetById(id);

            Assert.Equal(id, competence.CompetenceId);
        }

    }
}
