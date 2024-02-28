using Microsoft.EntityFrameworkCore;
using StudentExchangeInfo.Domain.Entities;

namespace StudentExchangeInfo.Persistence.Concrete
{ 
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-OK3QKVJ;Database=StudentExchangeInfo;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true;Integrated Security=True;"); 
        }
        
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<About> About { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}
