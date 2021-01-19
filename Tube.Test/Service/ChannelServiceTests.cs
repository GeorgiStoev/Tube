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
    public class ChannelServiceTests
    {
        [Fact]
        public void TestCreate_ShouldCreateChannel()
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

            var userService = new UserService(context, mapper);
            var channelService = new ChannelService(context, userService, mapper);

            var channelName = "MyChannel";
            var channelDescription = "TestChannel";
            var tubeUsername = "TestUser";
            var channel = channelService.Create(channelName, channelDescription, string.Empty, tubeUsername);

            Assert.False(channel == null);
        }

        [Fact]
        public void TestGetChannelByUserName_ShouldReturnCorrectChannel()
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

            var userService = new UserService(context, mapper);
            var channelService = new ChannelService(context, userService, mapper);

            var channelName = "MyChannel";
            var channelDescription = "TestChannel";
            var tubeUsername = "TestUser";

            channelService.Create(channelName, channelDescription, string.Empty, tubeUsername);

            var expectedData = channelService.GetChannelByTubeUserName(tubeUsername);

            Assert.False(expectedData == null);
        }

        [Fact]
        public void TestGetSubscribersByChannelId_ShouldReturnChannelSubscribers()
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

            var userService = new UserService(context, mapper);
            var channelService = new ChannelService(context, userService, mapper);

            var expectedData = new List<TubeUserChannelDTO> { TubeUserChannel };
            var id = "1111";
            var actualData = channelService.GetSubscribersByChannelId(id);

            Assert.True(expectedData[0].TubeUserId == actualData[0].TubeUserId);
            Assert.True(expectedData[0].ChannelId == actualData[0].ChannelId);
        }

        [Fact]
        public void TestGetChannelById_ShouldReturnCorrectChannel()
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

            var userService = new UserService(context, mapper);
            var channelService = new ChannelService(context, userService, mapper);

            var channelName = "MyChannel";
            var channelDescription = "TestChannel";
            var tubeUsername = "TestUser";

            var channel = channelService.Create(channelName, channelDescription, string.Empty, tubeUsername);

            var expectedData = channelService.GetChannelById(channel.Id);

            Assert.False(expectedData == null);
        }

        [Fact]
        public void TestSubscribe_UserShouldSubscribeChannel()
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

            var userService = new UserService(context, mapper);
            var channelService = new ChannelService(context, userService, mapper);

            var channelName = "MyChannel";
            var channelDescription = "TestChannel";
            var tubeUsername = "TestUser";

            var channel = new Channel { Id = "1", Name = channelName, Description = channelDescription };
            //var channel = channelService.Create(channelName, channelDescription, string.Empty,  tubeUsername);
            var id = "1";

            channelService.Subscribe(id, tubeUsername);
            var subscribers = channelService.GetSubscribersByChannelId(id);

            Assert.Single(subscribers);
        }

        [Fact]
        public void TestIsYours_ShouldReturnTrue()
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

            var userService = new UserService(context, mapper);
            var channelService = new ChannelService(context, userService, mapper);

            var channelName = "MyChannel";
            var channelDescription = "TestChannel";
            var tubeUsername = "TestUser";

            var channel = channelService.Create(channelName, channelDescription, string.Empty, tubeUsername);
            var id = channel.Id;

            var isYours = channelService.IsYours(id, tubeUsername);

            Assert.True(isYours);
        }

        [Fact]
        public void TestIsYours_ShouldReturnFalse()
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

            var userService = new UserService(context, mapper);
            var channelService = new ChannelService(context, userService, mapper);

            var channelName = "MyChannel";
            var channelDescription = "TestChannel";
            var tubeUsername = "TestUser";

            var channel = channelService.Create(channelName, channelDescription, string.Empty, tubeUsername);
            var id = channel.Id;
            var username = Users[1].UserName;

            var isYours = channelService.IsYours(id, username);

            Assert.False(isYours);
        }

        private void SeedData(TubeDbContext context)
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TubeProfile());
            });
            var mapper = mockMapper.CreateMapper();

            var usersForDB = mapper.Map<List<TubeUser>>(Users);
            var usersChannelForDB = mapper.Map<TubeUserChannel>(TubeUserChannel);

            context.Channels.Add(Channel);
            context.Users.Add(User);
            context.Users.AddRange(usersForDB);
            context.TubeUserChannels.Add(usersChannelForDB);

            context.SaveChanges();
        }

        public TubeUser User = new TubeUser
        {
            Id = "1",
            UserName = "Test"
        };

        private Channel Channel = new Channel
        {
            Id = "1",
            Description = "Test",
            Name = "Test",
            TubeUserId = "1"
        };

        private TubeUserChannelDTO TubeUserChannel = new TubeUserChannelDTO
        {
            TubeUserId = "2222234434546456456",
            ChannelId = "1111"
        };

        private List<TubeUserDTO> Users = new List<TubeUserDTO>
        {
            new TubeUserDTO
            {
                Id = "2222234434546456456",
                UserName = "TestUser",
                FirstName = "Georgi",
                LastName = "Stoev"
            },
            new TubeUserDTO
            {
                Id = "2222234645418416",
                UserName = "TestUser2",
                FirstName = "Pesho",
                LastName = "Stoev"
            }
        };
    }
}
