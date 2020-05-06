namespace GuDash.CompetencesService.Domain.Progress
{
    public interface ITarget
    {
        bool IsAchieved(int value);

        void ValidateValue(int value);
    }
}