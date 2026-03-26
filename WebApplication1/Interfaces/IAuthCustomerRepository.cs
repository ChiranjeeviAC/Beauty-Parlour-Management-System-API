using WebApplication1.Model;

namespace WebApplication1.Interfaces
{
    public interface IAuthCustomerRepository
    {
        bool EmailExists(string email);

        Customer AddCustomer(Customer customer);

        void AddUser(UserC user);

        Customer GetCustomerByEmail(string email);

        UserC GetUserByEmail(string email);
    }
}