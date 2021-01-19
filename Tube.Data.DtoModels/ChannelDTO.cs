using System;
using System.Collections.Generic;
using System.Text;

namespace Tube.Data.DtoModels
{
    public class ChannelDTO
    {
        public ChannelDTO()
        {
            this.Subscribes = new List<TubeUserChannelDTO>();
            this.Videos = new List<VideoDTO>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ChannelPicUrl { get; set; }

        public string TubeUserId { get; set; }
        public virtual TubeUserDTO TubeUser { get; set; }

        public virtual ICollection<TubeUserChannelDTO> Subscribes { get; set; }

        public virtual ICollection<VideoDTO> Videos { get; set; }
    }
}
