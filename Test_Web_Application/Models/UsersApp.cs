using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Test_Web_Application.Models
{
    public class UsersApp
    {
        public int Id { get; set; }

        
        [DisplayName("UserName")]
        public string Name { get; set; }
    }
}
