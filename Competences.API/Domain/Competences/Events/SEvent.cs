using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuDash.Competences.Service.Domain.Competences.Events
{
    public class SEvent : IEvent
    {
        public SEvent(string id, string learnerId)
        {
            Id = id;
            LearnerId = learnerId;
        }

        public string Id { get; private set; }
        public string LearnerId { get; private set; }

    }
}
