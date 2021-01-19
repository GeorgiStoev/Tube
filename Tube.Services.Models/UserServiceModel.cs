using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Tube.Data.Models;
using Tube.Services.Mapping;

namespace Tube.Services.Models
{
    public class UserServiceModel : IdentityUser, IMapFrom<TubeUser>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ChannelServiceModel Channel { get; set; }

        public List<UserServiceModel> Subscriptions { get; set; }

        public List<HistoryServiceModel> History { get; set; }

        public List<PlaylistServiceModel> Playlist { get; set; }
    }
}
