using Microsoft.EntityFrameworkCore;
using RealEstates.Data;
using RealEstates.Services;

namespace RealEstates.ConsoleApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var db = new RealEstateDbContext();
            db.Database.EnsureCreated();
            db.Database.Migrate();
            IPropertiesService propertiesService = new PropertyService(db);
            propertiesService.Create("Dianabat", 120, 2018, 20000, "1-СТАЕН", "Тухла", 16, 20);
            propertiesService.Create("Dianabat", 100, 2018, 210000, "4-СТАЕН", "Тухла", 20, 20);
            propertiesService.UpdateTags(1);
            propertiesService.UpdateTags(2);
        }
    }
}
