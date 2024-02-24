using RealEstates.Data;

namespace RealEstates.ConsoleApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var db = new RealEstateDbContext();
            db.Database.EnsureCreated();
        }
    }
}
