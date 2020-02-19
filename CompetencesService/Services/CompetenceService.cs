using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Grpc.Core;
using GuDash.CompetencesService.Application.CommandHandlers;
using GuDash.CompetencesService.Proto;
using MediatR;
using static GuDash.CompetencesService.Proto.Competences;
using static GuDash.CompetencesService.Proto.Error;


namespace GuDash.CompetencesService.Services
    
{
    public class CompetenceService: CompetencesBase
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
                    request.Descripion,
                    false
                ));


            return result.IsSuccess ?
                  new DefineCompetenceResponse { Succes = true, CompetenceId = result.Value.Id }
                  : new DefineCompetenceResponse { Succes = false, Error = new Error { Code = result.Error.Code, Message = result.Error.Message } };
            
 
      /*  return new DefineCompetenceResponse
        {
            Succes = true,
            CompetenceId = new CompetenceId().Id,
            Error = null

        };*/
           
        }

        public override Task<GetCompetenceResponse> GetCompetence(GetCompetenceRequest request, ServerCallContext context)
        {


            return Task.FromResult(new GetCompetenceResponse
            {
                CompetenceDTO = new CompetenceDTO
                {
                    Id = request.CompetenceId,
                    Name = "Uprzejmość",
                    Description = "Uprzejmość na codzień",
                    IsActive = false,
                    IsRequired = false,
                    NumberOfActiveHabits = 0,
                    NumberOfDoneHabits = 0,
                    NumberOfHoldedHabits = 0

                }
            });
                
        }




    }
}
