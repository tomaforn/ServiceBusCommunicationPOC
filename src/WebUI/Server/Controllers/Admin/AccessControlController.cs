using Microsoft.AspNetCore.Mvc;
using ServiceBusPOC.Application.AccessControl.Commands;
using ServiceBusPOC.Application.AccessControl.Queries;
using ServiceBusPOC.WebUI.Shared.AccessControl;
using ServiceBusPOC.WebUI.Shared.Authorization;

namespace ServiceBusPOC.WebUI.Server.Controllers.Admin;

[Route("api/Admin/[controller]")]
public class AccessControlController : ApiControllerBase
{
    [HttpGet]
    [Authorize(Permissions.ViewAccessControl)]
    public async Task<ActionResult<AccessControlVm>> GetConfiguration()
    {
        return await Mediator.Send(new GetAccessControlQuery());
    }

    [HttpPut]
    [Authorize(Permissions.ConfigureAccessControl)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateConfiguration(RoleDto updatedRole)
    {
        await Mediator.Send(new UpdateAccessControlCommand(updatedRole.Id, updatedRole.Permissions));

        return NoContent();
    }
}
