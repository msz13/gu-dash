﻿namespace CompetencesService.Controllers
{
    public class CreateCompetenceDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool? IsRequired { get; set; }
    }
}