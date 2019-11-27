using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using GuDash.Competences.API.Application;
using GuDash.Competences.API.Domain.Competences;
using GuDash.Competences.API.Ports_Adapters.Repositories;

namespace GuDash.Competences.Service.Application
{
    public class CompetencesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CompetencesRepostiory>().As<ICompetencesRepository>().SingleInstance();
            builder.RegisterType<CompetenceApplicationService>().SingleInstance();
            builder.RegisterType<CompetenceViewModelService>().SingleInstance();
        }
    }
}
