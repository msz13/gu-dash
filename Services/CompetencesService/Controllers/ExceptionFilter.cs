using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetencesService.Controllers
{
    public class ExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; } = int.MaxValue - 10;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("Error");
            context.Result = new ObjectResult("Something bad happend")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }
    }
}
