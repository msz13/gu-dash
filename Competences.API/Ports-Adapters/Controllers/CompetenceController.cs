using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GuDash.Competences.API.Application;
using GuDash.Competences.Service.Application;
using GuDash.Competences.API.ReadModel;
using GuDash.Competences.Service.Ports_Adapters.Controllers.competenceDTO;

namespace Competences.API.Ports_Adapters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetenceController : ControllerBase
    {
        private readonly CompetenceApplicationService competenceService;
        private readonly CompetenceViewModelService competenceViewModelService;


        public CompetenceController(CompetenceApplicationService competenceService, CompetenceViewModelService competenceViewModelService)
        {
            this.competenceService = competenceService;
            this.competenceViewModelService = competenceViewModelService;
        }

        // GET: api/Competence
        [HttpGet]
        public async Task<List<CompetenceView>> Get()
        {
            return  await competenceViewModelService.Get();                        

        }

        // GET: api/Competence/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<CompetenceView>> Get(string id)
        {
            var competence = await competenceViewModelService.GetById(id);
            if (competence == null)
            {
                return NotFound();
            }

            return competence;
        }

        // POST: api/Competence
        [HttpPost]
        public IActionResult Post( CreateCompetenceDTO competenceDTO)
        {
            
            var id = Guid.NewGuid().ToString();
             var command = new AddCompetence
                (
                id,
                Guid.NewGuid().ToString(),
                competenceDTO.Name,
                competenceDTO.Description,
                competenceDTO.IsRequired
                );

            var resp = new { ok = true };
            competenceService.AddCompetence(command);
            return AcceptedAtAction(nameof(Get), new { id = id}, resp);
        }

        // PUT: api/Competence/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
