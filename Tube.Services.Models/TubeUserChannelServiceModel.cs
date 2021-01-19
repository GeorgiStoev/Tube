using System;
using System.Collections.Generic;
using System.Text;

namespace Tube.Services.Models
{
    public class TubeUserChannelServiceModel
    {
        public string TubeUserId { get; set; }
        public UserServiceModel TubeUser { get; set; }

        public string ChannelId { get; set; }
        public ChannelServiceModel Channel { get; set; }
    }
}
