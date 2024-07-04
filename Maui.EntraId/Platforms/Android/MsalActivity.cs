using Android.App;
using Android.Content;
using Microsoft.Identity.Client;

namespace Maui.EntraId;

[Activity(Exported = true)]
[IntentFilter(new[] { Intent.ActionView },
    Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault },
    DataHost = "auth",
    DataScheme = IdentityConfigurationConstants.AndroidDataScheme)]
public class MsalActivity : BrowserTabActivity
{
}