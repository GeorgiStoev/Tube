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
    public class HistoryService : IHistoryService
    {
        private readonly TubeDbContext context;
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public HistoryService(TubeDbContext context, IUserService userService, IMapper mapper)
        {
            this.context = context;
            this.userService = userService;
            this.mapper = mapper;
        }

        public void Create(string tubeUserName, string videoName)
        {
            var tubeUser = this.userService.GetDomainUserFromDbByUserName(tubeUserName);

            var history = new History
            {
                TubeUserId = tubeUser.Id,
                VideoName = videoName,
                Date = DateTime.UtcNow
            };

            this.context.Histories.Add(history);
            this.context.SaveChanges();
        }

        public List<HistoryDTO> GetTubeUserHistoriesById(string tubeUserId)
        {
            var histories = this.context
                                .Histories.AsNoTracking()
                                .Where(history => history.TubeUserId == tubeUserId)
                                .OrderByDescending(history => history.Date)
                                .ToList();

            var historiesDto = this.mapper.Map<List<HistoryDTO>>(histories);
            return historiesDto;
        }

        public void DeleteAllHistory(string tubeUserId)
        {
            var histories = this.context
                                .Histories.AsNoTracking()
                                .Where(history => history.TubeUserId == tubeUserId)
                                .ToList();

            this.context.RemoveRange(histories);
            this.context.SaveChanges();
        }

        public void DeleteLastHistory(string tubeUserId)
        {
            var history = this.GetTubeUserHistoriesById(tubeUserId).First();
            var historyFromDB = this.mapper.Map<History>(history);

            this.context.Histories.Remove(historyFromDB);
            this.context.SaveChanges();
        }
    }
}