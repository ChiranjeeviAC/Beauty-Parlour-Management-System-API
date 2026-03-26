using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Model;

namespace WebApplication1.Repositories
{
    public class AuthCustomerRepository : IAuthCustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthCustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool EmailExists(string email)
        {
            return _context.UserCs.Any(u => u.Email == email);
        }

        public Customer AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return customer;
        }

        public void AddUser(UserC user)
        {
            _context.UserCs.Add(user);
            _context.SaveChanges();
        }

        public Customer GetCustomerByEmail(string email)
        {
            return _context.Customers
                .FirstOrDefault(c => c.Email == email);
        }

        public UserC GetUserByEmail(string email)
        {
            return _context.UserCs
                .FirstOrDefault(u => u.Email == email);
        }
    }
}