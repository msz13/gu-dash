using GuDash.Common.domainmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuDash.Common.applicationlayer
{
    public class CResult
    {

       public List<Error> Errors { get; set; }

       

       public static CResult Success()
        {
            return new CResult();
        }



      
        
        


    }
}
