using Tube.Data.Models;

namespace Tube.Web.ViewModels.Channel
{
    public class SubscribersViewModel
    {
        public string TubeUserId { get; set; }
        public TubeUser TubeUser { get; set; }

        public string ChannelId { get; set; }
        public Tube.Data.Models.Channel Channel { get; set; }
    }
}
