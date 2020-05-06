using GuDash.CompetencesService.Proto;
using MediatR;

namespace CompetencesService.Application.QueryHandlers
{
    public class CompetenceQuery : IRequest<CompetenceDTO>
    {
        public string CompetenceId { get; private set; }
        public string LearnerId { get; private set; }

        public CompetenceQuery(string competenceId, string learnerId)
        {
            this.CompetenceId = competenceId;
            this.LearnerId = learnerId;
        }
    }
}
