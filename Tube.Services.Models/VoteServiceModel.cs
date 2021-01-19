using System;
using System.Collections.Generic;
using System.Text;

namespace Tube.Services.Models
{
    public class VoteServiceModel
    {
        public string Id { get; set; }

        public string TubeUserName { get; set; }

        public string VideoId { get; set; }
        public VideoServiceModel Video { get; set; }

        public string CommentId { get; set; }
        public CommentServiceModel Comment { get; set; }
    }
}
//