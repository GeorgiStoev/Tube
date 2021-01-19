using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tube.Common;

namespace Tube.Data.Models
{
    public class Channel
    {
        public Channel()
        {
            this.Subscribes = new List<TubeUserChannel>();
            this.Videos = new List<Video>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ChannelPicUrl { get; set; }

        public string TubeUserId { get; set; }
        public virtual TubeUser TubeUser { get; set; }

        public virtual ICollection<TubeUserChannel> Subscribes { get; set; }

        public virtual ICollection<Video> Videos { get; set; }
    }
}
