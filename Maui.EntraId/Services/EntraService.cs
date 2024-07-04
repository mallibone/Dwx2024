using System.Diagnostics;
using Microsoft.Identity.Client;

namespace Maui.EntraId.Services;

public class EntraService : IEntraService
{
    private readonly IPublicClientApplication _identityClient;

    public EntraService(IPublicClientApplication identityClient)
    {
        _identityClient = identityClient;
    }
    
    public Credentials LastResult { get; private set; } = new Credentials("no token", "no token", DateTimeOffset.MinValue, true, "No token acquired yet");


    public async Task<Credentials> RefreshToken()
    {
        try
        {
            // Try getting a token silently (no login requested from user)
            return await TryAcquireToken(true);
        }
        catch (Exception ex)
        {
            return new Credentials("no token", "no token", DateTimeOffset.MinValue, true,
                $"Silent Error: {ex.Message}");
        }
    }
    
    public async Task<Credentials> Authenticate()
    {
        try
        {
            // Try getting a token silently (no login requested from user)
            return await TryAcquireToken();
        }
        catch (MsalUiRequiredException)
        {
             // We need to do an interactive login
             return await PerformUserLogin();
        }
        catch (Exception ex)
        {
            LastResult =  new Credentials("no token", "no token", DateTimeOffset.MinValue, true,
                $"Silent Error: {ex.Message}");
            return LastResult;
        }
    }

    public async Task Logout()
    {
        // await WebLogout();
        var accounts = (await _identityClient.GetAccountsAsync()).ToList();
        if (!accounts.Any()) return;
        foreach (var account in accounts)
        {
            await _identityClient.RemoveAsync(account);
        }
    }

    private async Task WebLogout()
    {
        try
        {
            _ = await _identityClient
                .AcquireTokenInteractive(IdentityConfigurationConstants.Scopes)
                .ExecuteAsync();
        }
        catch (Exception)
        {
            // we have to abort the login process to logout...
        }
    }

    private async Task<Credentials> TryAcquireToken(bool forceRefresh = false)
    {
        var accounts = await _identityClient.GetAccountsAsync();
        
        AuthenticationResult result = await _identityClient
            .AcquireTokenSilent(IdentityConfigurationConstants.Scopes, accounts.FirstOrDefault())
            .WithForceRefresh(forceRefresh)
            .ExecuteAsync();
        LastResult = new Credentials(result.AccessToken, result.IdToken, result.ExpiresOn);
        return LastResult;
    }

    private async Task<Credentials> PerformUserLogin()
    {
        try
        {
            var result = await _identityClient
                .AcquireTokenInteractive(IdentityConfigurationConstants.Scopes)
                // .WithPrompt(Prompt.ForceLogin)
                .ExecuteAsync();
            LastResult = new Credentials(result.AccessToken, result.IdToken, result.ExpiresOn);
            return LastResult;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"MSAL Interactive Error: {ex.Message}");
                LastResult = new Credentials("no token", "no token", DateTimeOffset.MinValue, true,
                    $"Interactive Error: {ex.Message}");
                return LastResult;
        }
    }
}

public record Credentials(string AccessToken, string IdentityToken, DateTimeOffset AccessTokenExpiration, bool HasError = false, string Error = "");
