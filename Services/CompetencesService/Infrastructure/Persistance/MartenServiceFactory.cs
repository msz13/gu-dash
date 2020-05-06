using CompetencesService.Infrastructure.Persistance;
using Microsoft.Extensions.DependencyInjection;

namespace GuDash.CompetencesService.Infrastructure.Persistance
{
    public static class MartenServiceFactory
    {
        public static void AddMartenStore(this IServiceCollection services, PostgresSettings settings)
        {
            services.AddSingleton(CompetencesDocumentStore.CreateStore(settings.GetConnectionString()));
            services.AddScoped<ICompetencesStore, MartenDataStore>();
        }
    }
}
