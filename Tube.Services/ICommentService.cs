using System.Collections.Generic;
using Tube.Data.DtoModels;
using Tube.Data.Models;

namespace Tube.Services
{
    public interface ICommentService
    {
        void CreateComment(string text, string username, string videoId, string channelPicUrl);

        List<Comment> GetDomainVideoCommentsByVideoId(string videoId);

        CommentDTO GetCommentById(string id);

        CommentDTO Like(CommentDTO comment, string username);

        void UnLike(CommentDTO comment, string username);
    }
}
