using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace RealEstates.Models
{
    public class RealEstateProperty
    {
        public RealEstateProperty()
        {
            this.Tags=new HashSet<RealEstatePropertyTag>();
        }
        public int Id { get; set; }
        
        public int Size {  get; set; }
        public int? Floor { get; set; }
        public int? TotalNumbersOfFloors { get; set; }
        public int DistrictId { get; set; }
        public virtual District District { get; set; }
        public int? Year { get; set; }
        public int TypeOfBuildingId { get; set; }
        public virtual TypeOfBuilding TypeOfBuilding { get; set; }

        public int PropertyTypeId { get; set; }
        public virtual PropertyType PropertyType {  get; set; }
        public int Price { get; set; }
        public virtual ICollection<RealEstatePropertyTag> Tags { get; set; }

    }
}
