using System;
using System.Collections.Generic;
using System.Text;
using Tube.Data.Models;
using Tube.Services.Mapping;

namespace Tube.Services.Models
{
    public class ChannelServiceModel : IMapTo<Channel>, IMapFrom<Chann>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ChannelPicUrl { get; set; }

        public string TubeUserId { get; set; }
        public UserServiceModel TubeUser { get; set; }

        public List<TubeUserChannelServiceModel> Subscribes { get; set; }

        public List<VideoServiceModel> Videos { get; set; }
    }
}
