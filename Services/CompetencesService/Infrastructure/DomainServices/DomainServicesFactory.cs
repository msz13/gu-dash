using CompetencesService.Domain.Competences;
using GuDash.CompetencesService.Infrastructure.DomainServices;
using Microsoft.Extensions.DependencyInjection;

namespace CompetencesService.Infrastructure.DomainServices
{
    public static class DomainServicesFactory
    {
        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<ICompetenceUniqueNameService, CompetenceUniqueNameService>();
        }
    }
}
