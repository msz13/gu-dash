using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuDash.Competences.API.Application;
using GuDash.Competences.API.ReadModel;
using GuDash.Competences.Service.Domain.Competences.Events;
using NServiceBus;

namespace GuDash.Competences.Service.Ports_Adapters.EventHandlers
{
    public class CompetenceViewGenerator : IHandleMessages<CompetenceAdded>
    {
        readonly CompetenceViewModelService competencesViewService;

        public CompetenceViewGenerator(CompetenceViewModelService competencesViewService)
        {
            this.competencesViewService = competencesViewService;
        }

        public  Task Handle(CompetenceAdded message, IMessageHandlerContext context)
        {
            competencesViewService.Create(new CompetenceView(message.CompetenceId.Id,
                                                             message.LearnerId.Id,
                                                             message.Name,
                                                             message.Description,
                                                             message.IsRequired));            
            return Task.CompletedTask;
        }
    }
}
