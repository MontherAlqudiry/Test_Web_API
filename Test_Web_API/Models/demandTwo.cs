using System.ComponentModel.DataAnnotations.Schema;

namespace Test_Web_API.Models
{
    public class demandTwo
    {

        public int Id { get; set; }

        [ForeignKey("ComplaintsApp")]
        public int ComplaintId { get; set; }
        public string demandTwoText { get; set; }
        public int UserId { get; set; }
        [NotMapped]
        public ComplaintsApp ComplaintsApp { get; set; }
    }
}
