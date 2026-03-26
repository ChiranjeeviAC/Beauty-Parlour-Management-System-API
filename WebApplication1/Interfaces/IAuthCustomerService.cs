using WebApplication1.DTOs;
using WebApplication1.DTOs.Customer;

namespace WebApplication1.Interfaces
{
    public interface IAuthCustomerService
    {
        object Register(CustomerRegisterDto dto);
        object Login(LoginDto dto);
    }
}