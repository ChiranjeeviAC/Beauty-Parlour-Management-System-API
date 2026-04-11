using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Model;

namespace WebApplication1.Repositories
{
    public class AuthStaffRepository : IAuthStaffRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthStaffRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool EmailExists(string email)
        {
            return _context.UserSs.Any(u => u.Email == email);
        }

        public Staff AddStaff(Staff staff)
        {
            _context.Staffs.Add(staff);
            _context.SaveChanges();
            return staff;
        }

        public void AddUser(UserS user)
        {
            _context.UserSs.Add(user);
            _context.SaveChanges();
        }

        public Staff GetStaffByEmail(string email)
        {
            return _context.Staffs.FirstOrDefault(x => x.Email == email);
        }

        public UserS GetUserByEmail(string email)
        {
            return _context.UserSs.FirstOrDefault(x => x.Email == email);
        }
    }
}