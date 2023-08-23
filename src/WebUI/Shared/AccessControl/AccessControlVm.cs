﻿using ServiceBusPOC.WebUI.Shared.Authorization;

namespace ServiceBusPOC.WebUI.Shared.AccessControl;

public class AccessControlVm
{
    public AccessControlVm()
    {
    }

    public AccessControlVm(IList<RoleDto> roles, IList<Permissions> permissions)
    {
        Roles = roles;
        AvailablePermissions = permissions;
    }

    public IList<RoleDto> Roles { get; set; } = new List<RoleDto>();

    public IList<Permissions> AvailablePermissions { get; set; } = new List<Permissions>();
}
