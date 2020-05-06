using Competences;
using System.Threading.Tasks;

namespace CompetencesFunctionalTests
{
    public interface ICompetenceDriver
    {
        Task<DefineCompetenceResponse> DefineCompetence(DefineCompetenceRequest request, string userId);
        Task<GetCompetenceResponse> GetCompetence(GetCompetenceRequest request, string userId);
        Task<RenameCompetenceResponse> RenameCompetence(RenameCompetenceRequest request, string userId);
    }
}