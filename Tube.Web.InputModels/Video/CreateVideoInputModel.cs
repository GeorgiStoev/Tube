using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Tube.Common;

namespace Tube.Web.InputModels.Video
{
    public class CreateVideoInputModel
    {
        [Required]
        [StringLength(ValidationConstants.videoNameMaximumLength, MinimumLength = ValidationConstants.videoNameMinimumLength, ErrorMessage = ValidationConstants.videoNameErrorMessage)]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public IFormFile VideoUrl { get; set; }
    }
}
 