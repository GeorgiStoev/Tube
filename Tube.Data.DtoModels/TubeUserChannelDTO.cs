using System;
using System.Collections.Generic;
using System.Text;

namespace Tube.Data.DtoModels
{
    public class TubeUserChannelDTO
    {
        public string TubeUserId { get; set; }
        public virtual TubeUserDTO TubeUser { get; set; }

        public string ChannelId { get; set; }
        public virtual ChannelDTO Channel { get; set; }
    }
}
