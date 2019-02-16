using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace CityInfo.API.Models
{
    public class PointOfIterestForCreationDto
    {
        [Required(ErrorMessage = "POI Name is a required Field")]
       [MaxLength(50, ErrorMessage = "Max length exceeded")]
        public string Name { get; set; }
        [MaxLength(200,ErrorMessage = "Max length exceeded")]
        public string Description { get; set; }
    }

    public class PointOfIterestValidator : AbstractValidator<PointOfIterestForCreationDto>
    {

        public PointOfIterestValidator()
        {
            RuleFor(pointofinterrst => pointofinterrst.Name).NotNull().MaximumLength(50).WithMessage("Please ensure that POI Name is filled and is less than 50 character");
            RuleFor(pointofinterrst => pointofinterrst.Description.Length).LessThan(200).WithMessage("Please ensure that POI Description is less than 200 character");
        }

        
    }

    
}