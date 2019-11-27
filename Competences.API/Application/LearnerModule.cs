using Autofac;
using GuDash.Competences.Service.Domain.Learner;
using GuDash.Competences.Service.Ports_Adapters.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuDash.Competences.Service.Application
{
    public class LearnerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LearnerRepository>().As<ILearnerRepository>().SingleInstance();
           
        }
    }
}
