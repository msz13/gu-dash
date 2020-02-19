using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CompetencesService;
using CompetencesService.Infrastructure.Auth;
using GuDash.CompetencesService.Application.CommandHandlers;
using GuDash.CompetencesService.Domain.Competences;
using GuDash.CompetencesService.Infrastructure.Persistance;
using GuDash.CompetencesService.Services;
using Marten;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GuDash.CompetencesService
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            services.AddMediatR(typeof(DefineCompetenceCommand));
            services.AddMartenStore("host=manny.db.elephantsql.com;database=ambexvwj;password=PYJrzb2mLVb5Qh3XXA2KPNwaXNBzhc4P;username=ambexvwj;port=5432");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthenticationMiddlewere();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GreeterService>();

                endpoints.MapGrpcService<CompetenceService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
