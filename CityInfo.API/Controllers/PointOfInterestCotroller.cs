﻿using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class PointOfInterestCotroller : Controller
    {
        [HttpGet("{cityId}/pointofinterest")]
        public IActionResult GetPointsOfInterest(int cityId) {
            var city = CitiesDataStore.current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            return Ok(city.PointOfInterest);
        }

        [HttpGet("{Cityid}/pointofinterest/{Id}", Name = "GetPointOfinterest")]
        public IActionResult GetPointOfInterest(int cityId, int id)
        {
            var result = CitiesDataStore.current.Cities.FirstOrDefault(c => c.Id == cityId);
            var Poi = result.PointOfInterest.FirstOrDefault(p => p.Id == id);
            if (result == null || Poi == null)
            {
                return NotFound();
            }

            return Ok(Poi);
        }

        [HttpPost("{cityId}/pointofinterest")]
        public IActionResult CreatePointOfIterest(int cityId, [FromBody] PointOfIterestForCreationDto pointOfInterest) {

            if (pointOfInterest == null || (!ModelState.IsValid))
            {
                return BadRequest(ModelState);
            } 

                var city = CitiesDataStore.current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var MaxPoi = CitiesDataStore.current.Cities.SelectMany(c => c.PointOfInterest).Max(p => p.Id);

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

            var city = CitiesDataStore.current.Cities.FirstOrDefault(c => c.Id == cityId);
            var Poi = city.PointOfInterest.FirstOrDefault(p => p.Id == id);
            if (city == null || Poi == null)
            {
                return NotFound();
            }

            Poi.Name = pointOfInterest.Name;
            Poi.Description = pointOfInterest.Description;

            return NoContent();
        }

    }
}
