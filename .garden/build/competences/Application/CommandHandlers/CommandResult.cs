using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetencesService.Application.CommandHandlers
{
    public class CommandResult
    {
        public bool IsSucces { get; set; }
        public string CompetenceId{ get; set; }
    }
}
