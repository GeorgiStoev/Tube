using System;
using System.Collections.Generic;
using Tube.Data.Models;

namespace Tube.Web.ViewModels.Video
{
    public class VideoViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public TubeUser TubeUser { get; set; }

        public string Category { get; set; }

        public List<Tube.Data.Models.Comment> Comments { get; set; }

        public List<Vote> Likes { get; set; }

        public int Views { get; set; }

        public string VideoUrl { get; set; }

        public DateTime Date { get; set; }
    }
}
