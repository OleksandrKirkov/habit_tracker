using System.ComponentModel.DataAnnotations;

namespace Core.DTO.Auth
{
    public class RegisterRequest
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        public string? Name { get; set; }
    }

    public class LoginRequest
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }

    public class TokenResponse
    {
        public string AccessToken { get; set; } = null!;
    }

    public class MeResponse
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string AvatarUrl { get; set; } = null!;
    }
}
