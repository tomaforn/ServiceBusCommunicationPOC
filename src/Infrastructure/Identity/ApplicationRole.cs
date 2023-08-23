using Microsoft.AspNetCore.Identity;
using ServiceBusPOC.WebUI.Shared.Authorization;

namespace ServiceBusPOC.Infrastructure.Identity;

public class ApplicationRole : IdentityRole
{
    public Permissions Permissions { get; set; }
}
