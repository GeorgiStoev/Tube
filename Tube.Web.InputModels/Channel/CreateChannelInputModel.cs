using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Tube.Common;

namespace Tube.Web.InputModels.Channel
{
    public class CreateChannelInputModel
    {
        [Required]
        [StringLength(ValidationConstants.commentTextMaximumLength, MinimumLength = ValidationConstants.commentTextMinimumLength, ErrorMessage = ValidationConstants.commentTextErrorMessage)]
        public string Name { get; set; }

        [Required]
        [StringLength(ValidationConstants.commentTextMaximumLength, MinimumLength = ValidationConstants.commentTextMinimumLength, ErrorMessage = ValidationConstants.commentTextErrorMessage)]
        public string Description { get; set; }

        [Required]
        public IFormFile ChannelPicUrl { get; set; }
    }
}
 