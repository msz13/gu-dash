using GuDash.CompetencesService.Domain.Competences;
using GuDash.CompetencesService.Domain.Habit.Events;
using GuDash.CompetencesService.Domain.Learner;
using NEventStore.Domain;
using NEventStore.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuDash.CompetencesService.Domain.Habit
{
    public class Habit : AggregateBase, NEventStore.Domain.IAggregate
    {
        HabitId habitId;
        LearnerId learnerId;
        CompetenceId competenceId;
        string name;
        string description;
        Status status;
        TargetData target;
        List<DayOfWeek> progressdays;
       

        public enum Status
        {
            ACTIVE,
            HOLDED,
            ACCOMPLISHED
        }

        public Habit(HabitId habitId,
                     LearnerId learnerId,
                     CompetenceId competenceId,
                     string name,
                     string description,
                     TargetData target,
                     List<DayOfWeek> progressdays)
            : this(habitId)
        {
            
            this.RaiseEvent(new HoldedHabitAdded(
                habitId,
                learnerId,
                competenceId,
                name,
                description,
                target,
                progressdays));
        }

        public Habit(HabitId id)
        {
            this.habitId = id;
            this.Register<HoldedHabitAdded>(this.OnHoldedHabitAdded);
        }

        public Habit(HabitId habitId, LearnerId learnerId1, CompetenceId competenceId1, TargetData targetData)
        {
            this.habitId = habitId;
            this.learnerId = learnerId1;
            this.competenceId = competenceId1;
            this.target = targetData;
        }

        public void Activate()
        {
            this.RaiseEvent(new HabitActivated());
        }

        

        private void OnHoldedHabitAdded(HoldedHabitAdded ev)
        {
            this.learnerId = ev.LearnerId;
            this.competenceId = ev.CompetenceId;
            this.name = ev.Name;
            this.description = ev.Description;
            this.status = Status.HOLDED;
            this.target = ev.Target;
            this.progressdays = ev.Progressdays;
        }

        public override IMemento GetSnapshot()
        {
            return new HabitSnapshot(
                this.habitId,
                this.learnerId,
                this.competenceId,
                this.name,
                this.description,
                this.status,
                this.target,
                this.progressdays);
        }

        protected override IEnumerable<object> GetIdentityComponents()
        {
            yield return this.habitId;
            yield return this.learnerId;
        }

    }
}
