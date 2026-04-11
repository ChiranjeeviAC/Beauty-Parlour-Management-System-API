using WebApplication1.DTOs;

namespace WebApplication1.Interfaces
{
    public interface IAuthStaffService
    {
        object Register(StaffRegisterDto dto);
        object Login(LoginDto dto);
    }
}