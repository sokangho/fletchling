using Fletchling.Data.Repositories;
using Google.Cloud.Firestore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Fletchling.Data
{
    public static class Bindings
    {
        public static IServiceCollection RegisterDatabase(this IServiceCollection services)
        {
            string firebaseAdminSdkPath = AppDomain.CurrentDomain.BaseDirectory + "firebase-adminsdk-fletchling-dev.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", firebaseAdminSdkPath);
            services.AddSingleton<FirestoreDb>(f => FirestoreDb.Create("fletchling-dev"));

            services.AddTransient<IClientRepository, ClientRepository>();

            return services;
        }
    }
}
