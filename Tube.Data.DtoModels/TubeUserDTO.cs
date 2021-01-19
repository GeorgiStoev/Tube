using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tube.Data.DtoModels
{
    public class TubeUserDTO : IdentityUser
    {
        public TubeUserDTO()
        {
            this.Subscriptions = new List<TubeUserChannelDTO>();
            this.History = new List<HistoryDTO>();
            this.Playlist = new List<PlaylistDTO>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ChannelDTO Channel { get; set; }

        public virtual ICollection<TubeUserChannelDTO> Subscriptions { get; set; }

        public virtual ICollection<HistoryDTO> History { get; set; }

        public virtual ICollection<PlaylistDTO> Playlist { get; set; }
    }
}
