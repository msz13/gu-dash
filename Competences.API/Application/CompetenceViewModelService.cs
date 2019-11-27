using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GuDash.Competences.API.Ports_Adapters.MongoPersistance;
using GuDash.Competences.API.ReadModel;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GuDash.Competences.API.Application
{
   public class CompetenceViewModelService
    {
        private readonly MongoDbContext context;

        private readonly IMongoCollection<CompetenceView> collection;

        public CompetenceViewModelService(MongoDbContext context)
        {
            this.context = context;
            this.collection = context.GetCollection<CompetenceView>("competence_view");
        }

        public async void Create(CompetenceView competence)
        {
           await this.collection.InsertOneAsync(competence);
        }

        public async Task<List<CompetenceView>> Get()
        {
            var filter = Builders<CompetenceView>.Filter.Empty;
            return await this.collection.Find(filter).ToListAsync<CompetenceView>();
        }

        public async Task<CompetenceView> GetById (string id)
        {
            var filter = Builders<CompetenceView>.Filter.Eq("_id", id);

            return await this.collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}
