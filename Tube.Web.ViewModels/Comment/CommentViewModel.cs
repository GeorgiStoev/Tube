using System;
using System.Collections.Generic;
using Tube.Data.Models;

namespace Tube.Web.ViewModels.Comment
{
    public class CommentViewModel
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public string TubeUserName { get; set; }

        public string ChannelPicUrl { get; set; }

        public List<Vote> Likes { get; set; }

        public DateTime Date { get; set; }
    }
}
