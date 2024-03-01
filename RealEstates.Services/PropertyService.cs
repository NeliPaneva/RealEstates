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
    public class PropertyService:IPropertiesService
    {
        private RealEstateDbContext db;

        public PropertyService(RealEstateDbContext db)
        {
            this.db = db;
        }

        public void Create(string destrict,  int size, int? year, int price, string propertyType, string buildingType, int? floor, int? maxFloors)
        {
            if (destrict == null) return;
            var property = new RealEstateProperty
            {
                Size = size,
                Price = price,
                Year = year,
                Floor = floor,
                TotalNumbersOfFloors = maxFloors
            };
            if (property.Year < 1800)
            {
                property.Year = null;
            }
            if (property.Floor < 0)
            {
                property.Floor = null;
            }
            if (property.TotalNumbersOfFloors < 0)
            {
                property.TotalNumbersOfFloors = null;
            }

            //District
            var districtEntity = db.Districtss.FirstOrDefault(x => x.Name.Trim() == destrict.Trim());
            if (districtEntity == null)
            {
                districtEntity = new District  {  Name = destrict };
            }
            property.District = districtEntity;

            //BuildingType
            var buildingTypeEntity = db.BuildingsTypes.FirstOrDefault(x => x.Name.Trim() == buildingType.Trim());
            if (buildingTypeEntity == null)
            {
                buildingTypeEntity = new TypeOfBuilding { Name = buildingType };

            }
            property.TypeOfBuilding= buildingTypeEntity;

            //Property Type
            var propertyTypeEntity = this.db.PropertyTypes.FirstOrDefault(x => x.Name.Trim() == propertyType.Trim());
            //ако не го намерим в базата си го правим
            if (propertyTypeEntity == null)
            {
                propertyTypeEntity = new PropertyType { Name = propertyType };

            }
            property.PropertyType = propertyTypeEntity;

            
            this.db.RealEstateProperties.Add(property);
            this.db.SaveChanges();
            this.UpdateTags(property.Id);
        }

        public IEnumerable<PropertyViewModel> Search(int minYear, int maxYear, int minSize, int maxSize)
        {
            return db.RealEstateProperties
                 .Where(x => x.Year >= minYear && x.Year <= maxYear && x.Size >= minSize && x.Size <= maxSize)
                 .Select(MapToPropertyViewModel())
                 .OrderBy(x => x.Price)
                 .ToList();
        }

        private static Expression<Func<RealEstateProperty, PropertyViewModel>> MapToPropertyViewModel()
        {
            return x => new PropertyViewModel
            {
                Price = x.Price,
                Floor = (x.Floor ?? 0).ToString() + "/" + (x.TotalNumbersOfFloors ?? 0).ToString(),
                Size = x.Size,
                Year = x.Year,
                BuildingType = x.TypeOfBuilding.Name,
                District = x.District.Name,
                PropertyType = x.PropertyType.Name,
            };
        }

        public IEnumerable<PropertyViewModel> SearchByPrice(int minPrice, int maxPrice)
        {
            return this.db.RealEstateProperties
                .Where(x => x.Price >= minPrice && x.Price <= maxPrice)
               .Select(MapToPropertyViewModel())
                .OrderBy(x => x.Price)
                .ToList();
            
        }

        public void UpdateTags(int propertyId)
        {
            var property = this.db.RealEstateProperties.FirstOrDefault(x => x.Id == propertyId);
            property.Tags.Clear();
            if (property.Year.HasValue && property.Year < 1990)
            {
                property.Tags.Add(
                    new RealEstatePropertyTag 
                    { 
                        Tag = this.GetOrCreateTag("OldBuilding")
                    });
            }
            if (property.Size>120)
            {
                property.Tags.Add(
                    new RealEstatePropertyTag
                    {
                        Tag = this.GetOrCreateTag("HugeApartment")
                    });
            }
            if (property.Year>2018 &&property.TotalNumbersOfFloors>5)
            {
                property.Tags.Add(
                    new RealEstatePropertyTag
                    {
                        Tag = this.GetOrCreateTag("HasParking")
                    });
            }
            if (property.Floor==property.TotalNumbersOfFloors)
            {
                property.Tags.Add(
                    new RealEstatePropertyTag
                    {
                        Tag = this.GetOrCreateTag("LastFloor")
                    });
            }
            if (((double)property.Price/property.Size)<800)
            {
                property.Tags.Add(
                    new RealEstatePropertyTag
                    {
                        Tag = this.GetOrCreateTag("CheaperApartment")
                    });
            }
            if (((double)property.Price / property.Size) >2000)
            {
                property.Tags.Add(
                    new RealEstatePropertyTag
                    {
                        Tag = this.GetOrCreateTag("ExpensiveApartment")
                    });
            }
            this.db.SaveChanges();
        }
        private Tag GetOrCreateTag(string tag)
        {
            var tagEntity = this.db.Tags.FirstOrDefault(x => x.Name.Trim() == tag.Trim());
            if (tagEntity == null) 
            {
                tagEntity=new Tag { Name=tag }; 
            }
            return tagEntity;
        }
    }
}
