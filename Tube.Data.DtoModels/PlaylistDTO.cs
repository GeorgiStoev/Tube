using System;
using System.Collections.Generic;
using System.Text;

namespace Tube.Data.DtoModels
{
    public class PlaylistDTO
    {
        public PlaylistDTO()
        {
            this.Videos = new List<VideoPlaylistDTO>();
        }

        public string Id { get; set; }

        public string TubeUserId { get; set; }
        public virtual TubeUserDTO TubeUser { get; set; }

        public string Name { get; set; }

        public ICollection<VideoPlaylistDTO> Videos { get; set; }
    }
}
