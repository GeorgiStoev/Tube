using System;
using System.Collections.Generic;
using System.Text;

namespace Tube.Data.DtoModels
{
    public class VideoPlaylistDTO
    {
        public string VideoId { get; set; }
        public virtual VideoDTO Video { get; set; }

        public string PlaylistId { get; set; }
        public virtual PlaylistDTO Playlist { get; set; }
    }
}
