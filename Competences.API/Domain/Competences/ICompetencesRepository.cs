using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuDash.Competences.API.Domain.Competences
{
    public interface ICompetencesRepository
    {
         Task Add(Competence theCompetence);
    }
}
