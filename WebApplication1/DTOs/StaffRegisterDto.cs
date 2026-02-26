using WebApplication1.Model.Enums;

public class StaffRegisterDto
{
    public string StaffName { get; set; }
    public StaffRole Role { get; set; }
    public string Phone { get; set; }
    public int Experience { get; set; }
    public double Salary { get; set; }

    public string Email { get; set; }
    public string Password { get; set; }
}