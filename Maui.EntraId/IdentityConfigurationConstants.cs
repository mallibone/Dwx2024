namespace Maui.EntraId;

public static class IdentityConfigurationConstants
{
    public static string Authority =
        "https://login.microsoftonline.com/05eeea2e-8463-487e-b172-ec05bdcd966d/oauth2/v2.0/authorize";

    public const string ApplicationId = "59c42f9d-6af4-4f64-981c-eee0972fc8f8";
    public const string AndroidDataScheme = $"msal{ApplicationId}";

    public static string[] Scopes =
    [
        "openid", "profile", "offline_access"
    ];
}