using System;
using System.Collections.Generic;
using System.Text;

namespace Tube.Data.DtoModels
{
    public class VideoDTO
    {
        public VideoDTO()
        {
            this.Playlists = new List<VideoPlaylistDTO>();
            this.Comments = new List<CommentDTO>();
            this.Likes = new List<VoteDTO>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<VideoPlaylistDTO> Playlists { get; set; }

        public string Category { get; set; }

        public string ChannelId { get; set; }
        public virtual ChannelDTO Channel { get; set; }

        public virtual ICollection<CommentDTO> Comments { get; set; }

        public virtual ICollection<VoteDTO> Likes { get; set; }

        public int Views { get; set; }

        public string VideoUrl { get; set; }

        public DateTime Date { get; set; }
    }
}
