using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test_Web_Application.Models
{
    [NotMapped]
    public class UserLogin
    {


            [Required(ErrorMessage = "Username is required.")]
            public string Email { get; set; }


            [DataType(DataType.Password)]
            [Required(ErrorMessage = "Password is required.")]
            public string Password { get; set; }
        
    }
}
