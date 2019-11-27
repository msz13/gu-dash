using GuDash.Common.domainmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuDash.Competences.Service.Application
{
    public class Result
    {
       

        public List<Error> Errors { get; set; }


        
       static Result Success()
        {
            return new Result();
        }

        public bool IsSuccess() => true;


    }
}
