using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GuDash.Competences.API.ReadModel
{
    
    public class CompetenceView
    {
        public CompetenceView(string competenceId, string learner, string name, string description, bool isRequired)
        {
            CompetenceId = competenceId;
            Learner = learner;
            Name = name;
            Description = description;
            IsRequired = isRequired;         
        }

        //public  BsonObjectId Id { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string CompetenceId { get; set; } // wybrac typ

        public string Learner { get; set; } //wybrac typ
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsRequired { get; set; }

        public int NumberOfActiveGoals { get; set; } = 0;

        public int NumberOfHoldedGoals { get; set; } = 0;

        public int NumberOfDoneGoals { get; set; } = 0;


    }

}
