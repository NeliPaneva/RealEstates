using Microsoft.EntityFrameworkCore;
using RealEstates.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstates.Data
{
    public class RealEstateDbContext : DbContext
    {
        public RealEstateDbContext()
        {
        }
        public RealEstateDbContext(DbContextOptions options)
            : base(options) { }                                                         
        
        public  DbSet<RealEstateProperty> RealEstateProperties { get; set; }
        public  DbSet<District> Districtss { get; set; }
        public  DbSet<TypeOfBuilding> BuildingsTypes { get; set; }
        public  DbSet<PropertyType> PropertyTypes { get; set; }
        public  DbSet<Tag> Tags { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=RealEstate;Integrated Security=true;TrustServerCertificate=True");
            }
                
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //при изтриване на квартал да се трие само ако няма имоти
            modelBuilder.Entity<District>()
                .HasMany(x => x.Properties)
                .WithOne(x => x.District)
                .OnDelete(DeleteBehavior.Restrict);

            //при таблицата за връзка много към много така се дефинира композитен ключ от две полета
                   
            modelBuilder.Entity<RealEstatePropertyTag>()
               .HasKey(x => new { x.PropertyId, x.TagIdId });
        }
    }
}
