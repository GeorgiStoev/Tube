using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Tube.Data;
using Tube.Data.DtoModels;
using Tube.Data.Models;
using Tube.Services;
using Tube.Web.MappingConfiguration;
using Xunit;

namespace Tube.Test.Service
{
    public class CommentServiceTests
    {
        [Fact]
        public void TestGetCommentsByVideoId_ShouldReturnCorrectComments()
        {
            var options = new DbContextOptionsBuilder<TubeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new TubeDbContext(options);
            SeedData(context);

            var commentService = this.GetCommentService(context);
            var videoId = "1";

            var comments = commentService.GetDomainVideoCommentsByVideoId(videoId);

            Assert.True(comments.Count == 1);
        }

        [Fact]
        public void TestGetCommentById_ShouldReturnCorrectComment()
        {
            var options = new DbContextOptionsBuilder<TubeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new TubeDbContext(options);
            SeedData(context);

            var commentService = this.GetCommentService(context);

            var expectedComment = Comments[1];
            var actualComment = commentService.GetCommentById(expectedComment.Id);

            Assert.True(expectedComment.Id == actualComment.Id);
        }

        private CommentService GetCommentService(TubeDbContext context)
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TubeProfile());
            });
            var mapper = mockMapper.CreateMapper();

            var userService = new UserService(context, mapper);
            var channelService = new ChannelService(context, userService, mapper);
            var voteService = new VoteService(context, mapper);
            var commentService = new CommentService(context, voteService, mapper);

            return commentService;
        }

        private void SeedData(TubeDbContext context)
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TubeProfile());
            });
            var mapper = mockMapper.CreateMapper();

            var userForDB = mapper.Map<TubeUser>(User);
            var videoForDB = mapper.Map<Video>(Video);
            var video2ForDB = mapper.Map<Video>(Video2);
            var commentsForDB = mapper.Map<List<Comment>>(Comments);

            context.Users.Add(userForDB);
            context.Videos.Add(videoForDB);
            context.Videos.Add(video2ForDB);
            context.Comments.AddRange(commentsForDB);

            context.SaveChanges();
        }

        private VideoDTO Video = new VideoDTO
        {
            Id = "1",
            Name = "VideoTest",
            ChannelId = "1",
            Category = "Teeest"
        };

        private VideoDTO Video2 = new VideoDTO
        {
            Id = "2",
            Name = "VideoTest",
            ChannelId = "1",
            Category = "Teeest"
        };

        private TubeUserDTO User = new TubeUserDTO
        {
            Id = "1",
            UserName = "TestUser",
            FirstName = "Georgi",
            LastName = "Stoev"
        };

        private List<CommentDTO> Comments = new List<CommentDTO>
        {
            new CommentDTO
            {
                Id = "1",
                Text = "Test...Test...Test...",
                VideoId = "1"
            },
            new CommentDTO
            {
                Id = "2",
                Text = "Test...Test...Test...Test...Test...",
                VideoId = "2"
            }
        };
    }
}
