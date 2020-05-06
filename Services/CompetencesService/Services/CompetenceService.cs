using CompetencesService.Application.CommandHandlers;
using CompetencesService.Application.QueryHandlers;
using Grpc.Core;
using GuDash.CompetencesService.Application.CommandHandlers;
using GuDash.CompetencesService.Proto;
using MediatR;
using System.Security.Claims;
using System.Threading.Tasks;
using static GuDash.CompetencesService.Proto.Competences;
using Error = GuDash.CompetencesService.Proto.Error;

namespace GuDash.CompetencesService.Services

{
    public class CompetenceService : CompetencesBase
    {
        IMediator mediator;

        public CompetenceService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async override Task<DefineCompetenceResponse> DefineCompetence(DefineCompetenceRequest request, ServerCallContext context)
        {

            var userId = context.GetHttpContext().User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var result = await this.mediator.Send(
                new DefineCompetenceCommand(
                    userId,
                    request.Name,
                    request.Description,
                    false
                ));


            return result.IsSuccess ?
                   new DefineCompetenceResponse { Succes = true, CompetenceId = result.Value.Id }
                   : new DefineCompetenceResponse { Succes = false, Error = new Error { Code = result.Error.Code, Message = result.Error.Message } };

        }

        public async override Task<RenameCompetenceResponse> RenameCompetence(RenameCompetenceRequest request, ServerCallContext context)
        {
            var userId = context.GetHttpContext().User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var result = await this.mediator.Send(
                new RenameCompetenceCommand(
                    userId,
                    request.CompetenceId,
                    request.NewName
                    )
                );

            return new RenameCompetenceResponse
            {
                Succes = true
            };
        }

        public async override Task<GetCompetenceResponse> GetCompetence(GetCompetenceRequest request, ServerCallContext context)
        {
            var userId = context.GetHttpContext().User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var competence = await this.mediator.Send(new CompetenceQuery(request.CompetenceId, userId));

            return new GetCompetenceResponse
            {
                CompetenceDTO = competence

            };

        }




    }
}
