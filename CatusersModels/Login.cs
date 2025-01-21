
using System.ComponentModel.DataAnnotations;

namespace CatUsersModels;
public class Login
{
    [Required]
    public string username { get; set; }
    [Required]
    public string password { get; set; }
}
