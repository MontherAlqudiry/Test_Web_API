using Microsoft.EntityFrameworkCore;
using Test_Web_API.Models;

namespace Test_Web_API.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options) 
        {
            
        }

       
        public DbSet<Test_Web_API.Models.ComplaintsApp>? ComplaintsApp { get; set; }

       
        public DbSet<Test_Web_API.Models.Admin>? Admin { get; set; }

       
        public DbSet<Test_Web_API.Models.User>? User { get; set; }

       
        public DbSet<Test_Web_API.Models.UserLogin>? UserLogin { get; set; }
        public DbSet<Test_Web_API.Models.demandOne>? demandOne { get; set; }
        public DbSet<Test_Web_API.Models.demandTwo>? demandTwo { get; set; }

        //public DbSet<Test_Web_API.Models.UplaodFile>? UplaodFile { get; set; }



    }
}
