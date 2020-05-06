namespace CompetencesService.Application.CommandHandlers
{
    public class DefineCompetenceCommandResult : CommandResult
    {
        public string CompetenceId { get; set; }

        public void NotifyNonUniqueCompetenceName(string name)
        {
            this.AddError(new Error("NON_UNIQUE_COMPETENCE_NAME", $"Competence with name {name} already exists"));
        }
    }
}
