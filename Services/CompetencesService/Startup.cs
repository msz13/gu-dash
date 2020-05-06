using CompetencesService;
using CompetencesService.Infrastructure;
using CompetencesService.Infrastructure.Auth;
using CompetencesService.Infrastructure.DomainServices;
using CompetencesService.Infrastructure.Persistance;
using Grpc.HealthCheck;
using GuDash.CompetencesService.Application.CommandHandlers;
using GuDash.CompetencesService.Infrastructure.Persistance;
using GuDash.CompetencesService.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GuDash.CompetencesService
{
    public class Startup
    {
        public IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            services.AddControllers();
            services.AddMediatR(typeof(DefineCompetenceCommand));
            services.AddMartenStore(Configuration.GetSection("Postgres").Get<PostgresSettings>());
            services.AddDomainServices();
            services.AddSingleton<HealthServiceImpl>();
            services.AddSwaggerDocument();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }


            app.UseExceptionHandler(errorApp => {
                errorApp.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "text";
                    await context.Response.WriteAsync("Something bad has happend. Please try egain");
                });
            });

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseRouting();

           // app.UseHttpsRedirection();
                                  
            app.UseAuthenticationMiddlewere();          
                        

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<HealthServiceImpl>();
                endpoints.MapGrpcService<GreeterService>();

                endpoints.MapGrpcService<CompetenceService>();

                endpoints.MapControllers();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
