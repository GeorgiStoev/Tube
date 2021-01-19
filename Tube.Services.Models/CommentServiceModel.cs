using System;
using System.Collections.Generic;
using System.Text;

namespace Tube.Services.Models
{
    public class CommentServiceModel
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public string TubeUserName { get; set; }

        public string ChannelPicUrl { get; set; }

        public string VideoId { get; set; }
        public VideoServiceModel Video { get; set; }

        public List<VoteServiceModel> Likes { get; set; }

        public DateTime Date { get; set; }
    }
}
