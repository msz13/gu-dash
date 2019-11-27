using GuDash.Competences.Service.Domain.Learner;
using GuDash.Common.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuDash.Competences.API.Domain.Competences.Events;
using GuDash.Competences.Service.Domain.Competences.Events;
using System.Diagnostics.Contracts;
using GuDash.Competences.Service.Domain.Habit;
using GuDash.Competences.Service.Application;
using GuDash.Competences.Service.Domain.Competences;

namespace GuDash.Competences.API.Domain.Competences
{
    public class Competence : EventSourcedRootEntity
    {
        CompetenceId competenceId;

        string name;

        LearnerId learnerId;

        string description;
        
        public bool IsActive { get; private set; }

       public bool IsRequired { get; private set; }

        List<CompetenceHabit> habits = new List<CompetenceHabit> { };

        public Competence(CompetenceId id, string name, LearnerId learner, string description, bool isRequired)
            : base()
        {
            AssertionConcern.AssertArgumentNotEmpty(name, "Competence name should not be empty");
            AssertionConcern.AssertArgumentLength(name, 120, "Competence name should have max length 120");
            
            this.Apply(new CompetenceAdded
             (
                 learner,
                 id,
                 name,
                 description,
                 isRequired
             ));
        }

        
        public Competence()
        {
        }

      
        public static Competence LoadFromEvents(IEnumerable<IDomainEvent> eventStream, int streamVersion)
        {
            var competence = new Competence();

            foreach (var e in eventStream)
                competence.Apply(e);

            return competence;

        }

       public static Competence LoadFromSnapshot(CompetenceSnapshot snapshot)
        {
           return new Competence
           {
               competenceId = snapshot.CompetenceId,
               learnerId = snapshot.LearnerId,
               name = snapshot.Name,
               description = snapshot.Description,
               IsActive = snapshot.IsActive,
               IsRequired = snapshot.IsRequired
            };
        }

        public CompetenceSnapshot GetSnapshot()
       {
            return new CompetenceSnapshot
                (
                this.competenceId,
               this.learnerId,
               this.name,
               this.description,
              this.IsActive,
                this.IsRequired,
                this.habits
                );
        }

     public void MarkAsRequired()
        {

            if (!IsRequired)
            {
                Apply(new CompetenceMarkedAsRequired(this.competenceId, this.learnerId));
            }
            
        } 

        public void MarkAsNotRequired()
        {
            if (!IsRequired)
            {
                Apply(new CompetenceMarkedAsNotRequired(competenceId, learnerId));       

} 
        } 

        public Habit AddHoldedHabit(
            Guid id,
            string name,
            string description,
            TargetData target,
            List<DayOfWeek> progressDays,
            CompetenceNotification notification
            )
        {
            if (this.habits.Any(habit => habit.Name == name))
            {
                notification.NotifytNonUniqueHabitNameError(name);

                return null;
            }
                            

            return new Habit(
                new HabitId(id),
                this.learnerId,
                this.competenceId,
                name,
                description,
                target,
                progressDays);
        }

        public void HabitAdded (HabitId habitId, string name)
        {
            if(!this.habits.Any(habit=>habit.Id==habitId))
            {
                this.Apply(new CompetenceHabitAdded(this.learnerId, this.competenceId, habitId, name));
            }
                
            
        }

        /*
        activate()
        {

            if (!this.isActive())
            {
                this.apply(new CompetenceActivated(this._id))
            }
        }

        deactivate()
        {

            if (this.isActive())
            {
                this.apply(new CompetenceDeactivated(this._id))
            }

        }
        */


        public void OnCompetenceAdded(CompetenceAdded e)
        {

            this.competenceId = e.CompetenceId;
            this.name = e.Name;
            this.learnerId  = e.LearnerId;
            this.description = e.Description;
            IsActive = false;
            IsRequired = false;
                        
        }

        public void OnCompetenceMarkedAsRequired(CompetenceMarkedAsRequired e)
        {
            IsRequired = true;
        }

        public void OnCompetenceMarkedAsNotRequired(CompetenceMarkedAsNotRequired e)
        {
            IsRequired = false;
        }

        public void OnCompetenceHabitAdded(CompetenceHabitAdded e)
        {
            this.habits.Add(new CompetenceHabit(e.HabitId, e.Name));
        }

        protected override IEnumerable<object> GetIdentityComponents()
        {
            yield return this.competenceId;
            yield return this.learnerId;
            
        }
                  
            



    }
}
