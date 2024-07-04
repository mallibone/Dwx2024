using Microsoft.Identity.Client;

namespace Maui.EntraId;

internal static class IdentityBuilderExtensions
{
    public static IServiceCollection ConfigureIdentity(this IServiceCollection builder)
    {
        builder.AddSingleton<IPublicClientApplication>(_ => CreateIdentityClient());

        return builder;
    }
    
    private static IPublicClientApplication CreateIdentityClient()
    {
#if ANDROID
        return PublicClientApplicationBuilder
            .Create(IdentityConfigurationConstants.ApplicationId)
            // .WithAuthority(AzureCloudInstance.AzurePublic, "common")
            .WithAuthority(IdentityConfigurationConstants.Authority)
            .WithRedirectUri($"msal{IdentityConfigurationConstants.ApplicationId}://auth")
            .WithParentActivityOrWindow(() => Platform.CurrentActivity)
            .Build();
#elif IOS
        return PublicClientApplicationBuilder
            .Create(IdentityConfigurationConstants.ApplicationId)
            // .WithAuthority(AzureCloudInstance.AzurePublic, "common")
            .WithAuthority(IdentityConfigurationConstants.Authority)
            // .WithIosKeychainSecurityGroup("com.microsoft.adalcache")
            .WithRedirectUri($"msal{IdentityConfigurationConstants.ApplicationId}://auth")
            .Build();
#else
        return PublicClientApplicationBuilder
            .Create(IdentityConfigurationConstants.ApplicationId)
            // .WithAuthority(AzureCloudInstance.AzurePublic, "common")
            .WithAuthority(IdentityConfigurationConstants.Authority)
            .WithRedirectUri("https://login.microsoftonline.com/common/oauth2/nativeclient")
            .Build();
#endif
    }
}