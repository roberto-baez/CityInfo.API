using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Models
{
    public class CityForCreation
    {
        [Required(ErrorMessage = "POI Name is a required Field")]
        [MaxLength(50, ErrorMessage = "Max length exceeded")]
        public string Name { get; set; }
        [MaxLength(200, ErrorMessage = "Max length exceeded")]
        public string Description { get; set; }
    }
}
