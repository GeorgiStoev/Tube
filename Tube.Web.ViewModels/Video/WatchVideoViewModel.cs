using System;
using System.Collections.Generic;
using Tube.Data.Models;
using Tube.Web.ViewModels.Comment;

namespace Tube.Web.ViewModels.Video
{
    public class WatchVideoViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string TubeUserName { get; set; }

        public string ChannelId { get; set; }

        public string ChannelName { get; set; }

        public string ChannelPicUrl { get; set; }

        public string Category { get; set; }

        public List<CommentViewModel> Comments { get; set; }

        public List<Vote> Likes { get; set; }

        public int Views { get; set; }

        public string VideoUrl { get; set; }

        public DateTime Date { get; set; }
    }
}
