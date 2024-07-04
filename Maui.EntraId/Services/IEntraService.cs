namespace Maui.EntraId.Services;

public interface IEntraService
{
    Credentials LastResult { get; }
    Task<Credentials> RefreshToken();
    Task<Credentials> Authenticate();
    Task Logout();
}