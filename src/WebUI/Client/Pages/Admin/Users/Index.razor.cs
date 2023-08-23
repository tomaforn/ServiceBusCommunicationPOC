using Microsoft.AspNetCore.Components;
using ServiceBusPOC.WebUI.Shared.AccessControl;

namespace ServiceBusPOC.WebUI.Client.Pages.Admin.Users;

public partial class Index
{
    [Inject]
    public IUsersClient UsersClient { get; set; } = null!;

    public UsersVm? Model { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Model = await UsersClient.GetUsersAsync();
    }
}
