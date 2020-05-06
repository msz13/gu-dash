using CompetencesService.Application.QueryHandlers;
using CompetencesService.Infrastructure;
using GuDash.CompetencesService.Application.CommandHandlers;
using GuDash.CompetencesService.Proto;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CompetencesService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class СompetenceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public СompetenceController(IMediator mediator)
        {
            this._mediator = mediator;
        }
            


        // GET: api/Сompetence
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        
        [HttpGet("competence/{id}")]
        public async Task<CompetenceDTO> GetById(string id) //TODO zrobić nową klsę CompetenceDTO
        {
            return await _mediator.Send(new CompetenceQuery(id, GetUserId()));
        }

        // POST: api/Сompetence
        [HttpPost("/competence")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DefineCompetence ([FromBody] CreateCompetenceDTO request)
        {
            
            var result = await _mediator.Send(new DefineCompetenceCommand(
                 GetUserId(),
                 request.Name,
                 request.Description,
                 request.IsRequired
                 ));

            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, new { });
            } else
            {
                return BadRequest(result.Error);
            }                         
            
        }

        // PUT: api/Сompetence/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private string GetUserId()
        {
            return HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}

