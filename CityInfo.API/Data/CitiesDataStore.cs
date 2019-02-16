using CityInfo.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public static CitiesDataStore current { get; } = new CitiesDataStore();
        public List<CityDto> Cities { get; set; }

        public CitiesDataStore()
        {
            Cities = new List<CityDto>(){

                new CityDto() {
                    Id = 1,
                    Name = "Santo Domingo",
                    Description = "this is a test",
                    PointOfInterest =  {
                        new PointOfInterestDto() {
                            Id = 1,
                            Name = "Megacentro",
                            Description = "Este es un POI de prueba"
                        },

                        new PointOfInterestDto() {
                            Id = 2,
                            Name = "Corall  Mall",
                             Description = "this is a test description"
                        }


                    }
            },

                new CityDto() {
                Id = 2,
                Name = "Puerto Plata",
                Description = "this is a test description",
                PointOfInterest =  {
                        new PointOfInterestDto() {
                            Id = 3,
                            Name = "Hotel Bavaro",
                            Description = "Este es un POI de prueba"
                        },

                        new PointOfInterestDto() {
                            Id = 4,
                            Name = "Hotel Bavaro 2",
                             Description = "this is a test description"
                        }

                    }
                }
            };
        }

        
    }
}
