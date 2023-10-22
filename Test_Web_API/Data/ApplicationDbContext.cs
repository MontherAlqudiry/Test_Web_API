﻿using Microsoft.EntityFrameworkCore;
using Test_Web_API.Models;

namespace Test_Web_API.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options) 
        {
            
        }

       
        public DbSet<Test_Web_API.Models.ComplaintsApp>? ComplaintsApp { get; set; }

       
        public DbSet<Test_Web_API.Models.UplaodFile>? UplaodFile { get; set; }

       
       
    }
}