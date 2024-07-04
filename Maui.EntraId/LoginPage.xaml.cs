using Maui.EntraId.ViewModels;

namespace Maui.EntraId;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel loginViewModel)
    {
        InitializeComponent();
        BindingContext = loginViewModel;
    }
}