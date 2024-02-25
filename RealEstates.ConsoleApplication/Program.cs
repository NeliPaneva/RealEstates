using Microsoft.EntityFrameworkCore;
using RealEstates.Data;
using RealEstates.Services;
using RealEstates.Services.Models;
using System.Text;

namespace RealEstates.ConsoleApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            var db = new RealEstateDbContext();
            db.Database.EnsureCreated();
            db.Database.Migrate();
            IPropertiesService propertiesService=new PropertyService(db);
            propertiesService.Create("Ovcha kupel", 97, 1997, 200000, "1-Стаен", "тухла", 12, 6);
            propertiesService.Create("Lulin", 56, 1992, 100000, "1-Стаен", "панел", 7, 23);
            propertiesService.Create("Mladost", 64, 1991, 230000, "1-Стаен", "тухла", 12, 12);
            propertiesService.Create("Drujba",197, 1995, 2200000, "1-Стаен", "панел", 1, 4);
            Console.WriteLine(db.RealEstateProperties.Count());


            IDistrictService districtService = new DistrictServies(db);
            var districts = districtService.GetTopDiscritesByNumberOfProperties();
            foreach (var district in districts)
            {
                Console.WriteLine($"{district.Name}=> Price: {district.AveragePrice:0.00}  ({district.minPrice}-{district.maxPrice}) => {district.PropertiesCount}");
            }
        }
    }
}
