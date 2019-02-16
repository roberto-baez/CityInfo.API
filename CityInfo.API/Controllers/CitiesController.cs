using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        [HttpGet]
        public ActionResult GetCities() {
            var result = CitiesDataStore.current.Cities;
            return Ok(result); ;

        }

        [HttpGet("{id}", Name = "GetCity")]
        public ActionResult GetCity(int id) {
            var result = CitiesDataStore.current.Cities.FirstOrDefault(c => c.Id == id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost()]
        public IActionResult CreateCity([FromBody]  CityForCreation city)
        {
            if (city == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var MaxCityId = CitiesDataStore.current.Cities.Select(c => c.Id).Max();

            var FinalCity = new CityDto
            {
                Id = ++MaxCityId,
                Name = city.Name,
                Description = city.Description
            };

            CitiesDataStore.current.Cities.Add(FinalCity);


            return CreatedAtRoute("GetCity", new { id = FinalCity.Id }, FinalCity);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateCity(int cityid, [FromBody]  CityForCreation city)
        {
         
            var  result = CitiesDataStore.current.Cities.FirstOrDefault(c => c.Id == cityid);

            if (city == null || !ModelState.IsValid || result == null)
            {
                return BadRequest(ModelState);
            }

            result.Name = city.Name;
            result.Description = city.Description;

            return NoContent();
        }


        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateCity(int cityId,[FromBody] JsonPatchDocument<CityForCreation> patchdoc) {

            var city = CitiesDataStore.current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (patchdoc == null || city == null)
            {
                return BadRequest();
            }

            var cityToPatch = new CityForCreation()
            {
                Name = city.Name,
                Description = city.Description
            };

            patchdoc.ApplyTo(cityToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            city.Name = cityToPatch.Name;
            city.Description = cityToPatch.Description;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCity(int id) {
            var city = CitiesDataStore.current.Cities.FirstOrDefault(c => c.Id == id);

            if (city == null)
            {
                return NotFound();
            }

            CitiesDataStore.current.Cities.Remove(city);
            return NoContent();

            
        }

    }
}
