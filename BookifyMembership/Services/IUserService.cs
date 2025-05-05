using BookifyMembership.Models;
using BookifyMembership.Models.Dtos;

namespace BookifyMembership.Services
{
    public interface IUserService 
    {
        User? Register(RegisterRequest request);
        User? Login(LoginRequest request);
        User? GetById(int id);
        bool UpgradeMembership(int userId);
    }
}
