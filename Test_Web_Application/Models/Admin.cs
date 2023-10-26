using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Test_Web_Application.Models
{
    public class Admin
    {


        [Key]
        public int Id { get; set; }



        [Required(ErrorMessage = "Username is required.")]
        [MaxLength(100, ErrorMessage = "The Max Length Is 100")]
        public string Username { get; set; }



        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [MaxLength(100, ErrorMessage = "The Max Length Is 100")]
        public string Email { get; set; }



        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [MaxLength(100, ErrorMessage = "The Max Length Is 100")]
        public string Password { get; set; }
        [NotMapped]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }


        [JsonIgnore]
        public string Role { get; set; } = "user";

    }
}
