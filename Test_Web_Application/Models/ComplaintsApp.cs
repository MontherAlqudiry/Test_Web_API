﻿using System.ComponentModel;
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

        public string File {  get; set; }

        [NotMapped]
        public IFormFile uploadFile { get; set; }

        public string Status { get; set; } = "Pending"; 

        public int UserId { get; set; }
        public string UserGmail { get; set; }
        public string demandOneText { get; set; }
        [NotMapped]

        [ForeignKey("UserId")]
        public User User { get; set; }      

    }
}
