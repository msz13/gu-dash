using CompetencesService.Application.CommandHandlers;

namespace CompetencesService.Domain.Competences
{
    public static class CompetencesErrors
    {
        public static Error NonUniqueNameError(string name)
        {
            return new Error("NON_UNIQUE_COMPETENCE_NAME", $"Competence name {name} is not unique");
        }
    }
}
