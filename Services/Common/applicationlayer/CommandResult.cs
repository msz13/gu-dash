using GuDash.Common.domainmodel;
using System.Collections.Generic;

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
