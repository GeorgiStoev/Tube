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
    public class VideoServiceTests
    {
        [Fact]
        public void TestAdd_ShouldCreateVideo()
        {
            var options = new DbContextOptionsBuilder<TubeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new TubeDbContext(options);
            SeedData(context);

            var videoService = this.GetVideoService(context);

            var videoName = "TestAddVideo";
            var videoCategory = "TestAddVideoCategory";
            var tubeUsername = "TestUser";

            var video  = videoService.Add(videoName, videoCategory, string.Empty, tubeUsername);

            Assert.False(video == null);
        }

        [Fact]
        public void TestGetVideosByChannelId_ShouldReturnCorrectVideos()
        {
            var options = new DbContextOptionsBuilder<TubeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new TubeDbContext(options);
            SeedData(context);

            var videoService = this.GetVideoService(context);
            var channelId = "1";
            var videos = videoService.GetVideosByChannelId(channelId);

            Assert.True(videos.Count == 2);
        }

        [Fact]
        public void TestUnLike_ShouldDecreaseVideoLikes()
        {
            var options = new DbContextOptionsBuilder<TubeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new TubeDbContext(options);
            SeedData(context);

            var videoService = this.GetVideoService(context);
            var video = Videos[0];
            var username = "TestUser";

            videoService.Like(video, username);
            videoService.UnLike(video, username);

            Assert.True(video.Likes.Count == 1);
        }

        [Fact]
        public void TestGetVideoById_ShouldReturnCorrectVideo()
        {
            var options = new DbContextOptionsBuilder<TubeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new TubeDbContext(options);
            SeedData(context);

            var videoService = this.GetVideoService(context);
            var videoId = "1";

            var expectedVideo = Videos[0];
            var actualVideo = videoService.GetVideoById(videoId);

            Assert.True(expectedVideo.Id == actualVideo.Id);
        }

        [Fact]
        public void TestGetVideoByName_ShouldReturnCorrectVideo()
        {
            var options = new DbContextOptionsBuilder<TubeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new TubeDbContext(options);
            SeedData(context);

            var videoService = this.GetVideoService(context);
            var videoName = "VideoTest";

            var expectedVideo = Videos[0];
            var actualVideo = videoService.GetVideoByName(videoName);

            Assert.True(expectedVideo.Id == actualVideo.Id);
        }

        [Fact]
        public void TestGetSearchedVideos_ShouldReturnCorrectVideos()
        {
            var options = new DbContextOptionsBuilder<TubeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new TubeDbContext(options);
            SeedData(context);

            var videoService = this.GetVideoService(context);
            var searchText = "VideoT";

            var expectedData = Videos;
            var actualData = videoService.GetSearchedVideos(searchText);

            Assert.True(expectedData[0].Id == actualData[0].Id);
            Assert.True(expectedData[1].Id == actualData[1].Id);
        }

        [Fact]
        public void TestGetAllVideos_ShouldReturnAllVideos()
        {
            var options = new DbContextOptionsBuilder<TubeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new TubeDbContext(options);
            SeedData(context);

            var videoService = this.GetVideoService(context);
            var videos = videoService.GetAllVideos();

            Assert.True(videos.Count == 2);
        }

        [Fact]
        public void TestGetMostViewedVideos_ShouldReturnCorrectVideos()
        {
            var options = new DbContextOptionsBuilder<TubeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new TubeDbContext(options);
            SeedData(context);

            var videoService = this.GetVideoService(context);
            var videos = videoService.GetMostViewedVideos();

            Assert.True(videos[0].Id == videos[0].Id);
            Assert.True(videos[1].Id == videos[1].Id);
        }

        [Fact]
        public void TestGetMostCommentVideos_ShouldReturnCorrectVideos()
        {
            var options = new DbContextOptionsBuilder<TubeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new TubeDbContext(options);
            SeedData(context);

            var videoService = this.GetVideoService(context);
            var videos = videoService.GetMostCommendVideos();

            Assert.True(videos[0].Id == Videos[1].Id);
        }

        [Fact]
        public void TestGetMostLikedVideos_ShouldReturnCorrectVideos()
        {
            var options = new DbContextOptionsBuilder<TubeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new TubeDbContext(options);
            SeedData(context);

            var videoService = this.GetVideoService(context);
            var videos = videoService.GetMostLikedVideos();

            Assert.True(videos[0].Id == Videos[0].Id);
        }

        [Fact]
        public void TestGetVideosByCategory_ShouldReturnCorrectVideos()
        {
            var options = new DbContextOptionsBuilder<TubeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new TubeDbContext(options);
            SeedData(context);

            var videoService = this.GetVideoService(context);
            var videoCategory = "Music";
            var videos = videoService.GetVideosByCategory(videoCategory);

            Assert.True(videos[0].Id == Videos[0].Id);
        }

        private VideoService GetVideoService(TubeDbContext context)
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
            var videoService = new VideoService(context, voteService, channelService, commentService, mapper);

            return videoService;
        }

        private void SeedData(TubeDbContext context)
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TubeProfile());
            });
            var mapper = mockMapper.CreateMapper();

            var usersForDB = mapper.Map<List<TubeUser>>(Users);
            var videosForDB = mapper.Map<List<Video>>(Videos);
            var channelForDB = mapper.Map<Channel>(Channel);

            context.Users.AddRange(usersForDB);
            context.Videos.AddRange(videosForDB);
            context.Channels.Add(channelForDB);

            context.SaveChanges();
        }

        private ChannelDTO Channel = new ChannelDTO
        {
            Id = "1",
            Name = "TestChannel",
            Description = "Test...Test...Test",
            TubeUserId = "1"
        };

        private List<VideoDTO> Videos = new List<VideoDTO>
        {
            new VideoDTO
            {
                Id = "1",
                Name = "VideoTest",
                Views = 3,
                ChannelId = "1",
                Comments = new List<CommentDTO>(),
                Likes = new List<VoteDTO>(){ new VoteDTO { } },
                Category = "Music"
            },
            new VideoDTO
            {
                Id = "2",
                Name = "VideoTest2",
                ChannelId = "1",
                Views = 0,
                Comments = new List<CommentDTO>(){ new CommentDTO { Text = "Test"} },
                Category = "News"
            }
        };

        private List<TubeUserDTO> Users = new List<TubeUserDTO>
        {
            new TubeUserDTO
            {
                Id = "1",
                UserName = "TestUser",
                FirstName = "Georgi",
                LastName = "Stoev"
            },
            new TubeUserDTO
            {
                Id = "2",
                UserName = "TestUser2",
                FirstName = "Pesho",
                LastName = "Stoev"
            }
        };
    }
}
