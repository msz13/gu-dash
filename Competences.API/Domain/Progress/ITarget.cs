namespace GuDash.Competences.Service.Domain.Progress
{
    public interface ITarget
    {
        bool IsAchieved(int value);

        void ValidateValue(int value);
    }
}