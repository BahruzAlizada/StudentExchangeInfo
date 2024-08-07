﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentExchangeInfo.Domain.Entities;
using StudentExchangeInfo.Domain.Identity;

namespace StudentExchangeInfo.Persistence.Concrete
{ 
    public class Context : IdentityDbContext<AppUser,AppRole,int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=DESKTOP-OK3QKVJ;Database=StudentExchangeInfo;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true;Integrated Security=True;"); 
            optionsBuilder.UseSqlServer("Data Source=SQL5113.site4now.net;Initial Catalog=db_aa7bfc_bahruzalizada;User Id=db_aa7bfc_bahruzalizada_admin;Password=Behruz1234");

		}

		public DbSet<Slider> Sliders { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<About> About { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Prerequisite> Prerequisites { get; set; }
        public DbSet<Motivation> Motivations { get; set; }
        public DbSet<ExchangeProgram> ExchangePrograms { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<FaqCategory> FaqCategories { get; set; }
        public DbSet<Blog> Blogs { get; set; }
    }
}
