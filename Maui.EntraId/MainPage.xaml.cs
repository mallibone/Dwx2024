using Maui.EntraId.ViewModels;

namespace Maui.EntraId;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel mainViewModel)
    {
        InitializeComponent();
        BindingContext = mainViewModel;
    }
}