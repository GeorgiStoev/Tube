using System;
using System.Collections.Generic;
using System.Text;

namespace Tube.Services.Models
{
    public class VideoServiceModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public List<VideoServiceModel> Playlists { get; set; }

        public string Category { get; set; }

        public string ChannelId { get; set; }
        public ChannelServiceModel Channel { get; set; }

        public List<CommentServiceModel> Comments { get; set; }

        public List<VoteServiceModel> Likes { get; set; }

        public int Views { get; set; }

        public string VideoUrl { get; set; }

        public DateTime Date { get; set; }
    }
}
