using System;
using System.Collections.Generic;
using System.Text;

namespace Tube.Services.Models
{
    public class VideoPlaylistServiceModel
    {
        public string VideoId { get; set; }
        public VideoServiceModel Video { get; set; }

        public string PlaylistId { get; set; }
        public PlaylistServiceModel Playlist { get; set; }
    }
}
