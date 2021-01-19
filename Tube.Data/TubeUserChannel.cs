using System.ComponentModel.DataAnnotations;

namespace Tube.Data.Models
{

    public class TubeUserChannel
    {
        public string TubeUserId { get; set; }
        public virtual TubeUser TubeUser { get; set; }

        public string ChannelId { get; set; }
        public virtual Channel Channel { get; set; }
    }
}
