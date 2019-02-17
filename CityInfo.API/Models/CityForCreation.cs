using System.ComponentModel.DataAnnotations;

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
