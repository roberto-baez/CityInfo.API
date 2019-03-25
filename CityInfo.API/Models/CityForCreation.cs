using System.ComponentModel.DataAnnotations;
using FluentValidation;

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

    

    public class CityValidator : AbstractValidator<CityForCreation>
    {
        public CityValidator()
        {
            RuleFor(city => city.Name).NotNull().Length(0,50).WithMessage("POI Name is a required Field and is max length is 50");
            RuleFor(city => city.Description).Length(0, 200).WithMessage("POI Descriptions max length is 200");
        }
    }
}
