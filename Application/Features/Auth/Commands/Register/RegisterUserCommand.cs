using Application.Wrappers;
using Domain.Enums;
using MediatR;

namespace Application.Features.Auth.Commands.Register
{
    public record RegisterUserCommand(
        string FullName,
        string Email,
        string Password,
        string PhoneNumber,
        UserRole Role
    ) : IRequest<Response<bool>>;
}
