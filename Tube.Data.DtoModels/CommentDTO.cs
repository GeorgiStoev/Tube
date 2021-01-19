using System;
using System.Collections.Generic;
using System.Text;

namespace Tube.Data.DtoModels
{
    public class CommentDTO
    {
        public CommentDTO()
        {
            this.Likes = new List<VoteDTO>();
        }

        public string Id { get; set; }

        public string Text { get; set; }

        public string TubeUserName { get; set; }

        public string ChannelPicUrl { get; set; }

        public string VideoId { get; set; }
        public virtual VideoDTO Video { get; set; }

        public virtual ICollection<VoteDTO> Likes { get; set; }

        public DateTime Date { get; set; }
    }
}
