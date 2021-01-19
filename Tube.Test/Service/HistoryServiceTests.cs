using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Tube.Data;
using Tube.Data.Models;
using Tube.Services;
using Tube.Web.MappingConfiguration;
using Xunit;

namespace Tube.Test.Service
{
    public class HistoryServiceTests
    {
        [Fact]
        public void TestCreate_ShouldCreateHistory()
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
            var historyService = new HistoryService(context, userService, mapper);

            var historyTubeUserName = "TestUser";
            var historyVideoName = "TestVideo";
            var tubeUserId = "1";

            historyService.Create(historyTubeUserName, historyVideoName);
            var histories = historyService.GetTubeUserHistoriesById(tubeUserId);

            Assert.True(histories.Count == 3);
        }

        [Fact]
        public void TestGetTubeUserHistoriesById_ShouldReturnCorrectHistories()
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
            var historyService = new HistoryService(context, userService, mapper);

            var tubeUserId = "1";

            var histories = historyService.GetTubeUserHistoriesById(tubeUserId);

            Assert.True(histories.Count == 2);
        }

        private void SeedData(TubeDbContext context)
        {
            context.Users.Add(User);
            context.Histories.AddRange(Histories);

            context.SaveChanges();
        }

        private List<History> Histories = new List<History>
        {
            new History
            {
                Id = "1",
                TubeUserId = "1",
                VideoName = "TestVideo"
            },
            new History
            {
                Id = "2",
                TubeUserId = "1",
                VideoName = "TestVideo2"
            }
        };

        private TubeUser User = new TubeUser
        {
            Id = "1",
            UserName = "TestUser",
            FirstName = "Georgi",
            LastName = "Stoev"
        };
    }
}
