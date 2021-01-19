using System;
using System.Collections.Generic;
using System.Text;

namespace Tube.Services.Models
{
    public class PlaylistServiceModel
    {
        public string Id { get; set; }

        public string TubeUserId { get; set; }
        public UserServiceModel TubeUser { get; set; }

        public string Name { get; set; }

        public List<VideoPlaylistServiceModel> Videos { get; set; }
    }
}
