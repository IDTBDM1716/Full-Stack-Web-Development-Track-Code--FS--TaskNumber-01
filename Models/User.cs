using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace SecureUserAuthenticationSystem.Models
{
    // Enum to define user roles
    public enum Role
    {
        Admin,
        User,
        Guest
    }

    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(100, ErrorMessage = "Full Name cannot be longer than 100 characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        public string PasswordHash { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime DateCreated { get; set; } = DateTime.Now;

        // Role management (Optional)
        public Role UserRole { get; set; } = Role.User;

        // Email confirmation flag
        public bool IsEmailConfirmed { get; set; } = false;

        // Optional: Add a field for email verification token
        public string EmailVerificationToken { get; set; }

        // Method to set the password hash securely (bcrypt, PBKDF2 or another method can be used in place of SHA-256)
        public void SetPassword(string password)
        {
            this.PasswordHash = HashPassword(password); // Hash password using a secure algorithm
        }

        // Method to verify the password by comparing hashes
        public bool VerifyPassword(string password)
        {
            string hashedPassword = HashPassword(password);
            return PasswordHash == hashedPassword;
        }

        // Method to generate a password hash using SHA256 (consider using PBKDF2 or bcrypt for production)
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        // Method to generate an email verification token
        public string GenerateEmailVerificationToken()
        {
            return Guid.NewGuid().ToString();  // Generate a unique GUID for email verification
        }
    }
}
