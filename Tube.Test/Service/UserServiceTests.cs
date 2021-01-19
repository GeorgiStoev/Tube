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
    public class UserServiceTests
    {
        [Fact]
        public void TestGetTubeUserByUsername_WithoutAnyData_ShouldReturnNull()
        {
            var options = new DbContextOptionsBuilder<TubeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new TubeDbContext(options);

            var username = "TestUser";

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TubeProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var userService = new UserService(context, mapper);

            var actualData = userService.GetTubeUserByUsername(username);
            Assert.True(actualData == null);
        }

        [Fact]
        public void TestGetTubeUserByUsername_ShouldReturnTubeUserWithThatUsername()
        {
            var options = new DbContextOptionsBuilder<TubeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new TubeDbContext(options);
            SeedData(context);

            var username = "TestUser";

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TubeProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var userService = new UserService(context, mapper);

            var expectedData = TubeUser;
            var actualData = userService.GetTubeUserByUsername(username);

            Assert.True(expectedData.UserName == actualData.UserName);
        }


        [Fact]
        public void TestGetTubeUserById_ShouldReturnTubeUserWithThatId()
        {
            var options = new DbContextOptionsBuilder<TubeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new TubeDbContext(options);
            SeedData(context);

            var id = "2222234434546456456";

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TubeProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var userService = new UserService(context, mapper);

            var expectedData = TubeUser;
            var actualData = userService.GetTubeUserById(id);

            Assert.True(expectedData.Id == actualData.Id);
        }

        [Fact]
        public void TestGetSubscriptionsByUserName_ShouldReturnUserSubscriptions()
        {
            var options = new DbContextOptionsBuilder<TubeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new TubeDbContext(options);
            SeedData(context);

            var username = "TestUser";

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TubeProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var userService = new UserService(context, mapper);

            var expectedData = new List<TubeUserChannelDTO> { TubeUserChannel };
            var actualData = userService.GetSubscriptionsByUserName(username);

            Assert.True(expectedData[0].ChannelId == actualData[0].ChannelId);
            Assert.True(expectedData[0].TubeUserId == actualData[0].TubeUserId);
        }

        private void SeedData(TubeDbContext context)
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TubeProfile());
            });
            var mapper = mockMapper.CreateMapper();

            var userForDB = mapper.Map<TubeUser>(TubeUser);
            var tubeUserChannelForDB = mapper.Map<TubeUserChannel>(TubeUserChannel);

            context.Users.Add(userForDB);
            context.TubeUserChannels.Add(tubeUserChannelForDB);

            context.SaveChanges();
        }

        private TubeUserChannelDTO TubeUserChannel = new TubeUserChannelDTO
        {
            TubeUserId = "2222234434546456456",
            ChannelId = "1111"
        };

        private TubeUserDTO TubeUser = new TubeUserDTO
        {
            Id = "2222234434546456456",
            UserName = "TestUser",
            FirstName = "Georgi",
            LastName = "Stoev"
        };
    }
}
