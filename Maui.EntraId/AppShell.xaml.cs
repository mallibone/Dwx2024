namespace Maui.EntraId;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
    }
}