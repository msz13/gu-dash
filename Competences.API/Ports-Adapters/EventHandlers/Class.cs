using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NServiceBus;
using GuDash.Competences.API.Domain.Competences.Events;
using GuDash.Competences.Service.Domain.Competences.Events;

namespace GuDash.Competences.API.Ports_Adapters.EventHandlers
{
    public class SampleEventHandler : IHandleMessages<CompetenceMarkedAsRequired>
    {
        

        public Task Handle(CompetenceMarkedAsRequired ev, IMessageHandlerContext context)
        {
            var name = ev.Competence.Id;
            Console.WriteLine("sample event handler: "+ name);
            return Task.CompletedTask;
        }
    }
}
