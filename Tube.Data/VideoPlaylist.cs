using System.ComponentModel.DataAnnotations;

namespace Tube.Data.Models
{
    public class VideoPlaylist
    {
        public string VideoId { get; set; }
        public virtual Video Video { get; set; }

        public string PlaylistId { get; set; }
        public virtual Playlist Playlist { get; set; }
    }
}
