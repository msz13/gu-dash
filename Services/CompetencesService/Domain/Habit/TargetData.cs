namespace GuDash.CompetencesService.Domain.Habit
{

    public class TargetData
    {
        public TargetData(TargetType type, int value)
        {
            Type = type;
            Value = value;
        }

        public enum TargetType
        {
            CHECKBOX,
            NUMERIC
        }
        public TargetType Type { get; private set; }
        public int Value { get; private set; }
    }
}