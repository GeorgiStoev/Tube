using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Tube.Data;
using Tube.Data.DtoModels;
using Tube.Data.Models;

namespace Tube.Services
{
    public class CommentService : ICommentService
    {
        private readonly TubeDbContext context;
        private readonly IVoteService voteService;
        private readonly IMapper mapper;

        public CommentService(TubeDbContext context, IVoteService voteService, IMapper mapper)
        {
            this.context = context;
            this.voteService = voteService;
            this.mapper = mapper;
        }

        public void CreateComment(string text, string username, string videoId, string channelPicUrl)
        {
            var comment = new Comment
            {
                Text = text,
                TubeUserName = username,
                ChannelPicUrl = channelPicUrl,
                VideoId = videoId,
                Likes = new List<Vote>(),
                Date = DateTime.UtcNow
            };

            this.context.Comments.Add(comment);
            this.context.SaveChanges();

            var commentDto = this.mapper.Map<CommentDTO>(comment);
            this.DecreaseVideoViews(commentDto);
        }

        public CommentDTO GetCommentById(string id)
        {
            var comment = this.context
                              .Comments.AsNoTracking()
                              .FirstOrDefault(c => c.Id == id);

            return this.mapper.Map<CommentDTO>(comment);
        }

        public List<Comment> GetDomainVideoCommentsByVideoId(string videoId)
        {
            var comments = this.context.Comments.Where(comment => comment.VideoId == videoId).ToList();

            foreach (var comment in comments)
            {
                comment.Likes = this.voteService.GetDomainCommentVotesByCommenId(comment.Id);
            }

            return comments;
        }

        public CommentDTO Like(CommentDTO comment, string username)
        {
            var isVoted = this.voteService.IsVotedComment(comment, username);

            if (!isVoted)
            {
                var vote = this.voteService.CreateForComment(comment, username);
                comment.Likes.Add(vote);
            }

            this.DecreaseVideoViews(comment);

            var commentFromDB = this.context.Comments.FirstOrDefault(c => c.Id == comment.Id);

            this.context.Update(commentFromDB);
            this.context.SaveChanges();

            var commentDto = this.mapper.Map<CommentDTO>(comment);
            return commentDto;
        }

        public void UnLike(CommentDTO comment, string username)
        {
            var isVoted = this.voteService.IsVotedComment(comment, username);

            if (isVoted)
            {
                this.voteService.UnVoteComment(comment, username);
            }

            this.DecreaseVideoViews(comment);
        }

        private void DecreaseVideoViews(CommentDTO comment)
        {
            var video = this.context.Videos.AsNoTracking().FirstOrDefault(v => v.Id == comment.VideoId);
            video.Views--;

            this.context.Update(video);
            this.context.SaveChanges();
        }
    }
}
