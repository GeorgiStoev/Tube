using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tube.Common;

namespace Tube.Data.Models
{
    public class Video
    {
        public Video()
        {
            this.Playlists = new List<VideoPlaylist>();
            this.Comments = new List<Comment>();
            this.Likes = new List<Vote>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<VideoPlaylist> Playlists { get; set; }

        public string Category { get; set; }

        public string ChannelId { get; set; }
        public virtual Channel Channel { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Vote> Likes { get; set; }

        public int Views { get; set; }

        public string VideoUrl { get; set; }

        public DateTime Date { get; set; }
    }
}
