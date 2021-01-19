using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tube.Common;

namespace Tube.Data.Models
{
    public class TubeUser : IdentityUser
    {
        public TubeUser()
        {
            this.Subscriptions = new List<TubeUserChannel>();
            this.History = new List<History>();
            this.Playlist = new List<Playlist>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual Channel Channel { get; set; }

        public virtual ICollection<TubeUserChannel> Subscriptions { get; set; }

        public virtual ICollection<History> History { get; set; }

        public virtual ICollection<Playlist> Playlist { get; set; }
    }
}
