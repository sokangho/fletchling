using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Fletchling.Application.Config;
using Fletchling.Domain.ApiModels.Responses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Fletchling.Api.Auth;

public static class AuthExtensions
{
    public static IServiceCollection AddAndConfigureAuthentication(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var jwtConfig = config.GetSection(nameof(JwtConfig)).Get<JwtConfig>();
                var signingKey = Encoding.UTF8.GetBytes(jwtConfig.SigningKey);
                var encryptingKey = Encoding.UTF8.GetBytes(jwtConfig.EncryptingKey);
                
                options.IncludeErrorDetails = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    
                    ValidIssuer = jwtConfig.Issuer,
                    ValidAudience = jwtConfig.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(signingKey),
                    TokenDecryptionKey = new SymmetricSecurityKey(encryptingKey)
                };
                    
                options.Events = new JwtBearerEvents()
                {
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine(context);

                        return Task.CompletedTask;
                    }
                };
                    
                // Add custom error message on authentication fails
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        var res = new ErrorResponse
                        {
                            StatusCode = (int)HttpStatusCode.Unauthorized,
                            ErrorMessage = "JWT token is missing or invalid."
                        };

                        // Add response body for 401 error
                        context.Response.OnStarting(async () =>
                        {
                            await context.Response.WriteAsJsonAsync(res);
                        });

                        return Task.CompletedTask;
                    }
                };
            });
        
        return services;
    }
}