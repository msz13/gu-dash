using CompetencesService.Application.CommandHandlers;
using CompetencesService.Domain.Competences;
using CSharpFunctionalExtensions;
using GuDash.CompetencesService.Domain.Competences;
using NEventStore.Domain;
using NEventStore.Domain.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuDash.CompetencesService.Domain.Learner
{
    public class Learner : AggregateBase
    {
        LearnerId learnerId;

        public LearnerId LearnerId { get { return this.learnerId; } }

        string timezoneId;

        List<LearnerCompetence> learnerCompetences = new List<LearnerCompetence>();

        //private _habitActivationPolicy!: HabitActivationPolicy

        //private _numberOfActiveHabits!: NumberOfActiveHabits

        //private _numberOfInactiveRequiredCompetences!: NumberOfInactiveRequiredCompetences


        #region Contructors

        public Learner(LearnerId learnerId, string timezoneId) : this()
        {
            RaiseEvent(new LearnerCreated(learnerId, timezoneId));
        }

        public Learner() : base()
        {
            Register<LearnerCreated>(this.OnLearnerCreated);
        }

        #endregion



        public Result<Competence, Error> DefineCompetence(CompetenceId id, string name, string description, bool? isRequired, ICompetenceUniqueNameService uniqueNameService)
        {

            var competenceName = new CompetenceName(name);
            var nameIsUnique = Task.Run(async () => await uniqueNameService.IsUniqueName(this.learnerId, competenceName)).GetAwaiter().GetResult();
            if (!nameIsUnique)
                return Result.Failure<Competence, Error>(CompetencesErrors.NonUniqueNameError(name));

            var competence = new Competence(id, competenceName, this.learnerId, description, isRequired);

            return Result.Success<Competence, Error>(competence);
        }




        #region Handleres


        private void OnLearnerCreated(LearnerCreated ev)
        {
            this.learnerId = ev.LearnerId;
            this.timezoneId = ev.TimeZoneId;
        }


        #endregion

        public override IMemento GetSnapshot()
        {
            return new LearnerSnapshot(learnerId, this.timezoneId, this.learnerCompetences) as IMemento;
        }

        protected override IEnumerable<object> GetIdentityComponents()
        {
            yield return this.learnerId;
        }


    }



}
