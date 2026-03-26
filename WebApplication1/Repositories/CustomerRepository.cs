using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Model;

namespace WebApplication1.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Customer> GetAll()
        {
            return _context.Customers.ToList();
        }

        public Customer GetById(int id)
        {
            return _context.Customers.Find(id);
        }

        public Customer Update(Customer customer)
        {
            _context.Customers.Update(customer);
            _context.SaveChanges();
            return customer;
        }

        public bool Delete(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null) return false;

            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return true;
        }

        public Customer GetCustomerWithAppointments(int id)
        {
            return _context.Customers
                .Include(c => c.Appointments)
                .ThenInclude(a => a.Service)
                .FirstOrDefault(c => c.CustomerId == id);
        }

        public UserC GetUserByCustomerId(int customerId)
        {
            return _context.UserCs
                .FirstOrDefault(u => u.CustomerId == customerId);
        }

        public void UpdateUser(UserC user)
        {
            _context.UserCs.Update(user);
            _context.SaveChanges();
        }
    }
}