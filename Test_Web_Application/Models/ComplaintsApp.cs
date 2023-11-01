using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Test_Web_Application.Models
{
    public class ComplaintsApp
    {
        public int Id { get; set; }
        [DisplayName("Title")]
        public string Name { get; set; }
        [DisplayName("Discription")]
        public string Content { get; set; }
        [DisplayName("Type")]
        public string Type { get; set; }
        [AllowNull]
        public string? File {  get; set; }

        [NotMapped]
        [AllowNull]
        public IFormFile? uploadFile { get; set; }

        public string Status { get; set; } = "Pending"; 

        public int UserId { get; set; }
        public string UserGmail { get; set; }
        [AllowNull]
        public string? demandOneText { get; set; }
        [AllowNull]
        public string? demandTwoText { get; set; }
        [NotMapped]

        [ForeignKey("UserId")]
        public User User { get; set; }      

    }
}
