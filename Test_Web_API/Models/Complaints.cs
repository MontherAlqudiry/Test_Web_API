using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Test_Web_API.Models
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
       
    }
}
