using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceBusPOC.WebUI.Server.Filters;

namespace ServiceBusPOC.WebUI.Server.Controllers;

[ApiController]
[ApiExceptionFilter]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender _mediator = null!;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
