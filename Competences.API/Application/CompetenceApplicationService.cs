using GuDash.Competences.API.Domain.Competences;
using GuDash.Competences.Service.Domain.Learner;
using GuDash.Competences.API.Ports_Adapters.Repositories;
using GuDash.Competences.Service.Application;


namespace GuDash.Competences.API.Application
{
    public class CompetenceApplicationService
    {
        ICompetencesRepository competencesRepository;
        readonly ILearnerRepository learnerRepository;


        public CompetenceApplicationService(ICompetencesRepository competencesRepository, ILearnerRepository learnerRepository)
        {
            this.competencesRepository = competencesRepository;
            this.learnerRepository = learnerRepository;
        }

        public async void AddCompetence(AddCompetence command)
        {
            var learner = await this.learnerRepository.Get(new LearnerId(command.LearnerId));

            var competence = learner.InitiateCompetence(new CompetenceId(), command.Name, command.Description, command.IsRequired);

             await competencesRepository.Add(competence);
        }
    }
}
