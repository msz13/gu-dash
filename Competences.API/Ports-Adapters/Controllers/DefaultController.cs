using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuDash.Competences.Service.Ports_Adapters.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Competences.API.Ports_Adapters.Controllers
{
    [Route("api/")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        IEventStore eventStore;

        public DefaultController(IEventStore eventStore)
        {
            this.eventStore = eventStore;
        }

        [HttpGet]
       public  ActionResult<string> Get()
        {
           return "Competences service";
        }
    }
}