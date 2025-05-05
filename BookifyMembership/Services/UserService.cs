using BookifyMembership.Models;
using BookifyMembership.Models.Dtos;
using Microsoft.AspNetCore.Identity;

namespace BookifyMembership.Services
{
    public class UserService : IUserService
    {
        private static List<User> _users = new();
        private static int _idCounter = 1;
        private readonly PasswordHasher<User> _hasher = new();

        public User? Register(RegisterRequest request)
        {
            if (_users.Any(u =>
                u.Username.Equals(request.Username, StringComparison.OrdinalIgnoreCase) ||
                u.Email.Equals(request.Email, StringComparison.OrdinalIgnoreCase)))
            {
                return null;
            }


            var user = new User
            {
                UserId = _idCounter++,
                Username = request.Username,
                Email = request.Email,
                MembershipTier = string.IsNullOrWhiteSpace(request.MembershipTier) ? "Basic" : request.MembershipTier
            };

            user.PasswordHash = _hasher.HashPassword(user, request.Password);
            _users.Add(user);
            return user;
        }

        public User? Login(LoginRequest request)
        {
            var user = _users.FirstOrDefault(u =>
                u.Username == request.Username || u.Email == request.Email);

            if (user == null) return null;

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            return result == PasswordVerificationResult.Success ? user : null;
        }

        public User? GetById(int id) => _users.FirstOrDefault(u => u.UserId == id);

        public bool UpgradeMembership(int userId)
        {
            var user = GetById(userId);
            if (user == null) return false;

            user.MembershipTier = "Premium";
            return true;
        }
    }
}
