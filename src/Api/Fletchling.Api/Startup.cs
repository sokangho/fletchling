using System;
using Fletchling.Api.Authorization;
using Fletchling.Api.Logging;
using Fletchling.Api.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Fletchling.Api.Auth;
using Fletchling.Domain.ApiModels.Responses;

namespace Fletchling.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            
            services.AddCors();
            services.AddControllers();

            // Add and configure Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Fletchling.Api", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
                        
            // Add and configure JWT authentication
            services.AddAndConfigureAuthentication(Configuration);
            
            // Add and configure authorization
            services.AddSingleton<IAuthorizationHandler, IsOwnerAuthorizationHandler>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("OwnerPolicy", policy => policy.AddRequirements(new IsOwnerRequirement()));
            });

            // Register custom services 
            services.RegisterBindings(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable Request buffering here so that Request can be read multiple times
            app.Use((context, next) =>
            {
                context.Request.EnableBuffering();
                return next();
            });

            if (env.IsDevelopment())
            {                
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fletchling.Api v1"));
            }

            var forwardedHeaderOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            };
            app.UseForwardedHeaders(forwardedHeaderOptions);
            
            app.UseSerilogRequestLogging(options =>
            {
                async void OptionsEnrichDiagnosticContext(IDiagnosticContext diagosticContext, HttpContext httpContext)
                {
                    await SeriLogEnrichment.EnrichWithRequestDetails(diagosticContext, httpContext);
                }

                options.EnrichDiagnosticContext = OptionsEnrichDiagnosticContext;
            });

            // Custom exception handling middleware
            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.UseRouting();
            app.UseCors(options =>
                options.WithOrigins(Configuration["WebUrls"].Split(";"))
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
