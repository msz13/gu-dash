﻿using GuDash.CompetencesService.Domain.Habit;
using GuDash.CompetencesService.Domain.Learner;
using GuDash.CompetencesService.Domain.Progress.Events;
using NEventStore.Domain;
using NEventStore.Domain.Core;
using NodaTime;
using System;
using System.Collections.Generic;

namespace GuDash.CompetencesService.Domain.Progress
{
    public class Progress : AggregateBase, NEventStore.Domain.IAggregate
    {
        ProgressId progressId;
        LearnerId learnerId;
        HabitId habitId;
        ITarget target;
        ZonedDateTime startActive;
        ZonedDateTime? endActive;
        Dictionary<LocalDate, DayProgress> dayProgress = new Dictionary<LocalDate, DayProgress>();
        int daysWhenTargetReached = 0;
        List<IsoDayOfWeek> progressDays;


        public Progress(ProgressId id)
        {
            this.progressId = id;
            this.Register<HabitProgressStarted>(this.OnHabitProgressStarted);
            this.Register<HabitProgressDayTargetAchieved>(this.OnDayTargetAchieved);
            this.Register<HabitDayProgressUpdated>(this.OnDayProgressUpdated);
            this.Register<HabitProgressDayTargetAchievementCancelled>(this.OnDayTargetAchievementCancelled);
            this.Register<HabitProgressEnded>(this.OnProgressEnded);

        }

        public void Update(LocalDate day, int value)
        {
            CanUpdate(day, value);

            var ifPreviouslyTargetAchieved = IfPreviouslyAchievedDayTarget(day, value);

            if (!ifPreviouslyTargetAchieved && this.target.IsAchieved(value))
            {
                this.RaiseEvent(new HabitProgressDayTargetAchieved(
                    this.progressId,
                    this.learnerId,
                    this.habitId,
                    day,
                    value,
                    ++this.daysWhenTargetReached));
            }

            else if (ifPreviouslyTargetAchieved && !this.target.IsAchieved(value))

            {
                this.RaiseEvent(new HabitProgressDayTargetAchievementCancelled(
                    this.progressId,
                    this.learnerId,
                    this.habitId,
                    day,
                    value,
                    --this.daysWhenTargetReached));
            }

            else

            {
                this.RaiseEvent(new HabitDayProgressUpdated(
                    this.progressId,
                    this.learnerId,
                    this.habitId,
                    day,
                    value));
            }


        }



        public void End(ZonedDateTime endDate)
        {
            this.RaiseEvent(new HabitProgressEnded(this.progressId, this.learnerId, this.habitId, endDate));
        }

        #region privateMethods

        private void CanUpdate(LocalDate day, int value)
        {
            CheckIfProgressEnded();

            ValidateDay(day);

            this.target.ValidateValue(value);

        }

        private void CheckIfProgressEnded()
        {
            if (this.endActive.HasValue)
            {
                throw new InvalidOperationException("Progress Ended");
            }
        }

        private void ValidateDay(LocalDate day)
        {
            if (day.CompareTo(this.startActive.Date) < 0)
            {
                throw new ArgumentOutOfRangeException("Day", "Date of Progress Update before start of Progress");
            }

            if (!progressDays.Contains(day.DayOfWeek))
            {
                throw new InvalidOperationException($"{day.DayOfWeek} is not in Progress {this.progressId} Progress Days");
            }
        }

        private bool IfPreviouslyAchievedDayTarget(LocalDate day, int value)
        {
            DayProgress dayProgress;
            this.dayProgress.TryGetValue(day, out dayProgress);

            return dayProgress != null && dayProgress.IfAchievedTarget;
        }



        #endregion


        #region handlers

        void OnHabitProgressStarted(HabitProgressStarted evt)
        {
            this.progressId = evt.ProgressId;
            this.learnerId = evt.LearnerId;
            this.habitId = evt.HabitId;
            this.target = evt.Target;
            this.startActive = evt.StartActive;
            this.dayProgress = new Dictionary<LocalDate, DayProgress>();
            this.progressDays = evt.ProgressDays;

        }

        void OnDayTargetAchieved(HabitProgressDayTargetAchieved evt)
        {
            this.daysWhenTargetReached = evt.DaysWhenTargetReached;
            this.dayProgress[evt.Date] = new DayProgress(true, evt.DayProgressValue);

        }

        void OnDayTargetAchievementCancelled(HabitProgressDayTargetAchievementCancelled evt)
        {
            this.daysWhenTargetReached = evt.DaysWhenTargetReached;
            this.dayProgress[evt.Date] = new DayProgress(false, evt.DayProgressValue);
        }

        void OnDayProgressUpdated(HabitDayProgressUpdated evt)
        {
            this.dayProgress[evt.Date] = new DayProgress(false, evt.DayProgressValue);
        }

        void OnProgressEnded(HabitProgressEnded evt)
        {
            this.endActive = evt.EndDate;
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
                this.daysWhenTargetReached,
                this.progressDays) as ProgressSnapshot;
        }

        protected override IEnumerable<object> GetIdentityComponents()
        {
            yield return this.progressId;
            yield return this.learnerId;
        }


    }
}
