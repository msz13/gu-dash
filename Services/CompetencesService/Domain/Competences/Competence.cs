using CompetencesService.Application.CommandHandlers;
using CompetencesService.Domain.Competences;
using CompetencesService.Domain.Competences.Events;
using CSharpFunctionalExtensions;
using GuDash.Common.Domain.Model;
using GuDash.Competences.Service.Application;
using GuDash.CompetencesService.Domain.Competences.Events;
using GuDash.CompetencesService.Domain.Habit;
using GuDash.CompetencesService.Domain.Learner;
using NEventStore.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuDash.CompetencesService.Domain.Competences
{
    public class Competence : AggregateBase
    {

        public CompetenceId CompetenceId { get; private set; }

        CompetenceName name;

        LearnerId learnerId;

        string description;

        public bool IsActive { get; private set; }

        public bool IsRequired { get; private set; } 

        List<CompetenceHabit> habits = new List<CompetenceHabit> { };
       

        public Competence(CompetenceId id, CompetenceName name, LearnerId learner, string description, bool? isRequired)
            : this()
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (learner is null)
            {
                throw new ArgumentNullException(nameof(learner));
            }

            AssertionConcern.AssertArgumentLength(description, 240, "Competence description lenght can't be greater than 240 characters");

            RaiseEvent(new CompetenceDefined
             (
                 learner,
                 id,
                 name,
                 description,
                 isRequired ?? false
             ));
        }



        public Competence() : base()
        {
            this.Register<CompetenceDefined>(this.OnCompetenceDefined);
            this.Register<CompetenceMarkedAsRequired>(this.OnCompetenceMarkedAsRequired);
            this.Register<CompetenceMarkedAsNotRequired>(this.OnCompetenceMarkedAsNotRequired);
            this.Register<CompetenceHabitAdded>(this.OnCompetenceHabitAdded);
            this.Register<CompetenceRenamed>(this.OnCompetenceRenamed);
        }

       

        public static Competence LoadFromSnapshot(CompetenceSnapshot snapshot)
        {
            return new Competence
            {
                CompetenceId = snapshot.CompetenceId,
                learnerId = snapshot.LearnerId,
                name = new CompetenceName(snapshot.Name),
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
               name.Value,
               description,
              IsActive,
                IsRequired,
                habits
                );
        }

        public Result<int, Error> Rename(string newName, ICompetenceUniqueNameService uniqeNameSvc)
        {
            var competenceName = new CompetenceName(newName);
            var isUnique = Task.Run(async () => await uniqeNameSvc.IsUniqueName(this.learnerId, competenceName)).GetAwaiter().GetResult();
            if (isUnique)
            {
                RaiseEvent(new CompetenceRenamed(this.learnerId, this.CompetenceId, competenceName));
                return Result.Success<int, Error>(this.Version);
            }
            else
            {
                return Result.Failure<int, Error>(CompetencesErrors.NonUniqueNameError(newName));

            }



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
            Id = CompetenceId;
            name = e.Name;
            learnerId = e.LearnerId;
            description = e.Description;
            IsActive = false;
            IsRequired = false;

        }


        private void OnCompetenceMarkedAsRequired(CompetenceMarkedAsRequired e)
        {
            IsRequired = true;
        }

        private void OnCompetenceMarkedAsNotRequired(CompetenceMarkedAsNotRequired e)
        {
            IsRequired = false;
        }

        private void OnCompetenceHabitAdded(CompetenceHabitAdded e)
        {
            habits.Add(new CompetenceHabit(e.HabitId, e.Name));
        }

        private void OnCompetenceRenamed(CompetenceRenamed e)
        {
            name = e.NewName;
        }

        protected override IEnumerable<object> GetIdentityComponents()
        {
            yield return this.CompetenceId;
        }



    }
}
