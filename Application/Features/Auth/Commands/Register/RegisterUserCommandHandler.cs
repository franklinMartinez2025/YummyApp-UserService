using Application.Wrappers;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Features.Auth.Commands.Register
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Response<bool>>
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Response<bool>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>();

            try
            {
                if (!await _userRepository.IsEmailUniqueAsync(request.Email, cancellationToken))
                {
                    throw new Exception("El correo ya existe.");
                }

                var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

                var user = new User(
                    request.FullName,
                    request.Email,
                    passwordHash,
                    request.PhoneNumber,
                    request.Role
                );

                await _userRepository.AddAsync(user, cancellationToken);
                response.Succeeded = true;
                response.Message = "El usuario se ha registrado correctamente.";
                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Message = ex.Message;
                response.Errors = [ex.Message];
            }

            return response;
        }
    }
}
