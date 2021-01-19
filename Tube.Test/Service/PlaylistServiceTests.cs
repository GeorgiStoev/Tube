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
    public class PlaylistServiceTests
    {
        [Fact]
        public void TestCreate_ShouldCreatePlaylist()
        {
            var options = new DbContextOptionsBuilder<TubeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new TubeDbContext(options);
            SeedData(context);

            var playlistService = this.GetPlaylistService(context);

            var playlistName = "TestPlaylist3";
            var playlistTubeUserName = "TestUser";

            playlistService.Create(playlistName, playlistTubeUserName);
            var playlists = playlistService.GetUserPlayListsByUsername(playlistTubeUserName);

            Assert.True(playlists.Count == 3);
        }

        [Fact]
        public void TestGetUserPlayListsByUsername_ShouldReturnCorrectPlaylists()
        {
            var options = new DbContextOptionsBuilder<TubeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new TubeDbContext(options);
            SeedData(context);

            var playlistTubeUserName = "TestUser";
            var playlistService = this.GetPlaylistService(context);
            var playlists = playlistService.GetUserPlayListsByUsername(playlistTubeUserName);

            Assert.True(playlists.Count == 2);
        }

        private PlaylistService GetPlaylistService(TubeDbContext context)
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
            var playlistService = new PlaylistService(context, userService, videoService, mapper);

            return playlistService;
        }

        [Fact]
        public void TestGetPlaylistVideos_ShouldReturnCorrectPlaylistVideos()
        {
            var options = new DbContextOptionsBuilder<TubeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new TubeDbContext(options);
            SeedData(context);

            var playlistId = "1";
            var playlistService = this.GetPlaylistService(context);

            var videoPlaylists = playlistService.GetPlaylistVideos(playlistId);

            Assert.True(videoPlaylists.Count == 1);
        }

        [Fact]
        public void TestGetPlaylistById_ShouldReturnCorrectPlaylist()
        {
            var options = new DbContextOptionsBuilder<TubeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new TubeDbContext(options);
            SeedData(context);

            var playlistId = "1";
            var playlistService = this.GetPlaylistService(context);

            var expectedPlaylist = Playlists[0];
            var actualPlaylist = playlistService.GetPlaylistById(playlistId);

            Assert.True(expectedPlaylist.Id == actualPlaylist.Id);
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
            var playlistsForDB = mapper.Map<List<Playlist>>(Playlists);
            var videoPlaylistsForDB = mapper.Map<List<VideoPlaylist>>(VideoPlaylists);

            context.Users.Add(userForDB);
            context.Videos.Add(videoForDB);
            context.Playlists.AddRange(playlistsForDB);
            context.VideoPlaylists.AddRange(videoPlaylistsForDB);

            context.SaveChanges();
        }

        private List<VideoPlaylistDTO> VideoPlaylists = new List<VideoPlaylistDTO>
        {
            new VideoPlaylistDTO
            {
                PlaylistId = "1",
                VideoId = "1"
            },
            new VideoPlaylistDTO
            {
                PlaylistId = "2",
                VideoId = "1"
            }
        };

        private List<PlaylistDTO> Playlists = new List<PlaylistDTO>
        {
            new PlaylistDTO
            {
                Id = "1",
                Name = "TestPlaylist",
                TubeUserId = "1"
            },
            new PlaylistDTO
            {
                Id = "2",
                Name = "TestPlaylist2",
                TubeUserId = "1"
            }
        };

        private VideoDTO Video = new VideoDTO
        {
            Id = "1",
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
    }
}
