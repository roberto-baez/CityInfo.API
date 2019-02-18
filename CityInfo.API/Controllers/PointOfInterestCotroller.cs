using CityInfo.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class PointOfInterestCotroller : Controller
    {

        private  ILogger<PointOfInterestCotroller> _logger;
        public PointOfInterestCotroller(ILogger<PointOfInterestCotroller> logger)
        {
            _logger = logger;
           
        }

        [HttpGet("{cityId}/pointofinterest")]
        public IActionResult GetPointsOfInterest(int cityId) {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                _logger.LogCritical($"The Given cityid #{cityId} was not found");
                
                return NotFound();
            }

            return Ok(city.PointOfInterest);
        }

        [HttpGet("{Cityid}/pointofinterest/{Id}", Name = "GetPointOfinterest")]
        public IActionResult GetPointOfInterest(int cityId, int id)
        {
            var result = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            var Poi = result.PointOfInterest.FirstOrDefault(p => p.Id == id);
            if (result == null || Poi == null)
            {
                return NotFound();
            }

            return Ok(Poi);
        }

        [HttpPost("{cityId}/pointofinterest")]
        public IActionResult CreatePointOfIterest(int cityId, [FromBody] PointOfIterestForCreationDto pointOfInterest) {


            if (pointOfInterest == null ||(!ModelState.IsValid))
            {
                return BadRequest(ModelState);
            } 

                var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var MaxPoi = CitiesDataStore.Current.Cities.SelectMany(c => c.PointOfInterest).Max(p => p.Id);

            var FinalPointOfInterest = new PointOfInterestDto
            {
                Id = ++MaxPoi,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointOfInterest.Add(FinalPointOfInterest);

            return CreatedAtRoute("GetPointOfinterest",
                new { Cityid = cityId, Id = FinalPointOfInterest.Id }, FinalPointOfInterest);
        }


        [HttpPut("{cityId}/pointofinterest/{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int id,  [FromBody] PointOfIterestForCreationDto pointOfInterest) {

            if (pointOfInterest == null || (!ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            var Poi = city.PointOfInterest.FirstOrDefault(p => p.Id == id);
            if (city == null || Poi == null)
            {
                return NotFound();
            }

            Poi.Name = pointOfInterest.Name;
            Poi.Description = pointOfInterest.Description;

            return NoContent();
        }


        [HttpPatch("{cityId}/pointofinterest/{id}")]
        public IActionResult PartiallyUpdatePointOfInterest(int cityId, int id, [FromBody] JsonPatchDocument<PointOfIterestForCreationDto> patchdoc) {

            if (patchdoc == null) {
                return BadRequest();
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            var Poi = city.PointOfInterest.FirstOrDefault(p => p.Id == id);

            if (city == null || Poi == null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch = new PointOfIterestForCreationDto()
            {

                Name = Poi.Name,
                Description = Poi.Description
            };

            patchdoc.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Poi.Name = pointOfInterestToPatch.Name;
            Poi.Description = pointOfInterestToPatch.Description;

            return NoContent();
        }


        [HttpDelete("{cityId}/pointofinterest/{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id) {

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            var Poi = city.PointOfInterest.FirstOrDefault(p => p.Id == id);

            if (city == null || Poi == null)
            {
                return NotFound();
            }

            city.PointOfInterest.Remove(Poi);

            return NoContent();
        }

    }
}
