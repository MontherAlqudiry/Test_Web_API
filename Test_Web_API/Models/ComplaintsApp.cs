using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Test_Web_API.Models
{
    public class ComplaintsApp
    {
        public int Id { get; set; }
      
        public string Name { get; set; }
       
        public string Content { get; set; }
        
        public string Type { get; set; }
        public string File { get; set; }

        public int UserId { get; set; }
        public string Status { get; set; } = "Pending";
        [NotMapped]
        [JsonIgnore]
        [ForeignKey("UserId")]
        public User User { get; set; }

    }
}
