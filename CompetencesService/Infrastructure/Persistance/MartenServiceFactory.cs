using Marten;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuDash.CompetencesService.Infrastructure.Persistance
{
    public static class MartenServiceFactory
    {
        public static void AddMartenStore(this IServiceCollection services, string connectionStr)
        {
            services.AddSingleton(CompetencesDocumentStore.CreateStore(connectionStr));
            services.AddScoped<ICompetencesStore, MartenDataStore>();
        }
    }
}
