using WebApplication1.Model;

namespace WebApplication1.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(int userId, string email, string role);
    }
}