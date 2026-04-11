using WebApplication1.Model;

namespace WebApplication1.Interfaces
{
    public interface IAuthStaffRepository
    {
        bool EmailExists(string email);
        Staff AddStaff(Staff staff);
        void AddUser(UserS user);
        Staff GetStaffByEmail(string email);
        UserS GetUserByEmail(string email);
    }
}