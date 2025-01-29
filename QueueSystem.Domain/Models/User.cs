using QueueSystem.Domain.Enums;

namespace QueueSystem.Domain.Models
{
    public class UserModel
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public UserRole Role { get; set; }
    }
}