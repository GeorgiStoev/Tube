using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Tube.Data;
using Tube.Data.DtoModels;
using Tube.Data.Models;

namespace Tube.Services
{
    public class VoteService : IVoteService
    {
        private readonly TubeDbContext context;
        private readonly IMapper mapper;

        public VoteService(TubeDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public VoteDTO Create(VideoDTO video, string username)
        {
            var vote = new Vote
            {
                VideoId = video.Id,
                TubeUserName = username
            };

            this.context.Votes.Add(vote);
            this.context.SaveChanges();

            var voteDto = this.mapper.Map<VoteDTO>(vote);
            return voteDto;
        }

        public VoteDTO CreateForComment(CommentDTO comment, string username)
        {
            var vote = new Vote
            {
                CommentId = comment.Id,
                TubeUserName = username
            };

            this.context.Votes.Add(vote);
            this.context.SaveChanges();

            var voteDto = this.mapper.Map<VoteDTO>(vote);
            return voteDto;
        }

        public Vote GetDomainCommentVote(CommentDTO comment, string username)
        {
            var vote = this.context.Votes.SingleOrDefault(like => like.TubeUserName == username && like.CommentId == comment.Id);

            return vote;
        }

        public List<Vote> GetDomainCommentVotesByCommenId(string commentId)
        {
            var votes = this.context.Votes.Where(vote => vote.CommentId == commentId).ToList();

            return votes;
        }

        public VoteDTO GetVote(VideoDTO video, string username)
        {
            var vote = this.context.Votes.SingleOrDefault(like => like.TubeUserName == username && like.VideoId == video.Id);

            var voteDto = this.mapper.Map<VoteDTO>(vote);
            return voteDto; ;
        }

        public List<Vote> GteDomainVideoVotesByVideoId(string videoId)
        {
            var votes = this.context.Votes.Where(vote => vote.VideoId == videoId).ToList();

            return votes;
        }

        public bool IsVoted(VideoDTO video, string username)
        {
            var vote = this.context.Votes.SingleOrDefault(like => like.TubeUserName == username && like.VideoId == video.Id);

            if (vote == null)
            {
                return false;
            }

            return true;
        }

        public bool IsVotedComment(CommentDTO comment, string username)
        {
            var vote = this.context.Votes.SingleOrDefault(like => like.TubeUserName == username && like.CommentId == comment.Id);

            if (vote == null)
            {
                return false;
            }

            return true;
        }

        public void UnVote(VideoDTO video, string username)
        {
            var vote = this.GetVote(video, username);
            var votes = this.context.Votes.ToList();
            var videoFromDB = this.context.Videos.FirstOrDefault(v => v.Id == video.Id);

            if (votes != null && vote != null)
            {
                var voteFromDB = this.context.Votes.FirstOrDefault(v => v.Id == vote.Id);
                videoFromDB.Likes.Remove(voteFromDB);
                this.context.Votes.Remove(voteFromDB);
            }

            videoFromDB.Views--;

            this.context.Update(videoFromDB);
            this.context.SaveChanges();
        }

        public void UnVoteComment(CommentDTO comment, string username)
        {
            var voteFromDB = this.GetDomainCommentVote(comment, username);
            var votes = this.context.Votes.ToList();
            var commentFromDB = this.context.Comments.FirstOrDefault(c => c.Id == comment.Id);

            if (votes != null && voteFromDB != null)
            {
                commentFromDB.Likes.Remove(voteFromDB);
                this.context.Votes.Remove(voteFromDB);
            }

            this.context.SaveChanges();
        }
    }
}
