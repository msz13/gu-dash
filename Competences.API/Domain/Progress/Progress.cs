using GuDash.Common.Domain.Model;
using GuDash.Competences.Service.Domain.Habit;
using GuDash.Competences.Service.Domain.Learner;
using GuDash.Competences.Service.Domain.Progress.Events;
using NEventStore.Domain;
using NEventStore.Domain.Core;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuDash.Competences.Service.Domain.Progress
{
    public class Progress : AggregateBase, IAggregate
    {
        ProgressId progressId;
        LearnerId learnerId;
        HabitId habitId;
        ITarget target;
        ZonedDateTime startActive;
        ZonedDateTime endActive;
        int daysWhenTargetReached = 0;

        public Progress(ProgressId id)
        {
            this.progressId = id;
            this.Register<HabitProgressStarted>(this.OnHabitProgressStarted);
            this.Register<HabitProgressDayTargetAchieved>(this.OnDayTargetAchieved);
            this.Register<HabitDayProgressUpdated>(this.OnDayProgressUpdated);
            
        }

        public void Increase(LocalDate startActive, int value)
        {
            if(this.target.IsAchieved(value))
            {
                this.RaiseEvent(new HabitProgressDayTargetAchieved(
               this.progressId,
               this.learnerId,
               this.habitId,
               startActive,
               value,
               ++this.daysWhenTargetReached));
            } else
            {
                this.RaiseEvent(new HabitDayProgressUpdated(
                    this.progressId,
                    this.learnerId,
                    this.habitId,
                    startActive,
                    value));
            }
           

           
        }


        #region handlers

        void OnHabitProgressStarted(HabitProgressStarted evt) 
        {
            this.progressId = evt.ProgressId;
            this.learnerId = evt.LearnerId;
            this.habitId = evt.HabitId;
            this.target = evt.Target;
            this.startActive = evt.StartActive;
        }

        void OnDayTargetAchieved(HabitProgressDayTargetAchieved evt)
        {
            this.daysWhenTargetReached = evt.DaysWhenTargetReached;
        }

        void OnDayProgressUpdated(HabitDayProgressUpdated evt)
        {
            
        }

        #endregion

        public override IMemento GetSnapshot()
        {
            return new ProgressSnapshot(
                this.Version,
                this.progressId,
                this.learnerId,
                this.habitId,
                this.target,
                this.startActive,
                this.daysWhenTargetReached) as ProgressSnapshot;
        }
        public override void LoadSnapshot<TMemento>(TMemento snapshot)
        {
            throw new NotImplementedException();
        }

       
    }
}
