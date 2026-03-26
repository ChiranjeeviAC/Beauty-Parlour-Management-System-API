using WebApplication1.DTOs;
using WebApplication1.DTOs.Customer;

namespace WebApplication1.Interfaces
{
    public interface ICustomerService
    {
        List<CustomerResponseDto> GetAll();
        CustomerResponseDto GetById(int id);
        CustomerResponseDto Update(int id, CustomerUpdateDto dto);
        bool Delete(int id);

        bool ChangePassword(int id, ChangePasswordDto dto);
        object GetCustomerAppointments(int id);
    }
}