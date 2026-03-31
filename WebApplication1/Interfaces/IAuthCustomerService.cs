using WebApplication1.DTOs;
using WebApplication1.DTOs.Customer;
using WebApplication1.Model;

namespace WebApplication1.Interfaces
{
    public interface IAuthCustomerService
    {
        object Register(CustomerRegisterDto dto);
        object Login(LoginDto dto);
        public string GenerateJwtToken(Customer customer);
    }
}