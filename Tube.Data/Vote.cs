using System.ComponentModel.DataAnnotations;

namespace Tube.Data.Models
{
    public class Vote
    {
        public string Id { get; set; }

        public string TubeUserName { get; set; }

        public string VideoId { get; set; }
        public virtual Video Video { get; set; }

        public string CommentId { get; set; }
        public virtual Comment Comment { get; set; }
    }
}
