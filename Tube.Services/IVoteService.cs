using System.Collections.Generic;
using Tube.Data.DtoModels;
using Tube.Data.Models;

namespace Tube.Services
{
    public interface IVoteService
    {

        List<Vote> GetDomainCommentVotesByCommenId(string commentId);

        List<Vote> GteDomainVideoVotesByVideoId(string videoId);

        VoteDTO Create(VideoDTO video, string username);

        VoteDTO CreateForComment(CommentDTO comment, string username);

        bool IsVoted(VideoDTO video, string username);

        bool IsVotedComment(CommentDTO comment, string username);

        void UnVote(VideoDTO video, string username);

        void UnVoteComment(CommentDTO comment, string username);

        VoteDTO GetVote(VideoDTO video, string username);

        Vote GetDomainCommentVote(CommentDTO comment, string username);
    }
}
