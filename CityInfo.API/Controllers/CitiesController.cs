using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpGet("{id}")]
        public ActionResult GetCity(int id) {
            var result = CitiesDataStore.current.Cities.FirstOrDefault(c => c.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            
            return Ok(result);
        }
    }
}
