using System.Net.Sockets;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Service.Shared;

public static class AddServiceExtensions
{
    public static IServiceCollection AddFirestore(this IServiceCollection services, IConfiguration configuration)
    {
        var projectId = configuration["Firebase:ProjectId"];
        var credentialPath = configuration["Firebase:CredentialsPath"];

        if (!string.IsNullOrEmpty(credentialPath))
        {
            if (!File.Exists(credentialPath))
                throw new FileNotFoundException($"Firebase credentials file not found at: {credentialPath}");

            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialPath);
        }
        else
        {
            throw new InvalidOperationException("Firebase:CredentialsPath is missing in configuration.");
        }

        services.AddSingleton(provider => FirestoreDb.Create(projectId));

        services.AddScoped<BaseRepository>();

        return services;
    }
}
