using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Model;

public class UserC
{
    public int UserCId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public int CustomerId { get; set; }
    [ForeignKey("CustomerId")]
    public Customer Customer { get; set; }
}