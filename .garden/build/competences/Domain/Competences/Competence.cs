using GuDash.Common.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using GuDash.CompetencesService.Domain.Competences.Events;
using GuDash.CompetencesService.Domain.Habit;
using GuDash.CompetencesService.Domain.Learner;
using GuDash.Competences.Service.Application;
using NEventStore.Domain.Core;
using System.Collections;

namespace GuDash.CompetencesService.Domain.Competences
{
    public class Competence : AggregateBase
    {
        
        public CompetenceId CompetenceId { get; protected set; }

        public string Id { get => CompetenceId.Id; set => CompetenceId = new CompetenceId(value); }

        string name;

        LearnerId learnerId;

        string description;

        public bool IsActive { get; private set; }

        public bool IsRequired { get; private set; }

        List<CompetenceHabit> habits = new List<CompetenceHabit> { };

        public Competence(CompetenceId id, string name, LearnerId learner, string description, bool isRequired = false)
            : this()
        {
            AssertionConcern.AssertArgumentNotEmpty(name, "Competence name should not be empty");
            AssertionConcern.AssertArgumentLength(name, 120, "Competence name should have max length 120");

           

            RaiseEvent(new CompetenceDefined
             (
                 learner,
                 id,
                 name,
                 description,
                 isRequired
             ));
        }


        public Competence(): base()
        {
            this.Register<CompetenceDefined>(this.OnCompetenceDefined);
            this.Register<CompetenceMarkedAsRequired>(this.OnCompetenceMarkedAsRequired);
            this.Register<CompetenceMarkedAsNotRequired>(this.OnCompetenceMarkedAsNotRequired);
            this.Register<CompetenceHabitAdded>(this.OnCompetenceHabitAdded);
        }

                

        public static Competence LoadFromSnapshot(CompetenceSnapshot snapshot)
        {
            return new Competence
            {
                CompetenceId = snapshot.CompetenceId,
                learnerId = snapshot.LearnerId,
                name = snapshot.Name,
                description = snapshot.Description,
                IsActive = snapshot.IsActive,
                IsRequired = snapshot.IsRequired
            };
        }

        public new CompetenceSnapshot GetSnapshot()
        {
            return new CompetenceSnapshot
                (
                CompetenceId,
               learnerId,
               this.Version,
               name,
               description,
              IsActive,
                IsRequired,
                habits
                );
        }

        public void MarkAsRequired()
        {

            if (!IsRequired)
            {
                RaiseEvent(new CompetenceMarkedAsRequired(CompetenceId, learnerId));
            }

        }

        public void MarkAsNotRequired()
        {
            if (!IsRequired)
            {
                RaiseEvent(new CompetenceMarkedAsNotRequired(CompetenceId, learnerId));

            }
        }

        public Habit.Habit AddHoldedHabit(
            Guid id,
            string name,
            string description,
            TargetData target,
            List<DayOfWeek> progressDays,
            CompetenceNotification notification
            )
        {
            if (habits.Any(habit => habit.Name == name))
            {
                notification.NotifytNonUniqueHabitNameError(name);

                return null;
            }


            return new Habit.Habit(
                new HabitId(id),
                learnerId,
                CompetenceId,
                name,
                description,
                target,
                progressDays);
        }

        public void HabitAdded(HabitId habitId, string name)
        {
            if (!habits.Any(habit => habit.Id == habitId))
            {
                RaiseEvent(new CompetenceHabitAdded(learnerId, CompetenceId, habitId, name));
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


        public void OnCompetenceDefined(CompetenceDefined e)
        {

            CompetenceId = e.CompetenceId;
            name = e.Name;
            learnerId = e.LearnerId;
            description = e.Description;
            IsActive = false;
            IsRequired = false;

        }

        public void Apply(CompetenceDefined e)
        {
            CompetenceId = e.CompetenceId;
            name = e.Name;
            learnerId = e.LearnerId;
            description = e.Description;
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
            habits.Add(new CompetenceHabit(e.HabitId, e.Name));
        }

        protected override IEnumerable<object> GetIdentityComponents()
        {
            yield return this.CompetenceId;
        }
                

       
    }
}
