using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuDash.Competences.API.Ports_Adapters.MongoPersistance;
using GuDash.Competences.API.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NServiceBus;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using GuDash.Competences.API.Domain.Competences;
using GuDash.Competences.API.Ports_Adapters.Repositories;
using Newtonsoft.Json;
using GuDash.Competences.Service.Application;
using GuDash.Competences.Service.Ports_Adapters.Repositories;

namespace Competences.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public ILifetimeScope AutofacContainer { get; private set; }

        IEndpointInstance BusEndpoint = null;

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
                        
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
           // services.AddSingleton<CompetenceViewModelService>();
            var builder = new ContainerBuilder();
            //services.AddSingleton<MongoDbContext>();
            var mongoContext = new MongoDbContext(Configuration["MongoDbConfiguration:Url"], Configuration["MongoDbConfiguration:DatabaseName"]);
            builder.RegisterInstance(mongoContext).AsSelf().SingleInstance();
            builder.RegisterModule(new CompetencesModule());
            builder.RegisterModule(new LearnerModule());
            //builder.RegisterType<EventStoreClient>().As<IEventStore>().SingleInstance();
           
            
            builder.Register(x => BusEndpoint)
                .As<IEndpointInstance>()
                .SingleInstance();
            builder.Populate(services);
            AutofacContainer = builder.Build();

           
            return new AutofacServiceProvider(AutofacContainer);

        

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

           // app.UseHttpsRedirection();
            app.UseMvc();

            ConfigureNServiceBus();
        }

        private  async void ConfigureNServiceBus()
        {
            var endpointConfiguration = new EndpointConfiguration("Samples.Autofac");
            endpointConfiguration.UseContainer<AutofacBuilder>(
            customizations: customizations =>
            {
                customizations.ExistingLifetimeScope(AutofacContainer);
            });

            
            endpointConfiguration.UsePersistence<LearningPersistence>();
            endpointConfiguration.UseTransport<LearningTransport>();
           endpointConfiguration.UseSerialization<NewtonsoftSerializer>();

            BusEndpoint = await Endpoint.Start(endpointConfiguration)
               .ConfigureAwait(false);

        }
    }
}
