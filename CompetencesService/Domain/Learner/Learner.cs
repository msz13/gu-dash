using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompetencesService.Application.CommandHandlers;
using CSharpFunctionalExtensions;
using GuDash.Common.Domain.Model;
using GuDash.CompetencesService.Domain.Competences;
using GuDash.CompetencesService.Domain.Learner.Events;
using NEventStore.Domain;
using NEventStore.Domain.Core;

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

        public Learner(LearnerId learnerId, string timezoneId) :this()
        {
            RaiseEvent(new LearnerCreated(learnerId,  timezoneId));
        }

        public Learner(): base()
        {
            Register<LearnerCreated>(this.OnLearnerCreated);
            Register<LearnerCompetenceDefined>(this.OnLearnerCompetenceDefined);
        }

        #endregion

   
        
        public  Result<Competence,Error>  DefineCompetence(CompetenceId id, string name, string description, bool isRequired) {

            if (this.learnerCompetences.Any(competence => competence.Name == name))
                return Result.Failure<Competence, Error>(new Error("NON_UNIQUE_COMPETENCE_NAME", $"Competence name {name} is not unique"));
            
            var competence = new Competence(id, name, this.learnerId, description, isRequired);

            return Result.Success<Competence,Error>(competence);
        }

        public void CompetenceDefined(CompetenceId competenceId, string name, bool isRequired)
        {
            if (this.learnerCompetences.All(competence => competence.CompetenceId != competenceId && competence.Name != name))
                RaiseEvent(new LearnerCompetenceDefined(this.learnerId, competenceId, name, isRequired));
        }



        #region Handleres

       
        public void OnLearnerCreated(LearnerCreated ev)
        {
            this.learnerId = ev.LearnerId;            
            this.timezoneId = ev.TimeZoneId;
        }

        public void OnLearnerCompetenceDefined(LearnerCompetenceDefined e)
        {
            this.learnerCompetences.Add(new LearnerCompetence(e.CompetenceId, e.Name, e.IsRequired));
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
