using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tube.Common;

namespace Tube.Data.Models
{
    public class Playlist
    {
        public Playlist()
        {
            this.Videos = new List<VideoPlaylist>();
        }

        public string Id { get; set; }

        public string TubeUserId { get; set; }
        public virtual TubeUser TubeUser { get; set; }

        public string Name { get; set; }

        public virtual ICollection<VideoPlaylist> Videos { get; set; }
    }
}
