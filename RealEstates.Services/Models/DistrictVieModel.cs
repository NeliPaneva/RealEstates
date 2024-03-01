using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstates.Services.Models
{
    public class DistrictVieModel
    {
        public string Name   { get; set; }
        public int minPrice {  get; set; }
        public int maxPrice {  get; set; }
        public double AveragePrice { get; set; }
        public int PropertiesCount {  get; set; }

    }
}
