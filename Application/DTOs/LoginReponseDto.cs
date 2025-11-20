using Domain.Enums;

namespace Application.DTOs
{
    public class LoginReponseDto
    {
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public UserRole Roles { get; set; } = default!;
        public string JwToken { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
    }
}
