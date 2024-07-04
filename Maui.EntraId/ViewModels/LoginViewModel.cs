using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Maui.EntraId.Services;

namespace Maui.EntraId.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly IEntraService _entraService;
    [ObservableProperty] private bool _hasError = false;
    [ObservableProperty] private string _errorText = "";

    public LoginViewModel(IEntraService entraService)
    {
        _entraService = entraService;
    }
    
    [RelayCommand]
    private async Task Login()
    {
        var result = await _entraService.Authenticate();
        HasError = result.HasError;
        ErrorText = result.Error;
        if(!result.HasError) await Shell.Current.GoToAsync(nameof(MainPage));
    }

}

