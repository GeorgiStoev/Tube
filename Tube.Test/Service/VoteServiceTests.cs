using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Tube.Data;
using Tube.Data.DtoModels;
using Tube.Data.Models;
using Tube.Services;
using Tube.Web.MappingConfiguration;
using Xunit;

namespace Tube.Test.Service
{
    public class VoteServiceTests
    {
        [Fact]
        public void TestGetCommentVotes_ShouldRetturnCorrectVotes()
        {
            var options = new DbContextOptionsBuilder<TubeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new TubeDbContext(options);
            SeedData(context);

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TubeProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var voteService = new VoteService(context, mapper);

            var commentId = "1";

            var expectedData = Comment.Likes.ToList();
            var actualData = voteService.GetDomainCommentVotesByCommenId(commentId);

            
            Assert.True(expectedData[0].CommentId == mapper.Map<List<VoteDTO>>(actualData)[0].CommentId);
        }

        [Fact]
        public void TestCreateForComment_ShouldCreateCommentVote()
        {
            var options = new DbContextOptionsBuilder<TubeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new TubeDbContext(options);
            SeedData(context);

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TubeProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var voteService = new VoteService(context, mapper);

            var vote = voteService.CreateForComment(Comment, "TestUsername");

            Assert.True(vote != null);
        }

        private void SeedData(TubeDbContext context)
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TubeProfile());
            });
            var mapper = mockMapper.CreateMapper();

            var videoForDB = mapper.Map<Video>(Video);
            var commentForDB = mapper.Map<Comment>(Comment);

            context.Videos.Add(videoForDB);
            context.Comments.Add(commentForDB);

            context.SaveChanges();
        }

        private VideoDTO Video = new VideoDTO
        {
            Id = "1",
            Name = "VideoTest",
            ChannelId = "1",
            Category = "Teeest"
        };

        private CommentDTO Comment = new CommentDTO
        {
            Id = "1",
            Text = "Test...Test...Test...",
            VideoId = "1",
            Likes = new List<VoteDTO>() { new VoteDTO { CommentId = "1" } }
        };
    }
}
