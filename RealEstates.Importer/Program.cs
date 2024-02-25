using Microsoft.EntityFrameworkCore;
using RealEstates.Data;
using RealEstates.Models;
using RealEstates.Services;
using System.Text.Json;

namespace RealEstates.Importer
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            var json = File.ReadAllText("imot.bg-raw-data-2020-07-23.json");
            
            var properties=JsonSerializer.Deserialize<IEnumerable<JsonProperty>>(json);
            var db = new RealEstateDbContext();
            db.Database.EnsureCreated();
            db.Database.Migrate();
            IPropertiesService propertyService = new PropertyService(db);
            foreach (var item in properties)//.Where(x => x.Price > 1000&&x.District.Length>0&&x.PropertyType.Length>0&&x.TypeOfBuilding.Length>0 ))
           //{if(item.Price > 1000&&item.PropertyType!=null&&item.TypeOfBuilding!=null&&item.District!=null)
            {if(item.Price > 1000)
                {
                    try
                    {
                        Console.WriteLine(item.District);

                        propertyService.Create(
                        item.District,
                        item.Size,
                        item.Year,
                        item.Price,
                        item.Type,
                        item.BuildingType,
                        item.Floor,
                        item.TotalFloors
                        );
                    }
                    catch { }
                   
                }
            }
        }
    }
}
