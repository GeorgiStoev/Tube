using System.ComponentModel.DataAnnotations;
using Tube.Common;

namespace Tube.Web.InputModels.Comment
{
    public class CreateCommentInputModel
    {
        [Required(ErrorMessage = ValidationConstants.commentTextErrorMessage)]
        [StringLength(ValidationConstants.commentTextMaximumLength, MinimumLength = ValidationConstants.commentTextMinimumLength, ErrorMessage = ValidationConstants.commentTextErrorMessage)]
        public string Text { get; set; }
    }
}
