using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tube.Services;
using Tube.Web.ViewModels.History;

namespace Tube.Web.Controllers
{
    public class HistoryController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly IHistoryService historyService;
        private readonly IVideoService videoService;

        public HistoryController(IMapper mapper ,IUserService userService, IHistoryService historyService, IVideoService videoService)
        {
            this.mapper = mapper;
            this.userService = userService;
            this.historyService = historyService;
            this.videoService = videoService;
        }

        [Authorize]
        public IActionResult Videos()
        {
            var username = this.User.Identity.Name;
            var user = this.userService.GetTubeUserByUsername(username);

            var histories = this.historyService.GetTubeUserHistoriesById(user.Id);

            var historiesViewModel = new List<HistoryViewModel>();
            foreach (var history in histories)
            {
                var historyViewModel = this.mapper.Map<HistoryViewModel>(history);
                var videoName = history.VideoName;
                var video = this.videoService.GetVideoByName(videoName);
                historyViewModel.VideoId = video.Id;
                historyViewModel.VideoUrl = video.VideoUrl;

                historiesViewModel.Add(historyViewModel);
            }

            return View(historiesViewModel);
        }

        [Authorize]
        public IActionResult DeleteAllHistory()
        {
            var username = this.User.Identity.Name;
            var user = this.userService.GetTubeUserByUsername(username);
            var tubeUserId = user.Id;

            this.historyService.DeleteAllHistory(tubeUserId);

            return this.RedirectToAction(nameof(Videos));
        }
    }
}