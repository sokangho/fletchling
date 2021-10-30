using Google.Cloud.Firestore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Fletchling.Application.Interfaces.Repositories;
using Fletchling.Data.FirebaseRepositories;

namespace Fletchling.Data
{
    public static class Bindings
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration config)
        {
            string firebaseAdminSdkPath = AppDomain.CurrentDomain.BaseDirectory + "firebase-adminsdk-fletchling-dev.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", firebaseAdminSdkPath);
            services.AddSingleton<FirestoreDb>(f => FirestoreDb.Create(config["Firebase:ProjectId"]));
            Console.Write(config["Firebase:ProjectId"]);

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITimelineRepository, TimelineRepository>();

            return services;
        }
    }
}
