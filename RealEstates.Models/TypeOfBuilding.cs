using System.ComponentModel.DataAnnotations;

namespace RealEstates.Models
{
    public class TypeOfBuilding
    {
        public TypeOfBuilding()
        {
            this.Properties = new HashSet<RealEstateProperty>();
        }

        public int Id { get; set; }
  
        public string Name { get; set; }
        public virtual ICollection<RealEstateProperty> Properties { get; set; }
    }
}