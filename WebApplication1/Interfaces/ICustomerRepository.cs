using WebApplication1.Model;

namespace WebApplication1.Interfaces
{
    public interface ICustomerRepository
    {
        List<Customer> GetAll();
        Customer GetById(int id);
        Customer Update(Customer customer);
        bool Delete(int id);
        Customer GetCustomerWithAppointments(int id);

        UserC GetUserByCustomerId(int customerId);
        void UpdateUser(UserC user);
    }
}