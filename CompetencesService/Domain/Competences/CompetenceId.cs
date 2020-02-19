namespace GuDash.CompetencesService.Domain.Competences
{
    using GuDash.Common.Domain.Model;
    using Newtonsoft.Json;
    using System;

    public class CompetenceId : Identity, IIdentity
    {



        public CompetenceId() : base()
        {
        }

        public CompetenceId(Guid id) : base(id.ToString())
        {

        }

        public CompetenceId(string id) : base(id)
        {

        }
    }
}