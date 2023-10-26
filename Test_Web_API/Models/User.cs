using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Test_Web_API.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }



        [Required(ErrorMessage = "Firstname is required.")]
        [MaxLength(100, ErrorMessage = "The Max Length Is 100")]
        public string Firstname { get; set; }


        [Required(ErrorMessage = "Lastname is required.")]
        [MaxLength(100, ErrorMessage = "The Max Length Is 100")]
        public string Lastname { get; set; }



        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [MaxLength(100, ErrorMessage = "The Max Length Is 100")]
        public string Email { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [MaxLength(100, ErrorMessage = "The Max Length Is 100")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
       
       
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [NotMapped]
        public string Role { get; set; } = "user";


    }
}
