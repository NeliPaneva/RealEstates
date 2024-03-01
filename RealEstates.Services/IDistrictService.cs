using RealEstates.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstates.Services
{
    public interface IDistrictService
    {
        IEnumerable<DistrictVieModel> GetTopDiscritesByAveragePrice(int count=10);
     IEnumerable<DistrictVieModel> GetTopDiscritesByNumberOfProperties(int count=10);
    }
}
