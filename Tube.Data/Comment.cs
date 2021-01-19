using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tube.Common;

namespace Tube.Data.Models
{
    public class Comment
    {
        public Comment()
        {
            this.Likes = new List<Vote>();
        }

        public string Id { get; set; }

        public string Text { get; set; }

        public string TubeUserName { get; set; }

        public string ChannelPicUrl { get; set; }

        public string VideoId { get; set; }
        public virtual Video Video { get; set; }

        public virtual ICollection<Vote> Likes { get; set; }

        public DateTime Date { get; set; }
    }
}
