using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

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



    }
}
