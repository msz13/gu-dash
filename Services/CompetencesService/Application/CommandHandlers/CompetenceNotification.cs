using GuDash.Common.applicationlayer;

namespace GuDash.Competences.Service.Application
{
    public class CompetenceNotification : Notification
    {
        public CompetenceNotification() : base() { }


        public void NotifytNonUniqueHabitNameError(string name)
        {
            this.AddError(new Error("NON_UNIQUE_HABIT_NAME", $"Habit name: {name}"));
        }
    }
}
