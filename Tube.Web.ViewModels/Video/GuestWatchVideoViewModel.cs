using System;

namespace Tube.Web.ViewModels.Video
{
    public class GuestWatchVideoViewModel
    {
        public string Name { get; set; }

        public int Comments { get; set; }

        public int Likes { get; set; }

        public int Views { get; set; }

        public string VideoUrl { get; set; }

        public DateTime Date { get; set; }
    }
}
