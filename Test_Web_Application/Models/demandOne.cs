using System.ComponentModel.DataAnnotations.Schema;

namespace Test_Web_Application.Models
{
    public class demandOne
    {
       
        public int Id { get; set; }

        [ForeignKey("ComplaintsApp")]
        public int ComplaintId { get; set; }
        public string demandOneText { get; set; }
        public int UserId { get; set; }
        [NotMapped]
        public ComplaintsApp ComplaintsApp { get; set; }

    }
}
