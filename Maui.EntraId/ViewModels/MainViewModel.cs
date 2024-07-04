using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Maui.EntraId.Services;
using Microsoft.Identity.Client;

namespace Maui.EntraId.ViewModels;

public partial class MainViewModel : ObservableObject
{
    public MainViewModel(IEntraService entraService)
    {
        _entraService = entraService;
        TokenExpirationText = _entraService.LastResult.AccessTokenExpiration.ToString("u");
        AccessTokenText = _entraService.LastResult.AccessToken;
        IdTokenText = _entraService.LastResult.IdentityToken;
    }
    
    [RelayCommand]
    private async Task Logout()
    {
        await _entraService.Logout();
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task CopyAccessToken()
    {
        await Clipboard.Default.SetTextAsync(AccessTokenText);
    }

    [RelayCommand]
    private async Task CopyIdentityToken()
    {
        await Clipboard.Default.SetTextAsync(IdTokenText);
    }
    
    [RelayCommand]
    private async Task Refresh()
    {
        var credentials = await _entraService.RefreshToken();
        TokenExpirationText = credentials.AccessTokenExpiration.ToString("u");
        AccessTokenText = credentials.AccessToken;
        IdTokenText = credentials.IdentityToken;
    }

    [ObservableProperty] private string _tokenExpirationText = "";
    [ObservableProperty] private string _accessTokenText = "";
    [ObservableProperty] private string _idTokenText = "";
    private readonly IEntraService _entraService;
}