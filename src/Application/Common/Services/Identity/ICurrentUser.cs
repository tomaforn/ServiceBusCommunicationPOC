namespace ServiceBusPOC.Application.Common.Services.Identity;

public interface ICurrentUser
{
    string? UserId { get; }
}
