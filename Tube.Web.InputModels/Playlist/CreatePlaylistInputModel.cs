using System.ComponentModel.DataAnnotations;
using Tube.Common;

namespace Tube.Web.InputModels.Playlist
{
    public class CreatePlaylistInputModel
    {
        [Required]
        [StringLength(ValidationConstants.playlistNameMaximumLength, MinimumLength = ValidationConstants.playlistNameMinimumLength, ErrorMessage = ValidationConstants.playlistNameErrorMessage)]
        public string Name { get; set; }
    }
}
 