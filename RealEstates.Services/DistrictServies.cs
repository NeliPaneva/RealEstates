using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using RealEstates.Data;
using RealEstates.Models;
using RealEstates.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RealEstates.Services
{
    public class DistrictServies : IDistrictService
    {
        private RealEstateDbContext db;

        public DistrictServies(RealEstateDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<DistrictVieModel> GetTopDiscritesByAveragePrice(int count = 10)
        {
            return db.Districtss
                .OrderByDescending(x => x.Properties.Average(x=>(double)x.Price/x.Size))
                .Select(MapToDistrictViewModel())
                .Take(count)
                .ToList();
        }

        private static Expression<Func<District, DistrictVieModel>> MapToDistrictViewModel()
        {
            return x => new DistrictVieModel
            {
                Name = x.Name,
                AveragePrice = x.Properties.Average(x =>(double) x.Price),
                minPrice = x.Properties.Min(x => x.Price),
                maxPrice = x.Properties.Max(x => x.Price),
                PropertiesCount = x.Properties.Count(),

            };
        }

        public IEnumerable<DistrictVieModel> GetTopDiscritesByNumberOfProperties(int count = 10)
        {
            return this.db.Districtss
                 .OrderByDescending(x => x.Properties.Count())
                 .Select(MapToDistrictViewModel())
                 .Take(count)
                 .ToList();
        }
    }
}
