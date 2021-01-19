using System;
using System.Collections.Generic;
using System.Text;

namespace Tube.Data.DtoModels
{
    public class VoteDTO
    {
        public string Id { get; set; }

        public string TubeUserName { get; set; }

        public string VideoId { get; set; }
        public virtual VideoDTO Video { get; set; }

        public string CommentId { get; set; }
        public virtual CommentDTO Comment { get; set; }
    }
}
