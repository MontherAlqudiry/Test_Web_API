using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Test_Web_Application.Models
{
    [NotMapped]
    public class UserLogin
    {

            [NotNull]
            [Required(ErrorMessage = "Username is required.")]
            public string Email { get; set; }


            [DataType(DataType.Password)]
            [Required(ErrorMessage = "Password is required.")]
            [NotNull]
            public string Password { get; set; }

           
        
    }
}
