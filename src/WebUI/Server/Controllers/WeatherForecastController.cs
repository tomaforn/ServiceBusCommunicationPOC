using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceBusPOC.Application.WeatherForecasts.Queries;
using ServiceBusPOC.WebUI.Shared.WeatherForecasts;

namespace ServiceBusPOC.WebUI.Server.Controllers;

[Authorize]
public class WeatherForecastController : ApiControllerBase
{
    [HttpGet]
    public async Task<IList<WeatherForecast>> Get()
    {
        return await Mediator.Send(new GetWeatherForecastsQuery());
    }
}
