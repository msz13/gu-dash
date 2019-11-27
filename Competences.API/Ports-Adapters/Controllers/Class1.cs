using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using GuDash.Competences.API.Application;
using GuDash.Competences.API.ReadModel;


namespace GuDash.Competences.API.Ports_Adapters.Controllers
{
    [Route("api/competence")]
    [ApiController]
    class CompetenceController : ControllerBase
    {
        //private readonly CompetenceViewModelService competenceService;

       // public CompetenceController(CompetenceViewModelService competenceService)
       // {
       //     this.competenceService = competenceService;
       // }

       /// [HttpPost]
        //public IActionResult Create([FromBody]CompetenceView competence) {
        //    this.competenceService.Create(competence);
        //    return  Ok();
       // }
        [HttpGet]
        public ActionResult<string> Get ()
        {
            return "competence";
        }


    }
}
