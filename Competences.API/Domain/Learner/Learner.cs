using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuDash.Common.Domain.Model;
using GuDash.Competences.API.Domain.Competences;
using GuDash.Competences.Service.Domain.Learner.Events;

namespace GuDash.Competences.Service.Domain.Learner
{
    public class Learner : EventSourcedRootEntity
    {
        LearnerId learnerId;

        string email;

        string timezoneId;

        List<LearnerCompetence> learnerCompetences = new List<LearnerCompetence>();

        //private _habitActivationPolicy!: HabitActivationPolicy

        //private _numberOfActiveHabits!: NumberOfActiveHabits

        //private _numberOfInactiveRequiredCompetences!: NumberOfInactiveRequiredCompetences


        #region Contructors

        public Learner(LearnerId learnerId, string email, string timezoneId) :base()
        {
            Apply(new LearnerCreated(learnerId, email, timezoneId));
        }

        public Learner()
        {
        }

        public static Learner LoadFromEvents(IEnumerable<IDomainEvent> eventsStream, int streamVersion)
        {
            var learner = new Learner();

            foreach (var e in eventsStream)
                learner.When(e);

            return learner;

        }

       public static Learner LoadFromSnapshot(LearnerSnapshot snapshot)
        {
            return new Learner(
                snapshot.LearnerId,
                snapshot.Email,
                snapshot.TimeZoneId
                );
        }

        #endregion

   

        
        public  Competence  InitiateCompetence(CompetenceId id, string name, string description, bool isRequired) {

            if (this.learnerCompetences.Any(competence => competence.Name == name))
                throw (new LearnerErrors());
            

                    return new Competence(id, name, this.learnerId, description, isRequired);
        }

        public void CompetenceInitiated(CompetenceId competenceId, string name, bool isRequired)
        {
            if (this.learnerCompetences.All(competence => competence.CompetenceId != competenceId && competence.Name != name))
                Apply(new LearnerCompetenceInitiated(this.learnerId, competenceId, name, isRequired));
        }



        #region Handleres

       
        public void OnLearnerCreated(LearnerCreated ev)
        {
            this.learnerId = new LearnerId(ev.LearnerId);
            this.email = ev.Email;
            this.timezoneId = ev.TimeZoneId;
        }

        public void OnLearnerCompetenceInitiated(LearnerCompetenceInitiated e)
        {
            this.learnerCompetences.Add(new LearnerCompetence(e.CompetenceId, e.Name, e.IsRequired));
        }

        #endregion

        public LearnerSnapshot GetSnapshot()
        {
            return new LearnerSnapshot(learnerId, email, this.timezoneId, this.learnerCompetences);
        }

        protected override IEnumerable<object> GetIdentityComponents()
        {
           yield return this.learnerId;
        }
                

    }

    

}
