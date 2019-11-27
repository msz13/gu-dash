using GuDash.Competences.Service.Domain.Learner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuDash.Competences.Service.Application
{
    public class AddCompetence
    {
        public AddCompetence(string id, string learnerId, string name, string description, bool isRequired)
        {
            Id = id;
            LearnerId = learnerId;
            Name = name;
            Description = description;
            IsRequired = isRequired;
        }

        public string Id { get; private set; }
        public string LearnerId { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }
        public bool IsRequired { get; private set; }
                

    }
}
